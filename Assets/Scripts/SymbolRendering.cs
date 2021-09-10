using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SymbolRendering : MonoBehaviour
{
    public SymbolObject sData;
    public Transform meshParent;
    public Material meshMaterial;

    public LineRenderer LeftR;
    public LineRenderer RightR;

    public GameObject LeftC;
    public GameObject RightC;

    public bool symbolDraw;
    public bool symbolSave;
    public bool rotateSymbol;


    [Range(0.01f, 0.1f)]
    public float tolerance;

    public GameObject spell;
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

        //sData.Language.ReadLanguage();
    }

    void Draw(Transform t, LineRenderer r)
    {
        int a = r.positionCount++;
        var tmp = t.position;
        // tmp.z = 0;
        r.SetPosition(a, tmp);
    }

    bool SymbolExists(Symbol s)
    {
        foreach (Symbol i in sData.symbols)
        {
            float f = i.Compare(s);
            HelperFunctions.MyDebug(f.ToString());
            if (f > sData.SuccessPercentage)
                return true;
        }
        return false;
    }

    void BakeSymbolMesh(LineRenderer r, string name, bool t)
    {
        Mesh m = new Mesh();
        if (t)
        {
            r.positionCount = 0;
            foreach (var c in name)
            {
                int a = r.positionCount++;
                if (sData.Language.Language.ContainsValue(c))
                {
                    r.SetPosition(a, sData.Language.LanguageRev[c]);
                }
            }
        }

        r.BakeMesh(m, true);
        var g = new GameObject(name);
        g.transform.parent = meshParent;
        g.AddComponent<MeshFilter>().mesh = m;
        var s = g.AddComponent<MeshRenderer>();
        s.material = meshMaterial;
    }

    void ResetRenderers(LineRenderer r)
    {
        r.Simplify(tolerance);
        Symbol s = new Symbol(r, sData);

        if (symbolSave)
        {
            sData.symbols.Add(s);
            EditorUtility.SetDirty(sData);
        }
        else if (SymbolExists(s))
        {
            SpawnSpell(r);
        }

        BakeSymbolMesh(r, s.Name.value + " WithoutParser", false);
        BakeSymbolMesh(r, s.Name.value, true);
        r.positionCount = 0;
    }

    void SpawnSpell(LineRenderer r)
    {
        Instantiate(spell, r.transform.position, r.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        HelperFunctions.UpdateOVR();

        if (symbolDraw)
        {
            Vector2 ip = HelperFunctions.TrackControllerInput();
            if (ip.x > 0.5)
                Draw(LeftC.transform, LeftR);
            else if (LeftR.positionCount > 0 && LeftR.positionCount != 0)
                ResetRenderers(LeftR);
            if (ip.y > 0.5)
                Draw(RightC.transform, RightR);
            else if (RightR.positionCount > 0 && RightR.positionCount != 0)
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
