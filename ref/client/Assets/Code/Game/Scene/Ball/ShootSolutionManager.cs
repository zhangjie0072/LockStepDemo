using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class ShootSolutionData 
{
    public List<IM.Number> distanceList = new List<IM.Number>();
    public List<IM.Number> angleList = new List<IM.Number>();
    public Dictionary<int, ShootSolutionManager.ShootSolutionSector> shootSolutionSectors =
        new Dictionary<int, ShootSolutionManager.ShootSolutionSector>();
}

public class ShootSolutionManager
{
    private string name = GlobalConst.DIR_XML_SHOOT_SOLUTION; //+ "shootsolutionset";`
    private uint count = 0;
    private bool isLoadFinish = false;
    private static ShootSolutionData shootSolutionData = new ShootSolutionData();

	public ShootSolutionManager()
	{
        Initialize();
	}
    void Initialize() 
    {
        ResourceLoadManager.Instance.GetConfigResource(name + "shootsolutionset", LoadFinish);
        for (int i = 0; i < 36; i++)
        {
            ResourceLoadManager.Instance.GetConfigResource(name + "shoot_" + i.ToString(), LoadFinish);
        }
    }

    void LoadFinish(string vPath, object obj) 
    {
        ++count;
        if (count == 37) 
        {
            isLoadFinish = true;
            GameSystem.Instance.loadConfigCnt += 1;
        }
    }

    public void ReadConfig() 
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        GameSystem.Instance.readConfigCnt += 1;

