﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BallSpawner : MonoBehaviour {

    public int numOfBalls;
    private int count = 0;
    public Text countText; 
    public GameObject prefab;
    private int numOfBallsGenerated;
    private int randomQuadrent;

    // Use this for initialization
    void Start ()
    {
        numOfBallsGenerated = SceneManager.GetActiveScene().buildIndex;
        // Spawns the # of balls for the # that was entered by the user
        for (int i = 1; i <= numOfBallsGenerated; i++)
        {
            // Use different quadrents to stop balls from spawning on top of each other, or at least make it more difficult
            randomQuadrent = Random.Range(0, 2);
            if (randomQuadrent == 0)
            {
                Instantiate(prefab, new Vector3(Random.Range(0, 3), Random.Range(0, 3), 0), Quaternion.identity);
            }

            else if (randomQuadrent == 1)
            {
                Instantiate(prefab, new Vector3(Random.Range(-1, -4), Random.Range(-1, -4), 0), Quaternion.identity);
            }
            
        }

        // Sets our count value to the # set by user
        count = numOfBalls;

        // Sets the text to be displayed
         countText.text = "Balls: " + count.ToString();
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
