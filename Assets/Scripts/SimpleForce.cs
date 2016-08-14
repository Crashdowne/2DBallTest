using UnityEngine;
using System.Collections;


// Attached to Ball Prefabs
public class SimpleForce : MonoBehaviour {

    // Lets us set how much force (continuous 'push') we apply in the editor (up and right)
    public float upForceAmnt;
    public float rightForceAmnt;

    // Creats a new rigid body, so the ball can interact with things
    private Rigidbody2D rb;

    // So we know if the balls were already moving or not
    bool isMoving = false;

	// Use this for initialization
	void Start () {

        // ???
        rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        // Apply force to the balls!
        if (isMoving == false)
        {
            // Lets us give it angular force, otherwise it would just go up / down or left / right
            rb.AddForce(transform.up * upForceAmnt);
            rb.AddForce(transform.right * rightForceAmnt);

            // We only want to apply the force once
            isMoving = true;
        }
        
    }
}
