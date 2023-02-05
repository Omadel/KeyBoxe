using DG.Tweening;
using Etienne;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Route69
{
    public class GameManagerUI : Etienne.Singleton<GameManagerUI>
    {
        [SerializeField] TextMeshProUGUI countdownText;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private GameObject _endBG;
        [SerializeField] private GameObject _victoryButton;
        [SerializeField] private GameObject _defeatButton;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private string[] _victoryDefeat;
        [SerializeField] Etienne.Sound[] _victoryDefeatSounds;
        [SerializeField] private bool _isGameEnded;
        public bool IsGameEnded => _isGameEnded;
        private bool _hasFirstTime;

        public void StartCountDown(Sound[] sounds, Sound startSound, TweenCallback onComplete, string startWord)
        {
            countdownText.transform.DOScale(Vector3.one, 0);
            countdownText.DOFade(1, 0);
            var sequence = DOTween.Sequence();
            for (int i = sounds.Length; i > 0; i--)
            {
                var index = i - 1;
                var text = i.ToString();
                sequence.AppendCallback(() =>
                {
                    sounds[index].Play();
                    countdownText.text = text;
                    countdownText.transform.localScale = Vector3.zero;
                });
                sequence.Append(countdownText.transform.DOScale(Vector3.one, .4f));
                sequence.AppendInterval(.6f);
            }

            sequence.AppendCallback(() =>
            {
                startSound.Play();
                countdownText.text = startWord;
                countdownText.transform.localScale = Vector3.zero;
            });
            sequence.Append(countdownText.transform.DOScale(Vector3.one, .4f));
            sequence.AppendInterval(.6f);
            sequence.AppendCallback(onComplete);
            sequence.Append(countdownText.DOFade(0, .4f));

            if (!_hasFirstTime)
            {
                _hasFirstTime = true;
            }
            else
            {
                GameManager.Instance.Player.PlayIdle();
                ChronoManager.Instance.SetupChrono();
            }

            StartCoroutine(WaitToLaunchNextRound());
            GameManager.Instance.CurrentBoss.BlockAttack = true;
        }

        private IEnumerator WaitToLaunchNextRound()
        {
            yield return new WaitForSeconds(3);
            _isGameEnded = false;
            GameManager.Instance.CurrentBoss.BlockAttack = false;
        }

        public void Victory()
        {
            Cursor.visible = true;
            _isGameEnded = true;
            _endScreen.SetActive(true);
            _victoryButton.SetActive(true);
            _resultText.text = _victoryDefeat[0];
            _victoryDefeatSounds[0].Play();
            AnimSpawnResult();
            GameManager.Instance.TypingManager.ResetPhase();
        }

        public void Defeat()
        {
            Cursor.visible = true;
            _isGameEnded = true;
            _endScreen.SetActive(true);
            _endBG.SetActive(true);
            _defeatButton.SetActive(true);
            _resultText.text = _victoryDefeat[1];
            _victoryDefeatSounds[1].Play();
            AnimSpawnResult();
        }

        public void PutIsGameEnded(bool yesOrNot)
        {
            _isGameEnded = yesOrNot;
        }

        public void VictoryButton()
        {
            print("Round Won!");
            _endScreen.SetActive(false);
            _victoryButton.SetActive(false);
            Cursor.visible = false;
            ChronoManager.Instance.SetupChrono();
            GameManager.Instance.ChangeBoss();
            GameManager.Instance.StartCooldown();
        }

        public void DefeatButton()
        {
            print("Round Lost!");
            FadeManager.Instance.BackToMenu();
        }

        public void VictoryFinal()
        {
            print("Absolute Win!");
            _isGameEnded = true;
            _endScreen.SetActive(true);
            _victoryButton.SetActive(false);
            _defeatButton.SetActive(true);

            _defeatButton.GetComponent<Image>().color = Color.blue;
            _defeatButton.GetComponentInChildren<TextMeshProUGUI>().text = "<bounce>Back to Menu";
            _resultText.fontSize = 150;
            _resultText.text = _victoryDefeat[2];
            _victoryDefeatSounds[2].Play();
            AnimSpawnResult();
        }

        public void AnimSpawnResult()
        {
            _resultText.gameObject.transform.DOScale(0, 0);
            _resultText.gameObject.transform.DOScale(1, .5f);
        }
    }
}