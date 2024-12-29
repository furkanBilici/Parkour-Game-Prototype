using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Z_Dash : MonoBehaviour
{
    public Vector3 Yon;
    public Rigidbody2D rb;
    public MovementPlayer player;
    void Start()
    {
    }


    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "character" ) 
        {
            rb.AddForce(Yon * player.speed * 1.5f, ForceMode2D.Impulse);
            player.sayac++;
        }
    }
}
