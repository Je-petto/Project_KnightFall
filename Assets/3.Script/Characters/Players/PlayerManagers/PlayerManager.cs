using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KF
{
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        public string characterName;
        protected override void Awake()
        {
            // Has Aspects of CharacterManager but can add more just for Player
            base.Awake();
            isPlayer = true;

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        }

        protected override void Update()
        {
            base.Update();

            playerLocomotionManager.HandleAllMovement();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            PlayerCamera.instance.HandleAllCameraActions();
        }

        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            currentCharacterData.characterName = characterName;
            currentCharacterData.xCoord = transform.position.x;
            currentCharacterData.yCoord = transform.position.y;
            currentCharacterData.zCoord = transform.position.z;

            currentCharacterData.vitality = characterStatsManager.vitality;
            currentCharacterData.currentHealth = characterStatsManager.currentHealth;            
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            characterName = currentCharacterData.characterName;
            Vector3 myPosition = new Vector3(currentCharacterData.xCoord, currentCharacterData.yCoord, currentCharacterData.zCoord);
            transform.position = myPosition;

            // ▶ 스탯 로드
            characterStatsManager.vitality = currentCharacterData.vitality;

            // ▶ MaxHealth 재계산
            characterStatsManager.maxHealth =
            characterStatsManager.CalculateHealthBasedOnVitalityLevel(currentCharacterData.vitality);

            // ▶ 현재 체력 반영
            characterStatsManager.currentHealth = currentCharacterData.currentHealth;

            // ▶ UI 갱신
            if (characterStatsManager.healthBar != null)
            {
                characterStatsManager.healthBar.SetMaxStat(characterStatsManager.maxHealth);
                characterStatsManager.healthBar.SetStat(characterStatsManager.currentHealth);
            }
        }
    } 
}
