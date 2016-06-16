using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
public class LabelReplace : MonoBehaviour {

	// Use this for initialization
    UILabel _label = null;
    
    void Awake()
    {
        _label = transform.GetComponent<UILabel>();
        _label.text = CommonFunction.GetConstString(_label.text);
    }
}
