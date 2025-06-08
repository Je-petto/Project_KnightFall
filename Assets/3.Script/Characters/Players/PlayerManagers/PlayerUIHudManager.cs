using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] UI_StatBar healthBar;

        public void RefreshHUD()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);
        }

        public void SetNewHeathValue(float oldValue, float newValue)
        {
            healthBar.SetStat(Mathf.RoundToInt(newValue));
        }

        public void SetMaxHealthValue(int maxHeath)
        {
            healthBar.SetMaxStat(maxHeath);
        }

        
    }

}
