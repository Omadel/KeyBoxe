using UnityEngine;

namespace Route69
{
    public class Unit : MonoBehaviour
    {
        /// <summary>Event invoked wneh health is changed, float parameter is a 0-1 value</summary>
        public event System.Action<float> OnHealthChanged;
        [SerializeField, Etienne.ReadOnly] protected int currentHealth;

        protected void InvokeOnHealthChanged(float value) => OnHealthChanged?.Invoke(value);
    }
}
