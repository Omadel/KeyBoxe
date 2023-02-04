using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Route69
{
    public class UnitUI : MonoBehaviour
    {
        [SerializeField] Unit targetUnit;
        [SerializeField] TMPro.TextMeshProUGUI unitNameText;
        [SerializeField] Slider healthBar;
        [SerializeField] Slider hitHealthBar;

        private void Awake()
        {
            if (targetUnit == null)
            {
                Debug.LogError("No Player referenced.", gameObject);
                Destroy(this);
                return;
            }
            targetUnit.OnHealthChanged += UpdateHealth;
            targetUnit.OnBossChanged += UpdateBossName;
        }

        private void UpdateHealth(float value)
        {
            healthBar.value = value;
            hitHealthBar.DOValue(value, .4f).SetDelay(.4f);
        }

        void UpdateBossName(string name)
        {
            unitNameText.text = name;
        }
        
        
    }
}
