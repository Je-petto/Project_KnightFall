using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance;

        [Header("Damage Sounds")]
        public AudioClip[] physicalDamageSFX;

        [Header("Action SoundFX")]
        public AudioClip rollSFX;
        public AudioClip backStepSFX;
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

            for (int i = 0; i < physicalDamageSFX.Length; i++)
            {
                if (physicalDamageSFX[i] == null)
                    Debug.LogWarning($"physicalDamageSFX[{i}] is null!");
                else
                    Debug.Log($"physicalDamageSFX[{i}]: {physicalDamageSFX[i].name}");
            }
        }

        public AudioClip ChooseRandomSFXFromArray(AudioClip[] array)
        {
            int index = Random.Range(0, array.Length);


            return array[index];
        }

    }
}
