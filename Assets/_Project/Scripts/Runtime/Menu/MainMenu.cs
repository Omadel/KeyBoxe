using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Route69
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Button playButton, quitButton;
        [SerializeField] GameObject loadingScreen;

        private void Start()
        {
            loadingScreen.SetActive(false);
            playButton.onClick.AddListener(Play);
            quitButton.onClick.AddListener(Quit);
        }

        private void Play()
        {
            loadingScreen.SetActive(true);
            SceneManager.LoadScene(1);
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
	        Application.Quit();
#endif
        }
    }
}
