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
        var TopBarObj = GameObject.Find("Top Bar");
        TopBar = new Vector2(TopBarObj.GetComponent<Renderer>().bounds.min.x, (TopBarObj.GetComponent<Renderer>().bounds.min.y - 0.12f));
        //Debug.Log("New Top: " + TopBar.ToString());
        var BottomBarObj = GameObject.Find("Bottom Bar");
        BottomBar = new Vector2(BottomBarObj.GetComponent<Renderer>().bounds.min.x, (BottomBarObj.GetComponent<Renderer>().bounds.min.y + 0.30f));
        //Debug.Log("New Bottom: " + BottomBar.ToString());
        var RightBarObj = GameObject.Find("Right Bar");
        RightBar = new Vector2(RightBarObj.GetComponent<Renderer>().bounds.min.x - 0.30f, RightBarObj.GetComponent<Renderer>().bounds.max.y);
        //Debug.Log("New Right: " + RightBar.ToString());
        var LeftBarObj = GameObject.Find("Left Bar");
        LeftBar = new Vector2(LeftBarObj.GetComponent<Renderer>().bounds.min.x + 0.30f, LeftBarObj.GetComponent<Renderer>().bounds.max.y);
        //Debug.Log("New Left: " + LeftBar.ToString());
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
        //Debug.Log(string.Format("Checking play area at point:{0}, defined by origin:{1} w:{2} h:{3}", point.ToString(), origin.ToString(), w, h));
        if (point.x >= origin.x && point.x <= w + origin.x)
        {
            if (point.y <= origin.y && point.y >= -h + origin.y)
            {
                //Debug.Log("inside");
                return true;
            }
        }
        //Debug.Log("outside");
        return false;
    }
}

// Attached to Scene
public class WallSpawner : MonoBehaviour
{
    public GameObject prefab;
    public Button HorzButton;
    public Button VertButton;
    public PlayArea PlayArea;
    public PlayArea InitPlayArea;
    private bool IsHorizontal = true;
    public Button MenuButton;
    public bool Pause = false;

    private List<GameObject> expandyerWallyList = new List<GameObject>();

    void Start()
    {
        PlayArea = new PlayArea();
        InitPlayArea = new PlayArea();
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
                if (PlayArea.IsInsidePlayArea(point) == true)
                {
                    SpawnWall(point);
                }               
            }            
        }
    }

    public void SpawnWall(Vector3 point)
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
}
