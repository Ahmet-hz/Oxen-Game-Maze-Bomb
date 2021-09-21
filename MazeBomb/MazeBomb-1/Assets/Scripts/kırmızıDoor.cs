using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kırmızıDoor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "DostHero(Clone)" || other.gameObject.name == "DostHeroAmaClone(Clone)")
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.transform.GetChild(3).GetComponent<ParticleSystem>().Play();
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            Destroy(other.gameObject, 0.2f);
        }
    }
}
