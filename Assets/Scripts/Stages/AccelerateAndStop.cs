using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace KnifeHit.Game
{
    /// <summary>
    /// Sandbox subclass of Stage
    /// </summary>
    public class AccelerateAndStop : Stage
    {
        /// <summary>
        /// Sandbox method where stage defines the target behaviour
        /// </summary>
        /// <returns></returns>
        protected override IEnumerator TargetBehaviour()
        {
            float direction = _currentStage % 2 == 0? 1 : -1;
            _rigidBody.angularDrag = 2;

            while (true)
            {
                _rigidBody.AddTorque(direction*(1 + (_currentStage / 15))*1500);
                yield return new WaitForSeconds(1f);
                
                while (math.abs(_rigidBody.angularVelocity) > 20f)
                {
                    yield return new WaitForSeconds(0.25f);
                }
                yield return new WaitForSeconds(0.5f);
            }
            
        }
    }
}
