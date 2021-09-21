using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float power = 0.1f;
    public float duration = 0.2f;
    public Transform cam;
    public float slowDownAmount = 1;
    public bool shouldShake = false;

    Vector3 startPosition;
    float initialDuration;

    void Start()
    {
        startPosition = cam.localPosition;
        initialDuration = duration;
    }


    void Update()
    {
        if (shouldShake)
        {
            if (duration > 0)
            {
                cam.localPosition = startPosition + Random.insideUnitSphere * power;
                duration -= Time.deltaTime * slowDownAmount;
            }
            else
            {
                shouldShake = false;
                duration = initialDuration;
                cam.localPosition = startPosition;
            }
        }
    }
}
