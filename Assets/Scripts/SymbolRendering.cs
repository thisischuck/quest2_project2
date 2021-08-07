using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SymbolRendering : MonoBehaviour
{
    public SymbolObject sData;
    public Transform meshParent;

    public LineRenderer LeftR;
    public LineRenderer RightR;

    public GameObject LeftC;
    public GameObject RightC;

    public bool symbolDraw;
    public bool rotateSymbol;

    [Range(0.01f, 0.1f)]
    public float tolerance;

    bool IndexButton(bool isLeft)
    {
        if (isLeft)
            return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch) > 0.5;
        return OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch) > 0.5;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (sData.SaveMe)
        {
            sData.Language = new SymbolLanguage(sData.StepValue);
            sData.symbols.ForEach(
                s => s.CalculateLists(sData)
            );
            EditorUtility.SetDirty(sData);
            sData.SaveMe = false;
        }

        sData.Language.ReadLanguage();
    }

    void Draw(Transform t, LineRenderer r)
    {
        int a = r.positionCount++;
        var tmp = t.position;
        r.SetPosition(a, tmp);
    }

    bool SymbolExists(Symbol s)
    {
        foreach (Symbol i in sData.symbols)
        {
            if (i.Compare(s))
                return true;
        }
        return false;
    }

    void BakeSymbolMesh(LineRenderer r, string name)
    {
        Mesh m = new Mesh();
        r.BakeMesh(m, true);
        var g = new GameObject("Mesh");
        g.name = name;
        g.transform.parent = meshParent;
        g.AddComponent<MeshFilter>().mesh = m;
        g.AddComponent<MeshRenderer>();
    }

    void ResetRenderers(LineRenderer r)
    {
        r.Simplify(tolerance);
        Symbol s = new Symbol(r, sData);

        SymbolExists(s);
        BakeSymbolMesh(r, s.Name.value);
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


        if (IndexButton(true))
        {
            for (int i = 0; i < meshParent.childCount; i++)
            {
                Destroy(meshParent.GetChild(i).gameObject);
            }
        }
    }
}
