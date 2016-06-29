namespace IM
{
    public interface IRootMotionTarget
    {
        void ResetRoot();
        void Apply(Vector3 movement, Quaternion rotation);
        Quaternion GetInitRotation();
    }

    public class RootMotion
    {
        public Number scale;
        public Vector3 dirMove;

        Vector3 _lastPos;
        Number _lastAngle;
        Quaternion _initRot;

        IRootMotionTarget _target;

        public RootMotion(IRootMotionTarget target)
        {
            _target = target;
        }

        public void Reset()
        {
            _lastPos = Vector3.zero;
            _lastAngle = Number.zero;
            scale = Number.one;
            _initRot = _target.GetInitRotation();
        }

        public void Apply(Vector3 curPos, Number curAngle, IM.Number ratio)
        {
            //Debug.Log(string.Format("CalcRootMotion, LastPos:{0} CurPos:{1} LastAngle:{2} CurAngle:{3} Scale:{4}",
            //    _lastPos, curPos, _lastAngle, curAngle, scale));
            _target.ResetRoot();

            Vector3 deltaMoveRoot = Vector3.zero;
            if (!Number.Approximately(scale, Number.zero))
            {
                deltaMoveRoot = (curPos - _lastPos) * scale;
            }

            _lastPos = curPos;

            IM.Number deltaAngle = IM.Math.DeltaAngle(_lastAngle, curAngle);
            _lastAngle = curAngle;

            IM.Vector3 deltaMove;
            Vector3 velocity = deltaMoveRoot.normalized;
            Number magnitude = deltaMoveRoot.magnitude;
            if (dirMove != Vector3.zero)
            {
                //TODO Implement Quaternion.LookRotaion
                Number angle = Vector3.FromToAngle(Vector3.forward, dirMove);
                Quaternion rot = Quaternion.Euler(Number.zero, angle, Number.zero);
                deltaMove = rot * velocity * magnitude;
            }
            else
                deltaMove = _initRot * velocity * magnitude;

            //Debug.Log(string.Format("CalcRootMotion, DeltaMoveRoot{0} DeltaMove{1} DeltaAngle{2} Ratio:{3} DirMove:{4}", 
            //    deltaMoveRoot, deltaMove, deltaAngle, ratio, dirMove));

            _target.Apply(deltaMove * ratio, Quaternion.Euler(IM.Number.zero, deltaAngle, IM.Number.zero));
        }
    }
}
