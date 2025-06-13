using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class WeaponManager : MonoBehaviour
    {
        public MeleeWeaponDamageCollider meleeDamageCollider;

        private void Awake()
        {
            meleeDamageCollider = GetComponentInChildren<MeleeWeaponDamageCollider>();
        }

        public void SetWeaponDamage(CharacterManager characterWieldinfWeapon, WeaponItem weapon)
        {
            meleeDamageCollider.characterCausingDamage = characterWieldinfWeapon;
            meleeDamageCollider.physicalDamage = weapon.physicalDamage;
            meleeDamageCollider.specialDamage = weapon.specialDamage;

            meleeDamageCollider.light_Attack_01_Modifier = weapon.light_Attack_01_Modifier;

        }
    }
}
