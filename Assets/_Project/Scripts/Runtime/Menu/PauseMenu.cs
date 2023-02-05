using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Route69
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] Button resumeButton, quitButton;

        bool isPaused = false;

        private void Start()
        {
            resumeButton.onClick.AddListener(Disable);
            quitButton.onClick.AddListener(Quit);

            Disable();
        }
        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                TogglePause();
            }

        }

        private void TogglePause()
        {
            if (isPaused) Disable();
            else Enable();
        }

        private void Enable()
        {
            Cursor.visible = true;
            isPaused = true;
            Time.timeScale = 0f;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(isPaused);
            }
        }

        private void Disable()
        {
            Cursor.visible = false;
            isPaused = false;
            Time.timeScale = 1f;
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(isPaused);
            }
        }

        private void Quit()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
    }
}
