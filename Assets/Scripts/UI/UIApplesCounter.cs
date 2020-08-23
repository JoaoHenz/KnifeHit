using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit
{
    public class UIApplesCounter : MonoBehaviour
    {
        [SerializeField] Text _applesText;

        public void SetApplesCount(int applesCount)
        {
            _applesText.text = applesCount.ToString();
        }
    }
}
