using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Route69
{
    [CreateAssetMenu(fileName = "New Chara Word", menuName = "Route69/Chara Words Data")] 
    public class CharaWordsData : ScriptableObject
    {
        public WordsToTypePhase[] WordsToType;
        public float[] SpawnWordsRatePerPhase;
        public float[] TimeForNewPhase;


        public int LifeDamage;
        public int BackwardDamage;

        [System.Serializable]
        public struct WordsToTypePhase
        {
            public string[] Words;
        }
    }
}
