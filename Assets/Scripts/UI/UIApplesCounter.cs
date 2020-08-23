using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit
{
    public class UIApplesCounter : MonoBehaviour
    {
        [SerializeField] Text _applesText;

        private void Awake()
        {
            if(PlayerPrefs.HasKey("Apple Score"))
                SetApplesCount(PlayerPrefs.GetInt("Apple Score"));
        }

        public void SetApplesCount(int applesCount)
        {
            _applesText.text = applesCount.ToString();
        }
    }
}
