using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleProportionallyToParent : MonoBehaviour {
    private Vector2 _lastSelfSize = Vector2.zero;
    private Vector3 _lastSelfPosition = Vector3.zero;
    private Vector2 _lastParentSize = Vector2.zero;
    private RectTransform _parentTransform = null;

    private void Awake() {
        if (transform.parent == null)
            return;
        _lastSelfSize = (transform as RectTransform).sizeDelta;
        _lastSelfPosition = (transform as RectTransform).localPosition;
        _parentTransform = transform.parent.GetComponent<RectTransform>();
        _lastParentSize = _parentTransform.sizeDelta;
    }

    private void LateUpdate() {
        if (_parentTransform && _parentTransform.sizeDelta != _lastParentSize) {
            var rectTransform = transform as RectTransform;
            Vector2 scaleChanges = _parentTransform.sizeDelta / _lastParentSize;
            rectTransform.sizeDelta = _lastSelfSize * scaleChanges;
            rectTransform.localPosition = _lastSelfPosition * scaleChanges;
            _lastParentSize = _parentTransform.sizeDelta;
            _lastSelfPosition = rectTransform.localPosition;
            _lastSelfSize = rectTransform.sizeDelta;
        }
    }
}
