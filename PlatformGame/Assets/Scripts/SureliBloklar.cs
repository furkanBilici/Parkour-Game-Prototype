using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SureliBloklar : MonoBehaviour
{
    public GameObject blok1, blok2;
    float sure;
    public float frekans;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SureliBlok();
        
    }
    void SureliBlok()
    {
        sure=Mathf.Sin(frekans*Time.time);
        if (sure > 0)
        {
            blok1.SetActive(true);
            blok2.SetActive(false);
        }
        if (sure < 0)
        {
            blok1.SetActive(false);
            blok2.SetActive(true);
        }
    }
}
