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
            GameObject fireball = Instantiate(fireballPrefab, castPoint.position, castPoint.rotation);
            fireball.GetComponent<Rigidbody>().velocity = castPoint.forward * fireballSpeed;
        }
        else
        {
            Debug.Log("Not enough mana to cast fireball");
        }
    }
}