using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Route69
{
    public class TypingManager : MonoBehaviour
    {
        [SerializeField] private List<CharaWordsData> _charactersWords = new List<CharaWordsData>();
        [SerializeField] private GameObject _wordCardPrefab;
        [SerializeField] private GameObject _wordParent;
        [SerializeField] private string _sentenceToType;

        [SerializeField] private TextMeshProUGUI _sentenceTxt;
        // [SerializeField] private int _sizeOfLetterToWrite = 130;

        private List<string> _actualWords = new List<string>();
        private char _currentLetter;
        private string[] _characterSentences;
        private string _currentSentence;
        private string _result;
        private int _spawnWordIndex;
        private int _characterIndex;
        private int _phaseIndex;
        private string[] _currentPhase;


        private float _spawnRate;
        private float _spawnNext;

        private float _phaseRate;
        private float _phaseNext;

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
            GetActualPhaseRate();
            GetActualSpawnRate();
        }

        private void GetActualSpawnRate()
        {
            _spawnRate = _charactersWords[_characterIndex].SpawnWordsRatePerPhase[_phaseIndex];
        }

        private void GetActualPhaseRate()
        {
            _phaseRate = _charactersWords[_characterIndex].TimeForNewPhase[_phaseIndex];
        }

        private void Update()
        {
            if (_charactersWords[_characterIndex].WordsToType.Length == _actualWords.Count) return;
            
            _spawnNext -= Time.deltaTime;
            _phaseNext += Time.deltaTime;

            if (_spawnNext <= 0)
            {
                SpawnWord();
                _spawnNext = _spawnRate;
            }

            if (_phaseNext >= _phaseRate)
            {
                _phaseNext = 0;
                _phaseIndex++;
                GetActualPhaseRate();
                GetActualSpawnRate();
            }
        }

        void ChangeCharacter()
        {
            _characterIndex++;
        }

        private void SpawnWord()
        {
            GameObject go = Instantiate(_wordCardPrefab, _wordParent.transform);
            go.GetComponent<WordDisplay>().ChooseRandomWords(_charactersWords[_characterIndex].WordsToType[_phaseIndex].Words);
            
            var parentPos = _wordParent.transform.position;
            int nb = Random.Range(0, 6);
            go.transform.position = new Vector3(parentPos.x, parentPos.y + nb * 30, 10);
        }

        public List<string> GetActualWords()
        {
            return _actualWords;
        }

        public void AddWords(string newWords)
        {
            _actualWords.Add(newWords);
        }

        public void EndQTE(string finishedWord)
        {
            _actualWords.Remove(finishedWord);
            print("Fini le QTE");
        }
    }
}