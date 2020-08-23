using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KnifeHit
{
    /// <summary>
    /// Mediator for the GameScene
    /// </summary>
    public class Game : MonoBehaviour
    {
        public static bool IsPaused = true;
        public StageDatabase StageDatabase;
        public float TargetRadius = 1.6f;

        #pragma warning disable 0649
        [SerializeField] private int _appleScore = 2;
        [SerializeField] private int _goldenAppleScore = 4;
        [SerializeField] private UIKnivesThrown _knivesThrownUI;
        [SerializeField] private UIStageKnives _stageKnivesUI;
        [SerializeField] private UIStageProgression _stageProgressionUI;
        [SerializeField] private KnifeThrower _knifeThrower;
        [SerializeField] private Transform _knifeTransform;
        [SerializeField] private Transform _targetTransform;
        #pragma warning restore 0649

        private int _normalStageIndex = 1;
        private int _bossStageIndex = 0;
        private int _stagesCycle = 5;
        private int _knivesThrown = 0;
        private Stage _currentStage;

        #region singleton
        private static Game _instance;
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
            IsPaused = true;
            Session.GameOver();
        }

        public static void OnThrowKnife()
        {
            _instance._knivesThrown++;
            _instance._knivesThrownUI.SetKnivesCount(_instance._knivesThrown);
            _instance._stageKnivesUI.HandleThrowKnife();
        }

        public static void OnStageEnd()
        {
            if (Session.GameIsOver)
                return;

            IsPaused = true;

            List<Transform> children = new List<Transform>();
            foreach(Transform child in _instance._currentStage.transform.Find("Player Knives"))
                children.Add(child);

            foreach(Transform child in children)
                PoolManager.RePool(child);

            Destroy(_instance._currentStage.gameObject);
            _instance.PlayNextStage();
        }

        private void Awake()
        {
            SingletonAwake();
        }

        private void Start()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
            PlayNextStage();
        }

        private void PlayNextStage()
        {
            int currentStageIndex = _normalStageIndex + _bossStageIndex;
            GameObject stage;

            if (currentStageIndex % _stagesCycle > 0) //normal stage
            {
                stage = StageDatabase.GetStage(_normalStageIndex);
                OnStageStart(stage.GetComponent<Stage>(), currentStageIndex % _stagesCycle, currentStageIndex);
                CreateStage(stage, currentStageIndex);
                if(currentStageIndex % _stagesCycle == _stagesCycle-1)
                    _bossStageIndex++;
                else
                    _normalStageIndex++;
            }
            else //boss
            {
                stage = StageDatabase.GetBossStage(_bossStageIndex);
                OnStageStart(stage.GetComponent<Stage>(), _stagesCycle, currentStageIndex);
                CreateStage(stage, currentStageIndex);
                _normalStageIndex++;
            }

            IsPaused = false;
        }

        private void OnStageStart(Stage stage,int stageCount, int currentStageIndex)
        {
            _stageProgressionUI.HandleStageStart(stageCount, currentStageIndex, stage.Name);
            _stageKnivesUI.HandleStageStart(stage.Knives);
            _knifeThrower.HandleStageStart(stage.Knives);
        }

        private void CreateStage(GameObject stage,int currentStageIndex)
        {
            GameObject instance = Instantiate(stage);
            _currentStage = instance.GetComponent<Stage>();
            _currentStage.StageStart(currentStageIndex);
            instance.transform.position = _targetTransform.position;
        }
        
    }
}
