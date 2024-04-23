using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HiddenObjectClickHandler : MonoBehaviour, IPointerClickHandler {
    public void OnPointerClick(PointerEventData eventData) {
        ServiceLocator.Instance.Get<EventBus>().Invoke(new HiddenObjectClickSignal(gameObject));
    }
}
