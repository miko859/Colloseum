using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public List<Spell> ballSpells = new List<Spell>();
    public List<Spell> elementSpells = new List<Spell>();
    public List<Spell> utilitySpells = new List<Spell>();

    private int ballIdx = 0;
    private int elementIdx = 0;
    private int utilityIdx = 0;

    private void Start()
    {
        ballSpells.Add(new Fireball());
        elementSpells.Add(new Flamethrower());
        utilitySpells.Add(new HasteSpell());
    }

    public void SpellSwitch(string value)
    {
        if (value.Equals("1"))
        {
            if (ballSpells.Count - 1 > ballIdx)
            {
                ballIdx++;
                Debug.Log("Switching ball spell");
            }
            else
            {
                ballIdx = 0;
            }
            ballSpells[ballIdx].Initialize();
        }
        else if (value.Equals("2"))
        {
            if (elementSpells.Count - 1 > elementIdx)
            {
                elementIdx++;
                Debug.Log("Switching element spell");
            }
            else
            {
                elementIdx = 0;
            }
            elementSpells[elementIdx].Initialize();
        }
        else if (value.Equals("3"))
        {
            if (utilitySpells.Count - 1 > utilityIdx)
            {
                utilityIdx++;
                Debug.Log("Switching util spell");
            }
            else
            {
                utilityIdx = 0;
            }
            utilitySpells[utilityIdx].Initialize();
        }
        else
        {
            Debug.Log("YOU SEE DONT WORRY ROBO, I GOT IT COVERED");
        }
    }

    public void CallActive()
    {
        elementSpells[elementIdx].Activate();
    }

    public void CallActiveBall()
    {
        ballSpells[elementIdx].ActiveBall();
    }

    public void CallActiveUtility()
    {
        utilitySpells[utilityIdx].Activate();
    }

    public void CallDeactive()
    {
        elementSpells[elementIdx].Deactivate();
    }
}
