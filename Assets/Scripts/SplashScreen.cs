using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    IEnumerator Start()
    {
        // Not loading the main menu...
        yield return new WaitForSeconds(3);
        AsyncOperation async = SceneManager.LoadSceneAsync(0);
        Debug.Log("Loading complete");
        yield return async;
        
    }
}
