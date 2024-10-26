using System.Collections.Generic;
using UnityEngine;

public class EffectStatus : MonoBehaviour
{
    public List<BuffDebuff> listOfBuffDebuffs = new List<BuffDebuff>();
    public GameObject entity;
    public void InsertEffect(BuffDebuff effect)
    {
        if (HasEntityEffect(effect))
        {
            effect.AddStack();
        }
        else
        {
            listOfBuffDebuffs.Add(effect);
        }
    }

    public void DeleteEndedEffect(BuffDebuff effect)
    {
        listOfBuffDebuffs.Remove(effect);
    }

    private bool HasEntityEffect(BuffDebuff effect)
    {
        return listOfBuffDebuffs.Contains(effect);
    }

    public void Update()
    {
        if (listOfBuffDebuffs.Count != 0)
        {
            for (int index = listOfBuffDebuffs.Count - 1; index >= 0; index--)
            {
                BuffDebuff effect = listOfBuffDebuffs[index];

                if (effect.IsEffectEnded())
                {
                    Debug.Log("DELETE");
                    effect.SetEffectEnded(false);
                    Debug.Log(effect.IsEffectEnded());
                    listOfBuffDebuffs.RemoveAt(index);
                    continue;
                }
                if (effect.GetData() == null)
                {
                    Debug.Log("CREATE");
                    effect.CreateObject(entity);
                }

                effect.TimerEffect();
            }
        }
    }
}