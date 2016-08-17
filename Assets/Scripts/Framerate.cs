using UnityEngine;
using System.Collections;

public class Framerate : MonoBehaviour {

    void Awake()
    {
        Application.targetFrameRate = 30;
    }

}
