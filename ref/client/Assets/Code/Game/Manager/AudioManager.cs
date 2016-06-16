using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : Singleton<AudioManager>
{
	private Dictionary<string, AudioClip> m_audioCache = new Dictionary<string, AudioClip>();

    bool _mute;
    public bool mute
    {
        set
        {
            _mute = value;
            foreach (AudioSource source in sources)
                source.mute = value;
        }
        get { return _mute; }
    }

	public float m_fVolume = 1.0f;
	private AudioListener _listener;
	private AudioListener mListener
	{
		get
		{
			if (_listener == null)
			{
				_listener = GameObject.FindObjectOfType(typeof(AudioListener)) as AudioListener;
				if (_listener == null)
				{
					Camera cam = Camera.main;
					if (cam == null) cam = GameObject.FindObjectOfType(typeof(Camera)) as Camera;
					if (cam != null) _listener = cam.gameObject.AddComponent<AudioListener>();
				}
				if (_listener.GetComponent<AudioSource>() != null)
					sources.Add(_listener.GetComponent<AudioSource>());
			}
			return _listener;
		}
	}
	private const string m_strAudioPath = "Audio/";
	private List<AudioSource> sources = new List<AudioSource>();

	public AudioSource	GetSource()
	{ 
		AudioSource source = null;
		foreach (AudioSource s in sources)
		{
			if (!s.isPlaying)
				source = s;
		}

		if (source == null)
		{
			source = mListener.gameObject.AddComponent<AudioSource>();
            source.mute = mute;
			sources.Add(source);
		}
		return source; 
	}

	public AudioManager()
	{
	}
	
	public AudioClip GetClip( string path )
	{
		if( m_audioCache.ContainsKey(path) )
			return m_audioCache[path];

        AudioClip ac = ResourceLoadManager.Instance.GetResources(m_strAudioPath + path, typeof(AudioClip)) as AudioClip;
		if( ac != null )
			m_audioCache.Add( path, ac );
		
		return ac;
	}

	public void PlaySound(AudioClip clip, bool overlap = true, float volume = 1.0f, float pitch = 1.0f, bool loop = false)
	{
		volume *= m_fVolume;

		if (clip != null && volume > 0.01f)
		{
			if(mListener != null && mListener.enabled)
			{
				AudioSource source = GetSource();
				if (overlap || !source.isPlaying)
				{
					source.pitch = pitch;
					source.loop = loop;
					if (!loop)
						source.PlayOneShot(clip, volume);
					else
					{
						source.clip = clip;
						source.volume = volume;
						source.Play();
					}
				}
			}
		}
	}

	public void OnLevelWasLoaded(int level)
	{
		sources.Clear();
	}

}
