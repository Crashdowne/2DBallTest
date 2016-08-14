﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// Attached to Scene
public class GameState : MonoBehaviour {

    public int StartingLives = 3;
    public Text numLivesText;
    private int currentLives;
    public bool noLives = false;
    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject GameWonMenu;


    // Use this for initialization
    void Start ()
    {
        PauseMenu.gameObject.SetActive(false);
        GameOverMenu.gameObject.SetActive(false);
        GameWonMenu.gameObject.SetActive(false);
        currentLives = StartingLives;
        numLivesText.text = "Lives: " + currentLives.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentLives == 0)
        {
            numLivesText.text = "Lives: " + currentLives.ToString();
            noLives = true;
            Time.timeScale = 0;
            GameOverMenu.gameObject.SetActive(true);
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
}
