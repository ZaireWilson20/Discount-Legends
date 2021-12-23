using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
using UnityEngine.Audio;
using System;
public abstract class Character : MonoBehaviourPunCallbacks
{
    [SerializeField] protected CharacterStats _stats;
    protected string name;
    protected float dmgAmnt;
    protected float healthAmnt;
    [SerializeField] protected float speed;
    protected AudioClip _hit;
    protected AudioClip _stun;

    [SerializeField] protected PhotonView _pv;
    protected PlayerInput _playerInputActions;
    [SerializeField] protected Rigidbody _rigidBody;

    Vector3 move;
    [SerializeField] protected float movementSmoothing = 0.1f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 direction;
    private float yaw = 0f;
    private float camRotateSpeed;
    protected Vector2 input_view;
    protected Vector2 input_move;
    protected bool stunned = false;
    protected bool canAttack = true;
    protected int playerScore = 0;
    protected Animator _anim;
    [SerializeField] protected AudioSource _audio;

    private ScoreBoard _scoreBoard;
    private PlayerRecord _record;

    void Awake()
    {
        name = _stats.characterName;
        dmgAmnt = _stats.damageAmount;
        healthAmnt = _stats.healthAmount;
        speed = _stats.speed;
        _hit = _stats.Hit;
        _stun = _stats.Hit;

        _playerInputActions = new PlayerInput();
        _playerInputActions.Movement.Enable();
        _playerInputActions.Movement.Attack.performed += _ => Attack();
        _playerInputActions.Movement.View.performed += x => input_view = x.ReadValue<Vector2>().normalized;
        _playerInputActions.Movement.Move.performed += x => input_move = x.ReadValue<Vector2>();

        _pv = GetComponent<PhotonView>();
        _rigidBody = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _scoreBoard = GameObject.Find("Scoreboard").GetComponent<ScoreBoard>();
        _record = GameObject.Find("PlayerRecord").GetComponent<PlayerRecord>();
    }

    void Update()
    {
        if (!_pv.IsMine) return;

        RotateMove();
        //Instead of having it on Update, put it inside the relevant methods
        AnimationState();

    }

    void FixedUpdate()
    {
        if (!_pv.IsMine) return;
        Move();
    }

    protected void Move()
    {
        move = direction * speed;
        Vector3 targetVelocity = new Vector3(move.x * Time.fixedDeltaTime * speed, _rigidBody.velocity.y, move.z * speed * Time.fixedDeltaTime);
        _rigidBody.velocity = Vector3.SmoothDamp(_rigidBody.velocity, targetVelocity, ref velocity, movementSmoothing);
    }


    protected void RotateMove()
    {
        direction = (transform.rotation * Vector3.forward * input_move.y + transform.rotation * Vector3.right * input_move.x).normalized;
        yaw = input_view.normalized.x * Time.deltaTime * camRotateSpeed;
        transform.Rotate(Vector3.up * yaw);
    }
    protected void AnimationState()
    {
        if (_anim == null)
        {
            return;
        }
        
        if (direction.magnitude > 0)
        {
            _anim.SetBool("Running", true);
            _anim.SetBool("Idle", false);
        }else if(direction.magnitude <= 0)
        {
            _anim.SetBool("Running", false);
            _anim.SetBool("Idle", true);
        }

        if(stunned){
            _anim.SetBool("Stunned", true); 
        }

    }
    protected abstract void Attack();

    protected void TakeDamage(int damage)
    {
         if (!_pv.IsMine) return;
        _pv.RPC("RPC_TakeDamage", RpcTarget.All, damage);
    }

    [PunRPC]

    public virtual void RPC_TakeDamage(int damage)
    {
        if(!stunned){ // Necessary to stop playing hit sounds while player is stunned/ stop them from lowering health further
           _pv.RPC("RPC_PlayHit", RpcTarget.All);
            healthAmnt -= damage;
        }

        if (healthAmnt <= 0 && !stunned) {
           _pv.RPC("RPC_PlaySound", RpcTarget.All); //_pv.RPC calls are necessary to play sound for everyone involved
            stunned = true;
            // StartCoroutine(Stunned()); // delay movement
        }
    }

    protected virtual void Stun()
    {

    }

    [PunRPC]

    public virtual void RPC_Stun()
    {

    }

    protected virtual void PlaySound(string sound)
    {

    }

    public virtual void RPC_PlaySound(string sound)
    {

    }

    protected IEnumerator DamageOtherPlayer()
    {
        yield return null;
    }


    public void UpdateScoreboard(int score)
    {

    }

    public void UpdateRecord(int score)
    {

    }

}
