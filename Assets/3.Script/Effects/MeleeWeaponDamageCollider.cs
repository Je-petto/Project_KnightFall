using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class MeleeWeaponDamageCollider : DamageCollider
    {
        [Header("Attacking Character")]
        public CharacterManager characterCausingDamage;

        [Header("Weapon Attack Modifiers")]
        public float light_Attack_01_Modifier;

        protected override void Awake()
        {
            base.Awake();

            if (damageCollider == null)
            {
                damageCollider = GetComponent<Collider>();
            }
            damageCollider.enabled = false;
        }

        protected override void OnTriggerEnter(Collider other)
        {
            Debug.Log($"[Trigger] Collided with: {other.name}");

            CharacterManager damageTarget = other.GetComponentInParent<CharacterManager>();
            Debug.Log($"[Trigger] damageTarget: {damageTarget}");
            // if (damageTarget == null)
            // {
            //     damageTarget = other.GetComponent<CharacterManager>();
            // }

            if (damageTarget != null)
            {
                Debug.Log($"[Trigger] Attacker: {characterCausingDamage}, Target: {damageTarget}");
                if (damageTarget == characterCausingDamage)
                {
                    Debug.Log("[Trigger] Skipping self-hit");
                    return;
                }

                contactPoint = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);

                //Check if we can damage this target

                DamageTarget(damageTarget);
            }
        }

        protected override void DamageTarget(CharacterManager damageTarget)
        {
            Debug.Log("DamageTarget called");
            if (charactersDamaged.Contains(damageTarget))
                return;

            charactersDamaged.Add(damageTarget);

            TakeDamageEffect damageEffect = Instantiate(WorldCharacterEffectsManager.instance.takeDamageEffect);

            damageEffect.characterEffectsManager = damageTarget.characterEffectsManager;

            damageEffect.physicalDamage = physicalDamage;
            damageEffect.specialDamage = specialDamage;
            damageEffect.contactPoint = contactPoint;

            switch (characterCausingDamage.characterCombatManager.currentAttackType)
            {
                case AttackType.LightAttack01:ApplyAttackDamageModifiers(light_Attack_01_Modifier, damageEffect);
                    break;
                default:
                    break;
            }

            Debug.Log("Final Damage: " + damageEffect.physicalDamage);

            damageEffect.characterEffectsManager.ProcessInstantEffect(damageEffect);
        }

        private void ApplyAttackDamageModifiers(float modifier, TakeDamageEffect damage)
        {
            damage.physicalDamage *= modifier;
            damage.specialDamage *= modifier;
        }
    }
}

