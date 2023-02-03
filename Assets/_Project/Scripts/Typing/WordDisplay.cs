using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Route69
{
    public class WordDisplay : MonoBehaviour
    {
        public Words Word;

        [SerializeField] private TextMeshProUGUI _wordText;

        private int _currentIndex;
        private char _currentLetter;
        private string _currentSentence;
        private string _result;
        private int _spawnWordIndex;

        public void InitWord(Words word)
        {
            Word = word;
            UpdateTextString(Word.WordToType);
        }

        void Update()
        {
            WordToType();
        }

        void WordToType()
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    var stringConvert = vKey.ToString().ToUpper();

                    var word = Word.WordToType.ToUpper();
                    _currentLetter = stringConvert[0];

                    if (_currentLetter == word[_currentIndex])
                    {
                        _currentIndex++;
                        _currentSentence += _currentLetter;
                        print(_currentSentence);

                        if (_currentLetter == word[^1])
                        {
                            EndOfQTE();
                            return;
                        }
                    }
                    else
                    {
                        _currentIndex = 0;
                        _currentSentence = String.Empty;
                        print("fail : " + word);
                    }

                    UpdateTextString(word);
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

            TypingManager.Instance.EndQTE(gameObject);
        }
    }
}