using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class MovingP : MonoBehaviour
{
 
    public float amplitude = 10f; // Genlik
    public float frequency = 1f; // Frekans
    private Vector3 initialPosition;
    private Vector3 targetPosition;
    public int which;
    bool canMove;
    public Transform Nesne;
    public Transform Character;
    bool IsMove=false;

    void Start()
    {
        canMove = false;
        initialPosition = transform.position;
        targetPosition = transform.position+ new Vector3(5,0,0);
    }


    void FixedUpdate()
    {
        Move();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "character" && IsMove)
        {
            
            
            Character.position = new Vector3(Character.position.x + 0.041f, Character.position.y, Character.position.z);
        }
    }
    
    void Move()
    {

        if (canMove && (targetPosition.x > transform.position.x))
        {
            IsMove = true;
            Nesne.position = new Vector3(Nesne.position.x + 0.041f, Nesne.position.y, Nesne.position.z);
        }
        else
        {
            IsMove=false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            canMove = true;
        }
    }

}
