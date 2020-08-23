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

        protected Rigidbody2D _rigidBody;

        private void OnValidate()
        {
            GetComponent<SpriteRenderer>().sprite = TargetSprite;
        }

        private void Start()
        {
            StartCoroutine(TargetBehaviour());
        }

        protected virtual IEnumerator TargetBehaviour()
        {
            yield return new WaitForSeconds(0f);
        }
    }
}

