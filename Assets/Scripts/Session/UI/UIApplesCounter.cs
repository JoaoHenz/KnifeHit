using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit
{
    public class UIApplesCounter : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private Text _applesText;
        #pragma warning restore 0649

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
