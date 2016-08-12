using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class WallSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject topBar;
    public GameObject leftBar;
    public Button HorzButton;
    public Button VertButton;
    public Text percentCoveredText;
    private double percentCovered = 0.0;
    private bool IsHorizontal = true;

    //GameObject Top = GameObject.Find("Top Bar");
    //GameObject Bottom = GameObject.Find("Bottom Bar");
    //GameObject Left = GameObject.Find("Left Bar");
    //GameObject Right = GameObject.Find("Right Bar");

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Needs to be changed to reflect new calculation of play area coverage
        percentCoveredText.text = "Covered: " + percentCovered + "%";

        if (Input.GetButtonDown("Fire1"))
        {
            if (!WallGrower.IsGrowingSpawnerCheck && GameState.noLives == false)
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.z = 0;
                if (IsInsidePlayArea(point, new Vector3(topBar.GetComponent<Renderer>().bounds.min.x, 
                    leftBar.GetComponent<Renderer>().bounds.max.y, 0.0f),
                    leftBar.GetComponent<Renderer>().bounds.size.magnitude, 
                    topBar.GetComponent<Renderer>().bounds.size.magnitude) == true)
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
        Instantiate(prefab, point, rotation);
    }

    // Updated play area to be inside the bars, not the outisde
    public bool IsInsidePlayArea(Vector3 point, Vector3 origin, float h, float w)
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
}
