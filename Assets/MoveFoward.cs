using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFoward : MonoBehaviour
{
    public Vector3 direction;
    public int speed = 1;
    // Update is called once per frame

    void Start()
    {
        StartCoroutine("DieIn");
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }


    IEnumerator DieIn()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(this.gameObject);
    }
}
