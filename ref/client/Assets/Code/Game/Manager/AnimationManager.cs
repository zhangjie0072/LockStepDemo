using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

public class AnimationManager
{
    public class PlayInfo
    {
        public string clipName;
        public AnimData animData;
        public int keyFrameIndex1;
        public int keyFrameIndex2;
        public IM.Number time;
        public IM.Number realTime { get { return CalcRealTime(time, animData.duration, animData.wrapMode); } }
        public IM.Number normalizedTime { get { return time / animData.duration; } }
        public IM.Number blendWeight;
        public IM.Number speed = IM.Number.one;
        public bool isPlaying;
        public bool enableRootMotion;
        public IM.RootMotion rootMotion;

        public void Reset()
        {
            speed = IM.Number.one;
            time = IM.Number.zero;
            blendWeight = IM.Number.zero;
            keyFrameIndex1 = 0;
            keyFrameIndex2 = 0;
            isPlaying = false;
        }
    }

    static IM.Number CROSS_FADE_LENGTH = new IM.Number(0, 300);

    Player _player;
    PlayerActionEventHandler _eventHandler;
    Animation _animation;
    Dictionary<string, PlayInfo> _infos = new Dictionary<string, PlayInfo>();
    LinkedList<PlayInfo> _prevInfos = new LinkedList<PlayInfo>();
    PlayInfo _curInfo;
    public PlayInfo curPlayInfo { get { return _curInfo; } }
    bool _ignoreCurrent = false;

    public AnimationManager(Player player)
    {
        _player = player;
        _eventHandler = player.eventHandler;
        _animation = player.gameObject.GetComponent<Animation>();
    }

    public AnimationManager(Animation animation, PlayerActionEventHandler eventHandler)
    {
        _eventHandler = eventHandler;
        _animation = animation;
    }

    public PlayInfo GetPlayInfo(string clip)
    {
        PlayInfo playInfo = null;
        if (!_infos.TryGetValue(clip, out playInfo))
        {
            if (_animation[clip] == null)
                return null;
            playInfo = new PlayInfo();
            playInfo.clipName = clip;
            playInfo.animData = AnimationSampleManager.Instance.GetAnimData(GetOriginName(clip));
            _infos.Add(clip, playInfo);
        }
        return playInfo;
    }

    public IM.RootMotion GetRootMotion(string clip)
    {
        PlayInfo info = GetPlayInfo(clip);
        if (info.rootMotion == null)
            info.rootMotion = new IM.RootMotion(_player);
        return info.rootMotion;
    }

    public void SetSpeed(string clip, IM.Number speed)
    {
        PlayInfo playInfo = GetPlayInfo(clip);
        playInfo.speed = speed;
        _animation[clip].speed = (float)speed;
    }

    public void SetSpeed(IM.Number speed)
    {
        _curInfo.speed = speed;
        _animation[_curInfo.clipName].speed = (float)speed;
        //Debug.Log("Set speed " + curPlayInfo.clipName + " " + speed + " " + _animation[curPlayInfo.clipName].speed);
    }

    public bool IsPlaying(string clip)
    {
        PlayInfo playInfo = GetPlayInfo(clip);
        //Debug.Log("IsPlaying of " + clip + " is " + playInfo.isPlaying);
        return playInfo.isPlaying;
    }

    public IM.Number GetFrameRate(string clip)
    {
        clip = GetOriginName(clip);
        AnimData animData = AnimationSampleManager.Instance.GetAnimData(clip);
        return animData.frameRate;
    }

    public IM.Number GetDuration(string clip)
    {
        clip = GetOriginName(clip);
        AnimData animData = AnimationSampleManager.Instance.GetAnimData(clip);
        return animData.duration;
    }

    public IM.Number GetSpeed(string clip)
    {
        PlayInfo playInfo = GetPlayInfo(clip);
        return playInfo.speed;
    }

