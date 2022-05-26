using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float speed;
    public bool vertical;

    public float changeTime = 3.0f;
    float timer;
    int direction = 1;

    public float Hitpoints;
    public float MaxiHitpoints = 5;

    Rigidbody2D rigidbody2d;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        Hitpoints = MaxiHitpoints;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        timer = changeTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }



    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed;
           
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
           
        }

        rigidbody2d.MovePosition(position);
    }


    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        if(Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Character2DController player = other.gameObject.GetComponent<Character2DController>();

        if (player != null)
        {
            player.ChangeHealth(-20);
        }
    }
}
