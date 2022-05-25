using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public float Hitpoints;
    public float MaxiHitpoints = 5;

    // Start is called before the first frame update
    void Start()
    {
        Hitpoints = MaxiHitpoints;
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
            player.ChangeHealth(-1);
        }
    }
}
