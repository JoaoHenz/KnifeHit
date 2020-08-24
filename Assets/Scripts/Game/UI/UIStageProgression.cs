using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeHit.Game
{
    public class UIStageProgression : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Image[] _stageDots;
        [SerializeField] Image _boss;
        [SerializeField] Text _title;
        [SerializeField] Sprite _emptyStage;
        [SerializeField] Sprite _fullStage;
        [SerializeField] Sprite _emptyBoss;
        [SerializeField] Sprite _fullBoss;
        #pragma warning restore 0649

        public void HandleStageStart(int stageCount, int currentStage, string bossName)
        {
            foreach(Image stageDot in _stageDots)
            {
                if (stageCount > 0)
                {
                    stageDot.sprite = _fullStage;
                    stageCount--;
                }
                else
                    stageDot.sprite = _emptyStage;
            }
            if (stageCount > 0)
            {
                _boss.sprite = _fullBoss;
                _title.text = "BOSS: "+bossName;
            }
            else
            {
                _boss.sprite = _emptyBoss;
                _title.text = "STAGE " + currentStage.ToString();
            }
        }
    }
}

