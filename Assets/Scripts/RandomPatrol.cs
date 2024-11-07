using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomPatrol : MonoBehaviour
{
    // Define position values for the edge of the screen (based on circle size).
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    Vector2 targetPosition; // Stores target (x,y) coordinates.

    // Define movement speed range limits.
    public float minSpeed; 
    public float maxSpeed;
    private float speed;

    public GameObject restartPanel; // Stores Game Over UI

    public float secondsToMaxDifficulty; // Define total time elapsed in seconds until max game difficulty is reached.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards target position if not there already.
        if ((Vector2)transform.position != targetPosition)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercent()); // Calculate current movement speed based on current difficulty fraction.
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
        // Generate new random target position if previous target has already been reached.
        else
        {
            targetPosition = GetRandomPosition();
        }
    }

    // Generate random position coordinates within screen boundaries.
    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }

    // Detect if two asteroids collide with eachother.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroids")
        {
            restartPanel.SetActive(true); // Reveal Game Over Screen.
        }
    }

    // Calculate current game difficulty level as a fraction of the maximum difficulty.
    float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
