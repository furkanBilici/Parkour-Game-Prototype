using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[CreateAssetMenu(fileName = "SaveManager", menuName = "ScriptableObject/SaveManager", order = 0)]
public class SaveManager : ScriptableObject
{
    public int a = 0;

    // Checkpoint kaydetme fonksiyonu
    public void CheckpointSave(Vector3 lastCheckpoint)
    {
        PlayerPrefs.SetInt("konumX", Convert.ToInt32(lastCheckpoint.x));
        PlayerPrefs.SetInt("konumY", Convert.ToInt32(lastCheckpoint.y));  // 'konumy' yerine 'konumY' kullan�ld�
        PlayerPrefs.Save();
    }

    // Checkpoint y�kleme fonksiyonu
    public Vector3 CheckpointLoad()  // 'ChechPointLoad' -> 'CheckpointLoad' olarak d�zeltildi
    {
        // Konumun daha �nce kaydedilip edilmedi�ini kontrol et
        if (PlayerPrefs.HasKey("konumX") && PlayerPrefs.HasKey("konumY"))
        {
            return new Vector3(PlayerPrefs.GetInt("konumX"), PlayerPrefs.GetInt("konumY"), 0);
        }
        else
        {
            // E�er kay�tl� konum yoksa varsay�lan de�er d�nd�r
            return new Vector3(0, 0, 0);
        }
    }

    // �leride kullan�labilecek bir fonksiyon �ablonu
    void CheckPointtenmi()
    {
        // Bu fonksiyon i�eri�i doldurulabilir
    }
}