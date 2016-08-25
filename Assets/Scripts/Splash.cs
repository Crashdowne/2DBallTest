using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour {

	// Use this for initialization
    void Start ()
    {
        StartCoroutine(WaitToStart());      
    }

    IEnumerator WaitToStart()
    {
        Debug.Log("waiting");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("wait end");
        SceneManager.LoadScene("Menu");
    }
}
