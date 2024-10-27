using System.Collections;
using UnityEngine;

public class Flamethrower : Element
{
    private Coroutine manaSpendingCoroutine;
    private bool isFlameActive = false;
    public ParticleSystem flameEffect;

    private void Start()
    {
        manaCost = 5;
        damagePerSecond = 1; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Activate();
        }

        if (Input.GetKeyUp(KeyCode.F))
        {
            Deactivate();
        }
    }

    public override void Activate()
    {
        if (!isFlameActive)
        {
            isFlameActive = true;
            flameEffect.Play();
            StartManaSpending();
            base.Activate();  
        }
    }

    public override void Deactivate()
    {
        if (isFlameActive)
        {
            isFlameActive = false;
            flameEffect.Stop();
            StopManaSpending();
            base.Deactivate();  
        }
    }

    protected override void StartManaSpending()
    {
        if (manaSpendingCoroutine == null)
        {
            manaSpendingCoroutine = StartCoroutine(SpendManaContinuously());
        }
    }

    protected override void StopManaSpending()
    {
        if (manaSpendingCoroutine != null)
        {
            StopCoroutine(manaSpendingCoroutine);
            manaSpendingCoroutine = null;
        }
    }

    private IEnumerator SpendManaContinuously()
    {
        while (isFlameActive) 
        {
            if (!manaSystem.TrySpendMana(manaCost))
            {
                Debug.Log("Not enough mana!");
                Deactivate();
                yield break; 
            }

            yield return new WaitForSeconds(0.5f); 
        }
    }
}
