using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    [CreateAssetMenu(menuName = "Character Effects/Instant Effects/Take Damage")]
    public class TakeDamageEffect : InstantCharacterEffect
    {

        [Header("Character Causing Damage")]
        public CharacterManager characterCausingDamage;
        public CharacterStatsManager characterStatsManager;
        public CharacterEffectsManager characterEffectsManager;

        [Header("Damage")]
        public float physicalDamage = 0;
        public float specialDamage = 0;

        [Header("Final Damage")]
        private float finalDamageDealt = 0;

        [Header("Poise")]
        public float posieDamage = 0;
        public bool poiseIsBroken = false; //if a character's poise is broken, play stunned animation

        [Header("Animation")]
        public bool playDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageSFX = true;
        public AudioClip elementalDamageSoundFX; //used on top of regular sfx if there is elemental damage present

        [Header("Direction Damage Taken From")]
        public float angleHitFrom; //used to determine what damage animation to play
        public Vector3 contactPoint; //used to determine where strike fx instantiate

        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);

            characterStatsManager = character.GetComponent<CharacterStatsManager>();

            if (characterStatsManager == null)
            {
                Debug.LogError("[TakeDamageEffect] characterStatsManager is null on: " + character.name);
                return;
            }

            //If character is dead, no additional damage effects should be process
            if (character.isDead)
                return;

            //check for invulnerability
            CalculateDamage(character);
            PlayDamageSFX(character);
            PlayDamageVFX(character);
        }

        private void CalculateDamage(CharacterManager character)
        {
            if (characterCausingDamage != null)
            {
                //check for damage modifires and modify base damage
            }

            finalDamageDealt = Mathf.RoundToInt(physicalDamage + specialDamage);

            if (finalDamageDealt <= 0)
            {
                finalDamageDealt = 1;
            }

            var stats = character.GetComponent<CharacterStatsManager>();

            //character.GetComponent<CharacterStatsManager>().currentHealth -= (int)finalDamageDealt;
            int newHealth = stats.currentHealth - (int)finalDamageDealt;
            stats.SetCurrentHealth(newHealth);

            Debug.Log("FinalDamage Given" + finalDamageDealt);

            if (stats.healthBar != null)
            {
                stats.healthBar.SetStat(stats.currentHealth);
            }
        }

        private void PlayDamageVFX(CharacterManager character)
        {
            character.characterEffectsManager.PlayHitVFX(contactPoint);
        }

        private void PlayDamageSFX(CharacterManager character)
        {
            if (WorldSoundFXManager.instance == null)
            {
                Debug.LogError("WorldSoundFXManager.instance is null!");
                return;
            }
            AudioClip physicalDamageSFX = WorldSoundFXManager.instance.ChooseRandomSFXFromArray(WorldSoundFXManager.instance.physicalDamageSFX);
            if (physicalDamageSFX == null)
            {
                Debug.LogError("Selected physicalDamageSFX is null!");
                return;
            }
            character.characterSoundFXManager.PlaySoundFX(physicalDamageSFX);
        }
    }


}
