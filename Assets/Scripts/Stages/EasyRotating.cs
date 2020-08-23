using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    //sandbox superclass design pattern
    public class EasyRotating : Stage
    {
        protected override IEnumerator TargetBehaviour()
        {
            _rigidBody.angularDrag = 0;
            float direction = _currentStage % 2 == 0 ? 1 : -1;
            _rigidBody.angularVelocity = direction * (150 + _currentStage*10);
            yield return new WaitForSeconds(0f);
        }
    }
}
