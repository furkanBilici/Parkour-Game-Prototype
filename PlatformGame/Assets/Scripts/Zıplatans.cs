using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zıplatans : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            anim.SetBool("IsTouch", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            anim.SetBool("IsTouch", false);
        }
    }
}
