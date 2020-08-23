using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace KnifeHit
{
    [CustomEditor(typeof(Session))]
    public class SessionEditor : Editor
    {
        private int _appleScore;

        private void OnEnable()
        {
            InitializePlayerPrefs();
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            GUILayout.Space(20);
            EditorGUILayout.LabelField("Edit Player Prefs", EditorStyles.boldLabel);
            EditorGUI.indentLevel = (EditorGUI.indentLevel + 1);


            _appleScore = EditorGUILayout.IntField("Apple Score: ", _appleScore);

            if (GUILayout.Button("Set Player Prefs"))
            {
                PlayerPrefs.SetInt("Apple Score",_appleScore);
            }

            if (GUILayout.Button("Reset Player Prefs"))
            {
                PlayerPrefs.SetInt("Apple Score", 0);
                _appleScore = 0;
            }
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

    public class Session : MonoBehaviour
    {
        [SerializeField] private UIApplesCounter _applesCounterUI;
        [SerializeField] private GameObject _gameOverScreen;

        private int _appleScore;
        private Coroutine _gameOverCoroutine;

        static public int AppleScore
        {
            get { return _instance._appleScore; }
            set
            {
                _instance._applesCounterUI.SetApplesCount(value);
                _instance._appleScore = value;
            }
        }

        #region singleton
        private static Session _instance;
        private void SingletonAwake()
        {
            if (_instance)
                Destroy(this);
            else
                _instance = this;
        }
        #endregion

        private void Awake()
        {
            SingletonAwake();

            if(PlayerPrefs.HasKey("Apple Score"))
                AppleScore = PlayerPrefs.GetInt("Apple Score");
        }

        public static void GameOver()
        {
            if(_instance._gameOverCoroutine == null)
                _instance._gameOverCoroutine = _instance.StartCoroutine(GameOverSequence());
        }

        private static IEnumerator GameOverSequence()
        {
            SceneManager.UnloadSceneAsync("GameScene");
            _instance._gameOverScreen.SetActive(true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadSceneAsync("GameScene",LoadSceneMode.Additive);
            while(SceneManager.sceneCount<2)
                yield return new WaitForSeconds(0.25f);
            _instance._gameOverScreen.SetActive(false);

            _instance._gameOverCoroutine = null;
        }

    }
}
