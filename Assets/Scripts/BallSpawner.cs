using UnityEngine;
using System.Collections;

// Had to add the namespace for the UI stuffs
using UnityEngine.UI;

public class BallSpawner : MonoBehaviour {

    // Allows us to manually set the # of balls in the Ball Spawner in the editor
    public int numOfBalls;
    
    // Sets up the variables for displaying the text # of balls
    private int count = 0;
    public Text countText;

    // Creates a new "prefab" object of type GameObjest   
    public GameObject prefab;

    // Use this for initialization
    void Start ()
    {
        // Spawns the # of balls for the # that was entered by the user
        for (int i = 1; i <= numOfBalls; i++)
        {
            // From Unity docs, don't really know what's going on here
            // I guess we define the prefab in the editor?
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
        // Only need to do stuff @ start
	}


}