    public string GetOriginName(string clip)
    {
        AnimationState state = _animation[clip];
        if (state == null)
            Debug.LogError("There is no animation: " + clip);
        return state.clip.name;
    }

    public PlayInfo Play(string clip, IM.Number speed, bool enableRootMotion)
    {
        Play(clip, enableRootMotion);
        SetSpeed(speed);
        return _curInfo;
    }
    public PlayInfo Play(string clip, bool enableRootMotion)
    {
        //Debug.Log(Time.frameCount + " AnimationManager, Play " + clip);

        foreach (PlayInfo playInfo in _prevInfos)
            playInfo.Reset();
        _prevInfos.Clear();
        if (_curInfo == null || clip != _curInfo.clipName)  // Play new animation
        {
            if (_curInfo != null)
                _curInfo.Reset();
            _curInfo = GetPlayInfo(clip);
            _curInfo.Reset();
        }
        if (enableRootMotion && _curInfo.rootMotion == null)
            _curInfo.rootMotion = new IM.RootMotion(_player);
        _curInfo.enableRootMotion = enableRootMotion;
        _curInfo.isPlaying = true;
        _curInfo.blendWeight = CROSS_FADE_LENGTH;
        _animation[clip].speed = (float)_curInfo.speed;
        _animation.Play(clip);
        _ignoreCurrent = true;
        return _curInfo;
    }

    public PlayInfo CrossFade(string clip, IM.Number speed, bool enableRootMotion)
    {
        CrossFade(clip, enableRootMotion);
        SetSpeed(speed);
        return _curInfo;
    }
    public PlayInfo CrossFade(string clip, bool enableRootMotion)
    {
        //Debug.Log(Time.frameCount + " AnimationManager, CrossFade " + clip);

        //*
        if (_curInfo != null && !IsPlaying(_curInfo.clipName) && _curInfo.clipName != clip)
        {
            //有新的动画进入混合时，如果当前的动画正在反播，将其速度设置为0，并设置到最一帧，重启播放。否则混合可能失效
            if (_curInfo.speed < IM.Number.zero)
            {
                //_curInfo.speed = IM.Number.zero;
                _curInfo.time = IM.Number.zero;
                _animation[_curInfo.clipName].speed = 0f;
                _animation[_curInfo.clipName].time = 0f;
            }
            else    //如果是正播，设置到最后一帧，速度设置为0
            {
                //_curInfo.speed = IM.Number.zero;
                _curInfo.time = _curInfo.animData.duration;
                _animation[_curInfo.clipName].speed = 0f;
                _animation[_curInfo.clipName].time = _animation[_curInfo.clipName].length;
            }
            _animation.Play(_curInfo.clipName);
        }
        //*/

        if (_curInfo == null || _curInfo.clipName != clip)
        {
            //当前进入Prev
            if (_curInfo != null)
                _prevInfos.AddLast(_curInfo);
            //新动画如果在Prev中，则移除
            _curInfo = GetPlayInfo(clip);
            _prevInfos.Remove(_curInfo);
        }

        _curInfo.isPlaying = true;
        if (enableRootMotion && _curInfo.rootMotion == null)
            _curInfo.rootMotion = new IM.RootMotion(_player);
        _curInfo.enableRootMotion = enableRootMotion;
        _animation[clip].speed = (float)_curInfo.speed;
        _animation.CrossFade(clip);
        _ignoreCurrent = true;
        return _curInfo;
    }
    public void Stop()
    {
        foreach (PlayInfo prev in _prevInfos)
            prev.Reset();
        if (_curInfo != null)
            _curInfo.Reset();
        _animation.Stop();
    }

