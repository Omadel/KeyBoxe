using UnityEngine;

namespace Route69
{
    [CreateAssetMenu(menuName = "Route69/Boss")]
    public class BossData : ScriptableObject
    {
        public string StartWord => startWord;
        public int Health => health;
        public int AttackDamage => attackDamage;
        public float Stability => stability;
        public float AttackSpeed => attackSpeed;
        public GameObject Prefab => prefab;
        public CharaWordsData Words => words;

        [SerializeField] string startWord = "Fight !";
        [SerializeField] int health = 30;
        [SerializeField] int attackDamage = 3;
        [SerializeField] float stability = .4f;
        [SerializeField, Range(0f, 1f)] float attackSpeed = .2f;
        [SerializeField] GameObject prefab;
        [SerializeField] CharaWordsData words;
    }
}
