using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KF
{
    public class Item : ScriptableObject
    {
        [Header("Item Information")]
        public string itemName;
        public Sprite itemIcon;
        [TextArea] public string itmeDescription;
        public int itemID;
    }

}
