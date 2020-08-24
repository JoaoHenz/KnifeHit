using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace KnifeHit.Session
{
    public class SoundManager : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] private AudioClip[] _audioClips;
        #pragma warning restore 0649

        private Dictionary<string, AudioSource> _soundDict = new Dictionary<string, AudioSource>();

        #region singleton
        private static SoundManager _instance;
        private void SingletonAwake()
        {
            if (_instance)
                Destroy(this);
            else
                _instance = this;
        }
        #endregion

        public static void PlaySound(string soundName)
        {
            if (!_instance._soundDict.ContainsKey(soundName))
            {
                Debug.LogWarning("No entry for "+soundName+" in the sound manager!");
                return;
            }

            _instance._soundDict[soundName].Play();
        }

        private void Awake()
        {
            SingletonAwake();
            foreach(AudioClip clip in _audioClips)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();


                audioSource.clip = clip;
                audioSource.playOnAwake = false;
                audioSource.volume = 0.4f;
                

                _soundDict.Add(clip.name, audioSource);
            }
        }


    }
}

