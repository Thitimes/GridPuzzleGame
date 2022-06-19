using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swtich : MonoBehaviour
{
    public bool isPressed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            isPressed = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("box"))
        {
            isPressed = false;
        }
    }
}
