using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class DamageCollider : MonoBehaviour
    {
        [Header("Damage")]
        public float physicalDamage = 0;
        public float specialDamage = 0;

        [Header("Contact Point")]
        private Vector3 contactPoint;

        [Header("Characters Damaged")]
        protected List<CharacterManager> charactersDamaged = new List<CharacterManager>();

        private void OnTriggerEnter(Collider other)
        {
            CharacterManager damageTarget = other.GetComponent<CharacterManager>();

            if (damageTarget != null)
            {
                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                //Check if we can damage this target

                DamageTarget(damageTarget);
            }
        }

        protected virtual void DamageTarget(CharacterManager damageTarget)
        {
            if (charactersDamaged.Contains(damageTarget))
                return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);

            damageEffect.characterEffectsManager = damageTarget.characterEffectsManager;
            
            damageEffect.physicalDamage = physicalDamage;
            damageEffect.specialDamage = specialDamage;
            damageEffect.contactPoint = contactPoint;

            damageEffect.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }
    }

}
