using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        //HelperFunctions.MyDebug(other.name);
        if (other.gameObject.tag.Equals("GameController"))
        {
            if (other.gameObject.name.Contains("Left"))
            {
                if (HelperFunctions.TrackControllerIndex().x > 0.5f)
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
            if (other.gameObject.name.Contains("Right"))
            {
                if (HelperFunctions.TrackControllerIndex().y > 0.5f)
                    SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
        }
    }
}
