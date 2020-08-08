using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{
    public class CardDisplay : Interactable      
    {
        #region VARIABLES

        private bool hasInitialized;
        private CanvasGroup canvasGroup;
        private Image backgroundImage;
        private bool isTurned;

        private Pile pile;

        #endregion VARIABLES

        #region PROPERTIES

        public Card Card
        {
            get;
            private set;
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            backgroundImage = GetComponentInChildren<Image>();
        }

        public void Initialize(Card card)
        {
            if(hasInitialized)
            {
                return;
            }

            Card = card;

            ChangeSprite(card.FrontSprite);

            gameObject.name = card.name;

            hasInitialized = true;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);

            GameManager.Instance.CardSelectionPile.AddCardDisplayToContainer(this);
            canvasGroup.blocksRaycasts = false;
            canvasGroup.interactable = false;
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            transform.position += (Vector3)eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);

            canvasGroup.blocksRaycasts = true;
            canvasGroup.interactable = true;
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void Flip()
        {
            canvasGroup.blocksRaycasts = isTurned;
            canvasGroup.interactable = isTurned;

            isTurned = !isTurned;

            ChangeSprite(
                isTurned
                ? GameManager.Instance.CurrentBackSprite
                : Card.FrontSprite);          
        }

        private void ChangeSprite(Sprite newSprite)
        {
            backgroundImage.sprite = newSprite;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
