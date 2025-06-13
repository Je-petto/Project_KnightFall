using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        [Header("Damage")]
        public TakeDamageEffect takeDamageEffect;

        [Header("VFX")]
        public GameObject HitVFX;

        [SerializeField] List<InstantCharacterEffect> instantEffects;
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

            GenerateEffectIDs();
        }

        public void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void GenerateEffectIDs()
        {
            for (int i = 0; i < instantEffects.Count; i++)
            {
                instantEffects[i].instantEffectID = i;
            }
        }
    }
}
