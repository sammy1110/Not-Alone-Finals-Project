using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] BoxCollider2D boxCollider2d;
    [SerializeField] private LayerMask playerLayer; 
    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only whenplayer in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("BobDuncan_shoot");
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider2d.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider2d.bounds.size.x * range, boxCollider2d.bounds.size.y, boxCollider2d.bounds.size.z), 
            0, Vector2.left, 0, playerLayer);


        return hit.collider != null; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider2d.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider2d.bounds.size.x * range, boxCollider2d.bounds.size.y, boxCollider2d.bounds.size.z));

    }
}
