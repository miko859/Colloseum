using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{

    public List<Spell> ballSpells = new List<Spell>();
    public List<Spell> elementSpells = new List<Spell>();
    private int ballIdx = 0;
    private int elementIdx = 0;
    // Start is called before the first frame update
    private void Start()
    {
        ballSpells.Add(new Fireball());// This may not work
        elementSpells.Add(new Flamethrower()); //This may not work
    }
    public void SpellSwitch(string value)
    {


        if (value.Equals("1"))
        {
            if (ballSpells.Count - 1 > ballIdx)
            {
                ballIdx++;
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
            }
            else
            {
                elementIdx = 0;
            }
            elementSpells[elementIdx].Initialize();
        }
        else
        {
            Debug.Log("YOU SEE DONT WORRY ROBO, I GOT IT COVERED");
        }
    }

    public void CallActive() {
        elementSpells[elementIdx].Activate();
    
    }
    public void CallActiveBall() {
        ballSpells[elementIdx].ActiveBall();
    
    }
    public void CallDeactive() { 
        elementSpells[elementIdx].Deactivate();

    }
}
