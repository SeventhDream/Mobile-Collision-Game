using UnityEngine;
using UnityEngine.SceneManagement;

// GameMaster.cs handles loading and restarting Unity scenes.
// Author: Reuel Terezakis
public class GameMaster : MonoBehaviour
{

    public GameObject restartPanel;

    public void GameOver()
    {
        restartPanel.SetActive(true);
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
    }

    // Returns to main menu scene.
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}