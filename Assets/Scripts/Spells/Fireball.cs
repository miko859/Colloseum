using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 20f;
    public Transform castPoint;


    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            Debug.Log("pressed q casting fireball");
            CastFireball();
        }
    }

    void CastFireball()
    {
        Vector3 offsetPosition = castPoint.position + castPoint.forward * 25.5f;
        GameObject fireball = Instantiate(fireballPrefab, castPoint.position, castPoint.rotation);
        fireball.GetComponent<Rigidbody>().velocity = castPoint.forward * fireballSpeed;
    }
}
