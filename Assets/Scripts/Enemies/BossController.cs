using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
    public int maxHP;
    private int currentHP;

    private Rigidbody2D rigidbody2d;
    public GameObject projectilePrefab;

    public GameObject hammerPrefab;

    public float timeToMove;

    public float attackCooldown;
    private float attackTimer;

    public float hammerThrowCooldown;

    //determines which attack the boss will use next
    int attackPattern;

    public HealthBar healthBar;

    Animator animator;

    AudioSource audioSource;
    public AudioClip fireballSound;

    void Start()
    {
        attackTimer = 3;
        attackPattern = 0;
        currentHP = maxHP;

        healthBar.SetMaxHealth(maxHP);
        healthBar.SetHealth(currentHP);

        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();

        audioSource = GetComponent<AudioSource>();
    }

    void Update() {
        attackTimer -= Time.deltaTime;

        if(attackTimer <= 0) {
            switch(attackPattern) {
                case 0:
                    Fireball();
                    break;
                case 1:
                    StartCoroutine(Lunge());
                    break;
                case 2: 
                    StartCoroutine(HammerToss());
                    break;
            }
            attackTimer = attackCooldown;
            attackPattern = (attackPattern+1)%3;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController p = other.gameObject.GetComponent<PlayerController>();

        if(p != null) {
            p.TakeDamage(4);
        }
    }

    IEnumerator Lunge() {
        Vector3 pos = transform.position;
        Vector3 targetPos = pos + Vector3.left*10;
        
        float elapsedTime = 0;

        while(elapsedTime < timeToMove/2) {
            transform.position = Vector3.Lerp(pos, targetPos, (elapsedTime / (timeToMove)));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        while(elapsedTime >= timeToMove/2 && elapsedTime < timeToMove) {
            transform.position = Vector3.Lerp(targetPos, pos, (elapsedTime / (timeToMove)));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    void Fireball() {
        animator.SetTrigger("Shot");
        audioSource.PlayOneShot(fireballSound);
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position+Vector2.left*2, Quaternion.identity);

        BossProjectile projectile = projectileObject.GetComponent<BossProjectile>();

        projectile.Launch(Vector2.left,400);
    }

    IEnumerator HammerToss() {
        float timer = 0f;
        int numShots = 6;

        while(numShots > 0) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                GameObject hammerObject = Instantiate(hammerPrefab, rigidbody2d.position+Vector2.up*2, Quaternion.identity);
                EnemyBullet hammer = hammerObject.GetComponent<EnemyBullet>();

                float upMod = 0.75f + (2f*Random.value);
                float forceMod = 1300 + (200*Random.value);

                hammer.Launch(Vector2.left+Vector2.up*upMod,1400);

                timer = hammerThrowCooldown;
                numShots--;

                yield return null;
            }
            else {
                yield return null;
            }
        }
        yield return null;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        healthBar.SetHealth(currentHP);
        if(currentHP <= 0) {
            Destroy(gameObject);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Victory");
        }
    }
}
