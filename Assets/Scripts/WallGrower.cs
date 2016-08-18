using UnityEngine;
using System.Collections;


// Attached to Right & LeftWallyer
public class WallGrower : MonoBehaviour {
    public bool IsGrowing = true;
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
            //IsGrowingSpawnerCheck = true;
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        // Or here? If, on spawn, one of the Wallyers hits a Bar, despawn that wall

        // If we hit the outside box or
        //    we hit a wall and we didnt hit the other spawned wall
        if (coll.gameObject.name.Contains("Bar") || (coll.gameObject.name.Contains("Wallyer") && coll.gameObject != FindBrother()))
        {
            IsGrowing = false;
            var bro = FindBrother();
            var broScript = bro.GetComponent<WallGrower>();
            // If we are done growing and our brother is done growing
            if (!broScript.IsGrowing)
            {
                SpawnTestAreas();
            }
        }

        if (IsGrowing && coll.gameObject.name.Contains("Ball"))
        {
            var gameState = GameObject.Find(@"/GameState");
            var gameStateScript = gameState.GetComponent<GameState>();
            gameStateScript.RemoveLife();
            //Both walls are destroyed
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(this.FindBrother());
        }
    }

    public void SpawnTestAreas()
    {
        var topBar = GameObject.FindGameObjectWithTag("TopBar");
        var leftBar = GameObject.FindGameObjectWithTag("LeftBar");
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

            GameObject testArea1 = Instantiate(square, center, Quaternion.identity) as GameObject;
            testArea1.transform.localScale = new Vector3(w1, h1, 1);
            testArea1.GetComponent<VoidArea>().isTop = true;
            if (this.name.Contains("Left"))
            {
                testArea1.GetComponent<VoidArea>().parent = this.gameObject;
            }
            else
            {
                testArea1.GetComponent<VoidArea>().parent = FindBrother();
            }

            GameObject testArea2 = Instantiate(square, center2, Quaternion.identity) as GameObject;
            testArea2.transform.localScale = new Vector3(w2, h2, 1);
            testArea2.GetComponent<VoidArea>().isBottom = true;
            if (this.name.Contains("Left"))
            {
                testArea2.GetComponent<VoidArea>().parent = this.gameObject;
            }
            else
            {
                testArea2.GetComponent<VoidArea>().parent = FindBrother();
            }

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

            GameObject testArea1 = Instantiate(square, center, Quaternion.identity) as GameObject;
            testArea1.transform.localScale = new Vector3(w1, h1, 1);
            testArea1.GetComponent<VoidArea>().isLeft = true;
            if(this.name.Contains("Left"))
            {
                testArea1.GetComponent<VoidArea>().parent = FindBrother();
            }
            else
            {
                testArea1.GetComponent<VoidArea>().parent = this.gameObject;
            }

            GameObject testArea2 = Instantiate(square, center2, Quaternion.identity) as GameObject;
            testArea2.transform.localScale = new Vector3(w2, h2, 1);
            testArea2.GetComponent<VoidArea>().isRight = true;
            if (this.name.Contains("Left"))
            {
                testArea2.GetComponent<VoidArea>().parent = FindBrother();
            }
            else
            {
                testArea2.GetComponent<VoidArea>().parent = this.gameObject;
            }
        }
    }

    private GameObject FindBrother()
    {
        if(name.Contains("Left"))
        {
            return Helpers.FindChild(this.transform.parent.gameObject, "Right Wallyer");
        }
        else if(name.Contains("Right"))
        {
            return this.transform.parent.gameObject.FindChild("Left Wallyer");
        }

        return null;
    }
}
