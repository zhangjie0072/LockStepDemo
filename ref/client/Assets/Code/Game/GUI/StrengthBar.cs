using fogs.proto.msg;
using System;
using UnityEngine;

public class StrengthBar : MonoBehaviour
{
    private UIWidget _bar;
    private Transform _thumb;

    public Player player;
    public UIAtlas atlas;

    void Awake()
    {
        _bar = transform.FindChild("Bar").GetComponent<UIWidget>();
        _thumb = _bar.transform.FindChild("Thumb");
    }

    void Start()
    {
        SetupBarColors(player.shootStrength.config);
    }

    void Update()
    {
        if (player.shootStrength.running || player.shootStrength.unfinished)
        {
            _thumb.localPosition = new Vector3(_thumb.localPosition.x, _bar.height * (float)player.shootStrength.ratio, _thumb.localPosition.z);
            if (!_bar.gameObject.activeSelf)
                _bar.gameObject.SetActive(true);
        }
        else
        {
            if (_bar.gameObject.activeSelf)
                _bar.gameObject.SetActive(false);
        }
    }

    private void SetupBarColors(ArticleStrength config)
    {
        int y = 0;
        foreach (ArticleStrength.Section section in config.sections)
        {
            UISprite sprite = NGUITools.AddSprite(_bar.gameObject, atlas, "white_bg");
            sprite.pivot = UIWidget.Pivot.Bottom;
            sprite.color = GameSystem.Instance.ArticleStrengthConfig.GetSectionColor(section.section);
            sprite.depth = 1;
            sprite.width = _bar.width;
            sprite.height = (int)((float)section.end_time * _bar.height - y);
            sprite.transform.localPosition = new Vector3(0, y);
            y += sprite.height;
        }
    }
}
