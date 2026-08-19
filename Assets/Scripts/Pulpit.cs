using UnityEngine;

public class Pulpit : MonoBehaviour
{
    private float destroyTime;
    private float remainingTime;

    private bool playerHasLanded = false;

    public float RemainingTime => remainingTime;

    public void Initialize()
    {
        if (GameConfig.Instance == null ||
            GameConfig.Instance.Data == null)
        {
            Debug.LogError("GameConfig is not loaded.");
            return;
        }

        float minTime =
            GameConfig.Instance.Data.pulpit_data.min_pulpit_destroy_time;

        float maxTime =
            GameConfig.Instance.Data.pulpit_data.max_pulpit_destroy_time;

        destroyTime = Random.Range(minTime, maxTime);
        remainingTime = destroyTime;

        playerHasLanded = false;
    }

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (GameManager.Instance.IsGameOver)
            return;

        remainingTime -= Time.deltaTime;

        if (remainingTime <= 0f)
        {
            remainingTime = 0f;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (playerHasLanded)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        Collider playerCollider = collision.gameObject.GetComponent<Collider>();
        Collider pulpitCollider = GetComponent<Collider>();

        if (playerCollider == null || pulpitCollider == null)
            return;

        float playerBottom = playerCollider.bounds.min.y;
        float pulpitTop = pulpitCollider.bounds.max.y;

        bool landedOnTop =
            playerBottom >= pulpitTop - 0.15f &&
            collision.transform.position.y > transform.position.y;

        if (!landedOnTop)
            return;

        playerHasLanded = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoint();
        }
    }
}