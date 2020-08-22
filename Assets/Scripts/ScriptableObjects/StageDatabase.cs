﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    [CreateAssetMenu(fileName = "New Stage Database", menuName = "Databases/Stage Database")]
    public class StageDatabase : ScriptableObject
    {
        public GameObject[] Stages;
        public GameObject[] BossStages;

        public GameObject GetStage(int index)
        {
            if (index > Stages.Length)
                return Stages[Random.Range(0, Stages.Length)];
            else
                return Stages[index];
        }

        public GameObject GetBossStage(int index)
        {
            if (index > BossStages.Length)
                return BossStages[Random.Range(0, BossStages.Length)];
            else
                return BossStages[index];
        }
    }
}