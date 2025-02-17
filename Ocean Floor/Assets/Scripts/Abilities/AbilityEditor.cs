using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(BaseAbility))]
public class AbilityEditor : Editor //This script allows us to show different properties in the scriptable object editor depending on what ability type we choose
{
    private SerializedProperty abilityNameProp;
    private SerializedProperty iconProp;
    private SerializedProperty costProp;
    private SerializedProperty abilityRangeProp;
    private SerializedProperty abilityMinRangeProp;
    private SerializedProperty canTargetEmptyProp;
    private SerializedProperty targetingTypeProp;
    private SerializedProperty omniDirectionalProp;
    private SerializedProperty isAoEProp;


    private SerializedProperty attackAbilityProp;
    private SerializedProperty movementAbilityProp;
    private SerializedProperty specialAbilityProp;
    private SerializedProperty healAbilityProp;


    private SerializedProperty dmgAmountProp;
    private SerializedProperty targetedAttackProp;
    private SerializedProperty automaticAttackProp;

    private SerializedProperty movementTypeProp;
    private SerializedProperty healAmountProp;

    private void OnEnable()
    {
        // Cache "Always Visible" Fields
        abilityNameProp = serializedObject.FindProperty("abilityName");
        iconProp = serializedObject.FindProperty("icon");
        costProp = serializedObject.FindProperty("cost");
        canTargetEmptyProp = serializedObject.FindProperty("canTargetEmpty");
        abilityRangeProp = serializedObject.FindProperty("abilityRange");
        abilityMinRangeProp = serializedObject.FindProperty("abilityMinRange");
        targetingTypeProp = serializedObject.FindProperty("targetingType");
        omniDirectionalProp = serializedObject.FindProperty("omniDirectional");
        isAoEProp = serializedObject.FindProperty("isAoE");

        // Ability Type
        attackAbilityProp = serializedObject.FindProperty("attackAbility");
        movementAbilityProp = serializedObject.FindProperty("movementAbility");
        specialAbilityProp = serializedObject.FindProperty("specialAbility");
        healAbilityProp = serializedObject.FindProperty("healAbility");
        // Cache the properties
        dmgAmountProp = serializedObject.FindProperty("dmgAmount");
        targetedAttackProp = serializedObject.FindProperty("targetedAttack");
        automaticAttackProp = serializedObject.FindProperty("automaticAttack");
        movementTypeProp = serializedObject.FindProperty("movementType");
        healAmountProp = serializedObject.FindProperty("healAmount");


    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        // Draw "Always Visible" Fields as Read-Only

        if (abilityNameProp == null)
        {
            Debug.LogError("abilityNameProp is null! Check if 'abilityName' exists in BaseAbility.");
        }
        EditorGUILayout.PropertyField(abilityNameProp, new GUIContent("Ability Name"));
        
        EditorGUILayout.PropertyField(iconProp, new GUIContent("Icon"));
        EditorGUILayout.PropertyField(costProp, new GUIContent("Cost"));
        EditorGUILayout.PropertyField(canTargetEmptyProp, new GUIContent("Can Target Empty"));
        EditorGUILayout.PropertyField(abilityRangeProp, new GUIContent("Range"));
      
     
        GUI.enabled = true; // Re-enable editing for the rest

        EditorGUILayout.PropertyField(targetingTypeProp, new GUIContent("Targeting Type"));
        TargetingType targetingType = (TargetingType)targetingTypeProp.enumValueIndex;
        if(targetingType != TargetingType.MovementTargeting)
        {
            EditorGUI.indentLevel += 3;
            EditorGUILayout.PropertyField(isAoEProp, new GUIContent("AoE"));
            EditorGUILayout.PropertyField(omniDirectionalProp, new GUIContent("Omnidirectional"));
            EditorGUI.indentLevel -= 3;
        }
        switch (targetingType)
        {
            case TargetingType.MinimumRangedTargeting:
                EditorGUI.indentLevel += 3;
                EditorGUILayout.PropertyField(abilityRangeProp, new GUIContent("Minimum Range"));
                EditorGUI.indentLevel -= 3;

                break;
        }
       
        // Draw Ability Type
        EditorGUILayout.PropertyField(attackAbilityProp);
        if (attackAbilityProp.boolValue)
        {
            EditorGUI.indentLevel+=3;
            EditorGUILayout.PropertyField(dmgAmountProp, new GUIContent("Damage Amount"));
            EditorGUILayout.PropertyField(targetedAttackProp, new GUIContent("Targeted Attack"));
            EditorGUILayout.PropertyField(automaticAttackProp, new GUIContent("Automatic Attack"));
            EditorGUI.indentLevel-=3;
        }

        EditorGUILayout.PropertyField(movementAbilityProp);
        if (movementAbilityProp.boolValue)
        {
            EditorGUI.indentLevel += 3;
            EditorGUILayout.PropertyField(movementTypeProp, new GUIContent("Movement Type"));
            EditorGUI.indentLevel -= 3;
        }

        EditorGUILayout.PropertyField(specialAbilityProp);
        if (specialAbilityProp.boolValue)
        {
            // Add special properties here if needed
        }

        EditorGUILayout.PropertyField(healAbilityProp);
        if (healAbilityProp.boolValue)
        {
            EditorGUI.indentLevel += 3;
            EditorGUILayout.PropertyField(healAmountProp, new GUIContent("Heal Amount"));
            EditorGUI.indentLevel -= 3;
        }
        

        // Apply modifications
        serializedObject.ApplyModifiedProperties();
    }
}
