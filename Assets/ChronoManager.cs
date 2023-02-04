using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

namespace Route69
{
    public class ChronoManager : Etienne.Singleton<ChronoManager>
    {
        [SerializeField] private TextMeshProUGUI _chronoText;
        private double _actualChrono;

        private void Start()
        {
            SetupChrono();
        }

        public void SetupChrono()
        {
            _actualChrono = GameManager.Instance.TypingManager.GetTotalTimeRound();
        }

        private void Update()
        {
            if (_actualChrono < 0 || GameManagerUI.Instance.IsGameEnded) return;
            
            _actualChrono -= Time.deltaTime;
            UpdateChrono();
        }

        private void UpdateChrono()
        {
            TimeSpan time = TimeSpan.FromMinutes(_actualChrono);
            var format = time.ToString(@"hh\:mm\:ss");
            format = format.Substring(0, 5);
            _chronoText.text = format;

            if (_actualChrono < 5)
                _chronoText.color = Color.red;
        }
    }
}
