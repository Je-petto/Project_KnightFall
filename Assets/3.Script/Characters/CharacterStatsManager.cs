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
        private int _currentHealth;
        public int currentHealth
        {
            get => _currentHealth;
            set
            {
                if (_currentHealth == value) return; // 값이 같으면 아무것도 안함 (중복 호출 방지)

                int oldValue = _currentHealth;
                _currentHealth = value;

                if (healthBar != null)
                {
                    healthBar.SetStat(_currentHealth);
                }

                CheckHP(oldValue, _currentHealth);
            }
        }

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

        public void CheckHP(int oldValue, int newValue)
        {
            if (newValue <= 0 && !character.isDead)
            {
                character.isDead = true;
                StartCoroutine(character.ProcessDeathEvent());
            }

            //Prevent Max Healing
            if (character.isPlayer)
            {
                if (currentHealth > maxHealth)
                {
                    currentHealth = maxHealth;
                }
            }
        }
    }
}

    
