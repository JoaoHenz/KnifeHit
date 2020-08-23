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
            float direction = _currentStage % 2 == 0 ? 1 : -1;

            while (true)
            {
                _rigidBody.angularVelocity = direction * (1 + (_currentStage / 15));

                yield return new WaitForSeconds(2f + 2f / (_currentStage / 15));

                _rigidBody.velocity = Vector2.zero;
                _rigidBody.angularVelocity = 0f;

                _rigidBody.angularVelocity = direction *(-1)* (1 + (_currentStage / 15));

                yield return new WaitForSeconds(0.5f);

            }
        }
    }
}
