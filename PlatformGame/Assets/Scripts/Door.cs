using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public MovementPlayer character;
    public GameObject kilit;
    public Collider2D portal;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "character" && character.key > 0)
        {
            character.key--;
            kilit.SetActive(false);
            portal.isTrigger = true;
        }
    }
}
