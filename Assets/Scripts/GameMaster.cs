using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMaster : MonoBehaviour
{
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