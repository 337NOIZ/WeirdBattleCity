
using UnityEditor;

using UnityEditor.UI;

[CustomEditor(typeof(FixedText))]

public class FixedTextInspector : TextEditor
{
    private SerializedProperty disableWordWrap = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        disableWordWrap = serializedObject.FindProperty("disableWordWrap");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(disableWordWrap);

        serializedObject.ApplyModifiedProperties();
    }

}
