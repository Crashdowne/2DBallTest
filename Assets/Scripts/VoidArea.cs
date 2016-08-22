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
    public PlayArea parentPlayArea;
    public bool isDestroyed = false;

    public Material blackMaterial;
    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
        if(isDestroyed)
        {
            return;
        }

        // Once updateCount == 5 changes the material --> Delayed Start
        if(updateCount == 5) 
        {
            gameObject.GetComponent<Renderer>().sharedMaterial = blackMaterial;

            if (isBottom)
            {
                var wall = parent.FindChild("Wallyer");
                parentPlayArea.BottomBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.min.y);
                Debug.Log("New Bottom: " + parentPlayArea.BottomBar.ToString());
            }
            else if (isTop)
            {
                var wall = parent.FindChild("Wallyer");
                parentPlayArea.TopBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.min.y);
                Debug.Log("New Top: " + parentPlayArea.TopBar.ToString());
            }
            else if (isLeft)
            {
                var wall = parent.FindChild("Wallyer");
                parentPlayArea.LeftBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.max.y);
                Debug.Log("New Left: " + parentPlayArea.LeftBar.ToString());
            }
            else if (isRight)
            {
                var wall = parent.FindChild("Wallyer");
                parentPlayArea.RightBar = new Vector2(wall.GetComponent<Renderer>().bounds.min.x, wall.GetComponent<Renderer>().bounds.max.y);
                Debug.Log("New Right: " + parentPlayArea.RightBar.ToString());
            }
        }
        updateCount += 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isDestroyed)
        {
            return;
        }

        if (other.gameObject.name.Contains("Ball"))
        {
            isDestroyed = true;
            parent.GetComponent<WallGrower>().TestAreaDestroyed();
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (isDestroyed)
        {
            return;
        }

        if (other.gameObject.name.Contains("Ball"))
        {
            isDestroyed = true;
            parent.GetComponent<WallGrower>().TestAreaDestroyed();
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isDestroyed)
        {
            return;
        }

        if (other.gameObject.name.Contains("Ball"))
        {
            isDestroyed = true;
            parent.GetComponent<WallGrower>().TestAreaDestroyed();
            GameObject.Destroy(gameObject);
        }
    }
}
