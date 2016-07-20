using UnityEngine;
using System.Collections;

public class WallGrower : MonoBehaviour {
    private bool IsGrowing = true;

    public int GrowDirection = 1;
    public float GrowSpeed = 0.01f;

    public GameObject square;

    //TODO make work
    public bool IsHorizontal { get { return this.transform.parent.localRotation.z == 0; } }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if(IsGrowing)
        {
            transform.localScale += new Vector3(GrowDirection * GrowSpeed, 0f, 0f);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Needs to be if both Left & Right Wallyer have hit object "Bar"
        // Find brother is messing the intersection up
        if (coll.gameObject.name.Contains("Bar") || (coll.gameObject.name.Contains("Wallyer") && coll.gameObject != FindBrother()))
        {
            IsGrowing = false;
            SpawnTestAreas();
        }

        if (IsGrowing && coll.gameObject.name.Contains("Ball"))
        {
            var gameState = GameObject.Find(@"/GameState");
            var gameStateScript = gameState.GetComponent<GameState>();
            gameStateScript.RemoveLife();
            GameObject.Destroy(this.gameObject);
        }
    }

    public void SpawnTestAreas()
    {
        var topBar = GameObject.Find("Top Bar");
        var leftBar = GameObject.Find("Left Bar");
        var origin = new Vector3(topBar.GetComponent<Renderer>().bounds.min.x, leftBar.GetComponent<Renderer>().bounds.max.y, 0.0f);
        var width = topBar.GetComponent<Renderer>().bounds.size.magnitude;
        var height = leftBar.GetComponent<Renderer>().bounds.size.magnitude;
        var point = this.transform.position;
        if (IsHorizontal)
        {
            Vector3 origin2 = new Vector3(origin.x, point.y, 0.0f);
            float w1 = width;
            float w2 = width;
            float h1 = origin.y - origin2.y;
            float h2 = height - h1;

            Vector3 center = new Vector3(origin.x + w1 / 2, origin.y - h1 / 2, 0.0f);
            Vector3 center2 = new Vector3(origin2.x + w2 / 2, origin2.y - h2 / 2, 0.0f);

            GameObject spawnedSquare = Instantiate(square, center, Quaternion.identity) as GameObject;
            spawnedSquare.transform.localScale = new Vector3(w1, h1, 1);

            GameObject spawnedSquare2 = Instantiate(square, center2, Quaternion.identity) as GameObject;
            spawnedSquare2.transform.localScale = new Vector3(w2, h2, 1);
        }
        // IsVertical
        else
        {
            Vector3 origin2 = new Vector3(point.x, origin.y, 0.0f);
            float w1 = origin2.x - origin.x;
            float w2 = width - w1;
            float h1 = height;
            float h2 = height;

            Vector3 center = new Vector3(origin.x + w1 / 2, origin.y - h1 / 2, 0.0f);
            Vector3 center2 = new Vector3(origin2.x + w2 / 2, origin2.y - h2 / 2, 0.0f);

            GameObject spawnedSquare = Instantiate(square, center, Quaternion.identity) as GameObject;
            spawnedSquare.transform.localScale = new Vector3(w1, h1, 1);

            GameObject spawnedSquare2 = Instantiate(square, center2, Quaternion.identity) as GameObject;
            spawnedSquare2.transform.localScale = new Vector3(w2, h2, 1);
        }
    }

    private GameObject FindBrother()
    {
        if(name.Contains("Left"))
        {
            return FindChild(this.transform.parent.gameObject, "Right Wallyer");
        }
        else if(name.Contains("Right"))
        {
            return FindChild(this.transform.parent.gameObject, "Left Wallyer");
        }

        return null;
    }

    public GameObject FindChild(GameObject parent, string name)
    {
        return parent.transform.Find(name).gameObject;
    }
}
