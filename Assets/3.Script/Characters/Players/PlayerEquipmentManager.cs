using UnityEngine;

namespace KF
{
    public class PlayerEquipmentManager : CharacterEquipmentManager
    {
        PlayerManager player;

        public WeaponModelInstantiationSlot rightHandSlot;
        public WeaponModelInstantiationSlot leftHandSlot;
        //public WeaponModelInstantiationSlot backSlot;

        public WeaponManager rightWeaponManager;
        public WeaponManager leftWeaponManager;

        public GameObject rightHandWeaponModel;
        public GameObject leftHandWeaponModel;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
            InitializeWeaponSlots();
        }

        protected override void Start()
        {
            base.Start();

            LoadWeaponOnBothHands();
        }

        private void InitializeWeaponSlots()
        {
            WeaponModelInstantiationSlot[] weaponSlots = GetComponentsInChildren<WeaponModelInstantiationSlot>();

            foreach (var weaponSlot in weaponSlots)
            {
                if (weaponSlot.weaponSlot == WeaponModelSlot.RightHand)
                {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.weaponSlot == WeaponModelSlot.LeftHand)
                {
                    leftHandSlot = weaponSlot;
                }
                // else if (weaponSlot.weaponSlot == WeaponModelSlot.Back)
                // {
                //     backSlot = weaponSlot;
                // }
            }
        }

        public void LoadWeaponOnBothHands()
        {
            LoadRightWeapon();
            LoadLeftHandWeapon();
        }

        // 오른손 무기 로드 (playerInventoryManager.currentRightHandWeapon 기준)
        public void LoadRightWeapon()
        {
            if (player.playerInventoryManager.currentRightHandWeapon != null)
            {
                rightHandSlot.UnloadWeaponModel();

                rightHandWeaponModel = Instantiate(player.playerInventoryManager.currentRightHandWeapon.weaponModel, rightHandSlot.transform);
                rightHandSlot.LoadWeapon(rightHandWeaponModel);

                rightWeaponManager = rightHandWeaponModel.GetComponent<WeaponManager>();
                if (rightWeaponManager != null)
                    rightWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentRightHandWeapon);
                else
                    Debug.LogWarning("Right hand weapon model missing WeaponManager!");
            }
            else
            {
                // 무기 없을 때 모델 정리
                rightHandSlot.UnloadWeaponModel();
                rightHandWeaponModel = null;
                rightWeaponManager = null;
            }
        }

        // 왼손 무기 로드 (playerInventoryManager.currentLeftHandWeapon 기준)
        public void LoadLeftHandWeapon()
        {
            if (player.playerInventoryManager.currentLeftHandWeapon != null)
            {
                leftHandSlot.UnloadWeaponModel();

                leftHandWeaponModel = Instantiate(player.playerInventoryManager.currentLeftHandWeapon.weaponModel, leftHandSlot.transform);
                leftHandSlot.LoadWeapon(leftHandWeaponModel);

                leftWeaponManager = leftHandWeaponModel.GetComponent<WeaponManager>();
                if (leftWeaponManager != null)
                    leftWeaponManager.SetWeaponDamage(player, player.playerInventoryManager.currentLeftHandWeapon);
                else
                    Debug.LogWarning("Left hand weapon model missing WeaponManager!");
            }
            else
            {
                leftHandSlot.UnloadWeaponModel();
                leftHandWeaponModel = null;
                leftWeaponManager = null;
            }
        }

        /// <summary>
        /// 오른손 무기 교체 함수 (무기 인덱스 증가 후 재장착)
        /// </summary>
        public void SwitchRightWeapon()
        {
            if (!player.isPlayer)
                return;

            player.playerAnimatorManager.PlayTargetActionAnimation("Swap_Right_Weapon_01", false, true, true);

            if (rightHandWeaponModel != null)
            {
                Destroy(rightHandWeaponModel);
                rightHandWeaponModel = null;
                rightHandSlot.UnloadWeaponModel();
            }

            int maxWeapons = player.playerInventoryManager.weaponInRightHandSlots.Length;

            // 무기 인덱스 증가
            player.playerInventoryManager.rightHandWeaponIndex++;
            if (player.playerInventoryManager.rightHandWeaponIndex >= maxWeapons)
            {
                player.playerInventoryManager.rightHandWeaponIndex = 0;
            }

            WeaponItem selectedWeapon = null;

            // 무기 배열 내에서 착용 가능한 무기 탐색
            for (int i = 0; i < maxWeapons; i++)
            {
                int checkIndex = (player.playerInventoryManager.rightHandWeaponIndex + i) % maxWeapons;
                WeaponItem candidate = player.playerInventoryManager.weaponInRightHandSlots[checkIndex];

                // 무기가 존재하고, 무기 아이디가 Unarmed가 아니면 선택
                if (candidate != null && candidate.itemID != WorldItemDatabase.Instance.unarmedWeapon.itemID)
                {
                    selectedWeapon = candidate;
                    player.playerInventoryManager.rightHandWeaponIndex = checkIndex;
                    break;
                }
            }

            // 선택된 무기가 없으면 Unarmed 무기 착용
            if (selectedWeapon == null)
            {
                selectedWeapon = WorldItemDatabase.Instance.unarmedWeapon;
                player.playerInventoryManager.rightHandWeaponIndex = -1;
            }

            // 무기 장착
            player.EquipWeaponOnRightHand(selectedWeapon);
        }
    }
}
