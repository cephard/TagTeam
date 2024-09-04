using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Manages dropped tasks by recording and undoing drag-and-drop actions.
/// </summary>
public class DroppedTaskManager
{
    private const int NO_ACTION = 0;
    private Stack<DragDropAction> actionStack = new Stack<DragDropAction>();
    private RectTransform slotRectTransform;

    /// <summary>
    /// Gets the RectTransform of the currently dragged task.
    /// </summary>
    public RectTransform DraggedRectTransform { get; private set; }

    /// <summary>
    /// Gets the original position of the dragged task before any action.
    /// </summary>
    public Vector2 OriginalDraggedPosition { get; private set; }

    /// <summary>
    /// Gets the original position of the slot where the task was placed.
    /// </summary>
    public Vector2 OriginalSlotPosition { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DroppedTaskManager"/> class.
    /// </summary>
    /// <param name="draggedRectTransform">The RectTransform of the dragged task.</param>
    /// <param name="originalDraggedPosition">The original position of the dragged task.</param>
    /// <param name="originalSlotPosition">The original position of the slot.</param>
    /// <param name="slotRectTransform">The RectTransform of the slot.</param>
    public DroppedTaskManager(RectTransform draggedRectTransform, Vector2 originalDraggedPosition, Vector2 originalSlotPosition, RectTransform slotRectTransform)
    {
        DraggedRectTransform = draggedRectTransform;
        OriginalDraggedPosition = originalDraggedPosition;
        OriginalSlotPosition = originalSlotPosition;
        this.slotRectTransform = slotRectTransform;
    }

    /// <summary>
    /// Records the current drag-and-drop action by pushing the task onto the action stack.
    /// </summary>
    public void RecordAction()
    {
        actionStack.Push(new DragDropAction
        {
            DraggedRectTransform = DraggedRectTransform,
            OriginalDraggedPosition = DraggedRectTransform.anchoredPosition,
            OriginalSlotPosition = slotRectTransform.anchoredPosition
        });
    }

    /// <summary>
    /// Undoes the most recent drag-and-drop action by reverting to the previous positions.
    /// </summary>
    public void UndoLastAction()
    {
        if (actionStack.Count > NO_ACTION)
        {
            DragDropAction lastAction = actionStack.Pop();
            lastAction.DraggedRectTransform.anchoredPosition = lastAction.OriginalDraggedPosition;
            slotRectTransform.anchoredPosition = lastAction.OriginalSlotPosition;
        }
    }

    /// <summary>
    /// Represents a single drag-and-drop action, storing the state before the action was performed.
    /// </summary>
    private class DragDropAction
    {
        public RectTransform DraggedRectTransform { get; set; }
        public Vector2 OriginalDraggedPosition { get; set; }
        public Vector2 OriginalSlotPosition { get; set; }
    }
}
