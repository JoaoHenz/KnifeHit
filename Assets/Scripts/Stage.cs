using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KnifeHit
{
    //sandbox superclass design pattern
    public class Stage : MonoBehaviour
    {
        [Range(0, 359)] public int[] ApplePosition;
        [Range(0, 359)] public int[] KnifePosition;
        public Sprite TargetSprite;

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

