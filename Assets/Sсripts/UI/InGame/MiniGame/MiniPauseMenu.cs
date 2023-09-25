using UnityEngine.UI;
using UnityEngine;
using UI.Common;
using TMPro;
namespace UI.InGame {
    public class MiniPauseMenu :MenuWindow {
        [SerializeField] InGameUIMiniGameManager inGameUIMiniGameManager;
        [SerializeField] Button continueGameButton;
        [SerializeField] Button menuButton;
        [SerializeField] Button restartGameButton;

        public override void Init(bool isOpen = false) {
            base.Init(isOpen);
            continueGameButton.onClick.AddListener(ContinueGame);
            menuButton.onClick.AddListener(CloseLevel);
            restartGameButton.onClick.AddListener(RestartLevel);
        }
        private void ContinueGame() {
            inGameUIMiniGameManager.ClosePauseMenu();
            inGameUIMiniGameManager.UnfreezeGame();
        }
        private void RestartLevel() {
            inGameUIMiniGameManager.inGameManager.NewGame();
        }
        private void CloseLevel() {
            inGameUIMiniGameManager.inGameManager.CloseGame();
        }
    }
}