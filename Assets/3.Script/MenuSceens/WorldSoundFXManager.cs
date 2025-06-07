using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance;

        [Header("Action SoundFX")]
        public AudioClip rollSFX;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