    void GetNodePosAndRot(SampleNode node, out IM.Vector3 pos, out IM.Number horiAngle, bool calcRootMotion)
    {
        pos = IM.Vector3.zero;
        horiAngle = IM.Number.zero;
        if (node != SampleNode.Root && calcRootMotion)
            Debug.LogError("RootMotion must calc on Root node.");
        if (_curInfo == null)
            return;

        IM.Vector3 deltaMove = IM.Vector3.zero;
        IM.Number deltaAngle = IM.Number.zero;
        if (_curInfo.time < CROSS_FADE_LENGTH && _prevInfos.Count > 0)   // in cross fading
        {
            //Total weight
            IM.Number totalWeight = IM.Number.zero;
            foreach (PlayInfo prevInfo in _prevInfos)
                totalWeight += prevInfo.blendWeight;
            totalWeight += _curInfo.blendWeight;
            //RootMotion total weight
            IM.Number rmTotalWeight = IM.Number.zero;
            if (calcRootMotion)
            {
                foreach (PlayInfo prevInfo in _prevInfos)
                {
                    if (prevInfo.enableRootMotion)
                        rmTotalWeight += prevInfo.blendWeight;
                }
                if (_curInfo.enableRootMotion)
                    rmTotalWeight += _curInfo.blendWeight;
            }

            // Previous play info
            foreach (PlayInfo prevInfo in _prevInfos)
            {
                if (prevInfo.blendWeight <= IM.Number.zero)
                    continue;
                IM.Number ratio = prevInfo.blendWeight / totalWeight;
                IM.Vector3 position;
                IM.Number angle;
                GetNodePosAndRot(prevInfo, node, out position, out angle);
                pos += position * ratio;
                horiAngle += angle * ratio;
                /*
                Debug.Log(string.Format(Time.frameCount + 
                    " GetNodePos blend, {0} node:{8} weight:{1} {6} ratio:{2} pos:{3} speed:{4} time:{5} isPlaying:{7}",
                    GetOriginName(prevInfo.clipName), prevInfo.blendWeight, ratio, position,
                    _animation[prevInfo.clipName].speed, _animation[prevInfo.clipName].time,
                    _animation[prevInfo.clipName].weight, _animation.IsPlaying(prevInfo.clipName), node));
                //*/
                if (calcRootMotion && prevInfo.enableRootMotion)
                {
                    IM.Number rmRatio = prevInfo.blendWeight / rmTotalWeight;
                    prevInfo.rootMotion.Apply(position, angle, rmRatio);
                }
            }
            // Current play info
            IM.Number curRatio = _curInfo.blendWeight / totalWeight;
            IM.Vector3 curPosition;
            IM.Number curAngle;
            GetNodePosAndRot(_curInfo, node, out curPosition, out curAngle);
            pos += curPosition * curRatio;
            horiAngle += curAngle * curRatio;
            /*
                Debug.Log(string.Format(Time.frameCount + 
                    " GetNodePos blend, {0}, node:{8}, weight:{1} {4} ratio:{2} pos:{3} speed:{5} time:{6} isPlaying:{7}",
                    GetOriginName(_curInfo.clipName), _curInfo.blendWeight, curRatio, curPosition,
                    _animation[_curInfo.clipName].weight, _animation[_curInfo.clipName].speed,
                    _animation[_curInfo.clipName].time, _animation.IsPlaying(_curInfo.clipName), node));
            //*/
            if (calcRootMotion && _curInfo.enableRootMotion)
            {
                IM.Number rmRatio = _curInfo.blendWeight / rmTotalWeight;
                _curInfo.rootMotion.Apply(curPosition, curAngle, rmRatio);
            }
        }
        else
        {
            // No blend between 2 animation
            GetNodePosAndRot(_curInfo, node, out pos, out horiAngle);
            if (calcRootMotion && _curInfo.enableRootMotion)
            {
                _curInfo.rootMotion.Apply(pos, horiAngle, IM.Number.one);
            }
        }
        /*
        Debug.Log(string.Format(Time.frameCount + 
            " GetNodePos, clip:{0} node:{1} time:{2} - {3} pos:{4} key:{5} - {6}",
            _animation[_curInfo.clipName].clip.name, node, _curInfo.time,
            _animation[_curInfo.clipName].time,// % _animation[_curInfo.clipName].length,
            pos, _curInfo.keyFrameIndex1, _curInfo.keyFrameIndex2));
        //*/
        if (calcRootMotion)
        {
            pos = deltaMove;
            horiAngle = deltaAngle;
        }
    }

