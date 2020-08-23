using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace KnifeHit
{
    public class Game : MonoBehaviour
    {
        public static bool IsPaused = true;
        public StageDatabase StageDatabase;

        [SerializeField] UIKnivesThrown _knivesThrownUI;
        [SerializeField] UIStageKnives _stageKnivesUI;
        [SerializeField] UIStageProgression _stageProgressionUI;
        [SerializeField] KnifeThrower _knifeThrower;
        [SerializeField] int _appleScore = 2;
        [SerializeField] int _goldenAppleScore = 4;
        [SerializeField] GameObject _stagePrefab;
        [SerializeField] Transform _knifeTransform;
        [SerializeField] Transform _stageTransform;

        private int _normalStage = 0;
        private int _bossStage = 0;
        private int _stagesCycle = 5;

        #region singleton
        private static Game _instance;
        private void SingletonAwake()
        {
            if (_instance)
                Destroy(this);
            else
                _instance = this;

            DontDestroyOnLoad(this);
        }
        #endregion

        private void Awake()
        {
            SingletonAwake();
        }

        private void Start()
        {
            _normalStage = 1;
            _bossStage = 1;
            PlayNextStage();
        }

        private void PlayNextStage()
        {
            int currentStage = _normalStage + _bossStage;
            Stage stage;

            if (currentStage % _stagesCycle > 0) //normal stage
            {
                stage = StageDatabase.GetStage(_normalStage).GetComponent<Stage>();
                OnStageStart(stage, currentStage % _stagesCycle, currentStage);
                _normalStage++;
            }
            else //boss
            {
                stage = StageDatabase.GetBossStage(_bossStage).GetComponent<Stage>();
                OnStageStart(stage, _stagesCycle, currentStage);
                _bossStage++;
            }

            Game.IsPaused = false;
        }

        private void OnStageStart(Stage stage,int stageCount, int currentStage)
        {
            _stageProgressionUI.HandleStageStart(stageCount,currentStage,stage.Name);
            _stageKnivesUI.HandleStageStart(stage.Knives);
            _knifeThrower.HandleStageStart(stage.Knives);
        }

        private void CreateStage(Stage stage,int currentStage)
        {
            GameObject instance = Instantiate(_stagePrefab);
            stage = instance.AddComponent(stage.GetType()) as Stage;
            stage.StageStart(currentStage);
        }
        public static void GameOver()
        {
            IsPaused = true;
            Session.GameOver();
        }
    }
}
