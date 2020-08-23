using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class Knife : MonoBehaviour
    {
        public bool Thrown = false;

        private Collider2D _collider;
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        public void Throw()
        {
            _rigidBody.velocity = new Vector2(0f,50f);
            Thrown = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Knife>() && collision.gameObject.GetComponent<Knife>().Thrown && Thrown)
                Game.GameOver();
        }

    }
}

