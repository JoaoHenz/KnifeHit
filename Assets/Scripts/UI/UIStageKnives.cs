using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit
{
    public class UIStageKnives : MonoBehaviour
    {
        [SerializeField] private GameObject _knifeIcon;
        [SerializeField] private Sprite _fullKnifeIcon;
        [SerializeField] private Sprite _emptyKnifeIcon;

        private float _nextKnifeVertDist = 7354f;
        private int _knifeIndex = 0;
        private List<GameObject> _iconList = new List<GameObject>();

        public void HandleStageStart(int stageKnives)
        {
            if (_iconList != null)
            {
                foreach (GameObject icon in _iconList)
                {
                    Destroy(icon);
                }
            }

            int i;
            for(i = 0; i < stageKnives; i++)
            {
                GameObject knifeIcon = Instantiate(_knifeIcon,transform);
                knifeIcon.transform.position = new Vector3(0,i*_nextKnifeVertDist,0);

                _iconList.Add(knifeIcon);
            }
            _knifeIndex = i;
        }

        public void HandleThrowKnife()
        {
            _iconList[_knifeIndex].GetComponent<Image>().sprite = _emptyKnifeIcon;
            _knifeIndex--;
        }
    }
}

