using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    [CreateAssetMenu(menuName = "Character Actions/Weapon Actions/Test Action")]
    public class WeaponItemAction : ScriptableObject
    {
        public int actionID;

        public virtual void AttemptToPerformAction(PlayerManager playerPerformingAction, WeaponItem weaponPerformingAction)
        {
            if (playerPerformingAction.isPlayer)
            {
                playerPerformingAction.currentWeaponBeingUsed = weaponPerformingAction;
            }

            Debug.Log("THE ACTION HAS PERFORMED");
        }
    }

}
