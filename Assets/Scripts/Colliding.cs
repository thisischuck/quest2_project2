using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colliding : MonoBehaviour
{
    public Transform parent;
    public Material t, f, s;
    MeshRenderer renderer;
    public bool isChild;

    Vector2 ip;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        s = renderer.material;
    }

    void Update()
    {
        ip = HelperFunctions.TrackControllerInput();
        if (isChild)
        {
            var rb = GetComponent<Rigidbody>();
            HelperFunctions.MyDebug(
                rb.velocity + " " +
                rb.angularDrag + " " +
                rb.angularVelocity
            );
            if (!LeftOrRight(transform.parent.name))
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                transform.SetParent(parent);
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (isChild)
            return;
        renderer.material = t;
    }

    bool LeftOrRight(string name)
    {
        switch (name)
        {
            case "LeftHand":
                return ip.x > 0.5;
            case "RightHand":
                return ip.y > 0.5;
            default:
                return false;
        }
    }


    void OnTriggerStay(Collider other)
    {
        renderer.material = f;

        HelperFunctions.MyDebug(other.gameObject);
        if (!isChild)
        {
            if (LeftOrRight(other.gameObject.name))
            {
                if (other.transform.childCount == 0 && other.gameObject.tag.Equals("GameController"))
                {
                    var g = Instantiate(gameObject, other.transform.position, Quaternion.identity, other.gameObject.transform);
                    g.transform.localScale = Vector3.one;
                    g.GetComponent<Colliding>().isChild = true;
                    g.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (isChild)
            return;
        renderer.material = s;
    }
}
