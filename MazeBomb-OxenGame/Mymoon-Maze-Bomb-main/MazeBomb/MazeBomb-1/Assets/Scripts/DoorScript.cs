using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//KAPIDA CARPANA YADA TOPLAMA 0 DAN AKLI DEÐER VERMEZ ÝSEN 
// SORUN ÇIKACAKTIR
// TEXT BOÞTU TEXT YAZI FONTU VS NASIL ÝSTERSESN 
// KODLA ALAKALI SOUNLARI ÝLETÝSEN DÜZELTÝR ÝLETÝRÝM

public class DoorScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int toplam;
    [SerializeField] int carpan;
    [SerializeField] GameObject olusutulanKarakter;

    bool sec;//true ise carpma , false ise toplama

    float aktifSure = 0;


    private void Start()
    {
        if (toplam == 0)
        {
            text.text = "x" + carpan.ToString();
            sec = true;
        }
        else if (carpan == 0)
        {
            text.text = "+"+toplam.ToString();
            sec = false;
        }
    }

    private void FixedUpdate()
    {
        aktifSure -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerKarakter") 
        {
            if (sec)
            {
                for (int i = 0; i < carpan; i++)
                {
                   GameObject olusturulanKarakter = Instantiate(olusutulanKarakter, transform.position, Quaternion.identity);
                }
            }
            else if (!sec)
            {
                if (aktifSure <= 0 && toplam > 0)
                {
                    //for (int b = 0; b <= toplam; b++)
                    //{
                    //    Instantiate(olusutulanKarakter, transform.position, Quaternion.identity);

                    //}
                    //aktifSure = 5f;
                    Instantiate(olusutulanKarakter, transform.position, Quaternion.identity);
                    //toplam -= 1;
                    text.text = "+" + toplam.ToString();
                }

            }

        }
    }







}
