using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterManager characterCausingDamage;

    }
}

