using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class AiKarakter : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField] bool dusmanKont = true;
    [SerializeField] Transform hedef;
    [SerializeField] float range = 3f;
    Transform _hedef;

    bool �lmek;

    TextMeshProUGUI maviYaz�;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Hedef1") != null)
        {
            _hedef = GameObject.FindGameObjectWithTag("Hedef1").transform;
            hedef = _hedef;
        }

        maviYaz� = GameObject.Find("Canvas/Biz/text").GetComponent<TextMeshProUGUI>();

    }

    




    private void FixedUpdate()
    {
      
        HedefSecAi();

    }


  

    void HedefSecAi()
    {
        if (hedef == null)
        {
            hedef = _hedef;
        }

        if (!�lmek)
        {
            navMeshAgent.SetDestination(hedef.transform.position);
        }
        

    }

    
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Hedef1")
        {

            GameObject.Find("ExplosionNovaBlue").GetComponent<ParticleSystem>().Play();
            int skor = int.Parse(maviYaz�.text);
            skor -= 1;
            if(skor == 0)
            {
                Debug.Log("oyun bitti");
                Time.timeScale = 0;
            }
            maviYaz�.text = skor.ToString();
            Destroy(this.gameObject);
        }
    }

}
