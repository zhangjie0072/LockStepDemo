using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public class PlayerModel: SparkEffect.ISparkTarget
{
	public class Part
	{
		public SkinnedMeshRenderer 	mSkin;
		public Material				mOriginalMtl;
		public Color				mOriginalColor;
	}

	private Player				mPlayer;
    /**人物模型*/
	public GameObject 			gameObject;

	public 	ModelTagPoint[] 	mModelTagPts;

	private string				m_bipName;
	
	private Shader				m_toon;
	private Object[]			m_MatSkins;

    private uint _shapeID;
    private RoleShape _shape;

    private string _layerName = "Player";

	private bool 				m_bFashionMode = false;

	private Material			mOriginalMtl;
	private Color				mOriginalColor;

    public string layerName
    {
        set
        {
            _layerName = value;
        }
    }

    public Transform root;
    public Transform ball;
    public Transform head;

	public PlayerModel(Player player)	
	{
		mPlayer = player;
		m_bipName = "Bip001";
		m_toon = Shader.Find("Toon/Advanced Outline");
	}

    public void EnableDrag()
    {
       // Build();
        mPlayer.gameObject.AddComponent<UIModelRotate>();
    }

	public void RestoreMaterial()
	{
		if( mPlayer.gameObject.GetComponent<Renderer>() == null )
			return;
		mPlayer.gameObject.GetComponent<Renderer>().material = mOriginalMtl;
		mPlayer.gameObject.GetComponent<Renderer>().material.color = mOriginalColor;
	}
	
	public void SetSparkMaterial(Material mat)
	{
		if( mPlayer.gameObject.GetComponent<Renderer>() == null )
			return;
		mPlayer.gameObject.GetComponent<Renderer>().material = mat;
	}

	public void SetMainColor(Color color, bool bOrig = false)
	{
		if(bOrig)
			mOriginalColor = color;
		if( mPlayer.gameObject.GetComponent<Renderer>() == null )
			return;
		mPlayer.gameObject.GetComponent<Renderer>().material.color = color; 
	}

	public void EnableGrey(bool enable = true)
	{
		if( mPlayer.gameObject.GetComponent<Renderer>() == null )
			return;
		mPlayer.gameObject.GetComponent<Renderer>().material.SetFloat("_Grey", enable ? 0.855f : 0f);
	}

	public void _DressUp(string strType, uint itemId)
	{
        if (itemId == 0)
        {
            Debug.Log("fashion item id is : " + itemId);
            return;
        }

		FashionItem item = GameSystem.Instance.FashionConfig.GetConfig(itemId);
		if( item == null )
		{
			Debug.LogError("Can not fashion item: " + itemId);
			return;
		}

        BodyInfo bodyInfo = null;
        // TODO: check wheather use the shape id
        GameSystem.Instance.BodyInfoListConfig.configs.TryGetValue((uint)_shapeID, out bodyInfo);


        FashionMatch fMatch = null;
        item.fashion_matchs.TryGetValue(bodyInfo.type_id, out fMatch);

        if( fMatch == null )
        {
            return;
        }


        Transform fashonTran = mPlayer.gameObject.transform.FindChild(fMatch.shape_id.Remove(0, 5));

        if (fashonTran != null)
        {
            return;
        }


        string strResPath = string.Format("Object/Fashion/{0}/{0}", fMatch.shape_id);
        Object resDress = ResourceLoadManager.Instance.GetResources(strResPath);
		GameObject goDress = GameObject.Instantiate(resDress) as GameObject;
		GameUtils.ReSkinning(goDress, mPlayer.gameObject);
		GameObject.Destroy(goDress);

        foreach (uint hide in fMatch.hide_id)
		{
			HidePart hp = GameSystem.Instance.FashionConfig.MappingPart(hide);
			if( hp == null )
				continue;
			string strHidePart = (mPlayer.m_gender == fogs.proto.msg.GenderType.GT_FEMALE ? "G_" : "M_") + hp.part_id;
			Transform trSkin = GameUtils.FindChildRecursive( mPlayer.gameObject.transform, strHidePart );

			if( trSkin == null )
				continue;

			trSkin.gameObject.GetComponent<Renderer>().enabled = false;
		}
   
        GameUtils.SetLayerRecursive(mPlayer.gameObject.transform, LayerMask.NameToLayer(_layerName));
	}



    public void _DressDown(string strType, uint itemId)
    {
        FashionItem item = GameSystem.Instance.FashionConfig.GetConfig(itemId);
        if (item == null)
        {
            Debug.LogError("Can not fashion item: " + itemId);
            return;
        }

        BodyInfo bodyInfo = null;
        // TODO: check wheather use the shape id
        GameSystem.Instance.BodyInfoListConfig.configs.TryGetValue((uint)_shapeID, out bodyInfo);


        FashionMatch fMatch = null;
        item.fashion_matchs.TryGetValue(bodyInfo.type_id, out fMatch);

        if (fMatch == null)
        {
            return;
        }
        string shapeId = fMatch.shape_id;
        //if( shapeId.StartsWith("Skin_") )
        //{
        //    shapeId = shapeId.Remove(0, 5);
        //}

        Transform fashonTran = mPlayer.gameObject.transform.FindChild(fMatch.shape_id.Remove(0, 5));

		fashonTran = null;

        if( fashonTran == null )
        {
            string strResPath = string.Format("Object/Fashion/{0}/{0}", fMatch.shape_id);
            Object resDress = ResourceLoadManager.Instance.GetResources(strResPath);
            GameObject goDress = GameObject.Instantiate(resDress) as GameObject;


            SkinnedMeshRenderer[] skinMeshes = goDress.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (SkinnedMeshRenderer skin in skinMeshes)
            {
                Transform sfashonTran = mPlayer.gameObject.transform.FindChild(skin.name);
                if (sfashonTran!= null)
                {
					if( m_bFashionMode )
						GameObject.Destroy(sfashonTran.gameObject);
					else
                    	GameObject.DestroyImmediate(sfashonTran.gameObject);
                }
                else
                {
                    //Debug.LogError("get rid of skin failed for the skin.name=" + skin.name);
                }
              
            }

			if( m_bFashionMode )
				GameObject.Destroy(goDress);
			else
            	GameObject.DestroyImmediate(goDress);
           // return;
        }
        if (fashonTran!=null )
        {
			if( m_bFashionMode )
				GameObject.Destroy(fashonTran.gameObject);
			else
            	GameObject.DestroyImmediate(fashonTran.gameObject);
        }

        //string strResPath = string.Format("Object/Fashion/{0}/{0}", fMatch.shape_id);
        //Object resDress = ResourceLoadManager.Instance.GetResources(strResPath);
        //GameObject goDress = GameObject.Instantiate(resDress) as GameObject;
        //GameUtils.ReSkinning(goDress, mPlayer.m_goPlayer);
        //GameObject.Destroy(goDress);

		//if( m_bFashionMode )
		{
	        foreach (uint hide in fMatch.hide_id)
	        {
	            HidePart hp = GameSystem.Instance.FashionConfig.MappingPart(hide);
	            if (hp == null)
	                continue;
	            string strHidePart = (mPlayer.m_gender == fogs.proto.msg.GenderType.GT_FEMALE ? "G_" : "M_") + hp.part_id;
	            Transform trSkin = GameUtils.FindChildRecursive(mPlayer.gameObject.transform, strHidePart);

				if( trSkin == null )
					continue;
	            trSkin.gameObject.GetComponent<Renderer>().enabled = true;
	        }
		}
        //  mPlayer.m_goPlayer.layer = LayerMask.NameToLayer("Player");
        GameUtils.SetLayerRecursive(mPlayer.gameObject.transform, LayerMask.NameToLayer(_layerName));
        //Resources.UnloadUnusedAssets();
    }

    public void _FittingUp(uint itemId) 
    {
        if (itemId == 0)
        {
            Debug.Log("FittingUp itemId : " + itemId);
            return;
        }
        FashionItem item = GameSystem.Instance.FashionConfig.GetConfig(itemId);
        if (item == null)
        {
            Debug.LogError("Can not fashion item: " + itemId);
            return;
        }

        BodyInfo bodyInfo = null;
        // TODO: check wheather use the shape id
        GameSystem.Instance.BodyInfoListConfig.configs.TryGetValue((uint)_shapeID, out bodyInfo);


        FashionMatch fMatch = null;
        item.fashion_matchs.TryGetValue(bodyInfo.type_id, out fMatch);

        string strResPath = string.Format("Object/Fashion/{0}/{0}", fMatch.shape_id);
        Object resDress = ResourceLoadManager.Instance.GetResources(strResPath);
        GameObject goDress = GameObject.Instantiate(resDress) as GameObject;
        Transform parentTrans = GameUtils.FindChildRecursive(mPlayer.gameObject.transform, string.Format("{0} Spine2", m_bipName));

        goDress.transform.parent = parentTrans;
        goDress.transform.localPosition = ((GameObject)resDress).transform.position;
        goDress.transform.localRotation = ((GameObject)resDress).transform.rotation;
        goDress.transform.localScale = ((GameObject)resDress).transform.localScale;
        GameUtils.SetLayerRecursive(mPlayer.gameObject.transform, LayerMask.NameToLayer(_layerName));
    }

    public void _FittingDown(uint itemId)
    {
        if (itemId == 0)
        {
            Debug.Log("FittingUp itemId : " + itemId);
            return;
        }
        FashionItem item = GameSystem.Instance.FashionConfig.GetConfig(itemId);
        if (item == null)
        {
            Debug.LogError("Can not fashion item: " + itemId);
            return;
        }

        BodyInfo bodyInfo = null;
        // TODO: check wheather use the shape id
        GameSystem.Instance.BodyInfoListConfig.configs.TryGetValue((uint)_shapeID, out bodyInfo);

        FashionMatch fMatch = null;
        item.fashion_matchs.TryGetValue(bodyInfo.type_id, out fMatch);

        Transform FittingTrans = GameUtils.FindChildRecursive(mPlayer.gameObject.transform, string.Format("{0} Spine2", m_bipName)).FindChild(fMatch.shape_id + "(Clone)");
        if (FittingTrans == null) 
        {
            Debug.Log("Can't Find Fitting Down Goods");
            return;
        }

        GameObject.DestroyImmediate(FittingTrans.gameObject);
        GameUtils.SetLayerRecursive(mPlayer.gameObject.transform, LayerMask.NameToLayer(_layerName));
    }
   
	public Transform GetBone(string name)
	{
		Transform player = mPlayer.gameObject.transform;
		return GameUtils.FindChildRecursive( player, string.Format("{0} {1}", m_bipName, name) );
	}

    public void DressOnFashion( uint fashionId )
    {
		if( fashionId == 0 )
			return;

        Debug.Log("PlayerMode DressOnFashion=" + fashionId + " name=" + GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionId).name);
        RoleShape rs = null;

        GameSystem.Instance.RoleShapeConfig.configs.TryGetValue(mPlayer.m_roleInfo.id, out rs);

        if( rs==null)
        {
            Debug.LogError("cannot find roleShape in @DressOnFashion");
            return;
        }


        //FashionShopConfigItem item;
        //GameSystem.Instance.FashionShopConfig.configs.TryGetValue(fashionId, out item);
        fogs.proto.config.GoodsAttrConfig goodsAttr = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionId);
        if (goodsAttr == null)
        {
            Debug.LogError("cannot find FashionShopConfigItem for fashionID" + fashionId);
            return;
        }

        List<uint> fashionTypeA = new List<uint>();
        string[] types = goodsAttr.sub_category.Split('&');
        for (int i = 0; i < types.Length; i++)
        {
            if (uint.Parse(types[i]) != 0)
                fashionTypeA.Add(uint.Parse(types[i]));
        }
        bool isBack = false;
        foreach (uint fashionType in fashionTypeA)
        {
            uint hideId = 0;
            switch (fashionType)
            {
                case 1:
                    hideId = rs.hair_id;
                    //_fashionItem[0] = item;
                    // _lkHeadSprite.spriteName = item.getSpriteName();
                    break;

                case 2:
                    hideId = rs.upper_id;
                    //_fashionItem[1] = item;
                    //_lkClothesSprite.spriteName = item.getSpriteName();
                    break;

                case 3:
                    hideId = rs.lower_id;
                    //_fashionItem[2] = item;
                    // _lkTrousersSprite.spriteName = item.getSpriteName();
                    break;

                case 4:
                    hideId = rs.shoes_id;
                    //_fashionItem[3] = item;
                    //_lkShoesSprite.spriteName = item.getSpriteName();
                    break;
                case 5:
                    hideId = rs.back_id;
                    //_fashionItem[4] = item;
                    isBack = true;
                    //_lkShoesSprite.spriteName = item.getSpriteName();
                    break;
            }
            if (hideId != 0 && hideId != fashionId)
            {
                // to comment for debug.
                if (hideId != rs.back_id)
                    _DressDown("", hideId);
                else
                    _FittingDown(hideId);
            }
        }
        //Debug.Log("_DressUp fashionId fashionId=" + fashionId);
        if (!isBack)
            _DressUp("", fashionId);
        else
            _FittingUp(fashionId);
    }

    public void DressDownFashion(uint fashionId)
    {
        Debug.Log("PlayerMode DressDownFashion=" + fashionId + "name=" + GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionId).name);

        RoleShape rs = null;
        GameSystem.Instance.RoleShapeConfig.configs.TryGetValue(MainPlayer.Instance.CaptainID, out rs);

        if (rs == null)
        {
            Debug.LogError("cannot find roleShape in @DressOnFashion");
            return;
        }


        fogs.proto.config.GoodsAttrConfig goodsAttr = GameSystem.Instance.GoodsConfigData.GetgoodsAttrConfig(fashionId);
        if (goodsAttr == null)
        {
            Debug.LogError("cannot find FashionShopConfigItem for fashionID" + fashionId);
            return;
        }

        List<uint> fashionTypeA = new List<uint>();
        string[] types = goodsAttr.sub_category.Split('&');
        for (int i = 0; i < types.Length; i++)
        {
            if (uint.Parse(types[i]) != 0)
                fashionTypeA.Add(uint.Parse(types[i]));
        }

        Debug.Log("_DressDown fashionId fashionId=" + fashionId);
        if (!fashionTypeA.Contains(5))
            _DressDown("", fashionId);
        else
            _FittingDown(fashionId);

        foreach (uint fashionType in fashionTypeA)
        {
            uint hideId = 0;
            switch (fashionType)
            {
                case 1:
                    hideId = rs.hair_id;
                    //_fashionItem[0] = item;
                    // _lkHeadSprite.spriteName = item.getSpriteName();
                    break;

                case 2:
                    hideId = rs.upper_id;
                    //_fashionItem[1] = item;
                    //_lkClothesSprite.spriteName = item.getSpriteName();
                    break;

                case 3:
                    hideId = rs.lower_id;
                    //_fashionItem[2] = item;
                    // _lkTrousersSprite.spriteName = item.getSpriteName();
                    break;

                case 4:
                    hideId = rs.shoes_id;
                    //_fashionItem[3] = item;
                    //_lkShoesSprite.spriteName = item.getSpriteName();
                    break;
                case 5:
                    hideId = rs.back_id;
                    //_fashionItem[4] = item;
                    //_lkShoesSprite.spriteName = item.getSpriteName();
                    break;
            }
            if (hideId != 0)
            {
                // to comment for debug.
                if (hideId != rs.back_id)
                    _DressUp("", hideId);
                else
                    _FittingUp(hideId);
            }
        }

    }

	public void Init(RoleShape shape, bool isFashion= false)
	{
        _shapeID = shape.roleShapeId;
        _shape = shape;

		m_bFashionMode = isFashion;

		BodyInfo bodyInfo = GameSystem.Instance.BodyInfoListConfig.GetConfig(shape.body_id);
		string resPath = string.Format( "Object/Player/body/{0}", bodyInfo.type_id );
        ResourceLoadManager.Instance.LoadCharacter(resPath, OnLoadPlayerBody);

		if( mPlayer.gameObject.GetComponent<Renderer>() != null )
		{
			mOriginalMtl 	= mPlayer.gameObject.GetComponent<Renderer>().material;
			mOriginalColor 	= mPlayer.gameObject.GetComponent<Renderer>().material.color;
		}
    }

    public void OnLoadPlayerBody(GameObject resPlayer)
    {
		if( resPlayer == null )
			return;
		gameObject = GameObject.Instantiate(resPlayer) as GameObject;
		gameObject.name = mPlayer.m_id.ToString();

        Animation animation = gameObject.GetComponent<Animation>();

		mPlayer.scale = IM.Vector3.one * new IM.Number((int)_shape.height) / new IM.Number(175);
        if (m_MatSkins == null)
        {
            if (GlobalConst.IS_DEVELOP)
            {
                string path = "Object/Player/body/Materials";
                m_MatSkins = Resources.LoadAll<Material>(path);
            }
            else
            {
                string path = "object/object_player_body_all.assetbundle";
                var assetBundle = ResourceLoadManagerBase.Instance.GetLoadAssetBundle(path);
                m_MatSkins = assetBundle.LoadAllAssets(typeof(Material));
            }
        }
		Dictionary<string, Material> dictMaterials = new Dictionary<string, Material>();
		foreach( Object mat in m_MatSkins )
			dictMaterials.Add(mat.name, mat as Material);
		
		string strBodyMat = "{0}_{1}_{2}01";
		string strGenda = mPlayer.m_gender == fogs.proto.msg.GenderType.GT_FEMALE ? "G_" : "M_";
		string strComplexion = "Asians";
        BodyInfo bodyInfo = GameSystem.Instance.BodyInfoListConfig.GetConfig(_shape.body_id);
		if( bodyInfo.complexion == 1 )
		{
			strComplexion = "American";
		}
		else if( bodyInfo.complexion == 2 )
		{
			strComplexion = "Europeans";
		}
		else if( bodyInfo.complexion == 3 )
		{
			strComplexion = "Asians";
		}
		else if( bodyInfo.complexion == 4 )
        {
            strComplexion = "Wheat";
        }

		SkinnedMeshRenderer[] characterSkins = mPlayer.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach( SkinnedMeshRenderer skin in characterSkins )
		{
			string strOrigMat = skin.material.name;
			int idx = strOrigMat.LastIndexOf("_");
			if( idx == -1 )
				continue;
			strOrigMat = strOrigMat.Substring(0, idx + 1);
			string strNewMaterial = strOrigMat + strComplexion + "01";
			Material matSkin;
			if( !dictMaterials.TryGetValue(strNewMaterial, out matSkin) )
				continue;
			skin.material = matSkin;
		}
		Transform player = mPlayer.gameObject.transform;
		//extract bone info
        head = GameUtils.FindChildRecursive(player, string.Format("{0} Head", m_bipName));
        root = GameUtils.FindChildRecursive(player, "root");
        ball = GameUtils.FindChildRecursive(player, "ball");
		
		string resPath = string.Format( "Object/Player/head/{0}/{0}", bodyInfo.head_id );
        Object resPlayerHead = ResourceLoadManager.Instance.GetResources(resPath);
		GameObject goHead = GameObject.Instantiate(resPlayerHead) as GameObject;
		GameUtils.ReSkinning(goHead, mPlayer.gameObject);
		GameObject.Destroy(goHead);
		
		_DressUp( "hair",  _shape.hair_id );
		_DressUp( "lower", _shape.lower_id );
		_DressUp( "upper", _shape.upper_id );
		_DressUp( "shoes", _shape.shoes_id );

		//_FreashFashion(mPlayer.m_roleInfo);
		List<ulong> uuIds = new List<ulong>();
		foreach (var item in mPlayer.m_roleInfo.fashion_slot_info)
			DressOnFashion(item.goods_id);

		if( !m_bFashionMode )
			player = GameUtils.CombineSkin(player);
       
        mPlayer.gameObject.layer = LayerMask.NameToLayer("Player");

        GameUtils.SetLayerRecursive(mPlayer.gameObject.transform, LayerMask.NameToLayer("Player"));
	
   }
}