--声音设置
--音乐、音效的开关

AudioSettings = {
    musicMute = false,     -- 音乐静音
    soundMute = false,     -- 音效静音
}

local modname = "AudioSettings"

--public
--音乐静音
--参数: mute (true:静音, false:取消静音)
function AudioSettings.MuteMusic(mute)
    AudioSettings.musicMute = mute
    UnityEngine.PlayerPrefs.SetInt("MusicMute", mute and 1 or 0)
    AudioSettings.MuteBGMSource(mute)
end

--public
--音乐静音状态
--返回值: 是否静音
function AudioSettings.IsMusicMute()
    return AudioSettings.musicMute
end

--public
--音效静音
--参数: mute (true:静音, false:取消静音)
function AudioSettings.MuteSound(mute)
    AudioSettings.soundMute = mute
    UnityEngine.PlayerPrefs.SetInt("SoundMute", mute and 1 or 0)
    AudioSettings.MuteAudioManager(mute)
    AudioSettings.MuteListenerAudioSource(mute)
    AudioSettings.MuteModelShowItem(mute)
end

--public
--音效静音状态
--返回值: 是否静音
function AudioSettings.IsSoundMute(mute)
    return AudioSettings.soundMute
end

--初始化
function AudioSettings.Initialize()
    AudioSettings.musicMute = (UnityEngine.PlayerPrefs.GetInt("MusicMute") ~= 0)
    AudioSettings.soundMute = (UnityEngine.PlayerPrefs.GetInt("SoundMute") ~= 0)
    print(modname, "Initialize MusicMute:", AudioSettings.musicMute,
        "SoundMute:", AudioSettings.soundMute)
    AudioSettings.MuteMusic(AudioSettings.musicMute)
    AudioSettings.MuteSound(AudioSettings.soundMute)
end

--设置音源:BGMAudioSource
function AudioSettings.MuteBGMSource(mute)
    PlaySoundManager.Mute = mute
end

--设置AudioManager
function AudioSettings.MuteAudioManager(mute)
    AudioManager.Instance.mute = mute
end

--设置音源:listener
function AudioSettings.MuteListenerAudioSource(mute)
    --libing
    -- local go = GameObject.FindObjectOfType(UnityEngine.AudioListener.GetClassType())
    -- local listener = go:GetComponent("AudioListener")
    -- if listener and listener.audio then
    --     listener.audio.mute = mute
    --     print(modname, "Set audio source mute of listener", mute)
    -- end
end

--设置ModelShowItem
function AudioSettings.MuteModelShowItem(mute)
    ModelShowItem.Mute = mute
end

function AudioSettings.OnLevelWasLoaded()
    AudioSettings.MuteListenerAudioSource(AudioSettings.soundMute)
end

AudioSettings.Initialize()
