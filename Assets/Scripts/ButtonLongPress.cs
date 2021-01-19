using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonLongPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField]
    [Tooltip("How long must pointer be down on this object to trigger a long press")]
    private float holdTime = 1f;

    // Remove all comment tags (except this one) to handle the onClick event!
    private bool held = false;
    private bool canceled = false;
    private bool completed = false;
    public UnityEvent onClick = new UnityEvent();
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onLongPressUp = new UnityEvent();
    public UnityEvent onLongPressExit = new UnityEvent();

    public void OnPointerDown(PointerEventData eventData)
    {
        held = false;
        canceled = false;
        completed = false;
        Invoke("OnLongPress", holdTime);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!canceled) {
            CancelInvoke("OnLongPress");

            if (!held) {
                onClick.Invoke();
            } else {
                onLongPressUp.Invoke();
            }

            completed = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!completed) {
            CancelInvoke("OnLongPress");

            if (held) {
                onLongPressExit.Invoke();
            }

            canceled = true;
        }
    }

    private void OnLongPress()
    {
        held = true;
        onLongPress.Invoke();
    }
}