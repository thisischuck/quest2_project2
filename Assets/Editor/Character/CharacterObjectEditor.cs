using UnityEngine;
using UnityEditor;
using static UnityEditor.EditorGUILayout;

[CustomEditor(typeof(CharacterObject))]
public class CharacterObjectEditor : Editor
{
    SerializedProperty Defense;
    SerializedProperty Speed;
    SerializedProperty Hp;
    SerializedProperty Layers;
    SerializedProperty Knockback;


    SerializedProperty Prefab;
    Editor temp_editor;
    public Texture2D back;
    GUIStyle s;

    public virtual void OnEnable()
    {
        s = new GUIStyle();
        s.normal.background = back;
        Defense = FindProperty("Defense");
        Prefab = FindProperty("Prefab");
        Hp = FindProperty("Hp");
        Speed = FindProperty("Speed");
        Layers = FindProperty("Layers");
        Knockback = FindProperty("Knockback");
    }

    public override bool HasPreviewGUI() { return true; }

    public override void OnInteractivePreviewGUI(Rect r, GUIStyle background)
    {
        if (Prefab.objectReferenceValue)
        {
            if (temp_editor == null)
                temp_editor = CreateEditor(Prefab.objectReferenceValue);
            temp_editor.OnInteractivePreviewGUI(r, s);
        }
    }

    public SerializedProperty FindProperty(string name)
    {
        return serializedObject.FindProperty(name);
    }
    public void MakeLabel(string name)
    {
        Separator();
        LabelField(name, EditorStyles.boldLabel);
    }

    public void DefenseGUI()
    {
        PropertyField(Defense);
    }
    public void KnockbackGUI()
    {
        PropertyField(Knockback);
    }
    public void SpeedGUI()
    {
        PropertyField(Speed);
    }
    public void HpGUI()
    {
        PropertyField(Hp);
    }
    public void LayersGUI()
    {
        PropertyField(Layers);
    }
    public void PrefabGui()
    {
        EditorGUI.BeginChangeCheck();
        ObjectField(Prefab, typeof(GameObject));
        if (EditorGUI.EndChangeCheck())
        {
            temp_editor = CreateEditor(Prefab.objectReferenceValue);
        }
    }
}