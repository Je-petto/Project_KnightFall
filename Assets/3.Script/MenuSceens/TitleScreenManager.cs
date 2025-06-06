using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class TitleScreenManager : MonoBehaviour
    {
        public void StartNewGame()
        {
            StartCoroutine(WorldSaveGameManager.instance.LoadNewGame());
        }
    }
    
}
