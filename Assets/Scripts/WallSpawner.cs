using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class WallSpawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject topBar;
    public GameObject leftBar;
    public Button HorzButton;
    public Button VertButton;

    private bool wallDoneGrowing = true;
    private bool IsHorizontal = true;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (wallDoneGrowing)
            {
                var point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                point.z = 0;
                if (IsInsidePlayArea(point, new Vector3(topBar.GetComponent<Renderer>().bounds.min.x, leftBar.GetComponent<Renderer>().bounds.max.y, 0.0f), 
                    leftBar.GetComponent<Renderer>().bounds.size.magnitude, topBar.GetComponent<Renderer>().bounds.size.magnitude) == true)
                {
                    SpawnWall(point);
                    wallDoneGrowing = true;
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
            wallDoneGrowing = true;
    }

    // Updated play area to be inside the bars, not the outisde
    public bool IsInsidePlayArea(Vector3 point, Vector3 origin, float h, float w)
    {
        if (point.x >= (origin.x + 0.25) && point.x <= w + (origin.x - 0.25))
        {
            if ((point.y + 0.25) <= origin.y && point.y >= -h + (origin.y - 0.25))
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
            //rotationText.text = "Rotation = Vertical";
        }
        else
        {
            // Btn is horzintal
            IsHorizontal = true;
            
            VertButton.gameObject.SetActive(false);
            HorzButton.gameObject.SetActive(true);
            //rotationText.text = "Rotation = Horizontal";
        }
    }
}
