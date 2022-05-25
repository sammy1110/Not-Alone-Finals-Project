using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip hitSound;
    public AudioClip throwSound;
    Animator animator;
    public GameObject projectilePrefab;
    Vector2 lookDirection = new Vector2(1, 0);

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public float MovementSpeed = 1;
    [SerializeField] private LayerMask platformsLayerMask;


    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxcollider2d;

    public int maxHealth = 100;
    int currentHealth;
    public int health { get { return currentHealth; } }



    

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxcollider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        currentHealth = 1;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed; 
    }

    private void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float JumpVelocity = 30f;
            rigidbody2d.velocity = Vector2.up * JumpVelocity;

            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                    isInvincible = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Launch();
            }
        }
    }


    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(boxcollider2d.bounds.center, boxcollider2d.bounds.size, 0f, Vector2.down, .1f, platformsLayerMask);
        return raycastHit2d.collider !=  null;
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
        if (isInvincible)
            return;

        isInvincible = true;
        invincibleTimer = timeInvincible;
        PlaySound(hitSound);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
        PlaySound(throwSound);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
