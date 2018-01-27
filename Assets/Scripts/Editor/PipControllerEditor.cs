using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PipController))]
public class PipControllerEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();

        var pip = target as PipController;
        if (GUILayout.Button("Toggle")) {
            pip.SendMessage("Toggle");
        }
    }
}
