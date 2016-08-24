using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class MenuBehaviour : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject GameOverMenu;

    // Allows us to load different scenes through the editor
    // Passes in the scene name 
    public void LoadGame(string sceneName)
    {
        // New way of loading scenes, Application.Load... is depreciated
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneName);

        // !! Working around a unity bug where they don't respect the new time scale
        SceneManager.sceneLoaded += OnSceneLoadedEventHandler;
        WallSpawner.GetCurrentWallSpawnerState().Pause = false;
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

        if(WallSpawner.GetCurrentWallSpawnerState().Pause == true)
        {
            PauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }   
    }

    public void NextLevel()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene += 1);
    }

    public void HowTo()
    {
        SceneManager.LoadScene("HowTo");
    }

    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps;
    public Text FPSText;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }
    void OnGUI()
    {
        GUILayout.Label("" + fps.ToString("f2"));
    }
    void Update()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
            FPSText.text = fps.ToString();
        }
    }

}
