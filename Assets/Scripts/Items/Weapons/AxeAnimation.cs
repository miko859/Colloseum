using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeAnimation : Weapon
{

    public Collider Blade;

    private bool isBlocking = false;
    public GameObject myPrefab;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void LightAttack()
    {
        if (!isBlocking)
        {
            StartCoroutine(AttackAnim());
        }
    }

    public override void Block()
    {
        isBlocking = !isBlocking;
        animator.SetBool("AxeBlock", isBlocking);
    }

    public override void HandleInput(PlayerInputActions playerInputActions)
    {
        if (playerInputActions.Player.Attack.triggered)
        {
            LightAttack();
        }
        if (playerInputActions.Player.Block.triggered)
        {
            Block();
        }
    }

    public override void Equip()
    {
        gameObject.SetActive(true);
    }

    public override void Unequip()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator AttackAnim()
    {
        Blade.isTrigger = true;
        animator.SetBool("AxeAnim", true);
        yield return new WaitForSeconds(0.46f);
        Blade.isTrigger = false;
        animator.SetBool("AxeAnim", false);
        
    }

    



    /*
    private Animator animator;

    private bool inAnimation = false;

    bool holdBlock = false;
    bool bash = false;
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
            
            animator.SetBool("AxeBlock", true);
            inAnimation = true;

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Mouse0");
                StartCoroutine(bashAnimation());
            }
            

        
        }
        
        if (Input.GetMouseButtonUp(1))
        {
            animator.SetBool("AxeBlock", false);
            inAnimation = false;
            
        }
    }

    IEnumerator attackAnim()
    {
        Blade.isTrigger = true;
        inAnimation = true;
        animator.SetBool("AxeAnim",true);
        yield return new WaitForSeconds(0.46f);
        Blade.isTrigger = false;
        animator.SetBool("AxeAnim", false);
        yield return new WaitForSeconds(0.40f);
        inAnimation = false;
    }

    IEnumerator bashAnimation() {
        animator.SetBool("Bash", true);
        yield return new WaitForSeconds(0.25f);
        animator.SetBool("Bash", false);
    }
    */
}
