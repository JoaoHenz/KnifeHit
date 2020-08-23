using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class Apple : MonoBehaviour
    {
        [SerializeField] int _score;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.GetComponent<Knife>())
                return;

            Session.AppleScore += _score;
            Destroy(gameObject);
        }
    }
}