    static void GetNodePosAndRot(PlayInfo info, SampleNode node, out IM.Vector3 pos, out IM.Number horiAngle)
    {
        SampleData sample1 = info.animData.sampleDatas[info.keyFrameIndex1];
        IM.Vector3 pos1 = sample1.nodes[node].position;
        IM.Number angle1 = sample1.nodes[node].horiAngle;
        if (info.keyFrameIndex1 == info.keyFrameIndex2)
        {
            pos = pos1;
            horiAngle = angle1;
        }
        else
        {
            SampleData sample2 = info.animData.sampleDatas[info.keyFrameIndex2];
            IM.Vector3 pos2 = sample2.nodes[node].position;
            IM.Number angle2 = sample2.nodes[node].horiAngle;
            IM.Number t = (info.realTime - sample1.time) / (sample2.time - sample1.time);
            pos = IM.Vector3.Lerp(pos1, pos2, t);
            horiAngle = IM.Math.LerpAngle(angle1, angle2, t);
        }
    }

    public IM.Vector3 GetNodePosition(SampleNode node)
    {
        return GetNodePosition(node, false);
    }

    private IM.Vector3 GetNodePosition(SampleNode node, bool calcRootMotion)
    {
        IM.Vector3 pos;
        IM.Number horiAngle;
        GetNodePosAndRot(node, out pos, out horiAngle, calcRootMotion);
        return pos;
    }

    public static bool GetNodePosition(SampleNode node, string clip, IM.Number time, out IM.Vector3 position)
    {
        AnimData animData = AnimationSampleManager.Instance.GetAnimData(clip);
        return GetNodePosition(node, animData, time, out position);
    }

    public static bool GetNodePosition(SampleNode node, string clip, int frame, out IM.Vector3 position)
    {
        AnimData animData = AnimationSampleManager.Instance.GetAnimData(clip);
        IM.Number time = frame / animData.frameRate;
        return GetNodePosition(node, animData, time, out position);
    }

    public static bool GetNodePosition(SampleNode node, AnimData animData, IM.Number time, out IM.Vector3 position)
    {
        int count = animData.sampleDatas.Count;
        for (int i = 0; i < count; ++i)
        {
            SampleData sample1 = animData.sampleDatas[i];
            if (time == sample1.time)
            {
                position = sample1.nodes[node].position;
                return true;
            }
            else if (time > sample1.time && i < count - 1)
            {
                SampleData sample2 = animData.sampleDatas[i + 1];
                if (time < sample2.time)
                {
                    IM.Vector3 pos1 = sample1.nodes[node].position;
                    IM.Vector3 pos2 = sample2.nodes[node].position;
                    IM.Number t = (time - sample1.time) / (sample2.time - sample1.time);
                    position = IM.Vector3.Lerp(pos1, pos2, t);
                    return true;
                }
            }
        }
        position = IM.Vector3.zero;
        return false;
    }

    public void GetRootPosAndRot(out IM.Vector3 pos, out IM.Number horiAngle, bool calcRootMotion)
    {
        GetNodePosAndRot(SampleNode.Root, out pos, out horiAngle, calcRootMotion);
    }

