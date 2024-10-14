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
        // Mana check
        if (manaSystem.SpendMana(manaSystem.fireballManaCost))
        {
            Debug.Log("Cast Point Forward Direction: " + castPoint.forward);

            GameObject fireball = Instantiate(fireballPrefab, castPoint.position, Quaternion.identity);

            fireball.transform.LookAt(fireball.transform.position + castPoint.forward);

            Debug.Log("Fireball Rotation After Instantiation: " + fireball.transform.rotation.eulerAngles);

            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * fireballSpeed;
        }
        else
        {
            Debug.Log("Not enough mana to cast fireball");
        }
    }

}
