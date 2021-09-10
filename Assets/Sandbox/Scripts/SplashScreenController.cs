using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
    public string loadLevel;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(loadLevel);
    }
}
