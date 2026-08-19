using UnityEngine;

public class Pulpit : MonoBehaviour
{
    private float destroyTime;
    private float remainingTime;

    public float RemainingTime => remainingTime;

    public void Initialize()
    {
        if (GameConfig.Instance == null || GameConfig.Instance.Data == null)
        {
            Debug.LogError("GameConfig is not loaded.");
            return;
        }

        float minTime = GameConfig.Instance.Data.pulpit_data.min_pulpit_destroy_time;
        float maxTime = GameConfig.Instance.Data.pulpit_data.max_pulpit_destroy_time;

        destroyTime = Random.Range(minTime, maxTime);
        remainingTime = destroyTime;
    }

    private void Update()
    {
        if (GameManagerIsPlaying())
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                Destroy(gameObject);
            }
        }
    }

    private bool GameManagerIsPlaying()
    {
        return true;
    }
}