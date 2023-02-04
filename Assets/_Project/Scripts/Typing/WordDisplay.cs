using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = UnityEngine.Random;

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
        private string _result;
        private int _spawnWordIndex;

        public void ChooseRandomWords(string[] allWords)
        {
            var getActualWords = TypingManager.Instance.GetActualWords();
            //if (allWords.Length == getActualWords.Count)return;
            string word;
            do
            {
                var nb = Random.Range(0, allWords.Length);
                word = allWords[nb];
            } while (getActualWords.Contains(word));
            
            _wordToType = word;
            TypingManager.Instance.AddWords(_wordToType);
            UpdateTextString(_wordToType);
        }

        void Update()
        {
            WordToType();
            transform.position = new Vector3(transform.position.x - .1f, transform.position.y, 10);
        }

        void WordToType()
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    var stringConvert = vKey.ToString().ToUpper();

                    var upperWord = _wordToType.ToUpper();
                    _currentLetter = stringConvert[0];

                    if (_currentLetter == upperWord[_currentIndex])
                    {
                        _currentIndex++;
                        _currentSentence += _currentLetter;
                        // print(_currentSentence);

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
        }

        void UpdateTextString(string textToUpdate)
        {
            string sentence = textToUpdate.ToUpper();

            string before = sentence.Substring(0, _currentIndex);

            char nextToType = textToUpdate[_currentIndex];

            string after = sentence.Substring(Mathf.Clamp(_currentIndex + 1, 0, sentence.Length - 1),
                Mathf.Clamp((sentence.Length - before.Length - 1), 0, sentence.Length - 1));

            _result = "<color=red>" + before + "<color=purple>" + nextToType + "<color=white>" + after;

            UpdateTextVisu(_result);
        }

        void UpdateTextVisu(string textToReplace)
        {
            _wordText.text = textToReplace;
        }

        void EndOfQTE()
        {
            _currentIndex = 0;
            _currentSentence = String.Empty;

            TypingManager.Instance.EndQTE(_wordToType);
            
            Destroy(gameObject);
        }
    }
}