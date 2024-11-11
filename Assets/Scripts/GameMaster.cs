using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// GameMaster.cs handles loading and restarting Unity scenes.
// Author: Reuel Terezakis
public class GameMaster : MonoBehaviour
{  
    public static bool isGamePaused = false;
    public GameObject restartPanel; // Stores Game Over UI panel.
    public GameObject victoryPanel; // Stores Victory UI panel.
    public bool hasLost;

    // Shows Game Over UI after a short delay.
    public void GameOver()
    {
        hasLost = true;
        Invoke("ShowGameOverScreen", 1.5f);
    }

    // Enables Game Over UI panel.
    public void ShowGameOverScreen()
    {
        restartPanel.SetActive(true);
        PauseGame();
    }

    // Enables Victory UI panel.
    public void ShowVictoryScreen()
    {
        victoryPanel.SetActive(true);
        PauseGame();
    }

    // Loads the main game scene.
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameEnv");
    }

    // Restarts the current scene.
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame();
    }

    // Returns to main menu scene.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        ResumeGame();
    }
     
    // Pause game.
    public void PauseGame()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        AudioListener.pause = true;
    }

    // Unpause game.
    public void ResumeGame()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        AudioListener.pause = false;
    }
}