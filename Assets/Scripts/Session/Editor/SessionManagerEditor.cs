using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KnifeHit.Session
{
    [CustomEditor(typeof(SessionManager))]
    public class SessionManagerEditor : Editor
    {
        private int _appleScore;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(20);
            EditorGUILayout.LabelField("Edit Player Prefs", EditorStyles.boldLabel);
            EditorGUI.indentLevel += 1;

            _appleScore = EditorGUILayout.IntField("Apple Score: ", _appleScore);

            if (GUILayout.Button("Set Player Prefs"))
            {
                PlayerPrefs.SetInt("Apple Score", _appleScore);
            }

            if (GUILayout.Button("Reset Player Prefs"))
            {
                PlayerPrefs.SetInt("Apple Score", 0);
                _appleScore = 0;
            }
        }

        private void OnEnable()
        {
            InitializePlayerPrefs();
        }

        private void InitializePlayerPrefs()
        {
            if (PlayerPrefs.HasKey("Apple Score"))
                _appleScore = PlayerPrefs.GetInt("Apple Score");
            else
            {
                PlayerPrefs.SetInt("Apple Score", 0);
                _appleScore = PlayerPrefs.GetInt("Apple Score");
            }
        }
    }
}