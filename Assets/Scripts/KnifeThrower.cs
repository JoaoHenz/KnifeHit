using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class KnifeThrower : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private GameObject _knifePrefab;
        [SerializeField] private Transform _knifeTransform;
        [SerializeField] private Transform _targetTransform;
        #pragma warning restore 0649

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

            _knives--;
            _currentKnife.GetComponent<Knife>().Throw();
            Game.OnThrowKnife();

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

