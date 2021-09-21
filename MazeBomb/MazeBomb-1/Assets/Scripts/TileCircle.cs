using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCircle : MonoBehaviour
{
    public Rigidbody bulletPrefab;
    [SerializeField] GameObject cursor;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform shootPoint;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] float shootRange;
    public int lineSegment = 10;

    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
        lineRenderer.positionCount = lineSegment;
    }

    private void Update()
    {
        LounchProjectile();
    }

    void LounchProjectile()
    {
        Ray camRay = camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(camRay, out hit, shootRange, layer))
        {
            
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
            
            Vector3 Vo = CalculateVelocity(hit.point, shootPoint.position, 1f);

            Visualize(Vo);

            transform.rotation = Quaternion.LookRotation(Vo);

            if (Input.GetMouseButtonDown(0))
            {
                Rigidbody obj = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
                obj.velocity = Vo;
            }
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
        float sY= (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vo.y * time) + shootPoint.position.y;
        
        result.y=sY;
        return result;
    }


}
