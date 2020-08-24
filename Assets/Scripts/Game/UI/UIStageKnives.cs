using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit.Game
{
    public class UIStageKnives : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private GameObject _knifeIcon;
        [SerializeField] private Sprite _fullKnifeIcon;
        [SerializeField] private Sprite _emptyKnifeIcon;
        #pragma warning restore 0649

        private readonly float _nextKnifeVertDist = 60;
        private int _knifeIndex = 0;
        private List<GameObject> _iconList;

        public void HandleStageStart(int stageKnives)
        {
            if (_iconList != null)
            {
                foreach (GameObject icon in _iconList)
                {
                    Destroy(icon);
                }
            }
            _iconList = new List<GameObject>();

            int i;
            for(i = 0; i < stageKnives; i++)
            {
                GameObject knifeIcon = Instantiate(_knifeIcon,transform);
                knifeIcon.transform.localPosition = new Vector3(0,i*_nextKnifeVertDist,0);

                _iconList.Add(knifeIcon);
            }
            _knifeIndex = i-1;
        }

        public void HandleThrowKnife()
        {
            _iconList[_knifeIndex].GetComponent<Image>().sprite = _emptyKnifeIcon;
            _knifeIndex--;
        }
    }
}

