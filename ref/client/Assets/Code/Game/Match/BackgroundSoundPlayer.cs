using UnityEngine;
using System.Collections.Generic;

public class BackgroundSoundPlayer
{
	//[System.Serializable]
	public class Track
	{
		public string 		name;
		public float 		interval;
		public bool			loop;
	}

	public enum PlaySequence
	{
		eNormal,
		eRandom
	}

	public List<Track>		m_tracks = new List<Track>();

	private	PlaySequence	m_type;
	private int				m_curTrackIdx;

	private bool			m_bPlay = false;

	private AudioSource		m_audioSrc;

	private float m_fVolume = 0.2f;

    string tagBgSound = "BGMAudioSource";

	public BackgroundSoundPlayer ()
	{
		m_curTrackIdx = -1;
		m_type = PlaySequence.eNormal;

		GameObject go_bgSound = GameObject.Find(tagBgSound);
		if( go_bgSound == null )
			go_bgSound = new GameObject(tagBgSound);
		m_audioSrc = go_bgSound.GetComponent<AudioSource>();
		if( m_audioSrc == null )
			m_audioSrc = go_bgSound.AddComponent<AudioSource>();
	}

	public void Play(PlaySequence type)
	{
		if( m_tracks == null )
			return;

		if( m_tracks.Count == 0 )
			return;

		m_bPlay = true;

		if( m_type == PlaySequence.eNormal )
		{
			if( m_curTrackIdx == -1 )
				m_curTrackIdx = 0;
		}
		else if( m_type == PlaySequence.eRandom )
		{
			if( m_curTrackIdx == -1 )
				m_curTrackIdx = Random.Range(0, m_tracks.Count - 1);
		}
	}

	public void Stop()
	{
		m_bPlay = false;
		m_audioSrc.Stop();
	}

	public void Update()
	{
		if( !m_bPlay || m_curTrackIdx == -1 )
			return;
		Track curTrack = m_tracks[m_curTrackIdx];
		if( curTrack == null )
			return;
		if( m_audioSrc == null )
		{
			GameObject bgSound = new GameObject(tagBgSound);
			m_audioSrc = bgSound.AddComponent<AudioSource>();
		}
		if( m_audioSrc.isPlaying )
			return;

		if( !curTrack.loop )
		{
			if( m_type == PlaySequence.eNormal )
			{
				_PlayTrack(m_curTrackIdx);

				m_curTrackIdx++;
				m_curTrackIdx %= m_tracks.Count;
			}
			else if( m_type == PlaySequence.eRandom )
			{
				if( m_curTrackIdx == -1 )
					m_curTrackIdx = Random.Range(0, m_tracks.Count - 1);

				_PlayTrack(m_curTrackIdx);
			}
		}
		else
			_PlayTrack(m_curTrackIdx);
	}
		
	void _PlayTrack( int idx )
	{
		Track newTrack = m_tracks[idx];
		if( m_audioSrc != null )
		{
			AudioClip clip = AudioManager.Instance.GetClip(newTrack.name);
			if( clip == null )
				return;
			m_audioSrc.clip = clip;
			m_audioSrc.volume = m_fVolume;
			m_audioSrc.PlayDelayed(newTrack.interval);
			PlaySoundManager.ClearSoundName();
		}
	}
}


