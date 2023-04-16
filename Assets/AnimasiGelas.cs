using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimasiGelas : MonoBehaviour
{
    public float kecepatanPutaran = 30f; // Kecepatan putaran gelas
    public bool sedangBerputar = false; // Status apakah gelas sedang berputar

    void Start()
    {
        // Memulai animasi putaran gelas saat script dijalankan
        MulaiPutaranGelas();
    }

    void MulaiPutaranGelas()
    {
        if (!sedangBerputar)
        {
            // Memulai animasi putaran gelas jika belum sedang berputar
            StartCoroutine(PutaranGelas());
        }
    }

    IEnumerator PutaranGelas()
    {
        sedangBerputar = true;

        while (true)
        {
            // Melakukan putaran objek gelas di sekitar sumbu Y
            transform.Rotate(Vector3.down * kecepatanPutaran * Time.deltaTime);

            yield return null;
        }
    }
}