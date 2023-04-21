using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleScript : MonoBehaviour
{
    public float speed;
    public float waveSpeed;
    public float bonusHeight; 
    public Transform target;

    private float cycle; // This variable increases with time and allows the sine to produce numbers between -1 and 1.
    private Vector3 basePosition; // This variable maintains the location of the object without applying sine changes

    public float bonusHeightMin = 3f;// Set higher if you want more wave intensity
    public float bonusHeightMax = 6f;
    public float waveSpeedMin = 3f; // Higher make the wave faster
    public float waveSpeedMax = 8f;
    public float minSpeed = 2.5f; // more value going faster to target
    public float maxSpeed = 4.5f;


    private void Start()
    {
        // Freeze the object's rotation and position except for the forward direction
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                          RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX |
                          RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        rb.constraints &= ~RigidbodyConstraints.FreezePositionZ;

        basePosition = transform.position;

        waveSpeed = Random.Range(waveSpeedMin, waveSpeedMax);
        speed = Random.Range(minSpeed, maxSpeed);
        bonusHeight = Random.Range(bonusHeightMin, bonusHeightMax);
    }

    private void Update()
    {
        cycle += Time.deltaTime * waveSpeed;

        transform.position = basePosition + (Vector3.up * bonusHeight) * Mathf.Sin(cycle);

        if (target) basePosition = Vector3.MoveTowards(basePosition, target.position, Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wallEnd") || collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
