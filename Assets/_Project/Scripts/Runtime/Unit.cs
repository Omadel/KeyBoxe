﻿using DG.Tweening;
using UnityEngine;

namespace Route69
{
    public abstract class Unit : MonoBehaviour
    {
        /// <summary>Event invoked wneh health is changed, float parameter is a 0-1 value</summary>
        public event System.Action<float> OnHealthChanged;
        public event System.Action<string> OnBossChanged;
        public abstract string Name { get; }    

        [SerializeField, Etienne.ReadOnly] protected int currentHealth;

        protected Animator animator;

        protected bool isFalling;

        private void LateUpdate()
        {
            if (isFalling) return;
            Ray ray = new Ray(transform.position + Vector3.up, Vector3.down);
            if (!Physics.Raycast(ray))
            {
                animator.CrossFade("Fall", .4f);
                isFalling = true;
                animator.transform.DOMoveY(transform.position.y -4f, 2f).SetDelay(.4f).SetSpeedBased(true).SetEase(Ease.Linear);
                Die();
            }
        }

        protected abstract void Die();

        protected void InvokeOnHealthChanged(float value) => OnHealthChanged?.Invoke(value);
        protected void InvokeOnBossChanged(string name) => OnBossChanged?.Invoke(name);
    }
}
