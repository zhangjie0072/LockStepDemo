using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIAnchor))]

/* ***************************************************
 * 大厅界面使用描点，左右移动的时候会有重叠
 * 为了达到美术效果，写了个公式动态计算中间位置的内容
 * ***************************************************/
public class UIHall : MonoBehaviour
{
    void Start()
    {
        var anchor = GetComponent<UIAnchor>();

        float DesignWidth = 1280;
        float DesignHeight = 720;

        float height, width;
        if (Screen.height > Screen.width)
        {
            width = Screen.height;
            height = Screen.width;
        }
        else
        {
            width = Screen.width;
            height = Screen.height;
        }

        float ori = DesignWidth / DesignHeight;
        float des = width / height;

        float offset = (ori - des) * (0.03f / 0.4444f) * -1.0f - 0.5f;
        anchor.relativeOffset.x = offset;

        anchor.enabled = true;
    }
}
