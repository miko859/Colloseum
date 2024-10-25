using System.Collections.Generic;
using UnityEngine;

public class EffectStatus : MonoBehaviour
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
            for (int index = listOfBuffDebuffs.Count - 1; index >= 0; index--)
            {
                BuffDebuff effect = listOfBuffDebuffs[index];
                Debug.Log($"Effect status: našlo effect {effect.name}");

                if (effect.GetData() == null)
                {
                    effect.CreateObject(entity);
                    Debug.Log("Vytvorilo efekt");
                }


                if (effect.IsEffectEnded())
                {
                    Debug.Log(effect.IsEffectEnded());
                    listOfBuffDebuffs.RemoveAt(index);
                    Debug.Log("Vymaže efekt");
                    continue;
                }

                
                effect.TimerEffect();
            }
        }
    }
}