using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public class SaveLoadSystem : MonoBehaviour
{   
    // Public
    public static SaveLoadSystem Instance;

    // Private fields
    private string _savePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _savePath = Application.persistentDataPath + "/save.dat";
    }

    public void SaveGame(int score)
    {
        FileStream stream = File.Create(_savePath);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, score);
        stream.Close();
    }

    public int LoadGame()
    {
        if (File.Exists(_savePath))
        {
            FileStream stream = File.Open(_savePath, FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            int score = (int)formatter.Deserialize(stream);
            stream.Close();
            return score;
        }
        else
        {
            return 0;
        }
    }
}
