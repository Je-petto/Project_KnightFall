using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;
        protected override void Awake()
        {
            PlayerLocomotionManager playerLocomotionManager;
            // Has Aspects of CharacterManager but can add more just for Player
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update()
        {
            base.Update();

            playerLocomotionManager.HandleAllMovement();
        }
    } 
}
