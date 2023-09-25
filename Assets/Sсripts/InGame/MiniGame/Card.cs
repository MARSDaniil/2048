using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Game.FindCouple {
    public class Card :MonoBehaviour, IPointerDownHandler {
        public CardConfig cardConfig;
        [SerializeField] InMiniGameManager inMiniGameManager;
        public bool isDown => this.GetComponent<Image>().sprite == cardConfig.frontSprite;
        public bool canDown = true;

        private void Awake() {
            SetBackSprite();
        }


        public void OnPointerDown(PointerEventData pointerEventData) {
            
            if (canDown && inMiniGameManager.canOpenCards) {
                if (!isDown) {
                    SetFrontSprite();
                    inMiniGameManager.OpenCards();
                }
            }
        }

        public void InitGameManager(InMiniGameManager value) {
            inMiniGameManager = value;
        }

        public void SetFrontSprite() => this.GetComponent<Image>().sprite = cardConfig.frontSprite;
        public void SetBackSprite() => this.GetComponent<Image>().sprite = cardConfig.backSprite;
        public void SetWinSprite() {
            this.GetComponent<Image>().sprite = cardConfig.winSprite;
            canDown = false;
            inMiniGameManager.currentWinCard++;
            if (inMiniGameManager.currentWinCard >= inMiniGameManager.numOfChild) inMiniGameManager.GameOver();
        }
    }
}    