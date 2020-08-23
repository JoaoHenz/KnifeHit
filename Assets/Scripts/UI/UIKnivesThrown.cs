using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit
{
    public class UIKnivesThrown : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private Text _knivesText;
        #pragma warning restore 0649

        private void Awake()
        {
            SetKnivesCount(0);
        }

        public void SetKnivesCount(int knivesCount)
        {
            _knivesText.text = knivesCount.ToString();
        }
    }
}
