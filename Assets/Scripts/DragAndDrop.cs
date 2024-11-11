using UnityEngine;

// DragAndDrop.cs allows the player's 1st touch to drag GameObjects around the screen and shows the Game Over UI on asteroid GameObject collision triggers.
// Author: Reuel Terezakis
public class DragAndDrop : MonoBehaviour
{

    bool moveAllowed; // Represents whether parent GameObject is currently being moved by the player.
    
    // Particle effects.
    public GameObject selectionEffect; // Stores particle effect for when player selects this object.
    public GameObject deathEffect;  // Stores particle effect for when two asteroids collide.

    // Component variables.
    private Collider2D col;
    private GameMaster gm;
    private SpriteRenderer sprite;

    // Audio source and clips.
    private AudioSource audioSource;
    public AudioClip grab;
    public AudioClip crash;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        // Get component references.
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is touching the screen.
        if (Input.touchCount > 0 && !GameMaster.isGamePaused)
        {
            Touch touch = Input.GetTouch(0); // Store 1st touch in new variable.
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position); // Get 1st touch coordinates in worldspace (converted from pixelspace).

            // Check if player has just touched the screen for the first time.
            if (touch.phase == TouchPhase.Began)
            {
                Collider2D touchedCollider = Physics2D.OverlapPoint(touchPosition); // Create collider at finger touch coordinates.
                // Check if finger collider is touching the parent GameObject.
                if (col == touchedCollider)
                {
                    Instantiate(selectionEffect, transform.position, Quaternion.identity); // Spawn particle effect without rotation.
                    audioSource.PlayOneShot(grab);
                    moveAllowed = true; // Allow parent GameObject to be moved.
                }
            }

            // Check if player has moved their 1st touch without leaving the screen.
            if (touch.phase == TouchPhase.Moved)
            {
                // Check if object is being moved by the player.
                if (moveAllowed)
                {
                    transform.position = new Vector2(touchPosition.x, touchPosition.y); // Move parent GameObject to the current position of the player's 1st touch.
                }
            }

            // Check if player has removed their finger from the screen.
            if (touch.phase == TouchPhase.Ended)
            {
                moveAllowed = false; // Stop moving GameObject to finger position 
            }
        }
    }

    // Detect if two GameObjects with the "Asteroids" tag collide with eachother.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Asteroids")
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity); // Create crash particle effect.
            audioSource.PlayOneShot(crash); // Play crash audio clip.
            gm.GameOver(); // Call GameOver() function from the GameMaster script.
            sprite.enabled = false; // Hide sprite image while audio is playing.
            col.enabled = false; // Disable collider to prevent multiple collisions.
            Destroy(gameObject, crash.length); // Destroy GameObject after crash audio has finished playing.
        }
    }
}
