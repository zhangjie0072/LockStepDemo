using UnityEngine;
using System.Collections.Generic;

namespace IM
{
    public abstract class UnitTest : MonoBehaviour
    {
        public bool autoStart = true;
        public bool stopAtFail = true;

        protected delegate bool TestFunc(bool longTime);
        struct Step
        {
            public string name;
            public TestFunc test;
        }

        List<Step> steps = new List<Step>();

        public abstract string Name();

        public abstract void PrepareSteps();

        protected void AddStep(string name, TestFunc func)
        {
            steps.Add(new Step { name = name, test = func });
        }

        void Start()
        {
            if (autoStart)
                Test(false);
        }

        public void Test(bool longTime)
        {
            PrepareSteps();
            Logger.Log("--->IMath test, Begin unit: " + Name());
            foreach (Step step in steps)
            {
                Logger.Log("*******>IMath test, Begin step: " + step.name);
                bool passed = step.test(longTime);
                if (passed)
                    Logger.Log("<*******IMath test, End step: " + step.name + " passed.");
                else
                {
                    Logger.LogError("<*******IMath test, End step: " + step.name + " failed.");
                    if (stopAtFail)
                        break;
                }
                if (IM.Test.Utils.interruptTest)
                    break;
            }
            Logger.Log("<---IMath test, End unit: " + Name());
        }
    }
}
