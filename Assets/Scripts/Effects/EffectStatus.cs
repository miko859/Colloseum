using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EffectStatus
{
    public List<BuffDebuff> listOfBuffDebuffs = new List<BuffDebuff>();
    public GameObject entity;
    public void InsertNewEffect(BuffDebuff effect)
    {
        listOfBuffDebuffs.Add(effect);
    }

    public void DeleteEndedEffect(BuffDebuff effect)
    {
        listOfBuffDebuffs.Remove(effect);
    }


    public void Update()
    {
        if (listOfBuffDebuffs.Count != 0)
        {
            foreach (var effect in listOfBuffDebuffs)
            {
                if (effect.IsEffectEnded())
                {
                    listOfBuffDebuffs.Remove(effect);
                }

                if (effect.GetData() == null)
                {
                    effect.CreateObject(entity);
                }

                effect.TimerEffect();
            }
        }
    }
}