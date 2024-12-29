using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class karanahtar : MonoBehaviour
{
    public Rigidbody2D rb;
    public Collider2D col;
    [SerializeField]
    public GameObject target;
    public Vector3 targetPosition;
    public Vector3 velocity = Vector3.zero;
    bool control;
    public Transform item;

    public float smoothTime;

    private void Update()
    {
        ShiftDown();
        follow();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "platform" && !control)
        {
            col.isTrigger = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            control = true;
            rb.gravityScale = 0; 
            col.isTrigger = true;
        }
    }
    public void follow()
    {
        targetPosition = target.transform.position;
        if (control)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }

    }
    public void ShiftDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            control = false;
            rb.gravityScale = 1;
        }

    }
}
