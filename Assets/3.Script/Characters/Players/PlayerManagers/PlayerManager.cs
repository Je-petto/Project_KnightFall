using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KF
{
    public class PlayerManager : CharacterManager
    {
        [Header("DEBUG MENU")]
        [SerializeField] bool respawnCharacter = false;
        [SerializeField] bool switchRightWeapon = false;

        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;
        [HideInInspector] public PlayerInventoryManager playerInventoryManager;
        [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
        [HideInInspector] public PlayerCombatManager playerCombatManager;

        public string characterName;
        public WeaponItem currentWeaponBeingUsed;

        protected override void Awake()
        {
            base.Awake();
            isPlayer = true;

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerCombatManager = GetComponent<PlayerCombatManager>();
        }

        protected override void Update()
        {
            base.Update();

            if (!isPlayer)
                return;

            if (playerLocomotionManager != null)
            {
                playerLocomotionManager.HandleAllMovement();
            }

            DebugMenu();
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            PlayerCamera.instance.HandleAllCameraActions();
        }

        public override IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (isPlayer)
            {
                PlayerUIManager.instance.playerUIPopupManager.SendYouDiedPopup();
            }
            return base.ProcessDeathEvent(manuallySelectDeathAnimation);
        }

        public override void ReviveCharacter()
        {
            base.ReviveCharacter();

            if (isPlayer)
            {
                characterStatsManager.SetCurrentHealth(characterStatsManager.maxHealth);
                playerAnimatorManager.PlayTargetActionAnimation("Empty", false);
            }
        }

        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
            currentCharacterData.characterName = characterName;
            currentCharacterData.xCoord = transform.position.x;
            currentCharacterData.yCoord = transform.position.y;
            currentCharacterData.zCoord = transform.position.z;
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            characterName = currentCharacterData.characterName;
            Vector3 myPosition = new Vector3(currentCharacterData.xCoord, currentCharacterData.yCoord, currentCharacterData.zCoord);
            transform.position = myPosition;
        }

        /// <summary>
        /// 무기 장착 요청 함수. 무기가 null 이면 무기 해제.
        /// </summary>
        public void EquipWeaponOnRightHand(WeaponItem weapon)
        {
            if (playerEquipmentManager == null)
            {
                return;
            }
            if (playerEquipmentManager.rightHandSlot == null)
            {
                return;
            }

            // 기존 무기 모델이 있으면 제거
            playerEquipmentManager.rightHandSlot.UnloadWeaponModel();

            if (weapon == null)
            {
                // 무기 해제 상태 처리
                playerEquipmentManager.rightHandWeaponModel = null;
                playerInventoryManager.currentRightHandWeapon = null;
                return;
            }

            if (weapon.weaponModel == null)
            {
                return;
            }
            if (playerInventoryManager == null)
            {
                return;
            }

            // 무기 모델 생성 및 장착
            GameObject model = Instantiate(weapon.weaponModel, playerEquipmentManager.rightHandSlot.transform);
            playerEquipmentManager.rightHandWeaponModel = model;
            playerInventoryManager.currentRightHandWeapon = weapon;

            // 무기 매니저 세팅
            WeaponManager weaponManager = model.GetComponent<WeaponManager>();
            if (weaponManager != null)
            {
                weaponManager.SetWeaponDamage(this, weapon);
                playerEquipmentManager.rightWeaponManager = weaponManager;
            }
            else
            {
                Debug.LogWarning("Instantiated weapon model does not have WeaponManager component!");
            }
        }

        private void PerformWeaponBasedAction(int actionID, int weaponID)
        {
            WeaponItemAction weaponAction = WorldActionManager.instance.GetWeaponItemActionByID(actionID);


            if(weaponAction != null)
            {
                weaponAction.AttemptToPerformAction(this, WorldItemDatabase.Instance.GetWeaponByID(weaponID));
            }
            else
            {
                Debug.LogError("Action is Null, cannot be performed");
            }
        }

        private void DebugMenu()
        {
            if (respawnCharacter)
            {
                respawnCharacter = false;
                ReviveCharacter();
            }

            if (switchRightWeapon)
            {
                switchRightWeapon = false;
                playerEquipmentManager.SwitchRightWeapon();
            }
        }
    }
}
