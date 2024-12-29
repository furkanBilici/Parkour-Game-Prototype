using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    public GameObject myObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "character")
        {
            Invoke("Destroy", 3f);
        }
    }
    public void Destroy()
    {
        myObject.SetActive(false);
    }
 
}
