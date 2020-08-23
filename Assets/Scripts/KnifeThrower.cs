using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class KnifeThrower : MonoBehaviour
    {
        [SerializeField] GameObject _knifePrefab;
        [SerializeField] Transform _knifeTransform;

        private int _knives = 0;
        private Knife _currentKnife;

        public void HandleStageStart(int knives)
        {
            _knives = knives;

            CreateKnife();
        }

        public void ThrowKnife()
        {
            if (_knives == 0 || Game.IsPaused)
                return;

            _currentKnife.GetComponent<Knife>().Throw();

            CreateKnife();
        }

        public void CreateKnife()
        {
            GameObject knifeObject = PoolManager.Instantiate("Knife");
            knifeObject.transform.parent = transform;
            knifeObject.transform.position = _knifeTransform.position;

            _currentKnife = knifeObject.GetComponent<Knife>();
        }
    }
}

