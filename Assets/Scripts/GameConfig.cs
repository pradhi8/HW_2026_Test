using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float speed;
}

[System.Serializable]
public class PulpitData
{
    public float min_pulpit_destroy_time;
    public float max_pulpit_destroy_time;
    public float pulpit_spawn_time;
}

[System.Serializable]
public class DoofusDiary
{
    public PlayerData player_data;
    public PulpitData pulpit_data;
}

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance { get; private set; }

    public DoofusDiary Data { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadConfiguration();
    }

    private void LoadConfiguration()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("DoofusDiary");

        if (jsonFile == null)
        {
            Debug.LogError("DoofusDiary.json could not be found.");
            return;
        }

        Data = JsonUtility.FromJson<DoofusDiary>(jsonFile.text);

        if (Data == null || Data.player_data == null || Data.pulpit_data == null)
        {
            Debug.LogError("DoofusDiary.json contains invalid or incomplete data.");
            return;
        }

        Debug.Log("Doofus Diary loaded successfully.");
        Debug.Log("Player Speed: " + Data.player_data.speed);
        Debug.Log("Pulpit Lifetime: " +
                  Data.pulpit_data.min_pulpit_destroy_time +
                  " - " +
                  Data.pulpit_data.max_pulpit_destroy_time);

        Debug.Log("Pulpit Spawn Time: " +
                  Data.pulpit_data.pulpit_spawn_time);
    }
}