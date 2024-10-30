using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int value;
    public string description;
    List<BuffDebuff> listOfEffects = new List<BuffDebuff>();
}
