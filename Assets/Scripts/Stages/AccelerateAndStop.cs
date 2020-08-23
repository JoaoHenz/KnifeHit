using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    //sandbox superclass design pattern
    public class AccelerateAndStop : Stage
    {
        protected override IEnumerator TargetBehaviour()
        {
            float direction = _currentStage % 2 == 0? 1 : -1;

            while (true)
            {
                _rigidBody.AddTorque(direction*(1 + (_currentStage / 15)));

                while(_rigidBody.angularVelocity < 0.1f)
                {
                
                }
                yield return new WaitForSeconds(2f / (_currentStage / 15));
            }
            
        }
    }
}
