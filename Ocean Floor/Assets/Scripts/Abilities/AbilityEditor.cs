using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BaseAbility))]
public class AbilityEditor : Editor
{
    private SerializedProperty abilityNameProp;
    private SerializedProperty iconProp;
    private SerializedProperty costProp;
    private SerializedProperty abilityTypeProp;

    private SerializedProperty dmgAmountProp;
    private SerializedProperty dmgRangeProp;
    private SerializedProperty movementTypeProp;
    private SerializedProperty selfHealProp;
    private SerializedProperty healAmountProp;
    private SerializedProperty healRangeProp;

    private void OnEnable()
    {
        // Cache "Always Visible" Fields
        abilityNameProp = serializedObject.FindProperty("abilityName");
        iconProp = serializedObject.FindProperty("icon");
        costProp = serializedObject.FindProperty("cost");

        // Ability Type
        abilityTypeProp = serializedObject.FindProperty("abilityType");

        // Cache the properties
        dmgAmountProp = serializedObject.FindProperty("dmgAmount");
        dmgRangeProp = serializedObject.FindProperty("dmgRange");
        movementTypeProp = serializedObject.FindProperty("movementType");
        selfHealProp = serializedObject.FindProperty("selfHeal");
        healAmountProp = serializedObject.FindProperty("healAmount");
        healRangeProp = serializedObject.FindProperty("healRange");

    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw "Always Visible" Fields as Read-Only
       
        EditorGUILayout.PropertyField(abilityNameProp, new GUIContent("Ability Name"));
        EditorGUILayout.PropertyField(iconProp, new GUIContent("Icon"));
        EditorGUILayout.PropertyField(costProp, new GUIContent("Cost"));
        GUI.enabled = true; // Re-enable editing for the rest

        // Draw Ability Type
        EditorGUILayout.PropertyField(abilityTypeProp);

        // Conditional Fields Based on Ability Type
        AbilityType abilityType = (AbilityType)abilityTypeProp.enumValueIndex;
        switch (abilityType)
        {
            case AbilityType.Attack:
                EditorGUILayout.PropertyField(dmgAmountProp, new GUIContent("Damage Amount"));
                EditorGUILayout.PropertyField(dmgRangeProp, new GUIContent("Damage Range"));
                break;

            case AbilityType.Movement:
                EditorGUILayout.PropertyField(movementTypeProp, new GUIContent("Movement Type"));
                break;

            case AbilityType.Special:
                // Add special properties here if needed
                break;

            case AbilityType.Heal:
                EditorGUILayout.PropertyField(selfHealProp, new GUIContent("Self Heal"));
                EditorGUILayout.PropertyField(healAmountProp, new GUIContent("Heal Amount"));
                if (!selfHealProp.boolValue)
                {
                    EditorGUILayout.PropertyField(healRangeProp, new GUIContent("Heal Range"));
                }
                break;
        }

        // Apply modifications
        serializedObject.ApplyModifiedProperties();
    }
}
