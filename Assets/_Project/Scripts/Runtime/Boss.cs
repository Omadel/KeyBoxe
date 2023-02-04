using DG.Tweening;
using UnityEngine;

namespace Route69
{
    public class Boss : Unit
    {
        public override string Name => bossData.name;
        public BossData BossData => bossData;

        [SerializeField] BossData bossData;
        [SerializeField] Color hitColor = Color.white;
         [SerializeField, Etienne.MinMaxRange(.1f, 5f)] Etienne.Range attackspeedRange = new Etienne.Range(.4f, 2.5f);
        [SerializeField, Etienne.ReadOnly] State currentState;

        Animator animator;
        float attackTimer;
        Tween hitTween;

        public enum State { Entrance, Idle, Attack, Hit, Walking, KO, Win }

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
                animator.Play("Punch", 0, 0f);
            }
        }

        public void SetState(State state)
        {
            currentState = state;
            if (state == State.Idle) animator.Play("Idle", 0, Random.value);
            if (state == State.Hit)
            {
                animator.Play("Hit", 0, 0f);
                hitTween?.Complete();
                var material = animator.GetComponentInChildren<Renderer>().material;
                const string colorName = "_FillColor";
                material.SetColor(colorName, hitColor);
                hitTween = DOTween.ToAlpha(()=>material.GetColor(colorName), c => material.SetColor(colorName, c), 0f, .4f);
            }
            if (state == State.KO) animator.Play("Knocked Out", 0, 0f);
            if (state == State.Win) animator.Play("Victory", 0, 0f);
        }

        private void SetHealth(int health)
        {
            currentHealth = health;
            InvokeOnHealthChanged(health / (float)bossData.Health);
        }

        public float Hit(int strength)
        {
            SetHealth(currentHealth - strength);
            if (currentHealth <= 0)
            {
                SetState(State.KO);
                    GameManagerUI.Instance.Victory();
                return -1;
            }
            SetState(State.Hit);
            transform.DOMoveZ(transform.position.z + bossData.Stability, .4f).SetEase(Ease.OutCirc);
            return bossData.Stability;
        }

        private void Attack()
        {
            Debug.Log($"Attack {bossData.AttackDamage}");
            GameManager.Instance.TypingManager.SpawnWord();
        }

        public void StepForward(float push)
        {
            transform.DOMoveZ(transform.position.z - push, .4f).SetDelay(.2f);
        }
    }
}
