using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject c;
    [Range(0.01f, 0.5f)]
    public float speed;

    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        target = transform.position - c.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var r = c.transform.rotation.eulerAngles;
        Quaternion a = Quaternion.Euler(
            0,
            r.y,
            0
        );
        transform.rotation = Quaternion.Lerp(transform.rotation, a, speed);
        transform.position = Vector3.Lerp(transform.position, target + c.transform.position, speed);
    }
}
