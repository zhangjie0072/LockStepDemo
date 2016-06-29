using UnityEngine;
using System.Collections;
using System.Threading;

namespace IM.Test
{

    public class LongTimeTest : MonoBehaviour
    {
        UnitTest[] tests;
        Thread thread;

        void Awake()
        {
            if (!enabled)
                return;

            tests = GetComponentsInChildren<UnitTest>();
            foreach (UnitTest test in tests)
            {
                test.autoStart = false;
                test.stopAtFail = false;
            }

            //Application.RegisterLogCallbackThreaded(HandleLog);
            thread = new Thread(Test);
            UnityEngine.Debug.Log("Start testing thread.");
            thread.Start();
        }

        void Update() { }

        void OnDestroy()
        {
            Utils.interruptTest = true;
            //Application.RegisterLogCallbackThreaded(null);
        }

        void Test()
        {
            foreach (UnitTest test in tests)
            {
                test.Test(true);
                if (Utils.interruptTest)
                {
                    UnityEngine.Debug.Log("Test interrupted.");
                    break;
                }
            }
        }
        public void HandleLog(string logString, string stackTrace, LogType type)
        {
            ErrorDisplay.Instance.HandleLog(logString, stackTrace, type);
        }
    }
}