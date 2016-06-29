using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// For Create Item Async.
public class AsyncItem: MonoBehaviour
{
    Coroutine _coroutine = null;

    int _itemCount = 0;
    bool _isStart = false;
    public System.Func<int, Transform, GameObject> OnCreateItem = null;
    Transform _transform = null;


    void Awake()
    {
        _transform = transform;


    }

    System.Collections.IEnumerator LoadItems()
    {
        yield return null;
        for( int index = 0; index < _itemCount; index++)
        {
            if( OnCreateItem == null )
            {
                break;
            }
            OnCreateItem(index, _transform);
            yield return null;
        }
        _coroutine = null;
    }

    void LateUpdate()
    {
        if( _isStart )
        {
            _isStart = false;
            StartLoad();
        }
    }

    void StartLoad()
    {
        if( !NGUITools.GetActive(_transform.gameObject))
        {
            return;
        }

        if( OnCreateItem == null )
        {
            return;
        }

        _coroutine = StartCoroutine(LoadItems());
    }

    public void SetItemsCounter ( int itemCounter )
    {
        _itemCount = itemCounter;
        _isStart = true;
    }

}
