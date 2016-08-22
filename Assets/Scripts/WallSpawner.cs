using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayArea
{
    public Vector2 TopBar;
    public Vector2 BottomBar;
    public Vector2 RightBar;
    public Vector2 LeftBar;

    public Vector3 Origin { get { return new Vector3(LeftBar.x, TopBar.y, 0.0f); } }

    public float Width { get { return RightBar.x - LeftBar.x; } }

    public float Height { get { return TopBar.y - BottomBar.y; } }

    public PlayArea()
    {
    }

    public PlayArea(GameObject top, GameObject bottom, GameObject left, GameObject right)
    {
        TopBar = new Vector2(top.GetComponent<Renderer>().bounds.min.x, (top.GetComponent<Renderer>().bounds.min.y));
        Debug.Log("New Top: " + TopBar.ToString());
        BottomBar = new Vector2(bottom.GetComponent<Renderer>().bounds.min.x, (bottom.GetComponent<Renderer>().bounds.min.y));
        Debug.Log("New Bottom: " + BottomBar.ToString());
        LeftBar = new Vector2(left.GetComponent<Renderer>().bounds.min.x, left.GetComponent<Renderer>().bounds.max.y);
        Debug.Log("New Left: " + LeftBar.ToString());
        RightBar = new Vector2(right.GetComponent<Renderer>().bounds.min.x, right.GetComponent<Renderer>().bounds.max.y);
        Debug.Log("New Right: " + RightBar.ToString());
    }

    public bool IsInsidePlayArea(Vector3 point)
    {
        return IsInsideSquare(
            point,
            Origin,
            Width,
            Height);
    }

    public float GetArea()
    {
        return Width * Height;
    }

    // Updated play area to be inside the bars, not the outisde
    public bool IsInsideSquare(Vector3 point, Vector3 origin, float w, float h)
    {
        Debug.Log(string.Format("Checking play area at point:{0}, defined by origin:{1} w:{2} h:{3}", point.ToString(), origin.ToString(), w, h));
        if (point.x >= origin.x && point.x <= w + origin.x)
        {
            if (point.y <= origin.y && point.y >= -h + origin.y)
            {
                Debug.Log("inside");
                return true;
            }
        }
        Debug.Log("outside");
        return false;
    }

    public override string ToString()
    {
        return string.Format("top {0} bot {1} left {2} right {3}", TopBar, BottomBar, LeftBar, RightBar);
    }
}

// Attached to Scene
public class WallSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Button HorzButton;
    public Button VertButton;
    private List<PlayArea> PlayAreas;
    public PlayArea InitPlayArea;
    private bool IsHorizontal = true;
    public Button MenuButton;
    public bool Pause = false;

    private List<GameObject> expandyerWallyList = new List<GameObject>();

    void Start()
    {
        PlayAreas = new List<PlayArea>() { new PlayArea(GameObject.Find("Top Bar"), GameObject.Find("Bottom Bar"), GameObject.Find("Left Bar"), GameObject.Find("Right Bar")) };
        InitPlayArea = new PlayArea(GameObject.Find("Top Bar"), GameObject.Find("Bottom Bar"), GameObject.Find("Left Bar"), GameObject.Find("Right Bar"));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (!isAnyWallyGrowing() && GameState.GetCurrentGameState().noLives == false && Pause == false)
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.z = 0;
                foreach(var playArea in PlayAreas)
                {
                    if (playArea.IsInsidePlayArea(point) == true)
                    {
                        SpawnWall(point, playArea);
                        break;
                    }
                }            
            }            
        }
    }

    public void SpawnWall(Vector3 point, PlayArea playArea)
    {
        Quaternion rotation;

        if (IsHorizontal)
        {
            // Sets horizontal rotation
            rotation = Quaternion.identity;
        }
        else
        {
            // Sets vertical rotation
            rotation = Quaternion.Euler(0, 0, 90);
        }
        GameObject spanwedWally = Instantiate(prefab, point, rotation) as GameObject;
        spanwedWally.FindChild("Left Wallyer").GetComponent<WallGrower>().parentPlayArea = playArea;
        spanwedWally.FindChild("Right Wallyer").GetComponent<WallGrower>().parentPlayArea = playArea;
        expandyerWallyList.Add(spanwedWally);
    }

    public void ToggleRotation()
    {
        if (IsHorizontal)
        {
            // Btn is vertical
            IsHorizontal = false;
            HorzButton.gameObject.SetActive(false);
            VertButton.gameObject.SetActive(true);
        }
        else
        {
            // Btn is horzintal
            IsHorizontal = true;
            
            VertButton.gameObject.SetActive(false);
            HorzButton.gameObject.SetActive(true);
        }
    }

    public static WallSpawner GetCurrentWallSpawnerState()
    {
        return GameObject.Find("WallSpawner").GetComponent<WallSpawner>();
    }

    private bool isAnyWallyGrowing()
    {
        foreach (var expandyerWally in expandyerWallyList)
        {
            GameObject leftWallyer = expandyerWally.FindChild("Left Wallyer");
            GameObject rightWallyer = expandyerWally.FindChild("Right Wallyer");

            if (leftWallyer != null && rightWallyer != null)
            {
                if (leftWallyer.GetComponent<WallGrower>().IsGrowing == true || rightWallyer.GetComponent<WallGrower>().IsGrowing == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public float GetCurrentPlayArea()
    {
        float area = 0.0f;
        foreach(var playArea in PlayAreas)
        {
            area += playArea.GetArea();
        }

        return area;
    }

    public void SplitPlayArea(PlayArea playArea, GameObject splitBar, bool isHorizontal)
    {
        PlayArea p1 = new PlayArea();
        PlayArea p2 = new PlayArea();
        if(isHorizontal)
        {
            p1.TopBar = playArea.TopBar;
            p1.BottomBar = new Vector2(splitBar.GetComponent<Renderer>().bounds.min.x, (splitBar.GetComponent<Renderer>().bounds.min.y));
            p1.LeftBar = playArea.LeftBar;
            p1.RightBar = playArea.RightBar;


            p2.TopBar = new Vector2(splitBar.GetComponent<Renderer>().bounds.min.x, (splitBar.GetComponent<Renderer>().bounds.min.y));
            p2.BottomBar = playArea.BottomBar;
            p2.LeftBar = playArea.LeftBar;
            p2.RightBar = playArea.RightBar;
        }
        // Vertical
        else
        {
            p1.TopBar = playArea.TopBar;
            p1.BottomBar = playArea.BottomBar;
            p1.LeftBar = playArea.LeftBar;
            p1.RightBar = new Vector2(splitBar.GetComponent<Renderer>().bounds.min.x, splitBar.GetComponent<Renderer>().bounds.max.y);


            p2.TopBar = playArea.TopBar;
            p2.BottomBar = playArea.BottomBar;
            p2.LeftBar = new Vector2(splitBar.GetComponent<Renderer>().bounds.min.x, splitBar.GetComponent<Renderer>().bounds.max.y);
            p2.RightBar = playArea.RightBar;
        }
        Debug.Log("Splitting play area.");
        Debug.Log(p1.ToString());
        Debug.Log(p2.ToString());
        PlayAreas.Remove(playArea);
        PlayAreas.Add(p1);
        PlayAreas.Add(p2);
    }
}
