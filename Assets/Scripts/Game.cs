using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class Game : MonoBehaviour
    {
        public PoolManager PoolManager;

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
        }

    }
}
