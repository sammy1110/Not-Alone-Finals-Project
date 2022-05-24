using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int batteries = 0;

    [SerializeField] private Text batteriesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Battery"))
        {
            Destroy(collision.gameObject);
            batteries++;
            batteriesText.text = "Batteries: " + batteries;
        }
    }
}
