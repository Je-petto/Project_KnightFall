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
    }
}

    
