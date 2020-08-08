using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class Interactable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
           
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
          
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {

        }     
    }
}