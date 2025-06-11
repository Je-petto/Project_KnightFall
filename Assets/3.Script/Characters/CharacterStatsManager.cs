using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;
        public UI_StatBar healthBar;

        [Header("Character Stats")]
        public int vitality = 10;
        public int baseHealth = 20;
        public int maxHealth;
        public int currentHealth;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {
            maxHealth = CalculateHealthBasedOnVitalityLevel(vitality);
            currentHealth = maxHealth;

            if (healthBar != null)
            {
                healthBar.SetMaxStat(maxHealth);
            }
        }

        public int CalculateHealthBasedOnVitalityLevel(int vitality)
        {
            return vitality * baseHealth;
        }

        public void SetCurrentHealth(int newValue)
        {
            int oldValue = currentHealth;
            currentHealth = newValue;

            if (healthBar != null)
            {
                healthBar.SetStat(currentHealth);
            }

            CheckHP(oldValue, newValue);
        }

        public void CheckHP(int oldValue, int newValue)
        {
            if (currentHealth <= 0)
            {
                StartCoroutine(character.ProcessDeathEvent());
            }

            //prevents from over healing
            if (character)
            {
                if (character.isPlayer && currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }
    }
}

    
