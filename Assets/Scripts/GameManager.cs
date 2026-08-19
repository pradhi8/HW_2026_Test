using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; private set; }

    [SerializeField] private DoofusController doofus;
    [SerializeField] private float fallThreshold = -10f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (IsGameOver)
            return;

        if (doofus == null)
            return;

        if (doofus.transform.position.y <= fallThreshold)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        IsGameOver = true;

        Debug.Log("GAME OVER - Doofus fell.");
    }
}