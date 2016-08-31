using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

// Attached to Level Manager
public class ScoreScript : MonoBehaviour {

    public Text timerText;
    public Text scoreTimeText;
    private string timeOut;

    // Update is called once per frame
    void Update ()
    {
        timeOut = string.Format("{0:0.##}", Time.timeSinceLevelLoad);
        timerText.text = timeOut + " s";
        scoreTimeText.text = "Time Taken: " + timeOut + "s";
    }
}
