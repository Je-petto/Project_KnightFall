using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KF
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        PlayerControls playerControls;
        [SerializeField] Vector2 movementInput;

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
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // if we Destroy this Obbject, unsubscribe from this event
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

    }
    
}
