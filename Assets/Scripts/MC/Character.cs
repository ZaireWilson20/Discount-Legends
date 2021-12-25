using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.Audio;
using System;
public class Character : MonoBehaviourPunCallbacks
{
    [SerializeField] protected CharacterStats _stats;
    protected string name;
    protected float dmgAmnt;
    protected float healthAmnt;
    protected int id;
    [SerializeField] protected float speed;

    protected AudioClip _hit; // In Scriptable Object
    protected AudioClip _stun; // In Scriptable Object

    [SerializeField] protected PhotonView _pv;

    protected PlayerInput _playerInputActions;
    [SerializeField] protected Rigidbody _rigidBody;
    [SerializeField] protected GameObject _playerCam;
    [SerializeField] protected BoxCollider _trigger;

    protected Vector2 input_view;
    protected Vector2 input_move;
    private Vector3 direction;
    private Vector3 move;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] protected float movementSmoothing = 0.1f;
    private const float camRotateSpeed = 100f;
    private float yaw = 0f;

    protected bool stunned = false;
    protected bool canAttack = true;
    [SerializeField] protected Animator _anim;
    [SerializeField] protected AudioSource _audio;

    private ScoreBoard _scoreBoard;
    private PlayerRecord _record;
    protected int playerScore = 0;

    protected Character attackedPlayer;
    protected bool attackHit;
    protected float attackCoolDown;
    void Awake()
    {
        name = _stats.characterName;
        dmgAmnt = _stats.damageAmount;
        healthAmnt = _stats.healthAmount;
        speed = _stats.speed;
        _hit = _stats.Hit;
        _stun = _stats.Stun;
        attackCoolDown = _stats.attackCoolDown;

        _playerInputActions = new PlayerInput();
        _playerInputActions.Movement.Enable();
        _playerInputActions.Movement.Attack.performed += _ => Attack();
        _playerInputActions.Movement.Attack.performed += _ => TriggerOn("Attack");
        _playerInputActions.Movement.Attack.canceled += _ => TriggerOff("Attack");
        _playerInputActions.Movement.View.performed += x => input_view = x.ReadValue<Vector2>().normalized;
        _playerInputActions.Interact.Enable();
        _playerInputActions.Interact.PickUp.performed += _ => TriggerOn("Item");
        _playerInputActions.Interact.PickUp.canceled += _ => TriggerOff("Item");

        _pv = GetComponent<PhotonView>();
        _rigidBody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();

        if (_pv.IsMine && _playerCam != null)
        {
            _playerCam.SetActive(true);
        }

        //GameObject declaration/If statements are necessary for testing in levels that aren't Multiplayer/are Offline
        GameObject scoreBoard = GameObject.Find("Scoreboard");
        if (scoreBoard != null)
        {
            _scoreBoard = scoreBoard.GetComponent<ScoreBoard>();
        }

        GameObject record = GameObject.Find("PlayerRecord");
        if (record != null)
        {
            _record = record.GetComponent<PlayerRecord>();
        }
    }

    //FOR OFFLINE MODE ONLY
    //UNCOMMENT WHEN TESTING OFFLINE
    // void Start()
    // {
    //     PhotonNetwork.OfflineMode = true;

    // }


    void Update()
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        RotateMove();
        AnimationState();  //Instead of having it on Update, put it inside the relevant methods
        //DEBUG FOR TRIGGER ERROR
        if (!_trigger.enabled){
            attackedPlayer = null;
            attackHit = false;
        }
    }

    void FixedUpdate()
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        if (!stunned) Move();
    }

    protected void Move()
    {
        input_move = _playerInputActions.Movement.Move.ReadValue<Vector2>();
        direction = (transform.rotation * Vector3.forward * input_move.y + transform.rotation * Vector3.right * input_move.x).normalized;
        move = direction * speed;
        Vector3 targetVelocity = new Vector3(move.x * Time.fixedDeltaTime * speed, _rigidBody.velocity.y, move.z * speed * Time.fixedDeltaTime);
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    }
    protected void RotateMove()
    {
        yaw = input_view.normalized.x * Time.deltaTime * camRotateSpeed;
        transform.Rotate(Vector3.up * yaw);
    }
    protected void AnimationState()
    {
        if (_anim == null) return;

        if (direction.magnitude > 0)
        {
            _anim.SetBool("Running", true);
            _anim.SetBool("Idle", false);
        }
        else if (direction.magnitude <= 0)
        {
            _anim.SetBool("Running", false);
            _anim.SetBool("Idle", true);
        }

        if (stunned)
        {
            _anim.SetBool("Stunned", true);
        }

    }
    protected virtual void Attack()
    {
        if (!_pv.IsMine) return;
        if (canAttack && _pv.IsMine)
        {
            _anim.SetTrigger("Attack");
            StartCoroutine(AttackWait());
        }
    }

    protected void TakeDamage(float damage)
    {
        _pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]
    public virtual void RPC_TakeDamage(float damage)
    {
        if (!_pv.IsMine) return;
        if (!stunned)
        { // Necessary to stop playing hit sounds while player is stunned/ stop them from lowering health further
            Debug.Log("Playing Hit Sound");
            _pv.RPC("RPC_PlaySound", RpcTarget.All, "Hit");
            healthAmnt = healthAmnt - damage;
        }

        if (healthAmnt <= 0 && !stunned)
        {
            _pv.RPC("RPC_PlaySound", RpcTarget.All, "Stun"); //_pv.RPC calls are necessary to play sound for everyone involved
            stunned = true;
            attackHit = false;
            attackedPlayer = null;
            StartCoroutine(Stunned()); // delay movement
        }
    }

    [PunRPC]
    public virtual void RPC_PlaySound(string sound)
    {
        if (sound == "Stun" && _stun != null)
        {
            _audio.clip = _stun;
            _audio.Play();
        }
        if (sound == "Hit" && _hit != null)
        {
            _audio.clip = _hit;
            _audio.Play();
        }
    }

    protected IEnumerator Stunned()
    {
        yield return new WaitForSeconds(5f);
        healthAmnt = _stats.healthAmount;
        stunned = false;
        if (_anim != null)
        {
            _anim.SetBool("Stunned", false);
        }
    }


    public void SetPlayerScore(int score)
    {
        //This Code block is required if running offline
        if (PhotonNetwork.OfflineMode)
        {
            id = 0;
        }
        else
        {
            id = _pv.Owner.ActorNumber;
        }
        playerScore += score;
        UpdateRecord();
        UpdateScoreboard();
    }

    public void UpdateScoreboard()
    {
        if (_scoreBoard == null) return;
        _scoreBoard.UpdateScoreboardItem(playerScore, id);

    }

    public void UpdateRecord()
    {
        if (_record == null) return;
        string nickname;
        if (PhotonNetwork.OfflineMode)
        {
            nickname = "HelloWorld";
        }
        else
        {
            nickname = _pv.Owner.NickName;
        }
        _record.UpdateRecord(playerScore, nickname);
    }

    public void TriggerOn(string action)
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        _pv.RPC("UpdateTriggerEveryone", RpcTarget.All, _pv.ViewID, true, action);
    }

    public void TriggerOff(string action)
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        _pv.RPC("UpdateTriggerEveryone", RpcTarget.All, _pv.ViewID, false, action);
    }

    [PunRPC]
    protected void UpdateTriggerEveryone(int InstanceID, bool active, string action)
    {
        if (InstanceID == _pv.ViewID)
        {
            if (_trigger == null) return;
            _trigger.enabled = active;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Item")
        {
            Item points = other.gameObject.GetComponent<Item>();
            if (points == null) return;
            int point = points.getPoints();
            SetPlayerScore(point);
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            Debug.Log(other.name);
            attackedPlayer = other.gameObject.GetComponent<Character>();
            attackHit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackedPlayer = null;
            attackHit = false;
        }
    }

    protected IEnumerator AttackWait()
    {
        Debug.Log("IN COROUTINE");
        bool finishCheck = false;
        float timeElapsed = 0;
        canAttack = false;
        while (!finishCheck)
        {

            if (timeElapsed >= 1.5f)
            {
                finishCheck = true;
            }
            if (attackHit)
            {
                attackedPlayer.TakeDamage(dmgAmnt);
                attackedPlayer = null;
                attackHit = false;
                break;
            }
            timeElapsed += 1 * Time.deltaTime;
            yield return null;
        }

        canAttack = true;
    }
}
