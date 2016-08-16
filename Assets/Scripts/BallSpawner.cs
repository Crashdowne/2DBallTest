using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BallSpawner : MonoBehaviour {

    public int numOfBalls;
    private int count = 0;
    public Text countText; 
    public GameObject prefab;

    // Use this for initialization
    void Start ()
    {
        // Spawns the # of balls for the # that was entered by the user
        for (int i = 1; i <= numOfBalls; i++)
        {
            Instantiate(prefab, new Vector3(Random.Range(-2,2), Random.Range(-2, 2), 0), Quaternion.identity);
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
