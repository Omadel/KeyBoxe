using UnityEngine;

namespace Route69
{
    public class GameManager : Etienne.Singleton<GameManager>
    {
        public Player Player => player;
        public Boss CurrentBoss => currentBoss;
        public TypingManager TypingManager => typingManager;

        [SerializeField] private BossData[] _allBosses;

        [SerializeField] Player player;
        [SerializeField] Boss currentBoss;
        [SerializeField] TypingManager typingManager;

        private int _bossesIndex;


        GameManagerUI ui;

        protected override void Awake()
        {
            base.Awake();
            ChangeBoss();
        }

        private void Start()
        {
            ui = GetComponentInChildren<GameManagerUI>();
            ui.StartCountDown(3, StartFight, currentBoss.BossData.StartWord);
        }

        private void StartFight()
        {
            currentBoss.SetState(Boss.State.Attack);
            typingManager.StartFight();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                ChangeBoss();
        }

        public void ChangeBoss()
        {
            if (_bossesIndex >= _allBosses.Length)
            {
                GameManagerUI.Instance.VictoryFinal();
                return;
            }
            currentBoss.UpdateBoss(_allBosses[_bossesIndex]);
            _bossesIndex++;
        }

    }
}
