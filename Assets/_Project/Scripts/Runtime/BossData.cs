using UnityEngine;

namespace Route69
{
    [CreateAssetMenu(menuName = "Route66/Boss")]
    public class BossData : ScriptableObject
    {
        public uint Health => health;
        public uint Strength => strength;
        public uint Stability => stability;
        public float AttackSpeed => attackSpeed;
        public GameObject Prefab => prefab;
        public string[] Words => words;

        [SerializeField] uint health = 30;
        [SerializeField] uint strength = 40;
        [SerializeField] uint stability = 30;
        [SerializeField, Range(0f, 1f)] float attackSpeed = .2f;
        [SerializeField] GameObject prefab;
        [SerializeField] string[] words;
    }
}
