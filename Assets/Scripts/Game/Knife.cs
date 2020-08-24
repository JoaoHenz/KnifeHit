using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit.Game
{
    public class Knife : PoolableObject
    {
        public bool Thrown = false;
        public bool OnTarget = true;

        private Rigidbody2D _rigidBody;

        public override void OnRePool()
        {
            Thrown = false;
            OnTarget = true;
        }

        public void Throw()
        {
            _rigidBody.velocity = new Vector2(0f,50f);
            Thrown = true;
            OnTarget = false;
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
            if (!(collision.gameObject.GetComponent<Knife>() && collision.gameObject.GetComponent<Knife>().Thrown && Thrown))
                return;

            Session.SoundManager.PlaySound("DM-CGS-17");
            GameManager.GameOver();
        }
    }
}

