using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.FindCouple;
using UnityEngine.SceneManagement;
using UI.InGame;
namespace Game {
    public class InMiniGameManager :GameManager {
        [SerializeField] InGameUIMiniGameManager inGameUIManager;
        [SerializeField] Transform boardOfCards;
        [SerializeField] List<Card> cards;

        [SerializeField] private int numberCard;
        private int maximumOfTwins = 2;
        public bool canOpenCards = true;

        public int currentWinCard = 0;
        public int numOfChild;

        private int score;
        private void Awake() {
            Init();

        }
        private void Init() {
            inGameUIManager.Init();
            numOfChild = boardOfCards.childCount;
            for (int i = 0; i < boardOfCards.childCount; i++) {
                cards.Add(boardOfCards.GetChild(i).GetComponent<Card>());
                cards[i].InitGameManager(this);
            }
            NewGame();
        }

        public void NewGame() {
            UnfreezeGame();
            currentWinCard = 0;
            inGameUIManager.ClosePauseMenu();
            SetScore(-numOfChild/2);
            for (int i = 0; i < boardOfCards.childCount; i++) {
                cards[i].SetBackSprite();
                cards[i].transform.SetSiblingIndex(Random.Range(0, boardOfCards.childCount));
            }
          
        }

        public void OpenCards() {
                var openCard = 0;
                foreach (Card card in cards) {
                    if (card.isDown) {
                        if (openCard == 0) numberCard = card.cardConfig.number;
                        openCard++;
                    }
                }
                if (openCard == maximumOfTwins) CheckCards();
        }

        public void CheckCards() {
            var a = 0;
            foreach (Card card in cards) {
                if(card.isDown && card.cardConfig.number == numberCard) a++;
            }
            score++;
            inGameUIManager.IncreaseScore(score);
            if (a == maximumOfTwins) FindCards();
            else NotFindCards();

        }
        

        public void FindCards() {
            foreach (Card card in cards) {
                if (card.isDown) StartCoroutine(Win(card));
            }
        }

        public void NotFindCards() {
            foreach (Card card in cards) {
                if (card.isDown) StartCoroutine(NoFind(card));
            }
        }

        IEnumerator NoFind(Card card) {
            canOpenCards = false;
            yield return new WaitForSeconds(1f);
            canOpenCards = true;
            card.SetBackSprite();
        }
        IEnumerator Win(Card card) {
            canOpenCards = false;
           
            yield return new WaitForSeconds(1f);
            card.SetWinSprite();
            canOpenCards = true;
        }

        public void GameOver() {
            SaveHiscore();
            inGameUIManager.GameOver(score);
            Debug.Log("GameOver");
        }
        private void SetScore(int score) {
            this.score = score;
            inGameUIManager.IncreaseScore(score);
        }

        private void SaveHiscore() {
            int hiscore = LoadHiscore();

            if (score < hiscore) {
                PlayerPrefs.SetInt("FindTwinsScore", score);
                inGameUIManager.IncreaseRecordScore(score);
            }
        }

        private int LoadHiscore() {
            return PlayerPrefs.GetInt("FindTwinsScore", 10000);
        }

        public int GetScore() {
            return (score);
        }
    }

}