using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePlayer : MonoBehaviour
{
    [SerializeField] GameObject playerChracter;


    [SerializeField] float diceTime;
    [SerializeField] float diceSpeed;

    [SerializeField] int carpma = 0;
    [SerializeField] bool carpti = false;


    private void FixedUpdate()
    {
        if (carpti)
        {
            diceTime -= Time.deltaTime * diceTime;

            if (diceTime <= 1f)
            {
                //animasyon çalýþacak 

                Destroy(this.gameObject);
                Instantiate(playerChracter, transform.position, Quaternion.identity);
                
                carpti = false;
            }
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        

        if (carpma >= 2)
        {
            
            carpti = true;
        }

        carpma++;



        
    }
   
}
