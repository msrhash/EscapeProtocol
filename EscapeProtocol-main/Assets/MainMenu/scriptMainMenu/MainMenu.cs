using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial"); // Make sure this matches the gameplay scene name
    }

    
public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting..."); // This will log in the console when running in the editor
}
}

