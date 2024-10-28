using UnityEngine;

/// <summary>
/// Class for fireball spells.
/// </summary>
public class Fireball : Ball
{
    public override void Initialize()
    {
        manaCost = 10;
        speed = 20f;
        impactDamage = 3;
    }
}
