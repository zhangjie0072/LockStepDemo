using UnityEngine;
using System.Collections;
using fogs.proto.msg;
public class LocationManager : MonoSingleton<LocationManager>
{
    //public delegate void OnFinished(MyLocation vVector);
    //public OnFinished _onFinished;

    //public void  GetLocationPoint(OnFinished onFinished ) 
    //{
    //    StartCoroutine(GetPlayer(onFinished));
    //}

    //private IEnumerator GetPlayer(OnFinished onFinished) 
    //{
    //    MyLocation result = new MyLocation();
    //    result.latitude = 0;
    //    result.longitude = 0;
    //    if (!Input.location.isEnabledByUser) 
    //    {
    //        onFinished(result);
    //        if (_onFinished != null)
    //        {
    //            result.latitude = Input.location.lastData.latitude;
    //            result.longitude = Input.location.lastData.longitude;
    //            _onFinished(result);
    //        }
    //        yield break;
    //    }
    //    Input.location.Start();
    //    int maxWait = 10;
    //    while(Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
    //    {
    //        yield return new WaitForSeconds (1);
    //        maxWait--;
    //    }

    //    if (maxWait < 1)
    //    {
    //        onFinished(result);
    //        if (_onFinished != null)
    //        {
    //            result.latitude = Input.location.lastData.latitude;
    //            result.longitude = Input.location.lastData.longitude;
    //            _onFinished(result);
    //        }
    //        Input.location.Stop();
    //        yield break;
    //    }

    //    if (Input.location.status == LocationServiceStatus.Failed)
    //    {
    //        result.latitude = Input.location.lastData.latitude;
    //        result.longitude = Input.location.lastData.longitude;
    //        onFinished(result);
    //        if (_onFinished != null)
    //        {
    //            _onFinished(result);
    //        }
    //        Input.location.Stop();
    //        yield break;
    //    }
    //    else 
    //    {
    //        result.longitude = Input.location.lastData.longitude;
    //        result.latitude  = Input.location.lastData.latitude;
    //    }
    //    onFinished(result);
    //    if (_onFinished != null)
    //    {
    //        _onFinished(result);
    //    }
    //    Input.location.Stop();
    //}
}
