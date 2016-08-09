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

    // Use this for initialization
    void Start()
    {
        //rotationText.text = "Rotation = Horizontal";
    }

    // Update is called once per frame
    void Update()
    {
        // Don't really need to detect right click anymore
        if (Input.GetButtonDown("Fire2"))
        {
            ToggleRotation();
        }

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

    public bool IsInsidePlayArea(Vector3 point, Vector3 origin, float h, float w)
    {
        if (point.x >= origin.x && point.x <= w + origin.x)
        {
            if (point.y <= origin.y && point.y >= -h + origin.y)
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
