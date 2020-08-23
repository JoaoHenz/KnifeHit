using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class PoolableObject : MonoBehaviour
    {
        /// <summary>
        /// Called by PoolManager when RePooling
        /// </summary>
        public virtual void OnRePool()
        {

        }
    }
}

