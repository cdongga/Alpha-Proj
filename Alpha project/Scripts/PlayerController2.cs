using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float moveForce = 50f; // Force for up/down arrow keys
    public float forwardSpeed = 10.5f; // Speed to move forward
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;

    private float maxHeight = 100f; // Maximum height bird can reach
    private float minHeight = -5f; // Minimum height bird can go (optional)

    // Store the bird's starting position
    private Vector3 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAudio = GetComponent<AudioSource>();

        // Set the starting point to the bird's initial position
        startPoint = transform.position;

        // Initial upward force to get off the ground
        playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            // Constant forward motion
            playerRb.velocity = new Vector3(forwardSpeed, playerRb.velocity.y, playerRb.velocity.z);
        }

        // Prevent bird from going too high
        if (transform.position.y > maxHeight)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
        }

        // Prevent bird from going too low (optional, can adjust or remove)
        if (transform.position.y < minHeight)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z);
        }

        // Move up with Up Arrow
        if (Input.GetKey(KeyCode.UpArrow) && transform.position.y < maxHeight && !gameOver)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, moveForce, playerRb.velocity.z);  // Direct vertical movement
        }

        // Move down with Down Arrow
        if (Input.GetKey(KeyCode.DownArrow) && transform.position.y > minHeight && !gameOver)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, -moveForce, playerRb.velocity.z);  // Direct vertical movement
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game Over!");

            // Play explosion effects
            if (explosionParticle != null)
            {
                explosionParticle.transform.position = transform.position;
                explosionParticle.Play();
            }

            if (playerAudio != null && explodeSound != null)
            {
                playerAudio.PlayOneShot(explodeSound);
            }

            // Reset the bird to the start position and stop its movement
            transform.position = startPoint; // Reset to the start position
            playerRb.velocity = Vector3.zero; // Stop movement after collision

            // Reset bird rotation to prevent flipping
            transform.rotation = Quaternion.identity;

            // Optionally, you can set gameOver to false to resume the game
            gameOver = false;
        }
        else if (collision.gameObject.CompareTag("Ground"))
        {
            // Play bounce sound on ground collision
            if (playerAudio != null && bounceSound != null)
            {
                playerAudio.PlayOneShot(bounceSound);
            }
        }
    }
}
