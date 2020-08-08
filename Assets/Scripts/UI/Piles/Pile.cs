using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace Sweet_And_Salty_Studios
{
    public abstract class Pile : MonoBehaviour, 
        IDropHandler, 
        IPointerEnterHandler, 
        IPointerExitHandler
    {
        #region VARIABLES

        private Transform container;

        private Stack<CardDisplay> cardDisplaysInContainer = new Stack<CardDisplay>();

        private TextMeshProUGUI amountText;

        #endregion VARIABLES

        #region PROPERTIES

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            Initialize();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Debug.Log($"{name} -- OnPointerEnter", gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Debug.Log($"{name} -- OnPointerExit", gameObject);
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            Debug.Log($"{name} -- OnDrop -> {cardDisplaysInContainer.Count}", gameObject);

            //AddCardDisplayToContainer(eventData.selectedObject.GetComponent<CardDisplay>());
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        public void AddCardDisplayToContainer(CardDisplay cardDisplay)
        {
            if(cardDisplaysInContainer.Contains(cardDisplay))
            {
                Debug.LogWarning($"{cardDisplay.name} already in container.");
                return;
            }

            cardDisplaysInContainer.Push(cardDisplay);
            cardDisplay.transform.SetParent(container);
            cardDisplay.transform.localScale = Vector3.one;

            UpdateAmountText();
        }

        public List<CardDisplay> GetCardDisplayFromContainer(int amount)
        {
            var result = new List<CardDisplay>();

            if(cardDisplaysInContainer.Count - amount < 0)
            {
                Debug.LogError("FOO");
                
                return null;
            }

            for(int i = 0; i < amount; i++)
            {
                result.Add(cardDisplaysInContainer.Pop());
            }

            UpdateAmountText();

            return result;
        }

        private void UpdateAmountText()
        {
            amountText.text = $"{cardDisplaysInContainer.Count}";
        }    

        protected virtual void Initialize()
        {
            container = transform.GetChild(1);
            amountText = GetComponentInChildren<TextMeshProUGUI>();
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
