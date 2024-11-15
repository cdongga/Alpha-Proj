using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // The coordinates where the bird should respawn
    public Vector3 spawnPosition = new Vector3(-162.8f, 64.9f, -11.2f);

    // Reference to the PlayerControllerX script to control the bird's reset behavior
    public PlayerControllerX playerController;

    // Method to reset the bird's position and stop its movement
    public void ResetBird()
    {
        // Reset bird position to the spawn coordinates
        playerController.transform.position = spawnPosition;

        // Stop the bird's movement
        playerController.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Optionally, reset any other game parameters here (score, etc.)

        // Restart the game or trigger other reset actions if necessary
        playerController.gameOver = false;
    }
}
