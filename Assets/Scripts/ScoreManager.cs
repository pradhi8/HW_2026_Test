using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public int Score { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Score = 0;
    }

    public void AddPoint()
    {
        if (GameManager.Instance != null &&
            GameManager.Instance.IsGameOver)
        {
            return;
        }

        Score++;

        Debug.Log("Score: " + Score);
    }

    public void ResetScore()
    {
        Score = 0;
    }
}