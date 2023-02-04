using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Route69
{
    [CreateAssetMenu(fileName = "New Chara Word", menuName = "Route69/Chara Words Data")] 
    public class CharaWordsData : ScriptableObject
    {
        public string[] WordsToType;
        public float SpawnWordsRate;
        public int LifeDamage;
        public int BackwardDamage;
        
    }
}
