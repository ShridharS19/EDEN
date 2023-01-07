using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string path = Application.persistentDataPath + "/SaveData.txt";

    public static void Save(SaveState data) {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Create);

      formatter.Serialize(stream, data);
      stream.Close();

      //Debug.Log("Saved");
    }

    public static SaveState Load() {
      if(File.Exists(path)) {

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

        SaveState data = formatter.Deserialize(stream) as SaveState;

        stream.Close();

        return(data);

      } else {
        Debug.LogError("Save file not found!!");
        return(null);
      }
    }
}
