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

        UIApplesCounter _applesCounterUI;
        [SerializeField] UIKnivesThrown _knivesThrownUI;
        [SerializeField] UIStageKnives _stageKnivesUI;
        [SerializeField] UIStageProgression _stageProgressionUI;

        private int _currentStage = 0;

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
            OnStageStart(1);
        }

        private void OnStageStart(int stageKnives)
        {
            _stageProgressionUI.HandleStageStart();
            _stageKnivesUI.HandleStageStart(stageKnives);
        }
    }
}
