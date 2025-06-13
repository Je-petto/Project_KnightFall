using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("VFX")]
        [SerializeField] GameObject HitVFX;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }
        // INSTANT EFFECTS
        public virtual void ProcessInstantEffect(InstantCharacterEffect effect)
        {
            effect.ProcessEffect(character);
        }

        public void PlayHitVFX(Vector3 contactPoint)
        {
            Debug.DrawRay(contactPoint, Vector3.up * 2f, Color.red, 1f); // 1초간 위로 빨간 선
            
            //If we manually have placed a blood splatter vfx on this model, play its version
            if (HitVFX != null)
            {
                Debug.Log("[VFX] Playing assigned HitVFX at: " + contactPoint);
                GameObject Hit_VFX = Instantiate(HitVFX, contactPoint, Quaternion.identity);
                Debug.Log("[VFX] Instantiated: " + Hit_VFX.name);
            }
            //else use normal
            else
            {
                Debug.Log("[VFX] HitVFX not assigned! Using WorldCharacterEffectsManager fallback.");
                GameObject Hit_VFX = Instantiate(WorldCharacterEffectsManager.instance.HitVFX, contactPoint, Quaternion.identity);
                Debug.Log("[VFX] Instantiated: " + Hit_VFX.name);
            }
        }
        // TIMED EFFECT
        //STATIC EFFECTS
    } 
}
