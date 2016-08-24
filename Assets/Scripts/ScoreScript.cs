using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading;

public class ScoreScript : MonoBehaviour {

    private float score = 100f;

	// Use this for initialization
	void Start ()
    {
        // Create new stopwatch.
        Stopwatch stopwatch = new Stopwatch();

        // Begin timing.
        stopwatch.Start();

        // Do something.
        for (int i = 0; i < 1000; i++)
        {
            Thread.Sleep(1);
        }

        // Stop timing.
        stopwatch.Stop();

    }

    // Update is called once per frame
    void Update () {
	
	}
}
