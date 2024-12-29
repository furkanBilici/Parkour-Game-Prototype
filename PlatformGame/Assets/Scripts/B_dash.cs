using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_dash : MonoBehaviour
{
    public Vector3 y�n;
    public Animator character;
    public Transform body;
    bool move=false;

    void FixedUpdate()
    {
        Moving();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "character" && character.GetBool("IsDash"))
        {
            move = true;
        }
        if (collision.gameObject.tag == "platform" || collision.gameObject.tag == "wall")
        {
            move = false;
            body.transform.position -= y�n;
        }
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "platform" || collision.gameObject.tag == "wall")
        {
            move = false;
        }
    }
    public void Moving()
    {
        if (move)
        {
            body.transform.position += y�n;
        }
        
    }
}
