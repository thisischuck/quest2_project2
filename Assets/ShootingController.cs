using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelperTypes;

public class ShootingController : MonoBehaviour
{
    public bool isLeft;
    bool shot;
    public int cooldown;
    public GameObject shootPrefab;

    // Update is called once per frame
    void Update()
    {
        //LEFT if X
        if (isLeft)
        {
            if (HelperFunctions.TrackControllerIndex().x > 0.5f)
            {
                if (!shot) StartCoroutine("Shoot");
            }
        }
        else if (HelperFunctions.TrackControllerIndex().y > 0.5f)
        {
            if (!shot) StartCoroutine("Shoot");
        }
    }

    IEnumerator Shoot()
    {
        shot = true;
        //Instanciate shot
        var a = Instantiate(shootPrefab, transform.position, transform.rotation);
        a.GetComponent<MoveFoward>().direction = transform.forward;
        yield return new WaitForSecondsRealtime(cooldown);
        shot = false;
    }
}
