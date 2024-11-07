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

    public float speed; // Defines how fast the object moves to target position.

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload Current Scene.
        }
    }
}
