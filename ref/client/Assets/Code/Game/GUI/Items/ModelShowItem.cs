using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class ModelShowItem : MonoBehaviour
{
    private uint _preModelID;
    private uint _modelID;
    private bool _needRotation = false;
    private bool _playNeedBall = true;
    private bool _isFashion = false;

    private GameObject _goModel;
    private GameObject _goBall;
    private int _initAnimID = 0;
    private int _repeatTimes = 0;
    private BaseDataConfig2 _baseDataConfig;
    private Dictionary<uint, Player> _playerList = new Dictionary<uint, Player>();
    public PlayerModel _playerModel;
    public float scaleValue = 0.5f;
    Player _player;

	private static HashSet<ModelShowItem> instances = new HashSet<ModelShowItem>();
	private static HashSet<ModelShowItem> hiddenInstances = new HashSet<ModelShowItem>();

    public string _layerName = "GUI";

    static private bool _mute = false;
    static public bool Mute
    {
        get { return _mute; }
        set
        {
            _mute = value;
            foreach (ModelShowItem item in instances)
            {
                AudioSource src = item.GetComponentInChildren<AudioSource>();
                if (src != null)
                    src.mute = value;
            }
            foreach (ModelShowItem item in hiddenInstances)
            {
                AudioSource src = item.GetComponentInChildren<AudioSource>();
                if (src != null)
                    src.mute = value;
            }
        }
    }

    void Awake()
    {
		instances.Add(this);
        AudioSource src = GetComponentInChildren<AudioSource>();
        if (src != null)
            src.mute = Mute;
    }

	void OnDestroy()
	{
		instances.Remove(this);
		hiddenInstances.Remove(this);
	}

	public static void HideExcept(ModelShowItem exception)
	{
		foreach (ModelShowItem instance in instances)
		{
			if (instance != exception)
			{
				hiddenInstances.Add(instance);
				instance.gameObject.SetActive(false);
			}
		}
	}

	public static void ResumeHidden()
	{
		foreach (ModelShowItem instance in hiddenInstances)
		{
			instance.gameObject.SetActive(true);
		}
		hiddenInstances.Clear();
	}

    // Use this for initialization
    void Start()
    {
        _baseDataConfig = GameSystem.Instance.RoleBaseConfigData2;
        _repeatTimes = Random.Range(1, 4);
    }

    public uint ModelID
    {
        get { return _modelID; }
        set
        {
            _modelID = value;
            ShowModel();
        }
    }

    public float ModelScale
    {
        set
        {
            _goModel.transform.localScale = new Vector3(
				_goModel.transform.localScale.x * value,
				_goModel.transform.localScale.y * value,
				_goModel.transform.localScale.z * value);
        }
    }

    public bool PlayNeedBall
    {
        set { _playNeedBall = value; }
    }

    public bool Rotation
    {
        set { _needRotation = value; }
    }

    public bool IsFashion 
    {
        set { _isFashion = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (_modelID != 0)
        {
            int posID = 0;
            if (_player.gameObject && !_player.gameObject.GetComponent<Animation>().isPlaying && _repeatTimes >= 0) 
            {
                posID = _initAnimID;
                _repeatTimes -= 1;
            }
            if (_repeatTimes < 0)
            {
                posID = (int)_baseDataConfig.GetRandomAnimationID(_player.m_id);
                _repeatTimes = Random.Range(1, 4);
            }
            CommonFunction.PlayAnimation(_player, posID, _playNeedBall);
        }
    }


    public void ShowModel()
    {
        if (_modelID == 0)
        {
            if (_goModel != null)
                _goModel.SetActive(false);
            return;
        }
        if (_goModel != null)
        {
            GameObject.Destroy(_goModel);
            //_goModel.SetActive(false);
        }
		_player = null;
        _playerModel = null;
		//_playerList.TryGetValue(_modelID, out _player);
        if (_player == null)
        {
            if (_playerModel != null && _player != null)
            {
                Object.Destroy(_player.gameObject);
                //GameObject.Destroy(_player.m_goPlayer);
                _playerModel = null;
                //Resources.UnloadUnusedAssets();
                System.GC.Collect();
            }

			/*
			RoleInfo newRoleInfo = new RoleInfo();
			newRoleInfo.id = _modelID;
			//newRoleInfo.bias = 0;
			newRoleInfo.quality = (uint)QualityType.QT_NONE;
			_player = new Player(newRoleInfo, new Team(Team.Side.eNone));
			//_playerList.Add(_modelID, _player);
			*/

			_player = MainPlayer.Instance.GetRole(_modelID);
            if (_player == null)
            {
                RoleInfo newRoleInfo = new RoleInfo();
                newRoleInfo.id = _modelID;
                newRoleInfo.level = 1;
                newRoleInfo.quality = (uint)QualityType.QT_NONE;
                _player = new Player(newRoleInfo, new Team(Team.Side.eNone));
            }

            _initAnimID = (int)GameSystem.Instance.RoleBaseConfigData2.GetInitAnimationID(_player.m_id);
            if (_playerModel == null)
            {
                _playerModel = new PlayerModel(_player);
                _player.model = _playerModel;
            }


            // init player mode
            uint shapeID = _modelID;
            RoleBaseData2 roleBaseData2 = GameSystem.Instance.RoleBaseConfigData2.GetConfigData(_modelID);
            if (roleBaseData2 == null )
            {
                Logger.LogError("No shape found!shapeID=" + shapeID);
             //   _playerModel.Init((uint)shapeID);
            }
            else
            {
                RoleShape roleShape = GameSystem.Instance.RoleShapeConfig.GetConfig((uint)shapeID);
                if (roleShape == null)
                {
                    Logger.LogError("No role shape data found for player: " + shapeID);
                    return;
                }

                _playerModel.Init(roleShape, true);
            }

            GameUtils.SetLayerRecursive(_player.gameObject.transform, LayerMask.NameToLayer(_layerName) ); 
   

            _player.CreateAnimation(false);

            GameObject model = _player.gameObject;

            //model.AddComponent<AudioSource>();//给模型增加声音
            //AudioSource sound = model.GetComponent<AudioSource>();
            //string audioStr = "Audio/ChooseRole/" + _modelID;
            //sound.clip = ResourceLoadManager.Instance.GetResources(audioStr) as AudioClip;
            //sound.Play();
            ////PlaySoundManager.PlaySoundOneShot("Audio/ChooseRole/" + _modelID);

            model.transform.parent = transform;
            //模型展示的缩放比例修改 //modify by lizhiping @0301 
            if (_isFashion)
            {
                model.transform.localPosition = new Vector3(0, 0, -200.0f);
            }
            else
            {
                model.transform.localPosition = new Vector3(0, -170.0f, -200.0f);
            }
            //model.transform.localPosition = new Vector3(0, 0, -200.0f);
            model.transform.localRotation = Quaternion.Euler(0, 180, 0);
            model.transform.localScale = new Vector3(
				model.transform.localScale.x * scaleValue / UIManager.m_ratioScale,
				model.transform.localScale.y * scaleValue / UIManager.m_ratioScale,
				model.transform.localScale.z * scaleValue / UIManager.m_ratioScale);
            model.name = _modelID.ToString();
            if (_needRotation)
            {
                model.AddComponent<UIModelRotate>();   //给模型添加拖拽转动脚本
            }
            //播放球员声音
            List<string> soundPath = GameSystem.Instance.RoleBaseConfigData2.GetPlayerSoundByID(_modelID);
            if (soundPath != null && soundPath.Count > 0 && _preModelID != _modelID) 
            {
                if (!Mute)
                {
                    string clipPath = string.Format("Audio/ChooseRole/{0}", soundPath[0]);
                    AudioSource modelAudio = model.AddComponent<AudioSource>();
                    AudioClip audioClip = ResourceLoadManager.Instance.GetResources(clipPath) as AudioClip;
                    modelAudio.clip = audioClip;
                    if (this.gameObject.activeSelf) //设置modelID，就显示模型。会给出提示：游戏对象还是disenabled状态
                        modelAudio.Play();
                }
            }

            GameUtils.SetLayerRecursive(model.transform, LayerMask.NameToLayer(_layerName));
        }

		_goModel = _player.gameObject;
		_goModel.transform.parent = transform;

        if (_preModelID != _modelID)
            _preModelID = _modelID;
        //delete by lizhiping @0301 
        //if (!_isFashion)
        //{
        //    _goModel.transform.localPosition = new Vector3(0, -170.0f, -200.0f);
        //    _goModel.transform.localScale = new Vector3(360.0f, 360.0f, 360.0f);
        //}
        //else 
        //{
        //    _goModel.transform.localPosition = new Vector3(0, 0, -200.0f);
        //    _goModel.transform.localScale = new Vector3(275.0f, 275.0f, 275.0f);
        //}
		_playerModel = _player.model;

        if (_goBall == null)
        {
            GameObject ballPrefab = ResourceLoadManager.Instance.LoadPrefab("Prefab/DynObject/basketBall") as GameObject;
            _goBall = GameObject.Instantiate(ballPrefab) as GameObject;
			_goBall.transform.FindChild("Shadow").gameObject.SetActive(false);
            _goBall.layer = LayerMask.NameToLayer(_layerName);
        }

        _goBall.transform.parent = GameUtils.FindChildRecursive(_goModel.transform, "ball").transform;
        _goBall.transform.localPosition = Vector3.zero;
        _goBall.transform.localRotation = Quaternion.identity;
        _goBall.transform.localScale = Vector3.one;

        _goModel.SetActive(true);
        _goBall.SetActive(true);
        _goBall.transform.localRotation = Quaternion.AngleAxis(180, Vector3.up);
        this.gameObject.layer = LayerMask.NameToLayer(_layerName);

    }
}
