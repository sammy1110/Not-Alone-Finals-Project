using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    [HideInInspector]
    public bool mustPatrol;
    private bool mustTurn;

    Rigidbody2D rigidbody2d;

    public Transform groundCheckPos;
    public LayerMask platfromLayer;

    public float walkSpeed;

    private void Start()
    {
        mustPatrol = true;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(mustPatrol)
        {
            Patrol();
        }
    }

    private void FixedUpdate()
    {
        if (mustPatrol)
        {
            mustTurn = !Physics2D.OverlapCircle(groundCheckPos.position, 0.1f, platfromLayer);
        }
    }

    void Patrol()
    {
        if (mustTurn)
        {
            Flip();
        }

        rigidbody2d.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rigidbody2d.velocity.y);
    }

    void Flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
