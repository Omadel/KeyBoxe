using UnityEngine;

namespace Route69
{
    public class GameManager : Etienne.Singleton<GameManager>
    {
        public Player Player => player;
        public Boss CurrentBoss => currentBoss;
        public TypingManager TypingManager => typingManager;


        [SerializeField] Player player;
        [SerializeField] TypingManager typingManager;

        public Boss currentBoss;
        GameManagerUI ui;

        private void Start()
        {
            ui = GetComponentInChildren<GameManagerUI>();
            ui.StartCountDown(3, StartFight);
        }

        private void StartFight()
        {
            currentBoss.SetState(Boss.State.Attack);
            typingManager.StartFight();
        }
        
    }
}
