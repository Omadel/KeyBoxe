using DG.Tweening;
using UnityEngine;

namespace Route69
{
    public class Boss : Unit
    {
        public override string Name => bossData.name;
        public BossData BossData => bossData;

        [SerializeField] BossData bossData;
        [SerializeField, Etienne.MinMaxRange(.1f, 5f)] Etienne.Range attackspeedRange = new Etienne.Range(.4f, 2.5f);
        [SerializeField, Etienne.ReadOnly] State currentState;

        Animator animator;
        float attackTimer;

        public enum State { Entrance, Idle, Attack, Hit, Walking }

        private void Start()
        {
            var go = GameObject.Instantiate(bossData.Prefab, transform);
            animator = go.GetComponent<Animator>();
            var animationListener = go.AddComponent<AnimationListener>();
            animationListener.OnAttack += Attack;
            animationListener.OnHitend += () => SetState(State.Attack);
            SetState(State.Idle);
            SetHealth(bossData.Health);
        }

        private void Update()
        {
            if (currentState == State.Attack) HandleAttackTimer();
        }

        private void HandleAttackTimer()
        {
            attackTimer += Time.deltaTime;
            float attackDuration = 1 / GameManager.Instance.TypingManager.SpawnRate;
            if (attackTimer >= attackDuration)
            {
                attackTimer -= attackDuration;
                animator.SetFloat("PunchSpeed", Mathf.Max(1f, 1.6f / attackDuration));
                animator.CrossFadeInFixedTime("Punch", .1f);
            }
        }

        public void SetState(State state)
        {
            currentState = state;
            if (state == State.Idle) animator.Play("Idle", 0, Random.value);
            if (state == State.Hit) animator.Play("Hit", 0, 0f);
        }

        private void SetHealth(int health)
        {
            currentHealth = health;
            InvokeOnHealthChanged(health / (float)bossData.Health);
        }

        public float Hit(int strength)
        {
            SetHealth(currentHealth - strength);
            SetState(State.Hit);
            transform.DOMoveZ(transform.position.z + bossData.Stability, .4f).SetEase(Ease.OutCirc);
            return bossData.Stability;
        }

        private void Attack()
        {
            Debug.Log($"Attack {bossData.AttackDamage}");
            GameManager.Instance.TypingManager.SpawnWord();
        }
    }
}
