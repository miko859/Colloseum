using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretRoomLogic : MonoBehaviour
{

    [SerializeField] private GemButton[] offObjects;
    [SerializeField] private GemButton[] onObjects;
    public DoorScript doorToUnlock;


    public void OnChanged()
    {
        if (checkOffObjects() && checkOnObjects())
        {
            doorToUnlock.OpenDoor();
        }
    }

    private bool checkOffObjects()
    {
        foreach (GemButton button in offObjects)
        {
            if (button.GetStatus() == true)
            {
                return false;
            }
        }
        return true;
    }

    private bool checkOnObjects()
    {
        foreach (GemButton button in onObjects)
        {
            if (button.GetStatus() == false)
            {
                return false;
            }
        }
        return true;
    }
}
