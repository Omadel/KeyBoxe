using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Route69
{
    [CreateAssetMenu(fileName = "New Word", menuName = "Words")] 
    public class Words : ScriptableObject
    {
        public string WordToType;
        public int LifeDamage;
        public int BackwardDamage;
        
    }
}
