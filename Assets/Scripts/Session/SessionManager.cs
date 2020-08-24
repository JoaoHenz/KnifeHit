using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace KnifeHit.Session
{
    /// <summary>
    /// Mediator for the SessionScene
    /// </summary>
    public class SessionManager : MonoBehaviour
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
        private static SessionManager _instance;
        private void SingletonAwake()
        {
            if (_instance)
                Destroy(this);
            else
                _instance = this;
        }
        #endregion

        public static void EndGame()
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

            if (SceneManager.sceneCount < 2)
                SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        }
    }
}
