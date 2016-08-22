using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


// Attached to Scene
public class GameState : MonoBehaviour
{

    public int StartingLives = 3;
    public Text numLivesText;
    public Text percentCovered;
    public Text levelNumberText;
    private int currentLives;
    public bool noLives = false;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject GameWonMenu;
    private double percentTotalCovered;

    // Use this for initialization
    void Start()
    {
        PauseMenu.gameObject.SetActive(false);
        GameOverMenu.gameObject.SetActive(false);
        GameWonMenu.gameObject.SetActive(false);
        currentLives = StartingLives;
        numLivesText.text = "Lives: " + currentLives.ToString();
        levelNumberText.text = "Level: " + SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentLives == 0)
        {
            numLivesText.text = "Lives: " + currentLives.ToString();
            noLives = true;
            Time.timeScale = 0;
            GameOverMenu.gameObject.SetActive(true);
            PauseMenu.gameObject.SetActive(false);
            GameWonMenu.gameObject.SetActive(false);
        }

        var wallSpawner = WallSpawner.GetCurrentWallSpawnerState();
        percentTotalCovered = (100 - Math.Ceiling((wallSpawner.GetCurrentPlayArea() / wallSpawner.InitPlayArea.GetArea()) * 100));
        percentCovered.text = "Covered: " + percentTotalCovered.ToString() + "%";

        if (percentTotalCovered >= 75.0)
        {
            OnWin();
        }

    }

    public void RemoveLife()
    {
        currentLives -= 1;
        numLivesText.text = "Lives: " + currentLives.ToString();
    }

    public static GameState GetCurrentGameState()
    {
        return GameObject.Find("GameState").GetComponent<GameState>();
    }

    public void OnWin()
    {
        Time.timeScale = 0;
        WallSpawner.GetCurrentWallSpawnerState().Pause = true;
        GameOverMenu.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(false);
        GameWonMenu.gameObject.SetActive(true);
    }
}
