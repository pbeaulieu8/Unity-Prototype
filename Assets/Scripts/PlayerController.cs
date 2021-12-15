using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    BoxCollider2D boxCollider2d;
    Animator animator;

    [SerializeField] private LayerMask groundLayerMask;

    public GameObject bulletPrefab;
    public GameObject explosivePrefab;

    public int maxHP;
    private int currentHP;

    public float speed;
    public float jumpForce;

    float horizontal;
    float vertical;
    
    //-1 = left, 1 = right
    float lookDirection;
    //true if character is looking up
    bool lookUp;

    //number of consecutive jumps allowed without toubching the ground
    //default 1, increases when player unlocks double jump
    public int maxJumps;
    int currentJumps;

    //timer set when player jumps
    //without timer, current jumps may be reset before leaving the ground
    //allowing for unintentional extra jumps
    public float jumpTimer;

    //set to true when the player gets the stopwatch
    //allows the player to slow time
    bool hasStopwatch;
    bool hasExplosive;

    public HealthBar healthBar;

    private int money;
    public MoneyText moneyText;
    public AttackText attackText;
    public ArmorText armorText;

    private int attack;
    private int armor;

    private AudioSource audioSource;
    public AudioClip shootSound;

    // Start is called before the first frame update
    void Start()
    {
        attack = 1;
        armor = 0;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //default facing right
        lookDirection = 1;
        lookUp = false;
        currentJumps = maxJumps;
        hasStopwatch = false;
        hasExplosive = false;
        money = 0;

        currentHP = maxHP;
        healthBar.SetMaxHealth(maxHP);
        healthBar.SetHealth(currentHP);

        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(jumpTimer > 0) {
            jumpTimer -= Time.deltaTime;
        }


        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (boxCollider2d.bounds.extents.y + 0.1f), Color.green);

        //get movement input
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(horizontal != 0) {
            lookDirection = horizontal;
            animator.SetFloat("Direction", horizontal);
        }
        //tell animator to animate character looking up when UP is pressed
        if(vertical == 1) {
            lookUp = true;
            animator.SetFloat("LookUp", 1);
        } else {
            lookUp = false;
            animator.SetFloat("LookUp", 0);
        }

        
        bool isGrounded = IsGrounded();
        if(isGrounded && jumpTimer <= 0) {
            currentJumps = maxJumps;
        }
        animator.SetBool("Grounded", isGrounded);

        animator.SetFloat("Speed", Mathf.Abs(horizontal*speed));

        //jump
        if (Input.GetKeyDown(KeyCode.Z) && currentJumps > 0)
        {
            jumpTimer = 0.5f;
            --currentJumps;
            rigidbody2d.AddForce(transform.up*jumpForce, ForceMode2D.Impulse);
        }
        //shoot
        if(Input.GetKeyDown(KeyCode.X))
        {
            Launch();
        }

        //shoot explosive
        if(Input.GetKeyDown(KeyCode.C) && hasExplosive)
        {
            LaunchExplosive();
        }

        //slow time while holding spacebar
        if(Input.GetKeyDown(KeyCode.Space) && hasStopwatch) {
            Time.timeScale = 0.5f;
        } 
        //check if timescale>0 to ensure the game isn't paused
        //otherwise time will resume while in the pause menu
        else if(Input.GetKeyUp(KeyCode.Space) && Time.timeScale>0f) {
            Time.timeScale = 1f;
        }
    }

    void FixedUpdate() {   
        //move character
        Vector3 movement = new Vector3(horizontal, 0.0f, 0.0f);
        transform.position += movement * speed * Time.deltaTime;

        //rigidbody2d.MovePosition(position);
    }

    //check if player is on the ground
    //used for determining if player is able to jump
    private bool IsGrounded() {
        float extraHeight = 0.05f;
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeight, groundLayerMask);

        return raycastHit.collider != null;
    }    

    void Launch()
    {
        Vector2 shotDirection;
        if(lookUp) {
            shotDirection = Vector2.up;
        } else if(lookDirection == -1) {
            shotDirection = Vector2.left;
        } else {
            shotDirection = Vector2.right;
        }

        GameObject bulletObject = Instantiate(bulletPrefab, rigidbody2d.position+Vector2.up*1.45f+shotDirection*0.8f, Quaternion.identity);

        Bullet bullet = bulletObject.GetComponent<Bullet>();

        PlaySound(shootSound);

        bullet.Launch(shotDirection,800,attack);
    }

    void LaunchExplosive() {
        Vector2 shotDirection;
        Quaternion rotation;
        if(lookUp) {
            shotDirection = Vector2.up;
            rotation = Quaternion.Euler(0f,0f,270f);
        } else if(lookDirection == -1) {
            shotDirection = Vector2.left;
            rotation = Quaternion.Euler(0f,0f,0f);
        } else {
            shotDirection = Vector2.right;
            rotation = Quaternion.Euler(0f,0f,180f);
        }

        GameObject explosiveObject = Instantiate(explosivePrefab, rigidbody2d.position+Vector2.up*1.45f+shotDirection*0.8f, transform.rotation * rotation);

        ExplosiveProjectile explosive = explosiveObject.GetComponent<ExplosiveProjectile>();

        explosive.Launch(shotDirection,800);

    }

    public void TakeDamage(int damage) {
        currentHP -= (damage - armor);
        healthBar.SetHealth(currentHP);

        if(currentHP <= 0) {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Game Over");
        }
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void GetStopwatch() {
        hasStopwatch = true;
    }

    public void GetExplosive() {
        hasExplosive = true;
    }

    public void AddMoney(int moneyIncrease) {
        money += moneyIncrease;
        moneyText.AddMoney(moneyIncrease);
    }

    public int GetMoney() {
        return money;
    }

    public int GetAttack() {
        return attack;
    }

    public void SetAttack(int attackValue) {
        attack = attackValue;
        attackText.UpdateAttack(attackValue);
    }

    public int GetArmor() {
        return armor;
    }

    public void SetArmor(int armorValue) {
        armor = armorValue;
        armorText.UpdateArmor(armor);
    }

}
