using UnityEngine;
using System.Collections;


// Attached to Ball Prefabs
public class SimpleForce : MonoBehaviour {

    // Lets us set how much force (continuous 'push') we apply in the editor (up and right)
    public float upForceAmnt;
    public float rightForceAmnt;
    private Rigidbody2D rb;
    private bool isMoving = false;
    private int directionLR;
    private int directionUD;


    // Use this for initialization
    void Start () {

        rb = GetComponent<Rigidbody2D>();

        // Apply force to the balls!
        if (isMoving == false)
        {
            // 0 == Up, 1 == Down
            directionUD = Random.Range(0, 2);
            Debug.Log(directionUD);
            // 0 == Left, 1 == Right
            directionLR = Random.Range(0, 2);
            Debug.Log(directionUD);

            if (directionUD == 0 && directionLR == 0)
            {
                rb.AddForce(transform.up * upForceAmnt);
                rb.AddForce((-transform.right) * rightForceAmnt);
            }

            else if (directionUD == 1 && directionLR == 0)
            {
                rb.AddForce((-transform.up) * upForceAmnt);
                rb.AddForce((-transform.right) * rightForceAmnt);
            }

            else if (directionUD == 0 && directionLR == 1)
            {
                rb.AddForce(transform.up * upForceAmnt);
                rb.AddForce(transform.right * rightForceAmnt);
            }

            else if (directionUD == 1 && directionLR == 1)
            {
                rb.AddForce((-transform.up) * upForceAmnt);
                rb.AddForce(transform.right * rightForceAmnt);
            }

            // We only want to apply the force once
            isMoving = true;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        
    }
}
