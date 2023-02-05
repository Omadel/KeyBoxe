using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

namespace Route69
{
    public class ChronoManager : Etienne.Singleton<ChronoManager>
    {
        [SerializeField] private TextMeshProUGUI _chronoText;
        [SerializeField] private GameObject _timeOut;
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

            if (_actualChrono < 15)
                _chronoText.color = Color.red;
            
            if(_actualChrono <= 0)
                LaunchTimeOut();
        }

        private void LaunchTimeOut()
        {
            StartCoroutine(WaitToDespawn());
        }
        
        private IEnumerator WaitToDespawn()
        {
            _timeOut.transform.DOScale(0, 0);
            _timeOut.transform.DOScale(1, .5f);
            yield return new WaitForSeconds(2);
            _timeOut.transform.DOScale(1.2f, .1f);
            yield return new WaitForSeconds(.2f);
            _timeOut.transform.DOScale(0, 4f);
        }
    }
}
