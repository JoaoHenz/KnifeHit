using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit
{
    public class UIKnivesThrown : MonoBehaviour
    {
        [SerializeField] Text _knivesText;

        public void SetKnivesCount(int knivesCount)
        {
            _knivesText.text = knivesCount.ToString();
        }
    }
}
