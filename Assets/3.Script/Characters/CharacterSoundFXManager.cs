using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource audioSource;

        protected virtual void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                Debug.LogError($"{name}: AudioSource is missing on {nameof(CharacterSoundFXManager)}.");
            }
        }

        public void PlaySoundFX(AudioClip soundFx, float volume = 1, bool randomizePitch = true, float pitchRandom = 0.1f)
        {
            if (audioSource == null)
            {
                Debug.LogWarning("PlaySoundFX called but audioSource is null.");
                return;
            }

            if (soundFx == null)
            {
                Debug.LogWarning("PlaySoundFX called with null soundFx.");
                return;
            }

            audioSource.PlayOneShot(soundFx, volume);

            audioSource.pitch = 1;

            if (randomizePitch)
            {
                audioSource.pitch += Random.Range(-pitchRandom, pitchRandom);
            }
        }

        public void PlayRollSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.rollSFX);
        }

        public void PlayBackStepSoundFX()
        {
            audioSource.PlayOneShot(WorldSoundFXManager.instance.backStepSFX);
        }
    }
}
