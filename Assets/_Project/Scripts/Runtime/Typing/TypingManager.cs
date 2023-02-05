using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Route69
{
    public class TypingManager : MonoBehaviour
    {
        public float SpawnRate => _spawnRate;
        public int PhaseIndex => _phaseIndex;

        [SerializeField] private GameObject _wordCardPrefab;
        [SerializeField] private GameObject _startWordPos;
        [SerializeField] private GameObject _endWordPos;

        private List<string> _currentWords = new List<string>();
        private int _phaseIndex;
        private float _spawnRate;
        private float _phaseRate;
        private float _phaseTimer;

        CharaWordsData GetCurrentWordData => GameManager.Instance.CurrentBoss.BossData.Words;


        private void Start()
        {
            enabled = false;
        }

        public void StartFight()
        {
            _phaseTimer = 0f;
            UpdatePhase();
            enabled = true;
        }

        private void UpdatePhase()
        {
            if (GameManagerUI.Instance.IsGameEnded) return;

            _phaseRate = GetCurrentWordData.TimeForNewPhase[_phaseIndex];
            _spawnRate = GetCurrentWordData.SpawnWordsRatePerPhase[_phaseIndex];
            Debug.Log($"Change phase to {_phaseIndex}");
        }

        private void Update()
        {
            if (GetCurrentWordData.WordsToType.Length == _currentWords.Count) return;

            _phaseTimer += Time.deltaTime;

            if (_phaseTimer >= _phaseRate)
            {
                _phaseTimer -= _phaseRate;
                _phaseIndex++;
                if (_phaseIndex >= GetCurrentWordData.WordsToType.Length)
                {
                    if (GameManager.Instance.ChechIfVictoryFinal())
                        GameManagerUI.Instance.VictoryFinal();
                    else
                        GameManagerUI.Instance.Victory();
                    return;
                }

                UpdatePhase();
            }
        }

        public float GetTotalTimeRound()
        {
            var totalTime = 0f;
            foreach (var wordTime in GetCurrentWordData.TimeForNewPhase)
            {
                totalTime += wordTime;
            }

            return totalTime;
        }

        public void SpawnWord()
        {
            if (GameManagerUI.Instance.IsGameEnded) return;

            if (AllWordsAreAlreadySpawned()) return;
            var word = GetRandomWord();
            GameObject go = Instantiate(_wordCardPrefab, _startWordPos.transform);
            go.GetComponent<WordDisplay>().SetWordtoType(word);

            var fitter = go.GetComponentInChildren<ContentSizeFitter>();
            var rectTransform = fitter.GetComponent<RectTransform>();
            fitter.SetLayoutHorizontal();
            float offsetX = rectTransform.rect.width;
            
            var parentPos = _startWordPos.transform.position;
            int nb = Random.Range(1, 6);
            go.transform.position = new Vector3(parentPos.x + offsetX, parentPos.y + nb * 50, 10);
            go.GetComponent<WordDisplay>()
                .GoToEndPoint(_endWordPos.transform, GetCurrentWordData.WordSpeedPerPhase[_phaseIndex] + offsetX * .001f);
        }

        private bool AllWordsAreAlreadySpawned()
        {
            string[] words = GetCurrentWordData.WordsToType[_phaseIndex].Words;
            foreach (var word in words)
            {
                if (!_currentWords.Contains(word)) return false;
            }

            return true;
        }

        private string GetRandomWord()
        {
            var allWords = GetCurrentWordData.WordsToType[_phaseIndex].Words;
            string word;
            do
            {
                var nb = Random.Range(0, allWords.Length);
                word = allWords[nb];
            } while (_currentWords.Contains(word));

            AddWords(word);
            return word;
        }

        private void AddWords(string newWords)
        {
            _currentWords.Add(newWords);
        }

        public void AttackPlayer()
        {
            GameManager.Instance.CurrentBoss.StartAttack();
        }

        public void EndQTE(string finishedWord)
        {
            _currentWords.Remove(finishedWord);
        }
    }
}