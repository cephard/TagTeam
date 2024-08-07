using UnityEngine;
using System.Collections.Generic;

public class DroppedTaskManager
{
    private Stack<DragDropAction> actionStack = new Stack<DragDropAction>();
    private RectTransform slotRectTransform;
    public RectTransform DraggedRectTransform { get; private set; }
    public Vector2 OriginalDraggedPosition { get; private set; }
    public Vector2 OriginalSlotPosition { get; private set; }

    public DroppedTaskManager(RectTransform draggedRectTransform, Vector2 originalDraggedPosition, Vector2 originalSlotPosition, RectTransform slotRectTransform)
    {
        DraggedRectTransform = draggedRectTransform;
        OriginalDraggedPosition = originalDraggedPosition;
        OriginalSlotPosition = originalSlotPosition;
        this.slotRectTransform = slotRectTransform;
    }

    public void RecordAction()
    {
        actionStack.Push(new DragDropAction
        {
            DraggedRectTransform = DraggedRectTransform,
            OriginalDraggedPosition = DraggedRectTransform.anchoredPosition,
            OriginalSlotPosition = slotRectTransform.anchoredPosition
        });
    }

    public void UndoLastAction()
    {
        if (actionStack.Count > 0)
        {
            DragDropAction lastAction = actionStack.Pop();
            lastAction.DraggedRectTransform.anchoredPosition = lastAction.OriginalDraggedPosition;
            slotRectTransform.anchoredPosition = lastAction.OriginalSlotPosition;
        }
    }

    private class DragDropAction
    {
        public RectTransform DraggedRectTransform { get; set; }
        public Vector2 OriginalDraggedPosition { get; set; }
        public Vector2 OriginalSlotPosition { get; set; }
    }
}
