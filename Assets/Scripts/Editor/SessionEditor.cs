using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.TerrainAPI;
using UnityEngine;
using UnityEditor;

namespace KnifeHit
{
    public class SessionEditor : Editor
    {
        private int _appleScore = 0;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();


            _appleScore = EditorGUILayout.IntField("Apple Score: ", _appleScore);

            if (GUILayout.Button("Set Player Prefs"))
            {

            }

            if (GUILayout.Button("Reset Player Prefs"))
            {

            }
        }
    }
}

