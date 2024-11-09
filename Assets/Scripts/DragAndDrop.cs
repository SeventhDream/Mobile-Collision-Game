using UnityEngine;

// DragAndDrop.cs allows the player's 1st touch to drag GameObjects around the screen and shows the Game Over UI on asteroid GameObject collision triggers.
// Author: Reuel Terezakis
public class DragAndDrop : MonoBehaviour
{

    bool moveAllowed; // Represents whether parent GameObject is currently being moved by the player.
    

    public GameObject selectionEffect; // Stores particle effect for when player selects this object.
    public GameObject restartPanel; // Stores Game Over UI panel.

    private Collider2D col; // Stores parent GameObject's collider component.
    private GameMaster gm; // Stores the GameMaster script component.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>(); // Find and store the GameMaster script component attached to the GameObject with the "GM" tag.
        col = GetComponent<Collider2D>(); // Assign variable to collider component attached to the this GameObject.
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is touching the screen.
        if (Input.touchCount > 0)
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
            gm.GameOver(); // Call GameOver() function from the GameMaster variable.
            Destroy(gameObject); // Destroy GameObject.
        }
    }
}
