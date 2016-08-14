using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayArea
{
    public GameObject TopBar;
    public GameObject BottomBar;
    public GameObject RightBar;
    public GameObject LeftBar;

    public PlayArea()
    {
        TopBar = GameObject.Find("Top Bar");
        BottomBar = GameObject.Find("Bottom Bar");
        RightBar = GameObject.Find("Right Bar");
        LeftBar = GameObject.Find("Left Bar");
    }

    public bool IsInsidePlayArea(Vector3 point)
    {
        return IsInsideSquare(
            point,
            new Vector3(TopBar.GetComponent<Renderer>().bounds.min.x, LeftBar.GetComponent<Renderer>().bounds.max.y, 0.0f),
            LeftBar.GetComponent<Renderer>().bounds.size.magnitude,
            TopBar.GetComponent<Renderer>().bounds.size.magnitude);
    }

    // Updated play area to be inside the bars, not the outisde
    public bool IsInsideSquare(Vector3 point, Vector3 origin, float h, float w)
    {
        if (point.x >= (origin.x + 0.30) && point.x <= w + (origin.x - 0.30))
        {
            if ((point.y) <= (origin.y - 0.10) && point.y >= -h + (origin.y + 0.10))
            {
                return true;
            }
        }
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
    public Text percentCoveredText;
    private double percentCovered = 0.0;
    private bool IsHorizontal = true;
    public Button MenuButton;
    public Button RestartButton;
    public bool Pause = false;

    private List<GameObject> expandyerWallyList = new List<GameObject>();

    //GameObject Top = GameObject.Find("Top Bar");
    //GameObject Bottom = GameObject.Find("Bottom Bar");
    //GameObject Left = GameObject.Find("Left Bar");
    //GameObject Right = GameObject.Find("Right Bar");

    void Start()
    {
        PlayArea = new PlayArea();
    }

    // Update is called once per frame
    void Update()
    {
        // Needs to be changed to reflect new calculation of play area coverage
        percentCoveredText.text = "Covered: " + percentCovered + "%";

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
