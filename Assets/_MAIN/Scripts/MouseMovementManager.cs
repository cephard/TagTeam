using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseMovementManager : UnityEngine.MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private const float Dragging_Alpha = 0.6f;
    private const float NORMAL_ALPHA = 1f;

    
    [SerializeField] Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform parentAfterDrag;
    public Image taskImage;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = Dragging_Alpha;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        taskImage.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = NORMAL_ALPHA;
        transform.SetParent(parentAfterDrag);
        taskImage.raycastTarget = true;
        transform.SetAsLastSibling();
    }
}
