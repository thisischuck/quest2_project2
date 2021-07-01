using UnityEditor;
using static UnityEditor.EditorGUILayout;

[CustomEditor(typeof(EnemyObject), true)]
public class EnemyObjectEditor : CharacterObjectEditor
{
    SerializedProperty BaseDamage;
    SerializedProperty SpeedModifier;
    SerializedProperty Type;
    public override void OnEnable()
    {
        base.OnEnable();
        BaseDamage = FindProperty("BaseDamage");
        SpeedModifier = FindProperty("SpeedModifier");
        Type = FindProperty("Type");
    }

    public void BaseDamageGUI()
    {
        PropertyField(BaseDamage);
    }
    public void SpeedModifierGUI()
    {
        PropertyField(SpeedModifier);
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        PrefabGui();

        PropertyField(Type);

        #region Base
        MakeLabel("Base");
        HpGUI();
        #endregion

        #region Movement
        MakeLabel("Movement");
        SpeedGUI(); SpeedModifierGUI();
        #endregion

        #region Combat
        MakeLabel("Combat");
        BaseDamageGUI();
        DefenseGUI();
        #endregion

        Separator();
        serializedObject.ApplyModifiedProperties();
    }
}