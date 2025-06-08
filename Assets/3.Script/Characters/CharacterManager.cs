using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class CharacterManager : MonoBehaviour
    {
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;

        [Header("Flags")]
        public bool isPerformingAction = false;
        public bool isSprinting = false;
        public bool isJumping = false;
        public bool isGrounded = true;
        public bool applyRootMotion = false;
        public bool canRotate = true;
        public bool canMove = true;


        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
            animator = GetComponent<Animator>();
            characterController = GetComponent<CharacterController>();
        }

        protected virtual void Update()
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        protected virtual void LateUpdate()
        {
            
        }
    }
}
