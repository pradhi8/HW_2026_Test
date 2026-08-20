using UnityEngine;
using UnityEngine.InputSystem;

public class DoofusController : MonoBehaviour
{
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = GameConfig.Instance.Data.player_data.speed;
    }

    private void Update()
    {
        if (GameManager.Instance == null)
            return;

        if (!GameManager.Instance.IsGameStarted)
            return;

        if (GameManager.Instance.IsGameOver)
            return;

        if (Keyboard.current == null)
            return;

        float horizontal = 0f;
        float vertical = 0f;

        // A / Left Arrow
        if (Keyboard.current.aKey.isPressed ||
            Keyboard.current.leftArrowKey.isPressed)
        {
            horizontal = -1f;
        }

        // D / Right Arrow
        if (Keyboard.current.dKey.isPressed ||
            Keyboard.current.rightArrowKey.isPressed)
        {
            horizontal = 1f;
        }

        // W / Up Arrow
        if (Keyboard.current.wKey.isPressed ||
            Keyboard.current.upArrowKey.isPressed)
        {
            vertical = 1f;
        }

        // S / Down Arrow
        if (Keyboard.current.sKey.isPressed ||
            Keyboard.current.downArrowKey.isPressed)
        {
            vertical = -1f;
        }

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;

        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}