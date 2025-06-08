using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KF
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        public PlayerManager player;

        PlayerControls playerControls;

        [Header("CAMERA MOVEMENT INPUT")]
        [SerializeField] Vector2 cameraInput;
        public float cameraHorizontalInput;
        public float cameraVerticalInput;

        [Header("PLAYER MOVEMENT INPUT")]
        [SerializeField] Vector2 movementInput;
        public float horizontalInput;
        public float verticalInput;
        public float moveAmount;

        [Header("PLAYER ACTION INPUT")]
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool sprintInput = false;
        [SerializeField] bool jumpInput = false;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            //When the Scene Changes, Run this logic 
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false;
        }
        private void OnSceneChange(Scene oldScene, Scene NewScene)
        {
            // If we are loading into world scene, enable our players controls
            if (NewScene.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            // otherwise disable our players control
            else
            {
                instance.enabled = false;
            }
        }

        private void OnEnable()
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Jump.performed += i => jumpInput = true;

                //Holding the input, Sets the bool to true, release to false
                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // if we Destroy this Obbject, unsubscribe from this event
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void Update()
        {
            HandleAllInputs();
        }

        private void HandleAllInputs()
        {
            HandlePlayerMovementInput();
            HandleCameraMovementInput();
            HandleDodgeInput();
            HandleSprintInput();
            HandleJumpInput();
        }

        //MOVEMENT
        private void HandlePlayerMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            if (moveAmount <= 0.5f && moveAmount > 0f)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5f && moveAmount <= 1f)
            {
                moveAmount = 1f;
            }

            //Why do we pass 0 on the horizontal? because we only want Non_Strafing movement
            // We Use the horizontal when we are strafing or locked on

            if (player == null)
                return;

            //If we are NOT locked on, only use the move amount
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.isSprinting);

            //if we are locked on pass the horizontal movement as well
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraInput.y;
            cameraHorizontalInput = cameraInput.x;

        }

        //ACTION
        private void HandleDodgeInput()
        {
            if (dodgeInput)
            {
                dodgeInput = false;
                // Return(do nothing) if Menu or UI Window is open

                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprintInput)
            {
                player.playerLocomotionManager.HandleSprintInput();
            }
            else
            {
                player.isSprinting = false;
            }
        }

        private void HandleJumpInput()
        {
            if (jumpInput)
            {
                jumpInput = false;

                player.playerLocomotionManager.AttemptToPerformJump();
            }
        }
    }
    
}
