using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_Ölümcül : MonoBehaviour
{
    public float cap = 2f;
    public Transform nesne;
    public float aci=2f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    public void Movement()
    {
        // Dairesel hareket için x ve y eksenlerini güncelle
        float x = Mathf.Cos(aci*Time.time) * cap;
        float y = Mathf.Sin(aci*Time.time) * cap;

        // Yeni pozisyonu ayarla
        Vector3 yeniPozisyon = new Vector3(nesne.position.x + x,nesne.position.y+y, transform.position.z);
        transform.position = yeniPozisyon;
    }
}
