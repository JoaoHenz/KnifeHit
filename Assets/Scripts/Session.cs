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

    /// <summary>
    /// Mediator for the SessionScene
    /// </summary>
    public class Session : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private UIApplesCounter _applesCounterUI;
        [SerializeField] private GameObject _gameOverScreen;
        #pragma warning restore 0649

        private int _appleScore;
        private Coroutine _gameOverCoroutine;

        static public int AppleScore
        {
            get { return _instance._appleScore; }
            set
            {
                _instance._applesCounterUI.SetApplesCount(value);
                PlayerPrefs.SetInt("Apple Score", value);
                _instance._appleScore = value;
            }
        }

        static public bool GameIsOver { get {return (_instance._gameOverCoroutine != null); } }

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

        public static void GameOver()
        {
            if (_instance._gameOverCoroutine == null)
                _instance._gameOverCoroutine = _instance.StartCoroutine(GameOverSequence());
        }

        private static IEnumerator GameOverSequence()
        {
            SceneManager.UnloadSceneAsync("GameScene");
            _instance._gameOverScreen.SetActive(true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
            while (SceneManager.sceneCount < 2)
                yield return new WaitForSeconds(0.25f);
            _instance._gameOverScreen.SetActive(false);

            _instance._gameOverCoroutine = null;
        }

        private void Awake()
        {
            SingletonAwake();

            if(PlayerPrefs.HasKey("Apple Score"))
                AppleScore = PlayerPrefs.GetInt("Apple Score");
        }
    }
}
