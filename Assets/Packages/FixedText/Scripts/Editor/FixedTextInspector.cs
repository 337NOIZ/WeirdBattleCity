
using UnityEditor;

using UnityEditor.UI;

[CustomEditor(typeof(FixedText))]

public class FixedTextInspector : TextEditor
{
    private SerializedProperty wordwrap = null;

    protected override void OnEnable()
    {
        base.OnEnable();

        wordwrap = serializedObject.FindProperty("wordwrap");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.PropertyField(wordwrap);

        serializedObject.ApplyModifiedProperties();
    }
}
