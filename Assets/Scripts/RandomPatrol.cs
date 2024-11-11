using UnityEngine;
using UnityEngine.SceneManagement;

// RandomPatrol.cs defines transform rules for moving to random coordinates in a straight line with a speed increasing proportionally with time to an upper limit.
// Author: Reuel Terezakis
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

    

    public float secondsToMaxDifficulty; // Define total time elapsed in seconds until max game difficulty is reached.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = GetRandomPosition();
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

    // Calculate current game difficulty level as a fraction of the maximum difficulty.
    float GetDifficultyPercent()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxDifficulty);
    }
}
