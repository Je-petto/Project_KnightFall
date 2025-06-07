using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class CharacterAnimatorManager : MonoBehaviour
    {
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        public void UpdateAnimatorMovementParameters(float horValue, float vertValue)
        {
            character.animator.SetFloat("Horizontal", horValue, 0.1f, Time.deltaTime);
            character.animator.SetFloat("Vertical", vertValue, 0.1f, Time.deltaTime);
        }
    }

}
