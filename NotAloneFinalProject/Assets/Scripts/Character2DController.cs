using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character2DController : MonoBehaviour
{
    public float MovementSpeed = 1;

    private Rigidbody2D rigidbody2d;
   

    public GameObject bulletPrefab;

    

    // Start is called before the first frame update
    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            float JumpVelocity = 30f;
            rigidbody2d.velocity = Vector2.up * JumpVelocity;
        }

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Launch();
        }
    }

    void Launch()
    {
        GameObject bulletObject = Instantiate(bulletPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        
        Projectile bullet = bulletObject.GetComponent<Projectile>();

        
    }
}
