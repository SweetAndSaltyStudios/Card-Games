using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class GameManager : Singelton<GameManager>
    {
        #region VARIABLES

        public CardDisplay CardDisplayPrefab;
        public Sprite[] CardBackSprites;
        private int backSpriteIndex;

        public List<Card> Cards = new List<Card>();

        private TableauPile[] tableauPiles;
        private StockPile stockPile;

        #endregion VARIABLES

        #region PROPERTIES

        public CardSelectionPile CardSelectionPile
        {
            get;
            private set;
        }
        public Sprite CurrentBackSprite
        {
            get
            {
                if(CardBackSprites == null || CardBackSprites.Length == 0 || CardBackSprites[backSpriteIndex] == null)
                {
                    return Sprite.Create(
                        new Texture2D(128, 128),
                        new Rect(Vector2.zero, Vector2.one),
                        Vector2.one * 0.5f);
                }

                return CardBackSprites[backSpriteIndex];
            }
        }

        #endregion PROPERTIES

        #region UNITY_FUNCTIONS

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            backSpriteIndex = Random.Range(0, CardBackSprites.Length - 1);

            StartCoroutine(SetUpGame());
        }

        private IEnumerator SetUpGame()
        {
            var allCardDisplays = CreateCardDisplays(Cards);

            CardDisplay cardDisplay = null;

            var index = 0;

            for(index = 0; index < allCardDisplays.Count; index++)
            {
                cardDisplay = allCardDisplays[index];

                cardDisplay.Flip();

                stockPile.AddCardDisplayToContainer(cardDisplay);

                yield return new WaitForSeconds(0.01f);
            }

            foreach(var tableauPile in tableauPiles)
            {
                var cardDisplays = stockPile.GetCardDisplayFromContainer(tableauPile.StartingRandomCardAmount);
                for(index = 0; index < cardDisplays.Count; index++)
                {
                    tableauPile.AddCardDisplayToContainer(cardDisplays[index]);
                    yield return new WaitForSeconds(0.1f);
                }

                cardDisplays[cardDisplays.Count - 1].Flip();
            }
        }

        #endregion UNITY_FUNCTIONS

        #region CUSTOM_FUNCTIONS

        private void Initialize()
        {
            stockPile = FindObjectOfType<StockPile>();
            CardSelectionPile = FindObjectOfType<CardSelectionPile>();
            tableauPiles = FindObjectsOfType<TableauPile>();

            TableauPile temp = null;
            var i = 0;
            var j = 0;

            for(i = 0; i < tableauPiles.Length - 1; i++)
                for(j = i + 1; j < tableauPiles.Length; j++)
                    if(tableauPiles[i].StartingRandomCardAmount > tableauPiles[j].StartingRandomCardAmount)
                    {
                        temp = tableauPiles[i];
                        tableauPiles[i] = tableauPiles[j];
                        tableauPiles[j] = temp;
                    }
        }

        private List<CardDisplay> CreateCardDisplays(List<Card> newCardDatas)
        {
            CardDisplay newCardDisplay = null;
            var result = new List<CardDisplay>();

            for(int i = 0; i < newCardDatas.Count; i++)
            {
                newCardDisplay = Instantiate(CardDisplayPrefab);
                newCardDisplay.Initialize(newCardDatas[i]);
                result.Add(newCardDisplay);
            }

            return result;
        }

        #endregion CUSTOM_FUNCTIONS
    }
}
