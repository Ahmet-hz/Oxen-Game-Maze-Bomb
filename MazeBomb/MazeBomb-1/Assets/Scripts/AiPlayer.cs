using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AiPlayer : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    [SerializeField] GameObject cursor;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform shootPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField][Range(0.1f,10)] float shootTime;
    public int lineSegment = 10;
    [SerializeField] int daire;

    [SerializeField] Vector3 aiVector = new Vector3(0f, 0.063f, 4f);
    [SerializeField] Vector3 refVector;
    [SerializeField] float refX;
    [SerializeField] float refZ;
    [SerializeField] bool calis = false;
    [SerializeField] bool range = false;
    [SerializeField] float timeCalis = 1f;
    [SerializeField] float speed=1f;


    private Camera camera;

   
    private void Start()
    {
        camera = Camera.main;
        lineRenderer.positionCount = lineSegment;
    }

    

    private void FixedUpdate()
    {
        AiMoved();
        LounchProjectile();
    }
    

    void LounchProjectile()
    {
        
       
        cursor.transform.position = aiVector + Vector3.up * 0.1f;
            
        

        Vector3 Vo = CalculateVelocity(aiVector, shootPoint.position, 1f);

            Visualize(Vo);

            transform.rotation = Quaternion.LookRotation(Vo);
            if (shootTime <= 0)
            {
                  
                   Rigidbody obj = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                    obj.velocity = Vo;
                    shootTime = Random.Range(0.1f,5);
                 
                   calis = true;

            }
            else if (shootTime <= 2f && cursor.active==false)
            {
                cursor.SetActive(true);
                lineRenderer.enabled = true;
            }
            else
            {
                shootTime -= Time.deltaTime;
            }
           
        
    }
    void Visualize(Vector3 vo)
    {
        for (int i = 0; i < lineSegment; i++)
        {
            Vector3 pos = CalculatePosInTime(vo, i / (float)lineSegment);
            lineRenderer.SetPosition(i, pos);
        }
    }



    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;


    }

    Vector3 CalculatePosInTime(Vector3 vo, float time)
    {
        Vector3 Vxz = vo;
        Vxz.y = 0f;

        Vector3 result = shootPoint.position + vo * time;
        float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;

        result.y = sY;
        return result;
    }


    void   AiMoved()
    {
        if (calis)
        {
            if (range)
            {
                refX = Random.Range(-4.57f, 4.57f);
                refZ = Random.Range(12.75f, 7.25f);
                range = false;
            }
            
             
            MoveVec(refX, refZ);

        }

        timeCalis -= Time.deltaTime * speed;


        if (timeCalis <= 0f)
        {
            range = true;
            calis = false;
            timeCalis = 1f;
        }
        
        //if (Random.Range(-4.57f,0f) < aiVector.x && aiVector.x < Random.Range(0f,4.57f))
        //{
        //    if (4.82f < aiVector.z && aiVector.x < 6.57f)
        //    {
        //        aiVector = aiVector + new Vector3(0.2f, 0f, 0.2f);
        //    }
        //    else
        //    {
        //        aiVector = aiVector - new Vector3(0.2f, 0f, 0.2f);
        //    }

        //}

    }

    void MoveVec(float _x , float _z)
    {

        if (_x > aiVector.x)
        {
            aiVector = aiVector + new Vector3(0.1f, 0f, 0f);
            //print(_x + " " + aiVector.x);
            //print(1);
        }
        if (_x < aiVector.x)
        {
            //print(_x + " " + aiVector.x);
            //print(2);
            aiVector = aiVector - new Vector3(0.1f, 0f, 0f);
        }

        if (_z > aiVector.z)
        {
            //print(_z + " " + aiVector.z);
            //print(1);

            aiVector = aiVector + new Vector3(0, 0f, 0.1f);
        }
        if (_z < aiVector.z)
        {
            //print(_z + " " + aiVector.z);
            //print(2);
            aiVector = aiVector - new Vector3(0f, 0f, 0.1f);

        }





    }




}
