using DG.Tweening;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Route69
{
    public class Boss : Unit
    {
        public bool BlockAttack = false;
        public override string Name => bossData.name;
        public BossData BossData => bossData;
        public Etienne.Sound CurrentHitSound => bossData.Words.PunchSoundPerPhase[GameManager.Instance.TypingManager.PhaseIndex];

        [SerializeField] BossData bossData;
        [SerializeField] Color hitColor = Color.white;
        [SerializeField, Etienne.ReadOnly] State currentState;

        float attackTimer;
        Tween hitTween;
        private GameObject _oldBoss;
        private Vector3 _initPos;

        public enum State { Entrance, Idle, Attack, Hit, Walking, KO, Win }

        private void Start()
        {
            _initPos = transform.position;
        }

        private void Update()
        {
            if (currentState == State.KO || BlockAttack) return;
            HandleAttackTimer();
        }

        public void UpdateBoss(BossData boss)
        {
            if (_oldBoss != null)
                Destroy(_oldBoss);

            bossData = boss;
            InvokeOnBossChanged(bossData.name);

            var go = GameObject.Instantiate(bossData.Prefab, transform);
            animator = go.GetComponent<Animator>();
            var animationListener = go.AddComponent<AnimationListener>();
            animationListener.OnAttack += Attack;
            animationListener.OnHitend += () => SetState(State.Attack);
            SetState(State.Idle);
            SetHealth(bossData.Health);

            if (_oldBoss != null)
            {
                gameObject.transform.position = _initPos;
                GameManager.Instance.Player.ResetPlayer();
            }
            _oldBoss = go;
            isFalling = false;
        }

        private void HandleAttackTimer()
        {
            attackTimer += Time.deltaTime;
            float attackDuration = 1 / GameManager.Instance.TypingManager.SpawnRate;
            if (attackTimer >= attackDuration)
            {
                attackTimer -= attackDuration;
                GameManager.Instance.TypingManager.SpawnWord();
            }
        }

        public void SetState(State state)
        {
            currentState = state;
            if (state == State.Idle) animator.Play("Idle", 0, Random.value);
            if (state == State.Hit)
            {
                animator.Play("Hit", 0, 0f);
                GameManager.Instance.Player.HitSound.Play();
                bossData.Words.PunchSoundPerPhase[GameManager.Instance.TypingManager.PhaseIndex].Play();
                hitTween?.Complete();
                var material = animator.GetComponentInChildren<Renderer>().sharedMaterial;
                const string colorName = "_FillColor";
                material.SetColor(colorName, hitColor);
                hitTween = DOTween.ToAlpha(() => material.GetColor(colorName), c => material.SetColor(colorName, c), 0f, .4f);
            }
            if (state == State.KO)
            {
                animator.Play("Knocked Out", 0, 0f);
                Die();
            }
            if (state == State.Win) animator.Play("Victory", 0, 0f);
            if (state == State.Attack) attackTimer = 0f;
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
                ChronoManager.Instance.LaunchInfo("KO!");
                SetState(State.KO);
                return -1;
            }
            SetState(State.Hit);
            float push = bossData.Stability;
            transform.DOMoveZ(transform.position.z + push, .4f).SetEase(Ease.OutCirc);
            return push;
        }

        protected override void Die()
        {
            if (GameManagerUI.Instance.IsGameEnded) return;
            base.Die();
            SetState(State.KO);
            GameManager.Instance.Player.LaunchWin();
            bossData.DeathSound.Play();
            
            if (GameManager.Instance.ChechIfVictoryFinal())
                GameManagerUI.Instance.VictoryFinal();
            else
                GameManagerUI.Instance.Victory();
        }

        internal void StartAttack()
        {
            if (currentState == State.KO) return;
            animator.SetFloat("PunchSpeed", 1 / GameManager.Instance.TypingManager.SpawnRate);
            animator.Play("Punch", 0, 0f);
        }

        private void Attack()
        {
            // Debug.Log($"Attack {bossData.AttackDamage}");
            int phaseIndex = GameManager.Instance.TypingManager.PhaseIndex;
            GameManager.Instance.Player.Hit(bossData.Words.LifeDamagePerPhase[phaseIndex],
                bossData.Words.PushDamagePerPhase[phaseIndex]);
            StepForward(bossData.Words.PushDamagePerPhase[phaseIndex]);
        }

        private void StepForward(float push)
        {
            transform.DOMoveZ(transform.position.z - push, .4f).SetDelay(.2f);
        }
    }
}
