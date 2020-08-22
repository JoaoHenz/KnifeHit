using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    //sandbox superclass design pattern
    public class DancingTomato : Stage
    {
        protected override IEnumerator TargetBehaviour()
        {
            yield return new WaitForSeconds(0f);
        }
    }
}
