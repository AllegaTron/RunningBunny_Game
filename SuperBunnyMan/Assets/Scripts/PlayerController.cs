using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SHG.AnimatorCoder;
using UnityEngine.Events;

public class PlayerController : AnimatorCoder
{
    public static PlayerController instance;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float sprintSpeed = 20.0f;
    [SerializeField]
    private float crouchSpeed = 5.0f;
    [SerializeField]
    private float jumpForce = 5f;
    private float moveHorizontal;
    private float currentSpeed;
    private bool isSprintingPressed = false;
    private bool isCrouchingPressed = false;
    private bool isJumpingPressed = false;
    Vector3 movement;

    public int maxHealth = 100;
    public static int currentHealth;
    private bool isDying = false;
    private float damageCooldown = 0f;
    public AudioClip deathSound;
    private AudioSource audioSource;

    private bool isGrounded = false;
    public Transform groundCheck;
    public LayerMask groundLayer;
    [SerializeField]
    private float checkGroundRadius = 0.3f;

    private Rigidbody rb;

    //Боевая система
    public GameObject hitbox1;
    public GameObject hitbox2;
    public GameObject hitbox3;

    //private bool isAttacking = false;

    private float punchCooldown = 1f;
    private float kickCooldown = 2f;
    private float flyingKickCooldown = 4f;

    private float nextPunchTime = 0f;
    private float nextKickTime = 0f;
    private float nextFlyingKickTime = 0f;

    private readonly AnimationData Idle = new(Animations.Idle);
    private readonly AnimationData Walk = new(Animations.Walk);
    private readonly AnimationData Run = new(Animations.Run);
    private readonly AnimationData Crouch = new(Animations.Crouch);
    private readonly AnimationData Boxing = new(Animations.Boxing);
    private readonly AnimationData Push = new(Animations.Push);
    private readonly AnimationData Punch = new(Animations.Punch);
    private readonly AnimationData Kick = new(Animations.Kick);
    private readonly AnimationData FlyingKick = new(Animations.FlyingKick);
    private readonly AnimationData Jump = new(Animations.Jump);
    private readonly AnimationData JumpDown = new(Animations.JumpDown);
    private readonly AnimationData JumpDown2 = new(Animations.JumpDown2);
    private readonly AnimationData Fall = new(Animations.Fall);
    private readonly AnimationData Climbing = new(Animations.Climbing);
    private readonly AnimationData ClimbingUpOnWall = new(Animations.ClimbingUpOnWall);
    private readonly AnimationData DodgingBack = new(Animations.DodgingBack);
    private readonly AnimationData RunningSlide = new(Animations.RunningSlide);
    private readonly AnimationData WalkingOnStairs = new(Animations.WalkingOnStairs);
    private readonly AnimationData Pickup = new(Animations.Pickup);
    private readonly AnimationData Hit1 = new(Animations.Hit1);
    private readonly AnimationData Hit2 = new(Animations.Hit2);
    private readonly AnimationData Hit3 = new(Animations.Hit3);
    private readonly AnimationData Hit4 = new(Animations.Hit4);
    private readonly AnimationData Dying = new(Animations.Dying);

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Initialize();
        rb = GetComponent<Rigidbody>();
        //audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        moveHorizontal = Input.GetAxis("Horizontal");
        isSprintingPressed = Input.GetKey(KeyCode.LeftShift);
        isCrouchingPressed = Input.GetKey(KeyCode.LeftControl);
        isJumpingPressed = Input.GetKeyDown(KeyCode.Space);

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);

        currentSpeed = isCrouchingPressed ? crouchSpeed : isSprintingPressed ? sprintSpeed : speed;
        DefaultAnimation(0);
        CheckJump();
        CheckDying();
        CheckAttack();

        if (moveHorizontal != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(movement);
            transform.rotation = rotation;
        }

        void CheckJump()
        {
            if (GetBool(Parameters.GROUNDED) && isJumpingPressed)
            {
                //this.enabled = false
                Play(new(Animations.Jump, true, new()));
                StartCoroutine(ReloadJumpColdown(0.5f));
            }
        }
        void CheckDying()
        {
            if (currentHealth <= 0 && !isDying)
            {
                currentSpeed = 0;
                isDying = true;
                this.enabled = false;
                SetLocked(false, 0);
                Play(new(Animations.Dying, true));
                //audioSource.PlayOneShot(deathSound);
                GameManager.instance.GameOver();
            }
        }
        void CheckAttack()
        {
            if (Input.GetMouseButtonDown(0) && Time.time >= nextPunchTime)
            {
                StartCoroutine(ActivateHitbox(hitbox1, 1f, "Punch"));
                nextPunchTime = Time.time + punchCooldown;
                Play(new(Animations.Punch, true, new()));
            }
            else if (Input.GetMouseButtonDown(1) && Input.GetKey(KeyCode.LeftShift) && Time.time >= nextFlyingKickTime)
            {
                StartCoroutine(ActivateHitbox(hitbox3, 4f, "FlyingKick"));
                nextFlyingKickTime = Time.time + flyingKickCooldown;
                Play(new(Animations.FlyingKick, true, new()));
            }
            else if (Input.GetMouseButtonDown(1) && Time.time >= nextKickTime)
            {
                StartCoroutine(ActivateHitbox(hitbox2, 2f, "Kick"));
                nextKickTime = Time.time + kickCooldown;
                Play(new(Animations.Kick, true, new()));
            }
        }

        if (damageCooldown > 0)
        {
            damageCooldown -= Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new(currentSpeed * moveHorizontal, rb.velocity.y);
        isGrounded = Physics.CheckSphere(groundCheck.position, checkGroundRadius, groundLayer);
        SetBool(Parameters.GROUNDED, isGrounded);
        SetBool(Parameters.FALLING, !GetBool(Parameters.GROUNDED) && rb.velocity.y < 0);
        //SetBool(Parameters.ATTACKING, isAttacking);
    }
    public override void DefaultAnimation(int layer)
    {
        if (moveHorizontal != 0)
        {
            if (isSprintingPressed)
            {
                Play(Run);
            }
            else if (isCrouchingPressed)
            {
                Play(Crouch);
            }
            else
            {
                Play(Walk);
            }
        }
        else
        {
            Play(Idle);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && damageCooldown <= 0)
        {
            TakeDamage(25);
            damageCooldown = 1f;
        }
        if (collision.gameObject.CompareTag("Carrot"))
        {
            GameManager.instance.Win();
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        int newHealth = Mathf.Max(GameManager.instance.currentHP - 25, 0);
        GameManager.instance.SetHealth(newHealth);

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }
    IEnumerator ReloadJumpColdown(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private IEnumerator ActivateHitbox(GameObject hitbox, float duration, string animation)
    {
        hitbox.SetActive(true);
        yield return new WaitForSeconds(duration);
        hitbox.SetActive(false);
    }
}
