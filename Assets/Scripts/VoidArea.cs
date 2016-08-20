using UnityEngine;
using System.Collections;


// Attached to SquareTrigger (for testing where to load the wall
public class VoidArea : MonoBehaviour {
    ulong updateCount = 0;
    public bool isLeft = false;
    public bool isRight = false;
    public bool isTop = false;
    public bool isBottom = false;
    public GameObject parent;

    public Material blackMaterial;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        // Once updateCount == 5 changes the material --> Delayed Start
        if(updateCount == 5) 
        {
            gameObject.GetComponent<Renderer>().sharedMaterial = blackMaterial;

            if (isBottom)
            {
                var wall = parent.FindChild("Wallyer");
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.BottomBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.min.y);
                //Debug.Log("New Bottom: " + WallSpawner.GetCurrentWallSpawnerState().PlayArea.BottomBar.ToString());
            }
            else if (isTop)
            {
                var wall = parent.FindChild("Wallyer");
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.TopBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.min.y);
                //Debug.Log("New Top: " + WallSpawner.GetCurrentWallSpawnerState().PlayArea.TopBar.ToString());
            }
            else if (isLeft)
            {
                var wall = parent.FindChild("Wallyer");
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.LeftBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.max.y);
                //Debug.Log("New Left: " + WallSpawner.GetCurrentWallSpawnerState().PlayArea.LeftBar.ToString());
            }
            else if (isRight)
            {
                var wall = parent.FindChild("Wallyer");
                WallSpawner.GetCurrentWallSpawnerState().PlayArea.RightBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.max.y);
                //Debug.Log("New Right: " + WallSpawner.GetCurrentWallSpawnerState().PlayArea.RightBar.ToString());
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
