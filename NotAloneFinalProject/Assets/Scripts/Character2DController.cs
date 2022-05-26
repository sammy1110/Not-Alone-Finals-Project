using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    
    Vector2 lookDirection = new Vector2(1, 0);


    public float MovementSpeed = 1;
    [SerializeField] private LayerMask platformsLayerMask;


    private Rigidbody2D rigidbody2d;
    private BoxCollider2D boxcollider2d;
    private Animator animator;

    public int maxHealth = 100;
    int currentHealth;
    public int health { get { return currentHealth; } }

    public Projectile ProjectilePrefab;
    public Transform LaunchOffset;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    public GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxcollider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        
    }

    public void Update()
    {

        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (movement == 0)
        {
            animator.SetBool("isRunning", false);
        }else
        {
            animator.SetBool("isRunning", true);
        }


        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            float JumpVelocity = 30f;
            rigidbody2d.velocity = Vector2.up * JumpVelocity;

        }
        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }

        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement < 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if(hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
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

        if (currentHealth <= 0)
        {

            currentHealth = 0;
            GetComponent<Character2DController>().enabled = false;
            animator.SetTrigger("Death");

        }
       
    }

  
   
}
