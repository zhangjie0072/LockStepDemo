using System;
using UnityEngine;

[RequireComponent(typeof(UISprite))]
public class FrameAnimation : MonoBehaviour
{
    public string spritePrefix;
    public int beginFrame = 0;
    public int endFrame = 0;
    public float frameRate = 10;
	public bool makePixelPerfect = false;
    public bool loop = true;
    public bool autoStart = true;

    private UISprite sprite;
    private bool playing = false;
    private int currFrame = 0;
    private float duration;
    private DateTime startTime;
    private string originSprite;

    void Awake()
    {
        sprite = GetComponent<UISprite>();
        if (sprite == null)
            Logger.LogError("The frame animation component rely on UISpite.");
        if (endFrame < beginFrame)
            Logger.LogError("End frame must larger than begin frame.");

        duration = (endFrame - beginFrame + 1) / frameRate;
    }

    void Start()
    {
        if (autoStart)
            Play();
    }

    void Update()
    {
        if (playing)
        {
            TimeSpan span = DateTime.Now - startTime;
            float delta = (float)(span.TotalMilliseconds / 1000);
            if (loop)
                delta = delta % duration;
            if (delta > duration)
            {
                delta = duration;
                playing = false;
            }
            currFrame = (int)(beginFrame + delta * frameRate);
            sprite.spriteName = spritePrefix + currFrame;
			if (makePixelPerfect)
				sprite.MakePixelPerfect();
        }
    }

    public void Play()
    {
        playing = true;
        currFrame = beginFrame;
        startTime = DateTime.Now;
        originSprite = sprite.spriteName;
    }

    public void Stop()
    {
        playing = false;
        if (!string.IsNullOrEmpty(originSprite))
            sprite.spriteName = originSprite;
    }
}
