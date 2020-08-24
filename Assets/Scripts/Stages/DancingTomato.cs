using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.Game
{
    /// <summary>
    /// Sandbox subclass of Stage
    /// </summary>
    public class DancingTomato : Stage
    {
        /// <summary>
        /// Sandbox method where stage defines the target behaviour
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator TargetBehaviour()
        {
            float direction = _currentStage % 2 == 0 ? 1 : -1;
            _rigidBody.angularDrag = 0;

            while (true)
            {
                _rigidBody.angularVelocity = direction * (150 + _currentStage * 10);
                yield return new WaitForSeconds(2f);

                _rigidBody.angularVelocity = -direction * (500 + _currentStage * 10);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
