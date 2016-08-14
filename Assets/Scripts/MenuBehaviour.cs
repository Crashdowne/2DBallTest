using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;


// Attached to buttons (when it works...)
public class MenuBehaviour : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

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

    public void MainMenu()
    {
        // New way of loading scenes, Application.Load... is depreciated
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene.name);

        // !! Working around a unity bug where they don't respect the new time scale
        SceneManager.sceneLoaded += OnSceneLoadedEventHandler;
        WallSpawner.GetCurrentWallSpawnerState().Pause = false;

    }

    private void OnSceneLoadedEventHandler(Scene scene, LoadSceneMode mode)
    {
        // Make sure the engine isn't paused on a scene change!
        // dERP.
        Time.timeScale = 1.0f;
    }

    public void Resume()
    {
        WallSpawner.GetCurrentWallSpawnerState().Pause = false;
        PauseMenu.gameObject.SetActive(false);
        GameOverMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void Pause()
    {
        WallSpawner.GetCurrentWallSpawnerState().Pause = true;
        PauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(scene);
        SceneManager.LoadScene(scene += 1);
    }
}
