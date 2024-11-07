using UnityEngine;
using UnityEngine.SceneManagement;
public class GameMaster : MonoBehaviour
{
    public void GoToGameScene()
    {
        SceneManager.LoadScene("GameEnv");
    }
}