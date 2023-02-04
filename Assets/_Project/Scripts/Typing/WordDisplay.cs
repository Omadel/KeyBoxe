using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Route69
{
    public class WordDisplay : MonoBehaviour
    {
        public CharaWordsData charaWordData;

        [SerializeField] private TextMeshProUGUI _wordText;

        private int _currentIndex;
        private char _currentLetter;
        private string _currentSentence;
        private string _wordToType;

        public void GoToEndPoint(Transform endPoint, float time)
        {
            gameObject.transform.DOMoveX(endPoint.position.x, time).SetEase(Ease.Linear).OnComplete(QTEGoneToEnd);
        }

        private void QTEGoneToEnd()
        {
            GameManager.Instance.TypingManager.AttackPlayer();
            EndOfQTE();
        }
        
        void Update()
        {
            WordToType();
            // transform.position = new Vector3(transform.position.x - .1f, transform.position.y, 10);
            if(GameManagerUI.Instance.IsGameEnded)
                EndOfQTE();
        }

        public void SetWordtoType(string word)
        {
            _wordToType = word.ToUpper();
            UpdateTextString(word);
        }

        void WordToType()
        {
            foreach (KeyCode vKey in Enum.GetValues(typeof(KeyCode)))
            {
                if (!Input.GetKeyDown(vKey)) continue;

                var stringConvert = vKey.ToString().ToUpper();

                var upperWord = _wordToType;
                _currentLetter = stringConvert[0];

                if (_currentLetter == upperWord[_currentIndex])
                {
                    _currentIndex++;
                    _currentSentence += _currentLetter;
                    if (_currentSentence == upperWord)
                    {
                        EndOfQTE();
                        return;
                    }
                }
                else
                {
                    _currentIndex = 0;
                    _currentSentence = String.Empty;
                    print("fail : " + upperWord);
                }

                UpdateTextString(upperWord);
            }
        }

        private void UpdateTextString(string textToUpdate)
        {
            string sentence = textToUpdate.ToUpper();

            string before = sentence.Substring(0, _currentIndex);

            char nextToType = sentence[_currentIndex];

            string after = sentence.Substring(Mathf.Clamp(_currentIndex + 1, 0, sentence.Length - 1),
                Mathf.Clamp((sentence.Length - before.Length - 1), 0, sentence.Length - 1));

            var _result = "<color=red>" + before + "<color=purple>" + nextToType + "<color=white>" + after;

            UpdateTextVisu(_result);
        }

        void UpdateTextVisu(string textToReplace)
        {
            _wordText.text = textToReplace;
        }

        void EndOfQTE()
        {
            _currentIndex = 0;
            _currentSentence = string.Empty;

            GameManager.Instance.TypingManager.EndQTE(_wordToType);
            transform.DOKill();
            Destroy(gameObject);
        }
    }
}