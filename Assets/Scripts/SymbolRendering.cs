using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolRendering : MonoBehaviour
{
    public Transform meshParent;

    public LineRenderer LeftR;
    public LineRenderer RightR;

    public GameObject LeftC;
    public GameObject RightC;

    public bool symbolDraw;

    [Range(0.01f, 0.1f)]
    public float tolerance;

    bool IndexButton()
    {
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    void Draw(Transform t, LineRenderer r)
    {
        int a = r.positionCount++;
        var tmp = t.position;
        r.SetPosition(a, tmp);
    }

    void ResetRenderers(LineRenderer r)
    {
        r.Simplify(tolerance);
        Mesh m = new Mesh();
        r.BakeMesh(m, true);

        var g = new GameObject("Mesh");
        g.transform.parent = meshParent;
        g.AddComponent<MeshFilter>().mesh = m;
        g.AddComponent<MeshRenderer>();

        r.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if (symbolDraw)
        {
            Vector2 ip = HelperFunctions.TrackControllerInput();
            if (ip.x > 0.5)
                Draw(LeftC.transform, LeftR);
            else if (LeftR.positionCount > 0)
                ResetRenderers(LeftR);
            if (ip.y > 0.5)
                Draw(RightC.transform, RightR);
            else if (RightR.positionCount > 0)
                ResetRenderers(RightR);
        }


        if (IndexButton())
        {
            for (int i = 0; i < meshParent.childCount; i++)
            {
                Destroy(meshParent.GetChild(i).gameObject);
            }
        }
    }
}
