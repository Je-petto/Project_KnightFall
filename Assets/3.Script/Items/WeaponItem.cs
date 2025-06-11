using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class WeaponItem : Item
    {
        [Header("Weapon Model")]
        public GameObject weaponModel;

        [Header("Weapom Requirements")]

        [Header("Weapon Base Poise Damage")]
        public float poiseDamage = 10;

        [Header("Weapon Base Damage")]
        public int physicalDamage = 0;
        public int specialDamage = 0;

        //Weapon Modifiers

        
    }

}
