using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace Route69
{
    public class Player : Unit
    {
        public override string Name => playerName;

        [SerializeField] string playerName;
        [SerializeField] int startHealth = 30;
        [SerializeField] int attackDamage = 3;
        [SerializeField] int stability = 30;

        Animator animator;

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            var animationListener = GetComponentInChildren<AnimationListener>();
            animationListener.OnAttack += Attack;
            SetHealth(startHealth);
        }

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                PlayAttackAnimation();
            }
        }

        private void SetHealth(int health)
        {
            currentHealth = health;
            InvokeOnHealthChanged(health / (float)startHealth);
        }

        private void PlayAttackAnimation()
        {
            animator.Play("Punch", 0, 0f);
        }

        private void Attack()
        {
            var pushDistance = GameManager.Instance.CurrentBoss.Hit(attackDamage);
            transform.DOMoveZ(transform.position.z + pushDistance, .4f).SetDelay(.2f);
        }

        public void Hit(int damage, float push)
        {
            SetHealth(currentHealth - damage);
            transform.DOMoveZ(transform.position.z - push, .4f).SetEase(Ease.OutCirc);
            print("aller lens : " + damage + " : " + push);
        }
    }
}
