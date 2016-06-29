using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public sealed class Scheduler
{

	public static readonly Scheduler Instance = new Scheduler();

	private class FrameScheduler
	{
		public uint ID = 0;
		public uint Frame = 0;
		public uint RealFrame = 0;
		public bool IsLoop = false;
		public System.Action Callback = null;
	}
	private List<FrameScheduler> _frameDelegates;

	private class TimeScheduler
	{
		public uint ID = 0;
		public float RealTime = 0.0F;
		public float Time = 0.0f;
		public bool IsLoop = false;
		public System.Action Callback = null;
	}
	private List<TimeScheduler> _timeSchedulers;

	private List<System.Action> _updateScheduler;

	private uint _curFrame;

	private uint _curAllotID;

	/// <summary>
	/// 构造
	/// </summary>
	private Scheduler()
	{
		_curFrame = 0;
		_curAllotID = 0;
		_frameDelegates = new List<FrameScheduler>();
		_timeSchedulers = new List<TimeScheduler>();
		_updateScheduler = new List<System.Action>();
	}

	~Scheduler()
	{
		_frameDelegates.Clear();
		_frameDelegates = null;
		_timeSchedulers.Clear();
		_timeSchedulers = null;
		_updateScheduler.Clear();
		_updateScheduler = null;
	}

	/// <summary>
	/// 更新
	/// </summary>
	public void Update()
	{
		++_curFrame;

		UpdateFrameScheduler();

		UpdateTimeScheduler();

		UpdateUpdator();
	}

	private void UpdateUpdator()
	{
		if (_updateScheduler.Count <= 0)
		{
			return;
		}

		for (var i = 0; i < _updateScheduler.Count; ++i)
		{
			_updateScheduler[i]();
		}
	}

	private void UpdateFrameScheduler()
	{
		for (var i = 0; i < _frameDelegates.Count; )
		{
			FrameScheduler obj = _frameDelegates[i];
			if (obj.RealFrame <= _curFrame)
			{
				obj.Callback();
				if (obj.IsLoop)
				{
					obj.RealFrame += obj.Frame;
				}
				else
				{
					_frameDelegates.RemoveAt(i);
					continue;
				}
			}
			++i;
		}
	}

	private void UpdateTimeScheduler()
	{
		for (var i = 0; i < _timeSchedulers.Count; )
		{
			TimeScheduler obj = _timeSchedulers[i];

			if (obj.RealTime <= Time.time)
			{
				if(obj.Callback!=null)
					obj.Callback();
				else{
					_timeSchedulers.RemoveAt(i);
					continue;
				}
				if (obj.IsLoop)
				{
					obj.RealTime += obj.Time;
				}
				else
				{
					_timeSchedulers.RemoveAt(i);
					continue;
				}
			}
			++i;
		}
	}

	public void AddFrame(uint frame, bool loop, System.Action callback)
	{
		++_curAllotID;
		var frameScheduler = new FrameScheduler
		{
			ID = _curAllotID,
			Frame = frame,
			RealFrame = frame + _curFrame,
			IsLoop = loop,
			Callback = callback
		};
		_frameDelegates.Add(frameScheduler);
	}

	public void RemoveFrame(System.Action callback)
	{
		for (var i = 0; i < _frameDelegates.Count; ++i)
		{
			var deleData = _frameDelegates[i];
			if (deleData.Callback == callback)
			{
				_frameDelegates.RemoveAt(i);
				break;
			}
		}
	}

	public void AddTimer(float time, bool loop, System.Action callback)
	{
		++_curAllotID;
		var timeScheduler = new TimeScheduler
		{
			ID = _curAllotID,
			Time = time,
			RealTime = time + Time.time,
			IsLoop = loop,
			Callback = callback
		};
		_timeSchedulers.Add(timeScheduler);
	}

	public void RemoveTimer(System.Action callback)
	{
		for (var i = 0; i < _timeSchedulers.Count; ++i)
		{
			var deleData = _timeSchedulers[i];
			if (deleData.Callback == callback)
			{
				_timeSchedulers.RemoveAt(i);
				break;
			}
		}
	}

	public void AddUpdator(System.Action callback)
	{
		_updateScheduler.Add(callback);
	}

	public void RemoveUpdator(System.Action callback)
	{
		_updateScheduler.Remove(callback);
	}

	public void ClearUpdator()
	{
		_updateScheduler.Clear();
	}
}
