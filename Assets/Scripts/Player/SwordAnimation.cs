using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwordAnimation : MonoBehaviour
{
    Animator animator;
    bool inAnimation = false;
    bool holdBlock = false;
    public Collider Blade;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (!inAnimation && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(attackAnim());
        }

        if (!inAnimation && Input.GetMouseButtonDown(1)){
            
            animator.SetBool("SwordBlock", true);
            inAnimation = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("SwordBlock", false);
            inAnimation = false;
        }
    }
    IEnumerator attackAnim()
    {
        Blade.isTrigger = true;
        inAnimation = true;
        animator.SetBool("SwordAnim",true);
        yield return new WaitForSeconds(0.46f);
        Blade.isTrigger = false;
        animator.SetBool("SwordAnim", false);
        yield return new WaitForSeconds(0.40f);
        inAnimation = false;
    }

}
