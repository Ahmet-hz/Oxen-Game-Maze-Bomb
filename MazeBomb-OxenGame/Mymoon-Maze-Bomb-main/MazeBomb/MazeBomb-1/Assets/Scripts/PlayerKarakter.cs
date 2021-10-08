using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerKarakter : MonoBehaviour
{
    NavMeshAgent navMeshAgent;
    GameManager gameManager;


    [SerializeField] bool dusmanKont = true;
    [SerializeField] Transform hedef;
    [SerializeField] float range = 10f;
    Transform _hedef;


    public GameObject yokOlmaEfekt;

    TextMeshProUGUI k�rm�z�Yaz�;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (GameObject.FindGameObjectWithTag("Hedef2") != null)
        {
            _hedef = GameObject.FindGameObjectWithTag("Hedef2").transform;
            hedef = _hedef;
        }

        k�rm�z�Yaz� = GameObject.Find("Canvas/D��man/text").GetComponent<TextMeshProUGUI>();

        HedefSecAi();

    }

    private void FixedUpdate()
    {

        

        Collider[] �evre = Physics.OverlapSphere(transform.position, 3f);
        
        
            foreach (Collider d�smanlar in �evre)
            {
                if (d�smanlar.tag == "AiKarakter" && d�smanlar.gameObject.GetComponent<BoxCollider>().enabled == true)
                {
                    hedef = d�smanlar.gameObject.transform;
                }
                else
                {
                    hedef = null;
                    HedefSecAi();
                }
            }
        
       

        navMeshAgent.SetDestination(hedef.transform.position);
    }

   
    void HedefSecAi()
    {
        if (hedef == null)
        {
            hedef = _hedef;
        }

    }

    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "AiKarakter" || other.gameObject.tag=="AiKarakterClone")
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            other.gameObject.GetComponent<BoxCollider>().enabled = false;

            gameObject.GetComponent<NavMeshAgent>().speed = 0;
            other.gameObject.GetComponent<NavMeshAgent>().speed = 0;

            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            gameObject.transform.GetChild(1).gameObject.SetActive(false);

            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            other.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            other.gameObject.transform.GetChild(1).gameObject.SetActive(false);


            gameObject.transform.GetChild(3).GetComponent<ParticleSystem>().Play();

            other.gameObject.transform.GetChild(3).GetComponent<ParticleSystem>().Play();

            Destroy(other.gameObject, 0.3f);
            Destroy(gameObject, 0.3f);

        }
        if (other.gameObject.tag == "Hedef2")
        {
            Destroy(this.gameObject);
            int skor = int.Parse(k�rm�z�Yaz�.text);
            skor -= 1;
            if (skor == 0)
            {
                gameManager.WinPanel();
                Debug.Log("Zafer");
                //Time.timeScale = 0;
            }
            k�rm�z�Yaz�.text = skor.ToString();
            GameObject.Find("ExplosionNovaFire").GetComponent<ParticleSystem>().Play();
        }


    }


}