        ReadShootSolution();
    }

	public class ShootSolutionSector
	{
		public List<ShootSolution> success;
		public List<ShootSolution> fail;

		public Vector2	distance;
		public Vector2	range;

		public int index{ get; private set;}

		public ShootSolutionSector(int idx)
		{
			success = new List<ShootSolution>();
			fail = new List<ShootSolution>();
			index = idx;
		}

		public void AddSolution(ShootSolution inSolution)
		{
			List<ShootSolution> solutions = inSolution.m_bSuccess ? success : fail;
			//TODO: check whether the same 
			foreach( ShootSolution solution in solutions )
			{
				if( solution == inSolution )
					return;
			}
			solutions.Add(inSolution);
		}
	}

    public List<IM.Number> m_DistanceList = new List<IM.Number>();
    public List<IM.Number> m_AngleList = new List<IM.Number>();

	public	List<ShootSolutionSector> m_ShootSolutionSectors = new List<ShootSolutionSector>();

	private	bool m_bGoalPosition;

	public void Reset()
	{
		m_DistanceList.Clear();
		m_AngleList.Clear();
		m_ShootSolutionSectors.Clear();
	}

	public int CalcSectorIdx( IM.Vector3 rim, IM.Vector3 pos )
	{
		IM.Vector3 dirToShootTarget = pos - rim;
        dirToShootTarget.y = IM.Number.zero;
        IM.Number distToShootTarget = dirToShootTarget.magnitude;
        IM.Number fAngle = IM.Vector3.FromToAngle(IM.Vector3.right, dirToShootTarget.normalized);
		if( fAngle > IM.Math.HALF_CIRCLE )
			fAngle = IM.Math.CIRCLE - fAngle;

		int iDistanceIndex = 0;
        foreach (IM.Number distInSector in m_DistanceList)
		{
			if(distToShootTarget < distInSector)		
				break;
			iDistanceIndex++;
		}

		int iAngleIndex = 0;
        foreach (IM.Number angleInSector in m_AngleList)
		{
			if(fAngle < angleInSector)			
				break;
			iAngleIndex++;
		}
		//if(iAngle > 3 * ANGLE_MODULAR / 4)		iAngleIndex = 0;
		return iDistanceIndex * (m_AngleList.Count + 1) + iAngleIndex;
	}

	public ShootSolution GetShootSolution(int id)
	{
		int iSecId = id / 1000;
		ShootSolutionSector sector = m_ShootSolutionSectors[iSecId];
		ShootSolution result = sector.success.Find( (ShootSolution solution)=>{ return solution.m_id == id; } );
		if( result == null )
			result = sector.fail.Find( (ShootSolution solution)=>{ return solution.m_id == id; } );
		return result;
	}
	
	public ShootSolution GetShootSolution(IM.Vector3 shootTarget, IM.Vector3 pos, bool bSuccess, 
		ShootSolution.Type type = ShootSolution.Type.Shoot, bool bCleanShot = false)
	{
		int iSector = CalcSectorIdx(shootTarget, pos);
		if( m_ShootSolutionSectors[iSector].success.Count == 0 && m_ShootSolutionSectors[iSector].fail.Count == 0 )
		{
			Logger.LogError("no solution found.");
			return null;
		}


		List<ShootSolution> validSolutions = new List<ShootSolution>();
		List<ShootSolution> solutionsByType = new List<ShootSolution>();
		List<ShootSolution> allSolutions = null;

		if(bSuccess)
			allSolutions = m_ShootSolutionSectors[iSector].success;
		else
			allSolutions = m_ShootSolutionSectors[iSector].fail;

		foreach (ShootSolution solution in allSolutions)
		{
			if (solution.m_type == type)
				solutionsByType.Add(solution);
		}
		if (solutionsByType.Count == 0)
		{
			Logger.Log("No corresponding solution of type: " + type + " sector:" + iSector);
			solutionsByType = allSolutions;
		}

		if (bSuccess && (type == ShootSolution.Type.Shoot || type == ShootSolution.Type.Layup))
		{
			foreach (ShootSolution solution in solutionsByType)
			{
				if (solution.m_bCleanShot == bCleanShot)
					validSolutions.Add(solution);
			}
			if (validSolutions.Count == 0)
				Logger.Log("No clean shot solution, sector: " + iSector);
		}
		if (validSolutions.Count == 0)
			validSolutions = solutionsByType;

		if (bSuccess)
		{
			return validSolutions[Random.Range(0, validSolutions.Count)];
		}
		else
		{
			CurveRateConfig.HeightRate heightRange = GameSystem.Instance.CurveRateConfig.GetHeightRange(iSector, IM.Random.value);
			List<ShootSolution> heightValidSolutions = new List<ShootSolution>();
			if (heightRange != null)
			{
				//Logger.Log("Solution height rang: " + heightRange.minHeight + ", " + heightRange.maxHeight);
				foreach (ShootSolution solution in validSolutions)
				{
					if (heightRange.minHeight <= solution.m_fMaxHeight && solution.m_fMaxHeight < heightRange.maxHeight)
						heightValidSolutions.Add(solution);
				}
				if (heightValidSolutions.Count == 0)
					Logger.Log("No solution of height range: " + heightRange.minHeight + " to " + heightRange.maxHeight + ", Sector: " + iSector);
			}
			if (heightValidSolutions.Count == 0)
				heightValidSolutions = validSolutions;
			ShootSolution selectedSolution = heightValidSolutions[Random.Range(0, heightValidSolutions.Count)];
			Debugger.Instance.m_steamer.message += " \nSector:" + iSector + ", Solution height: " + selectedSolution.m_fMaxHeight;
			return selectedSolution;
		}
	}

	public ShootSolution GetShootSolution(int iSector, bool bSuccess, int iIndex)
	{
		ShootSolutionSector sector = m_ShootSolutionSectors[iSector];
		if( sector == null )
			return null;

		if( bSuccess )
			return sector.success[iIndex];
		else
			return sector.fail[iIndex];
	}

	public bool LoadShootSolutionSet(bool bForEditor)
	{
        //Reset();

        m_DistanceList = shootSolutionData.distanceList;
        m_AngleList = shootSolutionData.angleList;
		
		int iNumSector = (m_DistanceList.Count + 1) * (m_AngleList.Count + 1);
		for( int idx = 0; idx < iNumSector; idx++)
		{
            if (shootSolutionData.shootSolutionSectors.ContainsKey(idx))
            {
                ShootSolutionSector sector = shootSolutionData.shootSolutionSectors[idx];
                if (sector != null)
                    m_ShootSolutionSectors.Add(sector);
            }
		}

        //if (bForEditor)
        //    CorrectSolutionSector();

		return true;
	}

	void CorrectSolutionSector()
	{
		foreach (ShootSolutionSector sector in m_ShootSolutionSectors)
		{
			CorrectSolutionSector(sector.index, ref sector.success);
			CorrectSolutionSector(sector.index, ref sector.fail);
		}
	}

	void CorrectSolutionSector(int index, ref List<ShootSolution> solutions)
	{
		for (int i = 0; i < solutions.Count;)
		{
			ShootSolution solution = solutions[i];
            int actualIndex = CalcSectorIdx(new IM.Vector3(IM.Number.zero, IM.Number.zero, new IM.Number(12,800)), solution.m_vInitPos);
			if (index != actualIndex)
			{
				Logger.Log("Sector mismatching, saved index : " + index + " actual index: " + actualIndex + ". Corrected. Please save again.");
				ShootSolutionSector actualSector = m_ShootSolutionSectors.Find((ShootSolutionSector sector) => { return sector.index == actualIndex; });
				actualSector.AddSolution(solution);
				solutions.RemoveAt(i);
			}
			else
				++i;
		}
	}

	public bool SaveShootSolutionSet(string filePath)
	{
		XmlDocument doc = new XmlDocument();
		XmlProcessingInstruction pPI = doc.CreateProcessingInstruction("xml","version=\"1.0\" encoding=\"UTF-8\"");
		doc.AppendChild(pPI);
		
		XmlElement pElementRoot = doc.CreateElement("root");
		doc.AppendChild(pElementRoot);
		
		XmlElement pNodeDistanceRange = pNodeDistanceRange = doc.CreateElement("distance_range");
		pElementRoot.AppendChild(pNodeDistanceRange);

        foreach (IM.Number distance in m_DistanceList)
		{
			XmlElement pNodeDistance = doc.CreateElement("distance");
			XmlText pDistanceValue = doc.CreateTextNode(distance.ToString());
			pNodeDistanceRange.AppendChild(pNodeDistance);
			pNodeDistance.AppendChild(pDistanceValue);
		}
		
		XmlElement pNodeAngleRange = doc.CreateElement("angle_range");
		pElementRoot.AppendChild(pNodeAngleRange);
        foreach (IM.Number angle in m_AngleList)
		{
			XmlElement pNodeAngle = doc.CreateElement("angle");
			XmlText pAngleValue = doc.CreateTextNode(angle.ToString());
			pNodeAngleRange.AppendChild(pNodeAngle);
			pNodeAngle.AppendChild(pAngleValue);
		}

		int iSectorCount = m_ShootSolutionSectors.Count;
		for(int idx = 0; idx < iSectorCount; idx++ )
		{
			XmlElement pNodeSector = doc.CreateElement("sector");
			XmlElement pNodeIndex  = doc.CreateElement("index");
			XmlElement pNodePath   = doc.CreateElement("path");
			
			XmlText pTextIndex = doc.CreateTextNode(idx.ToString());
			XmlText pTextPath  = doc.CreateTextNode("shoot_"+idx);
			
			pElementRoot.AppendChild(pNodeSector);
			pNodeSector.AppendChild(pNodeIndex);
			pNodeSector.AppendChild(pNodePath);
			pNodeIndex.AppendChild(pTextIndex);
			pNodePath.AppendChild(pTextPath);
		}

		doc.Save(filePath + ".xml");
		return true;
	}

	public void SaveShootSolutionSector(int iSector, string path)
	{
		if( iSector >= m_ShootSolutionSectors.Count )
			return;
		
		XmlDocument doc = new XmlDocument();
		XmlProcessingInstruction pPI = doc.CreateProcessingInstruction("xml","version=\"1.0\" encoding=\"UTF-8\"");
		doc.AppendChild(pPI);
		
		XmlElement pElementRoot = doc.CreateElement("root");
		doc.AppendChild(pElementRoot);
		
		List<ShootSolution> successShootSolutions = m_ShootSolutionSectors[iSector].success;
		foreach(ShootSolution solution in successShootSolutions)
		{
			XmlElement pNodeSuccess = doc.CreateElement("success");
			pElementRoot.AppendChild(pNodeSuccess);
			solution.Save(doc, pNodeSuccess);
		}
		
		List<ShootSolution> failShootSolutions = m_ShootSolutionSectors[iSector].fail;
		foreach(ShootSolution solution in failShootSolutions)
		{
			XmlElement pNodeFail = doc.CreateElement("fail");
			pElementRoot.AppendChild(pNodeFail);
			solution.Save(doc, pNodeFail);
		}
		
		doc.Save( path + "_" + iSector + ".xml");
	}

    public void ReadShootSolution()
    {
        string text = ResourceLoadManager.Instance.GetConfigText(name + "shootsolutionset");
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name + "shootsolutionset");
            return;
        }

        //读取以及处理XML文本的类
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(name + "shootsolutionset", text);
        //解析xml的过程
        XmlNode root = xmlDoc.ChildNodes[0];
        if (root == null)
            return;

        //ShootSolutionData shootSolutionData = new ShootSolutionData();
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectNodes("root/distance_range/distance");
        foreach (XmlElement elem in nodelist)
        {
            IM.Number distance = IM.Number.Parse(elem.InnerText);
            shootSolutionData.distanceList.Add(distance);
        }

        nodelist = xmlDoc.SelectNodes("root/angle_range/angle");
        foreach (XmlElement elem in nodelist)
        {
            IM.Number fAngle = IM.Number.Parse(elem.InnerText);
            shootSolutionData.angleList.Add(fAngle);
        }

        for (int i = 0; i < 36; i++)
        {
            string path = string.Format("shoot_{0}", i);
            ReadShootSolutionSector(i, path);
        }
    }

    void ReadShootSolutionSector(int index, string filePath) 
    {
        ShootSolutionSector sector = new ShootSolutionSector(index);

        //读取以及处理XML文本的类
        string text = ResourceLoadManager.Instance.GetConfigText(GlobalConst.DIR_XML_SHOOT_SOLUTION + filePath);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + name + filePath);
            return;
        }
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SHOOT_SOLUTION + filePath, text);
        //解析xml的过程

        int iSolutionIdx = 0;
        XmlNodeList nodelist = xmlDoc.SelectNodes("root/success");
        foreach (XmlElement elem in nodelist)
        {
            ShootSolution sucSolution = new ShootSolution(index * 1000 + iSolutionIdx);
            if (!sucSolution.Create(elem, true))
                continue;
            sector.success.Add(sucSolution);
            iSolutionIdx++;
        }

        nodelist = xmlDoc.SelectNodes("root/fail");
        foreach (XmlElement elem in nodelist)
        {
            ShootSolution failSolution = new ShootSolution(index * 1000 + iSolutionIdx);
            if (!failSolution.Create(elem, false))
                continue;
            sector.fail.Add(failSolution);
            iSolutionIdx++;
        }

        shootSolutionData.shootSolutionSectors.Add(index, sector);
    }

    //以下是编辑器*****************************************************
    public bool LoadShootSolutionSet(string path, bool bForEditor)
    {
        Reset();

        //读取以及处理XML文本的类
        XmlDocument xmlDoc;
        if (bForEditor)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load("Assets/Resources/" + path + ".xml");
        }
        else
            xmlDoc = CommonFunction.LoadXmlConfig(path);
        //解析xml的过程
        XmlNodeList nodelist = xmlDoc.SelectNodes("root/distance_range/distance");
        foreach (XmlElement elem in nodelist)
        {
            IM.Number distance = IM.Number.Parse(elem.InnerText);
            m_DistanceList.Add(distance);
        }

        nodelist = xmlDoc.SelectNodes("root/angle_range/angle");
        foreach (XmlElement elem in nodelist)
        {
            IM.Number fAngle = IM.Number.Parse(elem.InnerText);
            m_AngleList.Add(fAngle);
        }

        int iNumSector = (m_DistanceList.Count + 1) * (m_AngleList.Count + 1);
        for (int idx = 0; idx < iNumSector; idx++)
        {
            path = string.Format("shoot_{0}", idx);
            ShootSolutionSector sector = _LoadShootSolutionSector(idx, path, bForEditor);
            if (sector != null)
                m_ShootSolutionSectors.Add(sector);
        }

        if (bForEditor)
            CorrectSolutionSector();

        return true;
    }

    ShootSolutionSector _LoadShootSolutionSector(int idx, string strFileName, bool bForEditor)
    {
        ShootSolutionSector sector = new ShootSolutionSector(idx);

        //读取以及处理XML文本的类
        XmlDocument xmlDoc;
        if (bForEditor)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load("Assets/Resources/" + GlobalConst.DIR_XML_SHOOT_SOLUTION + strFileName + ".xml");
        }
        else
        {
            xmlDoc = CommonFunction.LoadXmlConfig(GlobalConst.DIR_XML_SHOOT_SOLUTION + strFileName);
        }
        //解析xml的过程

        int iSolutionIdx = 0;
        XmlNodeList nodelist = xmlDoc.SelectNodes("root/success");
        foreach (XmlElement elem in nodelist)
        {
            ShootSolution sucSolution = new ShootSolution(idx * 1000 + iSolutionIdx);
            if (!sucSolution.Create(elem, true, bForEditor))
                continue;
            sector.success.Add(sucSolution);
            iSolutionIdx++;
        }

        nodelist = xmlDoc.SelectNodes("root/fail");
        foreach (XmlElement elem in nodelist)
        {
            ShootSolution failSolution = new ShootSolution(idx * 1000 + iSolutionIdx);
            if (!failSolution.Create(elem, false, bForEditor))
                continue;
            sector.fail.Add(failSolution);
            iSolutionIdx++;
        }
        return sector;
    }
};
