using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement
{
	public enum Type
	{
		eRunWithoutBall = 0,
		eRunWithBall,
		eRushWithoutBall,
		eRushWithBall,
		eDefense,
		eBackToBackRun,

		eMax = 6,
	}

	public class MoveAttribute
	{
		public IM.Number m_curSpeed	 = IM.Number.one;
		public IM.Number m_initSpeed = IM.Number.one;
		public IM.Number m_playSpeed = IM.Number.one;
		public IM.Number m_TurningSpeed = new IM.Number(1, 500);
	}

	public Type				mType{ get; private set; }
	public MoveAttribute 	mAttr{ get; private set; }

	public PlayerMovement(Type type)
	{
		mType = type;
		mAttr = new MoveAttribute();
	}
}

public class PlayerAnimAttribute
{
	public class KeyFrame
	{
		public string	id{get; protected set;}
		public int		frame{get; protected set;}

		public KeyFrame(string keyId, int keyFrame)
		{
			id = keyId;
			frame = keyFrame;
		}
	}

	public class AnimAttr
	{
		public string 			strAnim;
		public List<KeyFrame>	keyFrame = new List<KeyFrame>();

		public KeyFrame GetKeyFrame( string id )
		{
			return keyFrame.Find( delegate(KeyFrame kf){ return kf.id == id; } );
		}
	}

	public class KeyFrame_MoveToStartPos
		: KeyFrame
	{
		public IM.Vector3	toBasketOffset{get; protected set;}
		public KeyFrame_MoveToStartPos(string id, int frame, IM.Vector3 inToBasketOffset)
			: base(id, frame)
		{
			toBasketOffset = inToBasketOffset;
		}
	}

	public class KeyFrame_LayupShootPos
		: KeyFrame
	{
		public IM.Vector3	toBasketOffset{get; protected set;}
		public KeyFrame_LayupShootPos(string id, int frame, IM.Vector3 inToBasketOffset)
			: base(id, frame)
		{
			toBasketOffset = inToBasketOffset;
		}
	}

	public class KeyFrame_Blockable
		: KeyFrame
	{
		public int blockFrame;
		public KeyFrame_Blockable(string id, int frame, int inBlockframe)
			: base(id, frame)
		{
			blockFrame = inBlockframe;
		}
	}

	public class KeyFrame_DefenderSpasticity
		: KeyFrame
	{
		public int frameLength;
		public KeyFrame_DefenderSpasticity(string id, int frame, int length)
			: base(id, frame)
		{
			frameLength = length;
		}
	}
	
	public class KeyFrame_RotateToBasketAngle
		: KeyFrame
	{
		public IM.Number angle;
		public KeyFrame_RotateToBasketAngle(string id, int frame,IM.Number angle)
			: base(id, frame)
		{
			this.angle = angle;
		}
	}


	public Dictionary<string, AnimAttr> m_dunkNear{get;private set;}
	public Dictionary<string, AnimAttr> m_dunkFar{get;private set;}
	public Dictionary<string, AnimAttr> m_layupNear{get;private set;}
	public Dictionary<string, AnimAttr> m_layupFar{get;private set;}

	public Dictionary<string, AnimAttr> m_rebound{get;private set;}

	public Dictionary<string, AnimAttr> m_catch{get;private set;}
	public Dictionary<string, AnimAttr> m_block{get;private set;}
	public Dictionary<string, AnimAttr> m_shoot{get;private set;}

	public Dictionary<string, AnimAttr> m_crossOver{get;private set;}
	public Dictionary<string, AnimAttr> m_backToBack{get;private set;}

	public Dictionary<string, AnimAttr> m_interception{get;private set;}

	public PlayerAnimAttribute()
	{
		m_dunkNear 	= new Dictionary<string, AnimAttr>();
		m_dunkFar 	= new Dictionary<string, AnimAttr>();

		m_layupNear = new Dictionary<string, AnimAttr>();
		m_layupFar 	= new Dictionary<string, AnimAttr>();

		m_catch 	= new Dictionary<string, AnimAttr>();
		m_block 	= new Dictionary<string, AnimAttr>();
		m_rebound 	= new Dictionary<string, AnimAttr>();
		m_shoot 	= new Dictionary<string, AnimAttr>();

		m_crossOver = new Dictionary<string, AnimAttr>();
		m_backToBack = new Dictionary<string, AnimAttr>();

		m_interception = new Dictionary<string, AnimAttr>();
	}

	public AnimAttr GetAnimAttrById(Command cmd, string id)
	{
		AnimAttr animAttr = new AnimAttr();
		if( cmd == Command.Dunk )
		{
			if( m_dunkNear.TryGetValue(id, out animAttr) )
				return animAttr;
			if( m_dunkFar.TryGetValue(id, out animAttr) )
				return animAttr;
		}
		else if( cmd == Command.Layup )
		{
			if( m_layupNear.TryGetValue(id, out animAttr) )
				return animAttr;
			if( m_layupFar.TryGetValue(id, out animAttr) )
				return animAttr;
		}
		else if( cmd == Command.Shoot )
		{
			if( m_shoot.TryGetValue(id, out animAttr) )
				return animAttr;
		}
		else if( cmd == Command.CrossOver )
		{
			if (m_crossOver.TryGetValue(id, out animAttr))
				return animAttr;
		}
		else if( cmd == Command.BackToBack )
		{
			if (m_backToBack.TryGetValue(id, out animAttr))
				return animAttr;
		}
		else if( cmd == Command.Interception )
		{
			if (m_interception.TryGetValue(id, out animAttr))
			    return animAttr;
		}
		return animAttr;
	}

}