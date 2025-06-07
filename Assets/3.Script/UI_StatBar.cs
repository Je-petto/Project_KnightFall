using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KF
{
    public class UI_StatBar : MonoBehaviour
    {
        private Slider slider;

        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
        }

    }

}
