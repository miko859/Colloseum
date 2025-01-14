using System.Collections;
using UnityEngine;

public class Flamethrower : Element
{
    public void Start()
    {
        particleSystem.Stop();
    }
    public override void Initialize()
    {
        manaCost = 5;
        damagePerSecond = 1;      
    }   
}
