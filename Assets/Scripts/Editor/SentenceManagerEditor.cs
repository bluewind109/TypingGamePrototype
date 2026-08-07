using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SentenceManager))]
public class SentenceManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SentenceManager sentenceManager = (SentenceManager)target;

        if (GUILayout.Button("Generate New Sentence"))
        {
            
        }
    }
}
