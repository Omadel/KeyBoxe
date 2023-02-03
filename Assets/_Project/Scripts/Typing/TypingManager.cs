using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Route69
{
    public class TypingManager : MonoBehaviour
    {
        // [SerializeField] private int currentCharIndex;
        // [SerializeField] private int sizeOfLetterToWrite = 130;

        [SerializeField] private List<GameObject> _wordToWrite = new List<GameObject>();
        [SerializeField] private string _sentenceToType;
        [SerializeField] private TextMeshProUGUI _sentenceTxt;
        
        private int _currentIndex;
        private char _currentLetter;
        private string _currentSentence;
        
        // private string convertPhrase;
        // private string result;

        const int afterIndex2 = 2;
        const int afterIndex3 = 3;

        void Update()
        {
            WordToType();
            if (_wordToWrite.Count == 0) return;
            
        }

        void WordToType()
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    var stringConvert = vKey.ToString().ToUpper();
                    _sentenceToType = _sentenceToType.ToUpper();
                    _currentLetter = stringConvert[0];

                    if (_currentLetter == _sentenceToType[_currentIndex])
                    {
                        _currentIndex++;
                        _currentSentence += _currentLetter;
                        print(_currentSentence);
                        
                        if(_currentLetter == _sentenceToType[^1])
                            EndOfQTE();
                    }
                    else
                    {
                        _currentIndex = 0;
                        _currentSentence = String.Empty;
                        print("fail");
                    }
                        
                    // print(vKey);
                    
                    // string str = convertPhrase;
                    // string before = str.Substring(0, currentCharIndex);
                    // string after = str.Substring(Mathf.Clamp((currentCharIndex + afterIndex2), 0, str.Length - 1),
                    //     Mathf.Clamp((str.Length - before.Length - afterIndex2), 0, str.Length - 1));
                    // string after2 = str.Substring(Mathf.Clamp((currentCharIndex + afterIndex3), 0, str.Length - 1),
                    //     Mathf.Clamp((str.Length - before.Length - afterIndex3), 0, str.Length - 1));
                    //
                    // result = "<color=red>" + before + str[currentCharIndex] + "<size=" + sizeOfLetterToWrite +
                    //          "%><color=purple>" + str[currentCharIndex + 1] + "</color><size=100%></color>" + after;
                    //
                    //
                    // if (getKeyStr[0] == convertPhrase[currentCharIndex])
                    // {
                    //     currentCharIndex++;
                    //     if (convertPhrase[currentCharIndex] == ' ')
                    //     {
                    //         result = "<color=red>" + before + str[currentCharIndex - 1] + str[currentCharIndex] +
                    //                  "<size=" + sizeOfLetterToWrite + "%><color=purple>" +
                    //                  str[currentCharIndex + 1] + "</color><size=100%></color>" + after2;
                    //         currentCharIndex++;
                    //     }
                    //
                    //     sentenceToWrite.text = result;
                    // }
                }
            }
        }

        void EndOfQTE()
        {
            print("Fini le QTE");
        }
    }
}