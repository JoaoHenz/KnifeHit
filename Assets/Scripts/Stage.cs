using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KnifeHit
{
    //sandbox superclass design pattern
    public class Stage : MonoBehaviour
    {
        [Range(5, 20)] public int Knives = 7;
        [Range(0, 359)] public int[] ApplePositions;
        [Range(0, 359)] public int[] GoldenApplePositions;
        [Range(0, 359)] public int[] KnifePositions;
        public Sprite TargetSprite;
        public string Name;

        protected int _currentStage;
        protected Rigidbody2D _rigidBody;
        protected Collider2D _collider;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnValidate()
        {
            GetComponent<SpriteRenderer>().sprite = TargetSprite;
        }

        public void StageStart(int currentStage)
        {
            _currentStage = currentStage;
            gameObject.name = "Target";
            StartCoroutine(TargetBehaviour());
        }

        protected virtual IEnumerator TargetBehaviour()
        {
            yield return new WaitForSeconds(0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.GetComponent<Knife>())
                return;

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.transform.parent = transform;
        }
    }
}

