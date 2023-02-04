using DG.Tweening;
using UnityEngine;

namespace Route69
{
    public class GameManagerUI : MonoBehaviour
    {
        [SerializeField] TMPro.TextMeshProUGUI countdownText;

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

    }
}
