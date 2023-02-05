using Etienne;
using UnityEngine;

namespace Route69
{
    public class GameManager : Etienne.Singleton<GameManager>
    {
        public Player Player => player;
        public Boss CurrentBoss => currentBoss;
        public TypingManager TypingManager => typingManager;

        [SerializeField] Sound[] countdownSound;
        [SerializeField] Sound countdownStartSound;
        [SerializeField] private BossData[] _allBosses;

        [SerializeField] Player player;
        [SerializeField] Boss currentBoss;
        [SerializeField] TypingManager typingManager;

        private int _bossesIndex;


        GameManagerUI ui;

        private void Start()
        {
            Cursor.visible = false;
            ui = GetComponentInChildren<GameManagerUI>();
            ChangeBoss();
            TypingManager.ResetPhase();
            StartCooldown();
        }

        public void StartCooldown()
        {
            ui.StartCountDown(countdownSound, countdownStartSound, StartFight, currentBoss.BossData.StartWord);
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
            if (ChechIfVictoryFinal())
            {
                GameManagerUI.Instance.VictoryFinal();
                return;
            }
            currentBoss.UpdateBoss(_allBosses[_bossesIndex]);
            // print(currentBoss.BossData.StartWord);
            _bossesIndex++;
        }

        public bool ChechIfVictoryFinal()
        {
            return _bossesIndex >= _allBosses.Length;
        }
    }
}
