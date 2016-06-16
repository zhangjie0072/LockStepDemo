using System;
using UE = UnityEngine;

namespace IM
{
    public class Transform
    {
        public Vector3 position { get; private set; }

        public Quaternion rotation { get; private set; }
        
        public Transform()
        {

        }
        public Transform(Vector3 pos)
        {
            this.position = pos;
        }

        public Transform(Vector3 pos,Quaternion rot)
        {
            this.position = pos;
            this.rotation = rot;
        }

        //public Transform(UE.Transform tans)
        //{
        //    //this.position = new Vector3(tans.localPosition);
        //    //this.rotation = new Quaternion(tans.localRotation);
        //}

        public void SetPosition(Vector3 pos)
        {
            this.position = pos;
        }

        public void SetRotaion(Quaternion rot)
        {
            this.rotation = rot;
        }
    }
}
