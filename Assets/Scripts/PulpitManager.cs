using UnityEngine;

public class PulpitManager : MonoBehaviour
{
    [SerializeField] private Pulpit pulpitPrefab;

    private Pulpit currentPulpit;
    private Pulpit nextPulpit;

    private void Start()
    {
        currentPulpit = FindAnyObjectByType<Pulpit>();

        if (currentPulpit == null)
        {
            Debug.LogError("No starting Pulpit found in the scene.");
            return;
        }

        currentPulpit.Initialize();
    }

    private void Update()
    {
        if (currentPulpit == null)
        {
            return;
        }

        if (nextPulpit == null && ShouldSpawnNextPulpit())
        {
            SpawnNextPulpit();
        }

        if (currentPulpit == null)
        {
            currentPulpit = nextPulpit;
            nextPulpit = null;
        }
    }

    private bool ShouldSpawnNextPulpit()
    {
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