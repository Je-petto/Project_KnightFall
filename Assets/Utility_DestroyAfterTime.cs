using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class Utility_DestroyAfterTime : MonoBehaviour
    {
        [SerializeField] float timeUntilDestroy = 5f;

        public void Awake()
        {
            Destroy(gameObject, timeUntilDestroy);
        }
    }

}
