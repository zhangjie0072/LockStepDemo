using UnityEngine;
using System;
using System.IO;

using System.Collections;
using System.Collections.Generic;

public class PlayerMaterialControl
{


    /*
    public enum PlayerState{
        none = 0,
        bati = 1,
        invincipal = 2,
        behit = 3,
        fury = 4,
        dead_white = 5
    }
	
    public struct StateDisplayConfig
    {
        public PlayerState 					state;
        public bool							bOrigalMtl;
        public UnityEngine.Object			mtlRes;
        public UnityEngine.Object			clipRes;
    };
	
    UPlayer mPlayer;
	
    public struct Part
    {
        public GameObject  mGO;
        public Material	mOriginalMtl;
        public Material mCopyMtl;
    }
	
    Part mHead;
    Part mBody;
	
    PlayerState mlastPlayerState;
	
    Dictionary<PlayerState, StateDisplayConfig>	mStateDisplayConfigs;
	
    float mTimeEllapsSinceHit = 10;
    bool mDeadWhite = false;
	
	
    public PlayerMaterialControl( UPlayer owner )
    {
        mPlayer = owner;
    }
	
    bool _ReadConfig( string strConfigPath, out Dictionary<PlayerState, StateDisplayConfig> configs )
    {
        TextAsset configFile = ResourceLoadManager.Instance.GetResources(strConfigPath) as TextAsset;
		
        configs = new Dictionary<PlayerState, StateDisplayConfig>();
        try
        {
            using(MemoryStream ms = new MemoryStream(configFile.bytes))
            {
                TextReader reader = new StreamReader(ms);
                string strFormatComment = reader.ReadLine();
				
                while(true)
                {
                    string strConfigContent = reader.ReadLine();
                    if( strConfigContent == null )
                        break;
                    strConfigContent = strConfigContent.Trim();
                    string[] strConfigItems = strConfigContent.Split(',');
                    foreach( string strConfigItem in strConfigItems )
                    {
                        strConfigItem.Replace('\t',' ');
                        strConfigItem.Trim();
                    }
					
                    StateDisplayConfig configItem = new StateDisplayConfig();
					
                    configItem.state   = _GetStateByString(strConfigItems[0]);
                    configItem.bOrigalMtl = strConfigItems[1] == "Default";
                    configItem.mtlRes  = _LoadMaterialByString(strConfigItems[1]);
                    configItem.clipRes = _LoadAnimByString(strConfigItems[2]);
					
                    configs.Add(configItem.state, configItem);
                }
				
                //none state
                StateDisplayConfig noneConfig = new StateDisplayConfig();
                noneConfig.state = PlayerState.none;
                noneConfig.clipRes = null;
                noneConfig.mtlRes = null;
                configs.Add(PlayerState.none, noneConfig);
            }
            return true;
        }
        catch(IOException exp)
        {
            Debug.LogError( "读取动作文件配置失败: " + exp.Message );
        }
        return false;
    }
	
    private PlayerState _GetStateByString( string strState )
    {
        PlayerState state;
        switch(strState)
        {
        case "Bati":
            state = PlayerState.bati;
            break;
        case "Invincipal":
            state = PlayerState.invincipal;
            break;
        case "Fury":
            state = PlayerState.fury;
            break;
        case "Behit":
            state = PlayerState.behit;
            break;	
        case "DeadWhite":
            state = PlayerState.dead_white;
            break;
        default:
            state = PlayerState.none;
            break;
        }
		
        return state;
    }
	
    private UnityEngine.Object _LoadMaterialByString( string strMaterial )
    {
        if( strMaterial == "Default" )
            return null;
		
        UnityEngine.Object mtlRes = ResourceLoadManager.Instance.GetResources("Object/Material/" + strMaterial);
        if( mtlRes == null )
            Debug.LogError("找不到材质: " + strMaterial);
        return mtlRes;
    }
	
    private UnityEngine.Object _LoadAnimByString( string strAnim )
    {
        UnityEngine.Object animRes = ResourceLoadManager.Instance.GetResources("Object/Effect/Animation/" + strAnim);
        if( animRes == null )
            Debug.LogError("找不到动画: " + strAnim);
        return animRes;
    }
	
    public void Start() {
        if( mPlayer == null ) 
            return;
		
        Transform tr = mPlayer.transform.Find( "model/body" );
        if( tr )
        {
            mBody.mGO = tr.gameObject;
            mBody.mOriginalMtl = mBody.mGO.renderer.material;
            mBody.mCopyMtl = new Material(mBody.mGO.renderer.material);
        }
        tr = mPlayer.transform.Find( "model/head" );
        if( tr )
        {
            mHead.mGO = tr.gameObject;
            mHead.mOriginalMtl = mHead.mGO.renderer.material;
            mHead.mCopyMtl = new Material(mHead.mGO.renderer.material);
        }
		
        _ReadConfig("Config/PlayerStateDisplay", out mStateDisplayConfigs);
    }
	
    void _CombineSkinnedMesh()
    {
		
    }
	
    public void Destroy(){
		
    }
	
	
    PlayerState _GetCurrentPlayerState()
    {
        PlayerState renderState = PlayerState.none;
        if( mDeadWhite )
            renderState = PlayerState.dead_white;
        else if( mPlayer.m_UstateMgr.m_UstateAttr.bInvincipal || mPlayer.m_StateMgr.m_StateMgrBattleAttr.invincipal )
            renderState = PlayerState.invincipal;
        else if( mPlayer.m_UstateMgr.m_UstateAttr.bBati || mPlayer.m_StateMgr.m_StateMgrBattleAttr.bati )
            renderState = PlayerState.bati;
        else if( mTimeEllapsSinceHit < 0.2f )
            renderState = PlayerState.behit;
		
        return renderState;
    }
	
    public void Update(){
		
        mTimeEllapsSinceHit += Time.deltaTime;
		
        if( mHead.mGO )
            mHead.mGO.SetActive( !mPlayer.m_StateMgr.m_StateMgrBattleAttr.hidebody );
        if( mBody.mGO )
            mBody.mGO.SetActive( !mPlayer.m_StateMgr.m_StateMgrBattleAttr.hidebody );
		
        if( mStateDisplayConfigs == null )
            return;
		
        PlayerState curPlayerState = _GetCurrentPlayerState();
        if( curPlayerState == mlastPlayerState )
            return;
		
        _OnStateChange( mlastPlayerState, curPlayerState );
		
        mlastPlayerState = curPlayerState;
    }
	
    private void _OnStateChange( PlayerState curPlayerState, PlayerState newPlayerState )
    {
        _ApplyEffect( mBody, curPlayerState, newPlayerState );
        _ApplyEffect( mHead, curPlayerState, newPlayerState );
		
    //	Debug.Log("state from: " + curPlayerState + " to: " + newPlayerState );
    }
	
    private void _ApplyEffect( Part target, PlayerState curPlayerState, PlayerState newPlayerState )
    {
        StateDisplayConfig curState = mStateDisplayConfigs[curPlayerState];
        StateDisplayConfig newState = mStateDisplayConfigs[newPlayerState];
		
        if( target.mGO == null )
            return;
		
        if( newState.bOrigalMtl )
        {
            Material[] material = new Material[1];
            material[0] = target.mCopyMtl;
            target.mGO.renderer.materials = material;
        }
        else
        {
            if( newState.mtlRes == null )
            {
                Material[] material = new Material[1];
                material[0] = target.mOriginalMtl;
                target.mGO.renderer.materials = material;
            }
            else
            {
                Material[] materials = new Material[2];
                materials[0] = target.mOriginalMtl;
                materials[1] = GameObject.Instantiate(newState.mtlRes) as Material;
                target.mGO.renderer.materials = materials;
            }
        }
		
        if( target.mGO.animation == null )
            target.mGO.AddComponent<Animation>();
		
        AnimationClip curClip = null;
        if( curState.clipRes != null )
            curClip = target.mGO.animation.GetClip( curState.clipRes.name );
		
        if( curClip != null )
            target.mGO.animation.RemoveClip(curClip);
		
        if( newState.clipRes == null )
            return;
		
        AnimationClip newClipInstance = GameObject.Instantiate(newState.clipRes) as AnimationClip;
        target.mGO.animation.AddClip(newClipInstance, newState.clipRes.name);
        target.mGO.animation.Play(newState.clipRes.name);
    }
	
    public void NotifyBeHit( )	{
        mTimeEllapsSinceHit = 0;
    }
	
    public void SetDeadWhite( bool v )	{
        mDeadWhite = v;
    }
    */
}