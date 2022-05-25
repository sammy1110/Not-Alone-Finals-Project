using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    public AudioClip collectedClip;
    private int batteries = 0;

    [SerializeField] private Text batteriesText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character2DController controller = collision.GetComponent<Character2DController>();

        if (collision.gameObject.CompareTag("Battery"))
        {
            Destroy(collision.gameObject);
            batteries++;
            batteriesText.text = "Batteries: " + batteries;
        }

    }
}
