using UnityEngine;
using System.Collections;


// Attached to SquareTrigger (for testing where to load the wall
public class VoidArea : MonoBehaviour {
    ulong updateCount = 0;
    public double coveredArea = 0.0;
    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;
    public bool isBottom = false;
    public GameObject parent;

    public Material blackMaterial;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Once updateCount == 5 changes the material --> Delayed Start
        if(updateCount == 5) 
        {
            gameObject.GetComponent<Renderer>().material = blackMaterial;

            if (isBottom)
            {
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.BottomBar = parent.FindChild("Wallyer");
            }
            else if (isTop)
            {
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.TopBar = parent.FindChild("Wallyer");
            }
            else if (isLeft)
            {
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.LeftBar = parent.FindChild("Wallyer");
            }
            else if (isRight)
            {
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.RightBar = parent.FindChild("Wallyer");
            }
        }
        updateCount += 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
