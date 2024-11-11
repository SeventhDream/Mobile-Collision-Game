using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneTimer : MonoBehaviour
{
    // Component variables
    public TMP_Text timerDisplay; // Stores score counter UI text element.
    private GameMaster gm;

    private float lvlTimer = 10;

    

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    private void Update()
    {
        // Update level timer if player hasn't lost the game.
        timerDisplay.text = lvlTimer.ToString("F0"); // convert time since scene start to 0dp into a string. 
    }

    private void FixedUpdate()
    {
        // Decrement timer each second until it reaches 0.
        if (lvlTimer <= 0)
        {
            gm.ShowVictoryScreen();
        }
        else if (gm.hasLost == false)
        {
            lvlTimer -= Time.fixedDeltaTime;
        }
    }

}