    public void Update(IM.Number deltaTime)
    {
        if (_animation == null || deltaTime == IM.Number.zero)
            return;

        //更新先前的动画
        foreach (PlayInfo prevInfo in _prevInfos)
        {
            if (!_ignoreCurrent)
                prevInfo.blendWeight -= deltaTime;
            UpdatePlayInfo(prevInfo, deltaTime);
        }
        //移除淡出完成
        List<PlayInfo> recycle = new List<PlayInfo>();
        foreach (PlayInfo playInfo in _prevInfos)
        {
            if (playInfo.blendWeight <= IM.Number.zero)
            {
                recycle.Add(playInfo);
                playInfo.Reset();
            }
        }
        foreach (PlayInfo playInfo in recycle)
        {
            _prevInfos.Remove(playInfo);
        }
        //更新当前动画
        if (_curInfo != null && !_ignoreCurrent)
        {
            UpdatePlayInfo(_curInfo, deltaTime);
            _curInfo.blendWeight = IM.Math.Min(CROSS_FADE_LENGTH, _curInfo.blendWeight + deltaTime);
        }
        _ignoreCurrent = false;

        //更新玩家数据（几个骨骼位置和球的位置）
        if (_player != null)
        {
            UpdatePlayer();
        }
    }

    void UpdatePlayer()
    {
        //Root
        bool calcRootMotion = false;
        foreach (PlayInfo prev in _prevInfos)
        {
            if (prev.enableRootMotion)
                calcRootMotion = true;
        }
        if (_curInfo.enableRootMotion)
            calcRootMotion = true;
        if (calcRootMotion)
        {
            IM.Vector3 deltaMove;
            IM.Number deltaAngle;
            GetRootPosAndRot(out deltaMove, out deltaAngle, true);
        }
        IM.Vector3 playerPos = _player.position;
        IM.Quaternion playerRotation = _player.rotation;
        IM.Vector3 playerScale = _player.scale;
        _player.rootPos = playerPos + playerRotation * _player.rootLocalPos * playerScale;
        //Ball
        _player.ballSocketLocalPos = GetNodePosition(SampleNode.Ball);
        _player.ballSocketPos = _player.TransformNodePosition(SampleNode.Ball, _player.ballSocketLocalPos);
        if (_player.m_bWithBall)
            _player.m_ball._position = _player.ballSocketPos;
        //RHand
        _player.rHandLocalPos = GetNodePosition(SampleNode.RHand);
        _player.rHandPos = _player.TransformNodePosition(SampleNode.RHand, _player.rHandLocalPos);
        //Pelvis
        _player.pelvisLocalPos = GetNodePosition(SampleNode.Pelvis);
        _player.pelvisPos = _player.TransformNodePosition(SampleNode.Pelvis, _player.pelvisLocalPos);
    }

    void UpdatePlayInfo(PlayInfo playInfo, IM.Number deltaTime)
    {
        IM.Number prevRealTime = playInfo.realTime;
        IM.Number prevTime = playInfo.time;

        //更新时间
        playInfo.time += deltaTime * playInfo.speed;
        // Trigger event
        if (TriggerEvent(playInfo, deltaTime, _eventHandler))
            FixView(_animation, playInfo);//回退了时间，强制修正显示层
        IM.Number realDeltaTime = playInfo.time - prevTime;   //可能在事件上停留，实际的deltaTime可能不同

        //动画wrap时修正显示层位置
        IM.Number duration = playInfo.animData.duration;
        IM.Number curRealTimeNoWrap = prevRealTime + realDeltaTime;
        if (curRealTimeNoWrap <= IM.Number.zero || curRealTimeNoWrap >= duration)
        {
            //修正显示层位置
            if (playInfo.animData.wrapMode == WrapMode.Loop)//TODO 非Loop动作在播动作开始时修正
                FixView(_animation, playInfo);
            else
                playInfo.isPlaying = false;
        }
        // Update frame index
        CalcKeyFrameIndex(playInfo);
    }

    //修正显示层位置
    static void FixView(Animation animation, PlayInfo playInfo)
    {
        //Debug.Log(string.Format("Fix view, clip:{0} time:{1} -> {2}",
        //    playInfo.clipName, animation[playInfo.clipName].time, playInfo.time));
        animation[playInfo.clipName].time = (float)playInfo.time;
        animation[playInfo.clipName].clip.SampleAnimation(animation.gameObject, (float)playInfo.time);
    }

