using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public static class SaveSystem
{
    // Save the player, including weapons
    public static void SavePlayer(Transform playerTransform, HealthBar healthBar, EquipedWeaponManager equipedWeaponManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.pepe";
        FileStream stream = null;

        try
        {
            stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            PlayerData data = new PlayerData(playerTransform, healthBar, equipedWeaponManager);

            // Debug log to display all weapon names being saved
            Debug.Log($"Saving Player Data: Position = ({data.position[0]}, {data.position[1]}, {data.position[2]}), Health = {data.healthData}, Weapons = {string.Join(", ", data.equippedWeapons)}");

            formatter.Serialize(stream, data);
        }
        catch (IOException ex)
        {
            Debug.LogError("Failed to save player data: " + ex.Message);
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
            }
        }
    }

    // Load the player data, including weapons
    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.pepe";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None);
                if (stream.Length > 0) // Check if the stream is not empty
                {
                    PlayerData data = formatter.Deserialize(stream) as PlayerData; // need to cast it to player data because cannot convert object

                    // Debug log to display all weapon names
                    Debug.Log($"Loading Player Data: Position = ({data.position[0]}, {data.position[1]}, {data.position[2]}), Health = {data.healthData}, Weapons = {string.Join(", ", data.equippedWeapons)}");

                    return data;
                }
                else
                {
                    Debug.LogError("Save file is empty.");
                    return null;
                }
            }
            catch (IOException ex)
            {
                Debug.LogError("Failed to load player data: " + ex.Message);
                return null;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
        }
        else
        {
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }
}
