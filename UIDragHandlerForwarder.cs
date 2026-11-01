using UnityEngine;
using UnityEngine.EventSystems;

namespace SaveMyEyes;

public class UIDragHandlerForwarder : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UIDragHandler handler;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (handler != null)
        {
            handler.OnBeginDrag(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (handler != null)
        {
            handler.OnDrag(eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (handler != null)
        {
            handler.OnEndDrag(eventData);
        }
    }
}