    //返回值：是否回退了时间(若下一帧需要停留在事件上，则需要回退一段时间。)
    static bool TriggerEvent(PlayInfo playInfo, IM.Number deltaTime, PlayerActionEventHandler handler)
    {
        System.Type type = handler.GetType();
        List<AnimEventData> events = GetAnimEvent(playInfo.animData.eventDatas,
            playInfo.realTime - deltaTime * playInfo.speed, playInfo.realTime);
        foreach (AnimEventData evt in events)
        {
            MethodInfo method = type.GetMethod(evt.funcName);
            if (method != null)
            {
                IM.Number prevSpeed = playInfo.speed;   //处理事件前，动画速度
                //组织事件参数
                ParameterInfo[] parameters = method.GetParameters();
                if (parameters.Length == 0)
                    method.Invoke(handler, null);
                else
                {
                    object[] objs = new object[parameters.Length];
                    for (int i = 0; i < parameters.Length; ++i)
                    {
                        ParameterInfo info = parameters[i];
                        if (info.ParameterType == typeof(string))
                            objs[i] = evt.stringParameter;
                        else if (info.ParameterType == typeof(int))
                            objs[i] = evt.intParameter;
                        else if (info.ParameterType == typeof(IM.Number))
                            objs[i] = evt.floatParameter;
                    }
                    method.Invoke(handler, objs);
                }
                IM.Number curSpeed = playInfo.speed;    //处理事件后，动画速度

                if (curSpeed != prevSpeed)  // 事件处理中改变了播放速度
                {
                    IM.Number prevSign = (prevSpeed == IM.Number.zero ? IM.Number.zero : IM.Math.Sign(prevSpeed));
                    IM.Number curSign = (curSpeed == IM.Number.zero ? IM.Number.zero : IM.Math.Sign(curSpeed));
                    if (prevSign != curSign)    // 动画播放被停止或反向，下一帧停在该事件上，且不再处理后续事件
                    {
                        //计算回退时间
                        //TODO 考虑Wrap的情况
                        IM.Number reverseTime = playInfo.realTime - evt.time;
                        playInfo.time -= reverseTime;
                        return true;
                    }
                }
            }
        }
        return false;
    }

    static List<AnimEventData> GetAnimEvent(List<AnimEventData> events, IM.Number time1, IM.Number time2)
    {
        List<AnimEventData> evts = new List<AnimEventData>();
        if (events != null)
        {
            foreach (AnimEventData evt in events)
            {
                if (time1 < evt.time && evt.time <= time2)
                    evts.Add(evt);
                else if (time2 < time1)     // Animation play looped
                {
                    if (evt.time <= time2 || evt.time > time1)
                        evts.Add(evt);
                }
            }
        }
        return evts;
    }

    static IM.Number CalcRealTime(IM.Number elapsedTime, IM.Number duration, WrapMode wrapMode)
    {
        IM.Number realTime = IM.Number.zero;
        if (wrapMode == WrapMode.Once)
            realTime = IM.Math.Min(duration, elapsedTime);
        else if (wrapMode == WrapMode.Loop)
            realTime = elapsedTime % duration;
        else
            Debug.LogError("Unsupported wrap mode: " + wrapMode);
        return realTime;
    }

    static void CalcKeyFrameIndex(PlayInfo playInfo)
    {
        for (int i = 0; i < playInfo.animData.sampleDatas.Count; ++i)
        {
            SampleData sample = playInfo.animData.sampleDatas[i];
            if (sample.time <= playInfo.realTime)
            {
                playInfo.keyFrameIndex1 = i;
                playInfo.keyFrameIndex2 = i + 1;
            }
            else
                break;
        }

        playInfo.keyFrameIndex1 = System.Math.Max(0, playInfo.keyFrameIndex1);
        playInfo.keyFrameIndex2 = System.Math.Min(playInfo.animData.sampleDatas.Count - 1, playInfo.keyFrameIndex2);
    }
}
