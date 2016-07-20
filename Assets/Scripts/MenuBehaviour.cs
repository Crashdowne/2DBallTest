using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour {


    // Allows us to load different scenes through the editor
    // Passes in the scene name
    public void LoadGame(string sceneName)
    {
        // New way of loading scenes, Application.Load... is depreciated
        SceneManager.LoadScene(sceneName);
    }

    // Function to quit the game (You can't quit the game in the editor, must be built and run
    public void QuitGame()
    {
        Application.Quit();
    }
}
