using UnityEngine;

public class PulpitManager : MonoBehaviour
{
    [SerializeField] private Pulpit startingPulpit;
    [SerializeField] private Pulpit pulpitPrefab;

    private Pulpit currentPulpit;
    private Pulpit nextPulpit;

    private void Start()
    {
        if (startingPulpit == null)
        {
            Debug.LogError("Starting Pulpit has not been assigned.");
            return;
        }

        currentPulpit = startingPulpit;
        currentPulpit.Initialize();
    }

    private void Update()
    {
        if (currentPulpit == null)
        {
            PromoteNextPulpit();
            return;
        }

        if (nextPulpit == null && ShouldSpawnNextPulpit())
        {
            SpawnNextPulpit();
        }
    }

    private bool ShouldSpawnNextPulpit()
    {
        if (GameConfig.Instance == null ||
            GameConfig.Instance.Data == null)
        {
            Debug.LogError("GameConfig is not available.");
            return false;
        }

        float spawnTime =
            GameConfig.Instance.Data.pulpit_data.pulpit_spawn_time;

        return currentPulpit.RemainingTime <= spawnTime;
    }

    private void SpawnNextPulpit()
    {
        Vector3 spawnPosition =
            GetAdjacentPosition(currentPulpit.transform.position);

        nextPulpit = Instantiate(
            pulpitPrefab,
            spawnPosition,
            Quaternion.identity
        );

        nextPulpit.Initialize();
    }

    private void PromoteNextPulpit()
    {
        if (nextPulpit == null)
        {
            Debug.LogError(
                "Current Pulpit was destroyed before a next Pulpit was available."
            );

            return;
        }

        currentPulpit = nextPulpit;
        nextPulpit = null;
    }

    private Vector3 GetAdjacentPosition(Vector3 currentPosition)
    {
        Vector2Int[] directions =
        {
            new Vector2Int(1, 0),
            new Vector2Int(-1, 0),
            new Vector2Int(0, 1),
            new Vector2Int(0, -1)
        };

        Vector2Int direction =
            directions[Random.Range(0, directions.Length)];

        const float pulpitSize = 9f;

        return currentPosition +
               new Vector3(
                   direction.x * pulpitSize,
                   0f,
                   direction.y * pulpitSize
               );
    }
}