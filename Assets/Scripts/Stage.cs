using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace KnifeHit
{
    /// <summary>
    /// Sandbox superclass. Defines target behaviour and other stage variables
    /// </summary>
    public class Stage : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private bool _testItems; //acting as a button
        #pragma warning restore 0649

        [Range(5, 20)] public int Knives = 7;
        [Range(0, 19)] public int[] ApplePositions;
        [Range(0, 19)] public int[] GoldenApplePositions;
        [Range(0, 19)] public int[] KnifePositions;
        public Sprite TargetSprite;
        public string Name;

        protected int _currentStage;
        protected Rigidbody2D _rigidBody;
        protected Collider2D _collider;

        #pragma warning disable 0649
        [SerializeField] private GameObject _applePrefab;
        [SerializeField] private GameObject _goldenApplePrefab;
        [SerializeField] private GameObject _knifePrefab;
        #pragma warning restore 0649

        private GameObject _playerKnives;
        private float _appleDistance = 2.65f;
        private float _knifeDistance = 2.4f;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
        }

        private void OnValidate()
        {
            GetComponent<SpriteRenderer>().sprite = TargetSprite;
            if (_testItems)
            {
                _testItems = false;
                CreateItems();
            }
                
        }

        public void StageStart(int currentStage)
        {
            _currentStage = currentStage;
            gameObject.name = "Target";
            _playerKnives = new GameObject();
            _playerKnives.transform.parent = transform;
            _playerKnives.name = "Player Knives";
            CreateItems();
            StartCoroutine(TargetBehaviour());
        }

        public void CreateItems()
        {
            GameObject items = new GameObject();
            items.transform.parent = transform;
            items.name = "Items";
            items.transform.position = new Vector3(0f, 0f, 5f);

            GameObject item;

            foreach (int applePos in ApplePositions)
            {
                item = Instantiate(_applePrefab);
                item.transform.parent = items.transform;
                float ang = 360 / 20 * applePos;
                Vector3 pos;
                pos.x = transform.position.x + _appleDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
                pos.y = transform.position.y + _appleDistance * Mathf.Cos(ang * Mathf.Deg2Rad);
                pos.z = transform.position.z + 5;
                item.transform.position = pos;
                item.transform.eulerAngles = new Vector3(0f, 0f, -ang);

                item.name = "Apple";
            }
            foreach (int goldenApplePos in GoldenApplePositions)
            {
                item = Instantiate(_goldenApplePrefab);
                item.transform.parent = items.transform;
                float ang = 360 / 20 * goldenApplePos;
                Vector3 pos;
                pos.x = transform.position.x + _appleDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
                pos.y = transform.position.y + _appleDistance * Mathf.Cos(ang * Mathf.Deg2Rad);
                pos.z = transform.position.z + 5;
                item.transform.position = pos;
                item.transform.eulerAngles = new Vector3(0f, 0f, -ang);

                item.name = "Golden Apple";
            }
            foreach (int KnifePos in KnifePositions)
            {
                item = Instantiate(_knifePrefab);
                item.transform.parent = items.transform;
                float ang = 360 / 20 * KnifePos;
                Vector3 pos;
                pos.x = transform.position.x + _knifeDistance * Mathf.Sin(ang * Mathf.Deg2Rad);
                pos.y = transform.position.y + _knifeDistance * Mathf.Cos(ang * Mathf.Deg2Rad);
                pos.z = transform.position.z + 5;
                item.transform.position = pos;
                item.transform.eulerAngles = new Vector3(0f, 0f, -ang+180);

                item.name = "Knife";
            }
        }

        /// <summary>
        /// Sandbox method
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerator TargetBehaviour()
        {
            yield return new WaitForSeconds(0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.GetComponent<Knife>() || collision.gameObject.GetComponent<Knife>().OnTarget)
                return;

            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            collision.transform.parent = _playerKnives.transform;

            collision.gameObject.GetComponent<Knife>().OnTarget = true;

            if (_playerKnives.transform.childCount == Knives)
                Game.OnStageEnd();
        }
    }
}

