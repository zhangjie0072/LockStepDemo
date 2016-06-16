using UnityEngine;
using System.Collections;
using System;

public class UIModelRotate : MonoBehaviour
{
    private float speed = 0.5f;

    void Awake()
    {
        UIEventListener.Get(transform.parent.gameObject).onDrag += OnDragResp;
    }

    void OnDestroy()
    {
        UIEventListener.Get(transform.parent.gameObject).onDrag -= OnDragResp;
    }

    private void OnDragResp(GameObject go, Vector2 delta)
    {
        transform.Rotate(0, -delta.x * speed, 0);
    }
}
