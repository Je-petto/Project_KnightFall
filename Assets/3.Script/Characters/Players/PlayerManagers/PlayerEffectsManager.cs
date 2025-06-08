using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [SerializeField] InstantCharacterEffect effectToTest;
        [SerializeField] bool proccesEffect = false;

        private void Update()
        {
            if (proccesEffect)
            {
                proccesEffect = false;
                InstantCharacterEffect effect = Instantiate(effectToTest);
                ProcessInstantEffect(effect);
            }
        }
    }

}
