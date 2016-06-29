using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaySoundManager  : Singleton<PlaySoundManager>
{
    const string mPlayerSoundVolumeKey = "MusicVolumeKey"; //“Ù¡ø
    const string mPlayerSoundIsClose = "MusicState";
    static AudioListener mAudioListener;
    static AudioSource mAudioSource;
    static bool mIsCloseSound;

    static float mVolume;
    static private bool _mute;
    static public bool Mute
    {
        get { return _mute; }
        set
        {
            _mute = value;
            if (mAudioSource != null)
                mAudioSource.mute = value;
        }
    }
    static string mCurSoundName;

	private List<GameUtils.Timer4View>	m_soundsToPlay = new List<GameUtils.Timer4View>();

	public void PlaySound(MatchSoundEvent evt, bool loop = false)
	{
		MatchSound ms = GameSystem.Instance.matchSoundConfig.GetSounds(evt);
		if (ms == null)
		{
			Debug.Log("No sounds for event:" + evt);
			return;
		}

		if( ms.sounds.Count > 0 )
		{
			MatchSound.Item sound = ms.sounds[UnityEngine.Random.Range(0, ms.sounds.Count)];
			//Debug.Log("Play sound, Event:" + evt + " Sound:" + sound.soundId);
			GameUtils.Timer4View timer = new GameUtils.Timer4View((float)sound.fLag, 
				()=>
				{
					AudioClip clip = AudioManager.Instance.GetClip("Misc/" + sound.soundId);
					if (clip != null)
						AudioManager.Instance.PlaySound(clip, true, 1f, 1f, loop);
					else
						Debug.LogWarning("Sound file " + sound + " not found");
				}, 1);
			m_soundsToPlay.Add(timer);
			timer.stop = false;
		}

		if( ms.narrator_sounds.Count > 0 )
		{
			MatchSound.Item sound = ms.narrator_sounds[UnityEngine.Random.Range(0, ms.narrator_sounds.Count)];
			//Debug.Log("Play narrator sound, Event:" + evt + " Sound:" + sound.soundId);
			GameUtils.Timer4View timer = new GameUtils.Timer4View((float)sound.fLag, 
				()=>
				{
					AudioClip clip = AudioManager.Instance.GetClip("Misc/" + sound.soundId);
					if (clip != null)
						AudioManager.Instance.PlaySound(clip, true, 1f, 1f, loop);
					else
						Debug.LogWarning("Sound file " + sound + " not found");
				}, 1);
			m_soundsToPlay.Add(timer);
			timer.stop = false;
		}
	}

	public static AudioSource PlaySound(AudioClip clip, float volume, float pitch)
    {
		if (clip != null)
		{
		    if (mAudioListener == null)
		    {
			    mAudioListener = GameObject.FindObjectOfType(typeof(AudioListener)) as AudioListener;
			    if (mAudioListener == null)
			    {
				    Camera cam = Camera.main;
				    if (cam == null) cam = GameObject.FindObjectOfType(typeof(Camera)) as Camera;
				    if (cam != null) mAudioListener = cam.gameObject.AddComponent<AudioListener>();
			    }
		    }
	
		    if (mAudioSource == null)
		    {
				GameObject sourceObj = GameObject.Find("BGMAudioSource");
				if (sourceObj == null)
				{
					sourceObj = new GameObject("BGMAudioSource", typeof(AudioSource));
					Object.DontDestroyOnLoad(sourceObj);
				}
				mAudioSource = sourceObj.GetComponent<AudioSource>();
                mAudioSource.mute = Mute;
			}

			if (mAudioSource.clip != clip)
			{
                mAudioSource.pitch = pitch;
                mAudioSource.clip = clip;
                mAudioSource.loop = true;
                mAudioSource.volume = volume;
                mAudioSource.Play();
				isCloseSound = false;
		    }
			return mAudioSource;
	    }
	    mAudioSource = null;
	    return null;
    }	
	
    public static void ClosePlaySound()
    {
	    if(isCloseSound) return ;
		    isCloseSound = true;
	    if (mAudioSource)
	    {
		    if(mAudioSource.isPlaying)
		    {
			    mAudioSource.Stop();
			    mAudioSource.clip = null;
		    }					
	    }
    }

	public void Update(float deltaTime)
	{
		//update sound timer
		for( int idx = m_soundsToPlay.Count - 1; idx >= 0; idx-- )
		{
			GameUtils.Timer4View timer = m_soundsToPlay[idx];
			if( timer == null )
				continue;
			timer.Update(deltaTime);
			if( timer.stop )
				m_soundsToPlay.RemoveAt(idx);
		}
	}

    public static void PlaySound(string iSoundName)
    {
        PlaySound(iSoundName, GetPlayerMusicVolume());
    }
	
    public static void PlaySound(string iSoundName ,float volume)
    {
        if(string.IsNullOrEmpty(iSoundName))
        {
	        return;
        }
        mVolume = volume;

        if (mCurSoundName != null && mCurSoundName.Equals(iSoundName))
        {
            return;
        }

        //AudioClip clip = ResourceLoadManager.Instance.GetResources(iSoundName) as AudioClip;
        ResourceLoadManager.Instance.LoadSound(iSoundName, OnSoundPlay);
        mCurSoundName = iSoundName;
    }

    // Reset in Game.
    public static void ClearSoundName()
    {
        mCurSoundName = "";
    }

    public static void OnSoundPlay(Object clip)
    {
        if(clip)
        {
            PlaySound((AudioClip)clip, mVolume, 1.0F);
        }
		else
		{
			if (mAudioSource)
			{
				mAudioSource.Stop();
			}
		}
    }
		
    public static bool isCloseSound
    {
	    get
	    {
            mIsCloseSound = !GetMusicIsOpen();
		    return mIsCloseSound;				
	    }
	    set
	    {
            SetMusicIsOpen(!value);
            if (value)
                SetMusicVolume(0);
            else
            {
                if (NGUITools.soundVolume > 0)
                {
                    //SetMusicVolume(NGUITools.soundVolume);
                    SetMusicVolume(GetPlayerMusicVolume());
                }
            }
                    
		    mIsCloseSound = value;
	    }
    } 
	
    public static void PlaySoundOneShot(string name )
    {
        ResourceLoadManager.Instance.LoadSound(name, OnOneShotPlay);
        //AudioClip clip = ResourceLoadManager.Instance.GetResources(name) as AudioClip;
        //if(clip)
        //{
        //    NGUITools.PlaySound(clip);
        //}
    }

    public static void OnOneShotPlay(Object clip)
    {
        NGUITools.PlaySound(clip as AudioClip);
    }
	  
	public static void SetMusicVolume(float val)
	{
		if(mAudioSource)
		{
			SetPlayerSoundVolume(val);
			mAudioSource.volume = val;
		}
	}

    public static bool GetMusicIsOpen() 
    {
        int value =1 -  PlayerPrefs.GetInt(mPlayerSoundIsClose);
        if (value > 0)
        {
            return true;
        }
        else 
        {
            return false;
        }       
    }

    public static void SetMusicIsOpen(bool volume) 
    {
        int value = 1;
        if (volume)
            value = 0;
        else
            value = 1;
        PlayerPrefs.SetInt(mPlayerSoundIsClose, value);
    }

    public static void SetPlayerSoundVolume(float fVolumeKey)
    {
        PlayerPrefs.SetFloat(mPlayerSoundVolumeKey, (1.0F - fVolumeKey));
    }

	public static float GetPlayerMusicVolume()
	{
        //return 1.0f - PlayerPrefs.GetFloat(mPlayerSoundVolumeKey);
        return 0.3f;
	}

    public static float GetPlayerSoundVolume()
    {
        //return 1.0f - PlayerPrefs.GetFloat(mPlayerSoundVolumeKey);
        return 1.0f;
    }

}
