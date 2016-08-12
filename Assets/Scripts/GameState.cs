using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    public int StartingLives = 3;
    public Text numLivesText;

    private int currentLives;

    public static bool noLives = false;

	// Use this for initialization
	void Start ()
    {
        currentLives = StartingLives;
        numLivesText.text = "Lives: " + currentLives.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (currentLives == 0)
        {
            noLives = true;
        }
	
	}

    public void RemoveLife()
    {
        currentLives -= 1;
        numLivesText.text = "Lives: " + currentLives.ToString();
    }
}
