using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionHorizontalInput : MonoBehaviour, IMoveHandler {

    public UnityEvent leftEvent;
    public UnityEvent rightEvent;

    public void OnMove(AxisEventData eventData)
    {
        switch (eventData.moveDir)
        {
            case MoveDirection.Left:
                leftEvent.Invoke();
                break;

            case MoveDirection.Right:
                rightEvent.Invoke();
                break;

        }
    }

}
