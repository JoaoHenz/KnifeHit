using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    /// <summary>
    /// Sandbox subclass of Stage
    /// </summary>
    public class EasyRotating : Stage
    {
        /// <summary>
         /// Sandbox method where stage defines the target behaviour
         /// </summary>
         /// <returns></returns>
        protected override IEnumerator TargetBehaviour()
        {
            _rigidBody.angularDrag = 0;
            float direction = _currentStage % 2 == 0 ? 1 : -1;
            _rigidBody.angularVelocity = direction * (150 + _currentStage*10);
            yield return new WaitForSeconds(0f);
        }
    }
}
