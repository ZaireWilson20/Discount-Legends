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

    //TODO: Section this class off into interface for movement, actions, and score update. 
    //TODO: Rework this class to allow for offline playtesting as well.

    /*
    * Variable declarations are sectioned off according to what they will be used for.
        1. Character type stats
        2. Audio
        3. Animation
        4. PhotonView
        5. Player Input/Movement objects 
        6. Player Input/Movement variables
        7. Attack
        8. Score Update
    
    * Method declarations are also sectioned off.
        1. Defualt Unity Methods (Awake, Update, etc.)
        2. Movement/Camera 
        3. Animation
        4. Attack 
        5. Damage
        6. Trigger/Trigger Sync
        7. Score Update

        Note: PhotonNetwork.OfflineMode checks are down since there was offline playtesting as well.
    */

    [Header("Stats")]
    [SerializeField] protected Stats _stats;
    protected string name;
    protected float dmgAmnt;
    protected float healthAmnt;
    protected int id; // Essential for updating player score/record correctly. Tied to PV
    protected float speed;

    [Header("Audio")]
    protected AudioClip _hit; // In Scriptable Object
    protected AudioClip _stun; // In Scriptable Object
    protected AudioClip _pickUp; // In Scriptable Object
    [SerializeField] protected AudioSource _audio; // Necessary to be on MC object

    [Header("Animator")]
    [SerializeField] protected Animator _anim;

    [Header("Photon View")]
    [SerializeField] protected PhotonView _pv;

    protected PlayerInput _playerInputActions;
    [Header("Movement")]
    [SerializeField] protected Rigidbody _rigidBody;
    [SerializeField] protected GameObject _playerCam;
    [SerializeField] protected BoxCollider _trigger; // In the future, can just be changed to Collider
    [SerializeField] protected ScoreManager _scoreManager; 

    protected Vector2 input_view;
    protected Vector2 input_move;
    private Vector3 move;
    private Vector3 direction;
    protected Vector3 slopeDirection;
    protected RaycastHit slopeHit;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] protected float movementSmoothing = 0.1f;
    private const float camRotateSpeed = 400f;
    private float yaw = 0f;
    private List<GameObject> itemsPickedUp = new List<GameObject>(); 

    protected Character attackedPlayer;
    protected bool attackHit;
    protected float attackCoolDown;
    protected bool stunned = false;
    protected bool canAttack = true;
    protected bool attack = false; // Different from canAttack. This is to stop people from collecting items while attacking
    protected bool item = false;

    private ScoreBoard _scoreBoard;
    private PlayerRecord _record;
    protected float playerScore = 0;

    void Awake()
    {
        if (_stats == null)
        { // Necessary because if player restarts the game, errors happen with a ghost object present from a previous playthrough
            Destroy(gameObject);
        }

        name = _stats.characterName;
        dmgAmnt = _stats.damageAmount;
        healthAmnt = _stats.healthAmount;
        speed = _stats.speed;
        _hit = _stats.Hit;
        _stun = _stats.Stun;
        _pickUp = _stats.Pickup;
        attackCoolDown = _stats.attackCoolDown;

        _playerInputActions = new PlayerInput();

        _pv = GetComponent<PhotonView>();
        _rigidBody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        _scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
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
            Debug.Log("RECORD IS HERE");
        }

        Cursor.lockState = CursorLockMode.Locked;
    }

    /*FOR OFFLINE MODE ONLY
        *UNCOMMENT WHEN TESTING OFFLINE
    */

    // void Start()
    // {
    //     PhotonNetwork.OfflineMode = true;

    // }

    private void OnEnable()
    {
        _playerInputActions.Movement.Enable();
        _playerInputActions.Movement.Attack.performed += _ => Attack();
        _playerInputActions.Movement.Attack.performed += _ => TriggerOn("Attack"); // Set to same function as Pickup because same trigger is used
        _playerInputActions.Movement.Attack.canceled += _ => TriggerOff("Attack");

        _playerInputActions.Movement.View.performed += x => input_view = x.ReadValue<Vector2>().normalized; // Normalized so both controller and mouse rotate at same speed
        _playerInputActions.Movement.View.canceled += x => input_view = x.ReadValue<Vector2>().normalized;
        _playerInputActions.Interact.Enable();
        _playerInputActions.Interact.PickUp.performed += _ => TriggerOn("Item");
        _playerInputActions.Interact.PickUp.canceled += _ => TriggerOff("Item");
    }

    private void OnDisable()
    {
        _playerInputActions.Movement.Disable();
        _playerInputActions.Movement.Attack.Disable();
        _playerInputActions.Movement.View.Disable();

        _playerInputActions.Interact.Disable();
        _playerInputActions.Interact.PickUp.Disable();

    }

    void Update()
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        RotateMove();
        AnimationState();  //TODO: Instead of having it called in Update, put it inside the relevant methods.


        if (!_trigger.enabled) // Necessary since onTriggerExit may not always work due to it being enabled/disabled by mouse click
        { // TODO: Move this code block outside of Update.
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
        slopeDirection = Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized; //Creates a Vector parallel to the slope

        if (onSlope())
        {
            move = slopeDirection * speed * .5f; //Move slower on ramps to combat lauching off of them.
        }
        else
        {
            move = direction * speed;
        }

        Vector3 targetVelocity = new Vector3(move.x * Time.fixedDeltaTime * speed, _rigidBody.velocity.y, move.z * speed * Time.fixedDeltaTime);
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    }
    protected void RotateMove()
    {
        //TODO: Apply smoothing to camera rotation. Not strictly necessary.
        yaw = input_view.normalized.x * Time.deltaTime * camRotateSpeed;
        transform.Rotate(Vector3.up * yaw);
    }

    protected bool onSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, .75f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
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
    protected virtual void Attack() // Since this is a virtual method, child classes should override and add behavior UNIQUE to them
    {
        if (!_pv.IsMine) return;
        if (canAttack && _pv.IsMine)
        {
            _anim.SetTrigger("Attack");
            StartCoroutine(AttackWait());
        }
    }

    protected IEnumerator AttackWait()
    { //TODO: Figure out a way to stop button mashing from happening.
        bool finishCheck = false;
        float timeElapsed = 0;
        canAttack = false;
        while (!finishCheck)
        { // Loop necessary to check for a hit as checking immediately during an Attack() call might not have correct values for attackHit

            if (timeElapsed >= 1.5f)
            {
                finishCheck = true;
            }
            if (attackHit)
            {
                attackedPlayer.TakeDamage(dmgAmnt, photonView.ViewID, attackedPlayer.photonView.ViewID);
                if (attackedPlayer.stunned && attackedPlayer.itemsPickedUp.Count > 0)
                {
                }
                else
                {
                    Debug.Log("Is Stunned: " + attackedPlayer.stunned);
                    Debug.Log("Player Item Count: " + attackedPlayer.itemsPickedUp.Count);
                }
                attackedPlayer = null;
                attackHit = false;
                break;
            }
            timeElapsed += 1 * Time.deltaTime;
            yield return null;
        }

        canAttack = true;
    }
    protected void TakeDamage(float damage, int attackingPlayerID, int attackedPlayerID)
    {
        _pv.RPC("RPC_TakeDamage", RpcTarget.All, damage, attackingPlayerID, attackedPlayerID);
    }

    [PunRPC]
    public virtual void RPC_TakeDamage(float damage, int attackingPlayerID, int attackedPlayerID)
    {

        if (!_pv.IsMine) return;
        if (!stunned) // Checks necessary to stop playing hit sounds while player is stunned / stop them from lowering health further
        {
            _pv.RPC("RPC_PlaySound", RpcTarget.All, "Hit");
            healthAmnt = healthAmnt - damage;
        }

        if (healthAmnt <= 0 && !stunned)
        {
            Debug.Log("player taking damage");

            _pv.RPC("RPC_PlaySound", RpcTarget.All, "Stun");
            stunned = true;
            if (itemsPickedUp.Count > 0)
            {
                photonView.RPC("StealItem", RpcTarget.All, attackingPlayerID, attackedPlayerID); // POTENTIAL PROBLEM: if multiple players have health below zero at the same time, same attacking player could potentially steal from them
            }
            attackHit = false;
            attackedPlayer = null;
            StartCoroutine(Stunned());
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
        if (sound == "PickUp" && _pickUp != null) // May be issues when picking up something and mid Hit/Stun
        {
            _audio.clip = _pickUp;
            _audio.Play();
        }
    }

    public void TriggerOn(string action)
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        _pv.RPC("UpdateTriggerEveryone", RpcTarget.All, true, action);
    }

    public void TriggerOff(string action)
    {
        if (!_pv.IsMine && !PhotonNetwork.OfflineMode) return;
        _pv.RPC("UpdateTriggerEveryone", RpcTarget.All, false, action);
    }

    [PunRPC]
    protected void UpdateTriggerEveryone(bool active, string action)
    {
        if (_trigger == null) return;
        if (active)
        {
            switch (action)
            {
                case "Attack":
                    attack = true;
                    item = false;
                    break;
                case "Item":
                    attack = false;
                    item = true;
                    break;
                default:
                    attack = false;
                    item = false;
                    break;
            }
        }
        else
        {
            attack = false;
            item = false;
        }
        _trigger.enabled = active;
    }
    private void OnTriggerEnter(Collider other)

    {

        if (other.gameObject.tag == "Item" && item)
        {
             _pv.RPC("RPC_PlaySound", RpcTarget.All, "PickUp");

            Item points = other.gameObject.GetComponent<Item>();
            if (points == null) return;
            //itemsPickedUp.Add(points.gameObject);
            Debug.Log("picking up trigger");
            //itemsPickedUp.Add(_scoreManager.itemMap[points.name].gameObject);
            this.photonView.RPC("PickUpItemRPC", RpcTarget.Others, this.photonView.ViewID, points.name);
            float point = points.getPoints();
            
            SetPlayerScore(point);
            Destroy(points.getText());
            other.gameObject.SetActive(false);
        }

        if (other.gameObject.tag == "Player" && attack)
        {
            attackedPlayer = other.gameObject.GetComponent<Character>();
            attackHit = true;
        }
    }

    [PunRPC]
    public void PickUpItemRPC(int playerID, string itemName)
    {
        //Character player = PhotonView.Find(playerID).GetComponent<Character>();

            itemsPickedUp.Add(_scoreManager.itemMap[itemName].gameObject);
            Debug.Log("Player Picked Up Item " + PhotonNetwork.IsMasterClient);

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            attackedPlayer = null;
            attackHit = false;
        }
    }


    [PunRPC]
    public void StealItem(int attackingPlayerID, int otherPlayerID)
    {
        Character otherPlayer = PhotonView.Find(otherPlayerID).GetComponent<Character>();
        Character attackingPlayer = PhotonView.Find(attackingPlayerID).GetComponent<Character>();

        int randItem = UnityEngine.Random.Range(0, otherPlayer.itemsPickedUp.Count - 1);
        GameObject itemTaken = otherPlayer.itemsPickedUp[randItem];
        Debug.Log(attackingPlayer.name + " stole " + itemTaken.name + " from " + otherPlayer.name);
        //ExitGames.Client.Photon.Hashtable playerItemHash = new ExitGames.Client.Photon.Hashtable();
        //playerItemHash["playerItems"] = itemsPickedUp; 
        attackingPlayer.SetPlayerScore(itemTaken.GetComponent<Item>().getPoints());
        otherPlayer.SetPlayerScore(-1 * itemTaken.GetComponent<Item>().getPoints());
        otherPlayer.itemsPickedUp.Remove(itemTaken);
        attackingPlayer.itemsPickedUp.Add(itemTaken);
    }

    public void SetPlayerScore(float score)
    {
        //Code block required if offline
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
        Debug.Log("Update Record with " + playerScore);
        _record.UpdateRecord(playerScore, nickname);
    }


}
