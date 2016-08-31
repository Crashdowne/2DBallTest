using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStartUp : MonoBehaviour {

    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps;
    public Text FPSText;

    // Use this for initialization
    void Start ()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
            FPSText.text = fps.ToString();
        }
    }

    void OnGUI()
    {
        GUILayout.Label("" + fps.ToString("f2"));
    }
}
