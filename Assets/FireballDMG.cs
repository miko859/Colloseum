using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDMG : MonoBehaviour
{
    public int damage = 2;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Health>()?.DealDamage(damage);
            
        }
        Destroy(gameObject);
    }
}
