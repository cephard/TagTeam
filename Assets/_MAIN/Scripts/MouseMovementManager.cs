using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Manages the drag-and-drop behavior of UI elements in the game, allowing them to be dragged using the mouse.
/// Implements the IBeginDragHandler, IEndDragHandler, and IDragHandler interfaces.
/// </summary>
public class MouseMovementManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private const float DRAGGING_ALPHA = 0.6f;
    private const float NORMAL_ALPHA = 1f;

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform parentAfterDrag;
    public Image taskImage;

    /// <summary>
    /// Initializes necessary unity components at the start.
    /// </summary>
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Called when dragging begins. Disables raycasting and lowers the opacity of the object to provide visual feedback. Temporarily changes the parent to keep it on top of other UI elements.
    /// </summary>
    /// <param name="eventData">PointerEventData associated with the drag event.</param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = DRAGGING_ALPHA;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        taskImage.raycastTarget = false;
    }

    /// <summary>
    /// Called during the dragging process. Updates the position of the object based on mouse movement.
    /// </summary>
    /// <param name="eventData">PointerEventData associated with the drag event.</param>
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    /// <summary>
    /// Called when the dragging ends. Restores raycasting, resets the opacity, and returns the object to its original parent.
    /// </summary>
    /// <param name="eventData">PointerEventData associated with the drag event.</param>
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = NORMAL_ALPHA;
        transform.SetParent(parentAfterDrag);
        taskImage.raycastTarget = true;
        transform.SetAsLastSibling();
    }
}