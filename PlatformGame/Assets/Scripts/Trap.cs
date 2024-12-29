using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject myObject;
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
        if (collision.gameObject.tag == "character")
        {
            rb.gravityScale = 2;
            Invoke("Destroy", 3f);
        }
    }
    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3000f);
    }
    
}
