using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class Game : MonoBehaviour
    {
        public PoolManager PoolManager;
        public StageDatabase StageDatabase;
        public bool GameIsPaused;
        [NonSerialized] public int StagesCycle;

        [SerializeField] UIKnivesThrown _knivesThrownUI;
        [SerializeField] UIStageKnives _stageKnivesUI;
        [SerializeField] UIStageProgression _stageProgressionUI;
        [SerializeField] int _appleScore = 2;
        [SerializeField] int _goldenAppleScore = 4;

        private UIApplesCounter _applesCounterUI;
        private int _normalStage = 0;
        private int _bossStage = 0;

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
            PoolManager.Awake();

            _applesCounterUI = GameObject.Find("Apples Counter").GetComponent<UIApplesCounter>();
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

            if (currentStage % StagesCycle > 0) //normal stage
            {
                OnStageStart(StageDatabase.GetStage(_normalStage).GetComponent<Stage>(), currentStage % StagesCycle, currentStage);
                _normalStage++;
            }
            else //boss
            {
                OnStageStart(StageDatabase.GetBossStage(_bossStage).GetComponent<Stage>(), StagesCycle, currentStage);
                _bossStage++;
            }
        }

        private void OnStageStart(Stage stage,int stageCount, int currentStage)
        {
            _stageProgressionUI.HandleStageStart(stageCount,currentStage,stage.Name);
            _stageKnivesUI.HandleStageStart(stage.Knives);
        }
    }
}
