using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class PlayerCombatManager : CharacterCombatManager
    {
        PlayerManager player;

        public WeaponItem currentWeaponBeingUsed;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void PerformWeaponBasedAction(WeaponItemAction weaponAction, WeaponItem weaponPerformingAction)
        {
            if (player.isPlayer)
            {
                weaponAction.AttemptToPerformAction(player, weaponPerformingAction);
            }
        }
    }
}
