using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeHit
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private Collider2D _collider;
        private Rigidbody2D _rigidBody;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        public void Throw()
        {
            _rigidBody.velocity = new Vector2(0f,70f);

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Knife>())
                Game.GameOver();
        }

        public void OnCollideTarget()
        {
            _collider.enabled = true;
        }
    }
}

