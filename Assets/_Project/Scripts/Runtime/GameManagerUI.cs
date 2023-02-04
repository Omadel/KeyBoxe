using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Route69
{
    public class GameManagerUI : Etienne.Singleton<GameManagerUI>
    {
        [SerializeField] TextMeshProUGUI countdownText;
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private string[] _victoryDefeat;
        [SerializeField] private bool _isGameEnded;
        public bool IsGameEnded => _isGameEnded;

        public void StartCountDown(int seconds, TweenCallback onComplete)
        {
            var sequence = DOTween.Sequence();
            for (int i = seconds; i > 0; i--)
            {
                var text = i.ToString();
                sequence.AppendCallback(() =>
                {
                    countdownText.text = text;
                    countdownText.transform.localScale = Vector3.zero;
                });
                sequence.Append(countdownText.transform.DOScale(Vector3.one, .4f));
                sequence.AppendInterval(.6f);
            }
            sequence.AppendCallback(() =>
            {
                countdownText.text = "Start";
                countdownText.transform.localScale = Vector3.zero;
            });
            sequence.Append(countdownText.transform.DOScale(Vector3.one, .4f));
            sequence.AppendInterval(.6f);
            sequence.AppendCallback(onComplete);
            sequence.Append(countdownText.DOFade(0, .4f));
        }

        public void Victory()
        {
            _isGameEnded = true;
            _endScreen.SetActive(true);
            _resultText.text = _victoryDefeat[0];
        }

        public void Defeat()
        {
            _isGameEnded = true;
            _endScreen.SetActive(true);
            _resultText.text = _victoryDefeat[1];
        }
    }
}
