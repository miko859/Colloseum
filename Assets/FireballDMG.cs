using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDMG : MonoBehaviour
{
    public float damage = 2;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("BALL HAS TRIGGERED WITH: " + other.gameObject.name);

        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("TRIGGERED WITH AN ENEMY - DEALING DAMAGE");
            other.gameObject.GetComponent<Health>()?.DealDamage(damage);
        }

        Destroy(gameObject);
    }

}
