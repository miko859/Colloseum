
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    public static void SavePlayer(Transform playerTransform, HealthBar healthBar)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.pepe";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(playerTransform, healthBar);

        Debug.Log($"Saving Player Data: Position = ({data.position[0]}, {data.position[1]}, {data.position[2]}), Health = {data.healthData}");

        formatter.Serialize(stream, data);
        stream.Close();
    }
    

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.pepe";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;//need to cast it to player data beacuse cannot convert object
            stream.Close(); //close the connection 
            return data;

        } else
        {
            Debug.Log("Save file not found in " + path);
            return null;
            
        }
    }

}
