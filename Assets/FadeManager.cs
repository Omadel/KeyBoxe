using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

namespace Route69
{
    public class FadeManager : Etienne.Singleton<FadeManager>
    {
        [SerializeField] private GameObject _bg;
        [SerializeField] private float _timeFadeOnMainMenu = 3f;
        [SerializeField] private float _timeFadeOn;
        [SerializeField] private float _timeFadeOff;

        private void Start()
        {
            _bg.SetActive(true);
            _bg.GetComponent<Image>().DOFade(1, 0);
            _bg.GetComponent<Image>().DOFade(0, _timeFadeOff).OnComplete(DeactivateFade);
        }

        public void DeactivateFade()
        {
            _bg.SetActive(false);
        }

        public void BackToMenu()
        {
            _bg.SetActive(true);
            _bg.GetComponent<Image>().DOFade(0, 0);
            _bg.GetComponent<Image>().DOFade(1, 3).OnComplete(LaunchMainMenu);
        }

        private void LaunchMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
}
