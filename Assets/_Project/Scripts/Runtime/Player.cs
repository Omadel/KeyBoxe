using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Collections;

namespace Route69
{
    public class Player : Unit
    {
        public override string Name => playerName;

        [SerializeField] string playerName;
        [SerializeField] int startHealth = 30;
        [SerializeField] int attackDamage = 3;
        [SerializeField] Color hitColor = Color.black;

        Animator animator;
        Tween hitTween;

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
            if (pushDistance<=-1)
            {
                StartCoroutine(Win());
                return;
            }
            transform.DOMoveZ(transform.position.z + pushDistance, .4f).SetDelay(.2f);
        }

        private IEnumerator Win()
        {
            yield return new WaitForSeconds(2f);
            animator.Play("Victory");
        }

        public void Hit(int damage, float push)
        {
            SetHealth(currentHealth - damage);
            animator.Play("Hit", 0, 0f);
            var material = animator.GetComponentInChildren<Renderer>().material;
            const string colorName = "_FillColor";
            material.SetColor(colorName, hitColor);
            hitTween = DOTween.ToAlpha(() => material.GetColor(colorName), c => material.SetColor(colorName, c), 0f, .4f);
            transform.DOMoveZ(transform.position.z - push, .4f).SetEase(Ease.OutCirc);
        }
    }
}
