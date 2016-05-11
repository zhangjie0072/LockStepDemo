using UE = UnityEngine;

namespace IM.Test
{
    public static class Debug
    {
        public static void DrawLine(string name, UE.Vector3 from, UE.Vector3 to, UE.Color color)
        {
            UE.GameObject go = UE.GameObject.CreatePrimitive(UE.PrimitiveType.Cylinder);
            go.name = name;
            go.transform.localScale = new UE.Vector3(0.5f, (to - from).magnitude / 2, 0.5f);
            go.transform.position = (from + to) / 2;
            go.transform.up = (to - from).normalized;
            go.GetComponent<UE.Renderer>().material.color = color;
        }
    }
}
