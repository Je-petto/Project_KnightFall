using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class CharacterManager : MonoBehaviour
    {
        [Header("Status")]
        public bool isDead = false;
        public bool isPlayer = false;
        [HideInInspector] public CharacterController characterController;
        [HideInInspector] public Animator animator;
        [HideInInspector] public CharacterEffectsManager characterEffectsManager;
        [HideInInspector] public CharacterStatsManager characterStatsManager;
        [HideInInspector] public CharacterAnimatorManager characterAnimatorManager;

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
            characterEffectsManager = GetComponent<CharacterEffectsManager>();
            characterStatsManager = GetComponent<CharacterStatsManager>();
            characterAnimatorManager = GetComponent<CharacterAnimatorManager>();
        }

        public void OnEquipAnimationComplete()
        {
            isPerformingAction = false;
            canMove = true;
            applyRootMotion = false;
            Debug.Log("무기 장착 애니메이션 완료: 상태 복원됨");
        }

        protected virtual void Update()
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        protected virtual void LateUpdate()
        {

        }

        public virtual IEnumerator ProcessDeathEvent(bool manuallySelectDeathAnimation = false)
        {
            if (isPlayer)
            {
                characterStatsManager.currentHealth = 0;
                isDead = true;
            }

            if (!manuallySelectDeathAnimation)
            {
                characterAnimatorManager.PlayTargetActionAnimation("Dead_01", true);
            }

            yield return new WaitForSeconds(5f);
        }

        public virtual void ReviveCharacter()
        {
            
        }
    }
}
