using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

public static class SaveGame 
{
    public static void save(Managment mangment)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string path = Application.persistentDataPath + "/explosive.rungame";
        using (FileStream stream = new FileStream(path, FileMode.Create))
        {

            GameData gamedata = new GameData(mangment);

            bf.Serialize(stream, gamedata);
            stream.Close();
        }
    }

    public static GameData loadGame()
    {
        string path = Application.persistentDataPath + "/explosive.rungame";
        if(File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {

                GameData gamedata = bf.Deserialize(stream) as GameData;
                stream.Close();

                return gamedata;
            }
        }
        else
        {
            Debug.LogError("File Not Found" + path);
            return null;
        }
    }
}
