using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public float fireballSpeed = 20f;
    public Transform castPoint;
    public ManaSystem manaSystem;
    public Transform cameraView;

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
        // mana chekc
        if (manaSystem.SpendMana(manaSystem.fireballManaCost))
        {
            Debug.Log("Cast Point Forward Direction: " + castPoint.forward);

            GameObject fireball = Instantiate(fireballPrefab, castPoint.position, cameraView.rotation);
            fireball.transform.rotation = cameraView.rotation;
            Debug.Log("Fireball Rotation After Instantiation: " + fireball.transform.rotation.eulerAngles);

            fireball.GetComponent<Rigidbody>().velocity = cameraView.forward * fireballSpeed;
            //fireball.transform.rotation = castPoint.rotation;
        }
        else
        {
            Debug.Log("Not enough mana to cast fireball");
        }
    }
}