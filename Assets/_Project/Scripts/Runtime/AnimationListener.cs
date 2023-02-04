using UnityEngine;

namespace Route69
{
    public class AnimationListener : MonoBehaviour
    {
        public event System.Action OnAttack, OnHitend;

        void Attack() => OnAttack?.Invoke();
        void HitEnd() => OnHitend?.Invoke();
    }
}
