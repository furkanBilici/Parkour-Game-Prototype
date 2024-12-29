using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashCristal : MonoBehaviour
{
    public MovementPlayer character;
    public GameObject gem;
    bool bok;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (character.GetComponent<MovementPlayer>().IsGrounded)
        {
            gem.GetComponent<SpriteRenderer>().color = Color.white;
            bok = true;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character" && bok)
        {
            gem.GetComponent<SpriteRenderer>().color = Color.black;
            if (character.GetComponent<MovementPlayer>().sayac == 0)
            {
                character.GetComponent<MovementPlayer>().sayac++;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character" )
        {
            
                bok = false;
            
        }
    }
}
