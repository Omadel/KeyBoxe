using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Route69
{
    public class TypingManager : MonoBehaviour
    {
        [SerializeField] private List<Words> _wordToWrite = new List<Words>();
        [SerializeField] private GameObject _wordCardPrefab;
        [SerializeField] private GameObject _wordParent;
        [SerializeField] private string _sentenceToType;

        [SerializeField] private TextMeshProUGUI _sentenceTxt;
        // [SerializeField] private int _sizeOfLetterToWrite = 130;

        private List<GameObject> _actualWords = new List<GameObject>();
        private int _currentIndex;
        private char _currentLetter;
        private string _currentSentence;
        private string _result;
        private int _spawnWordIndex;

        // private string convertPhrase;

        // const int afterIndex2 = 2;
        // const int afterIndex3 = 3;

        public static TypingManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SpawnWord();
            SpawnWord();
        }

        private void SpawnWord()
        {
            GameObject go = Instantiate(_wordCardPrefab, _wordParent.transform);
            go.GetComponent<WordDisplay>().InitWord(_wordToWrite[_spawnWordIndex]);
            go.transform.position = new Vector3(_wordParent.transform.position.x, _wordParent.transform.position.y + _spawnWordIndex * 50);
            _actualWords.Add(go);
            
            _spawnWordIndex++;
            if (_spawnWordIndex % _wordToWrite.Count == 0)
                _spawnWordIndex = 0;
        }

        public void EndQTE(GameObject finishedWord)
        {
            _actualWords.Remove(finishedWord);
            Destroy(finishedWord);
            print("Fini le QTE");
            SpawnWord();
        }
    }
}