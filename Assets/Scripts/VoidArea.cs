using UnityEngine;
using System.Collections;

public class VoidArea : MonoBehaviour {
    ulong updateCount = 0;
    public static double coveredArea = 0.0;

    public Material blackMaterial;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        // Once updateCount == 5 changes the material --> Delayed Start
        if(updateCount == 5) 
        {
            gameObject.GetComponent<Renderer>().material = blackMaterial;
            coveredArea += (gameObject.GetComponent<Renderer>().bounds.size.x * gameObject.GetComponent<Renderer>().bounds.size.y);
        }
        updateCount += 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            GameObject.Destroy(gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Contains("Ball"))
        {
            GameObject.Destroy(gameObject);
        }
    }
}
