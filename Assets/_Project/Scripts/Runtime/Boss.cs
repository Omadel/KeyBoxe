using UnityEngine;

namespace Route69
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] BossData bossData;
        [SerializeField, Etienne.MinMaxRange(.1f,5f)] Etienne.Range attackspeedRange = new Etienne.Range(.4f, 2.5f);

        Animator animator;
        float attackTimer;

        private void Start()
        {
            var go = GameObject.Instantiate(bossData.Prefab, transform);
            animator = go.GetComponent<Animator>();
        }

        private void Update()
        {
            attackTimer += Time.deltaTime;
            float attackDuration = (1 / attackspeedRange.Lerp(bossData.AttackSpeed));
            if (attackTimer >= attackDuration)
            {
                attackTimer -= attackDuration;
                Debug.Log("Attack");
                animator.Play("Punch");
            }
        }
    }
}
