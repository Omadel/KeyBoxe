using System;
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

        public float[] WordSpeedPerPhase;
        public int[] LifeDamagePerPhase;
        public float[] PushDamagePerPhase;

        [System.Serializable]
        public struct WordsToTypePhase
        {
            public string[] Words;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (var item in WordsToType)
            {
                for (int i = 0; i < item.Words.Length; i++)
                {
                    item.Words[i] = item.Words[i].ToUpper();
                    
                }
            }
        }
#endif
    }
}
