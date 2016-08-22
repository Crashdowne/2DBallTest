using UnityEngine;
using System.Collections;


// Attached to Right & LeftWallyer
public class WallGrower : MonoBehaviour {
    public bool IsGrowing = true;
    public int GrowDirection = 1;
    public float GrowSpeed = 0.01f;
    public GameObject square;
    public PlayArea parentPlayArea;
    public GameObject TestArea1;
    public GameObject TestArea2;
    private int currentTestAreas = 2;

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
        var topBar = parentPlayArea.TopBar;
        var leftBar = parentPlayArea.LeftBar;
        var origin = new Vector2(topBar.x, leftBar.y);
        var width = parentPlayArea.Width;
        var height = parentPlayArea.Height;
        var point = this.transform.position;

        if (IsHorizontal)
        {
            Vector2 origin2 = new Vector2(origin.x, point.y);
            float w1 = width;
            float w2 = width;
            float h1 = origin.y - origin2.y;
            float h2 = height - h1;

            Vector2 center = new Vector2(origin.x + w1 / 2, origin.y - h1 / 2);
            Vector2 center2 = new Vector2(origin2.x + w2 / 2, origin2.y - h2 / 2);

            TestArea1 = Instantiate(square, center, Quaternion.identity) as GameObject;
            TestArea1.transform.localScale = new Vector3(w1, h1, 1);
            TestArea1.GetComponent<VoidArea>().isTop = true;
            TestArea1.GetComponent<VoidArea>().parentPlayArea = parentPlayArea;
            if (this.name.Contains("Left"))
            {
                TestArea1.GetComponent<VoidArea>().parent = this.gameObject;
            }
            else
            {
                TestArea1.GetComponent<VoidArea>().parent = FindBrother();
            }

            TestArea2 = Instantiate(square, center2, Quaternion.identity) as GameObject;
            TestArea2.transform.localScale = new Vector3(w2, h2, 1);
            TestArea2.GetComponent<VoidArea>().isBottom = true;
            TestArea2.GetComponent<VoidArea>().parentPlayArea = parentPlayArea;
            if (this.name.Contains("Left"))
            {
                TestArea2.GetComponent<VoidArea>().parent = this.gameObject;
            }
            else
            {
                TestArea2.GetComponent<VoidArea>().parent = FindBrother();
            }

        }
        // IsVertical
        else
        {
            Vector2 origin2 = new Vector2(point.x, origin.y);
            float w1 = origin2.x - origin.x;
            float w2 = width - w1;
            float h1 = height;
            float h2 = height;

            Vector2 center = new Vector2(origin.x + w1 / 2, origin.y - h1 / 2);
            Vector2 center2 = new Vector2(origin2.x + w2 / 2, origin2.y - h2 / 2);

            TestArea1 = Instantiate(square, center, Quaternion.identity) as GameObject;
            TestArea1.transform.localScale = new Vector3(w1, h1, 1);
            TestArea1.GetComponent<VoidArea>().isLeft = true;
            TestArea1.GetComponent<VoidArea>().parentPlayArea = parentPlayArea;
            if (this.name.Contains("Left"))
            {
                TestArea1.GetComponent<VoidArea>().parent = FindBrother();
            }
            else
            {
                TestArea1.GetComponent<VoidArea>().parent = this.gameObject;
            }

            TestArea2 = Instantiate(square, center2, Quaternion.identity) as GameObject;
            TestArea2.transform.localScale = new Vector3(w2, h2, 1);
            TestArea2.GetComponent<VoidArea>().isRight = true;
            TestArea2.GetComponent<VoidArea>().parentPlayArea = parentPlayArea;
            if (this.name.Contains("Left"))
            {
                TestArea2.GetComponent<VoidArea>().parent = FindBrother();
            }
            else
            {
                TestArea2.GetComponent<VoidArea>().parent = this.gameObject;
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

    public void TestAreaDestroyed()
    {
        currentTestAreas -= 1;

        if(currentTestAreas == 0)
        {
            GameObject splitWall = null;
            if (currentTestAreas == 0)
            {
                if (IsHorizontal)
                {
                    if (this.name.Contains("Left"))
                    {
                        splitWall = this.gameObject;
                    }
                    else
                    {
                        splitWall = FindBrother();
                    }
                }
                else
                {
                    if (this.name.Contains("Left"))
                    {
                        splitWall = FindBrother();
                    }
                    else
                    {
                        splitWall = this.gameObject;
                    }
                }
            }

            WallSpawner.GetCurrentWallSpawnerState().SplitPlayArea(parentPlayArea, splitWall.FindChild("Wallyer"), IsHorizontal);
        }
    }
}
