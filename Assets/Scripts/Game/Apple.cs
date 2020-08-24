using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.Game
{
    public class Apple : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] int _score;
        #pragma warning restore 0649

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.GetComponent<Knife>())
                return;
            Session.SoundManager.PlaySound("DM-CGS-19");

            Session.SessionManager.AppleScore += _score;
            Destroy(gameObject);
        }
    }
}

