using UnityEngine;


public class DragAndDrop : MonoBehaviour
{

    bool moveAllowed; // Represents whether parent GameObject is currently being moved by the player.
    Collider2D col; // Stores parent object collider.

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        col = GetComponent<Collider2D>(); // Assign variable to collider component attached to the parent GameObject of this script.
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
}
