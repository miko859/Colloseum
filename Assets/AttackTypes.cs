using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTypes : MonoBehaviour
{
    public float heavyAttack = 0.2f; // how many seconds to considere the attack as heavy attack
    private bool isHolding = false;
    private float holdTime = 0f;

    private void Update()
    {
        //if the left mouse button is pressed
        if (Input.GetMouseButton(0))
        {
            // sets the holdTime
            if (!isHolding)
            {
                isHolding = true;
                holdTime = Time.time;
            }
        }
        else
        {

            if (isHolding)
            {   
                if (Time.time - holdTime >= heavyAttack)
                {
                    // Change tag to HeavyBlade
                    gameObject.tag = "HeavyBlade";
                }
                else
                {
                    
                    gameObject.tag = "Blade";
                }

                
                isHolding = false;
            }
        }
    }

    
}
