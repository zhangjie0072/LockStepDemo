using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ErrorDisplay : MonoBehaviour {
	public static ErrorDisplay Instance;

	int initialWidth;
	float scaleFactor = 1f;
	void Awake () {
		if (Instance != null)
		{
			Object.Destroy(gameObject);
			return;
		}
		Instance = this;
		Object.DontDestroyOnLoad(gameObject);

		ReadScriptHistory();
		for (int i = 0; ; ++i)
		{
			string f = PlayerPrefs.GetString("LogFilter" + i);
			if (string.IsNullOrEmpty(f))
				break;
			m_filters.Add(f);
		}
		int enableLog = PlayerPrefs.GetInt("LogEnableLog");
		if (enableLog != 0)
			showLog = (enableLog == 1);
		int enableWarning = PlayerPrefs.GetInt("LogEnableWarning");
		if (enableWarning != 0)
			showWarning = (enableWarning == 1);
		int enableError = PlayerPrefs.GetInt("LogEnableError");
		if (enableError != 0)
			showError = (enableError == 1);
		int enableErrorPause = PlayerPrefs.GetInt("LogErrorPause");
		if (enableErrorPause != 0)
			errorPause = (enableErrorPause == 1);

		float widthInch = (float)Screen.width / Screen.dpi;
		if (widthInch > 6)
			scaleFactor = widthInch / 6;
		initialWidth = Screen.width;
	}

	void Update()
	{
		if (initialWidth != Screen.width)
		{
			float widthInch = (float)Screen.width / Screen.dpi;
			if (widthInch > 6)
				scaleFactor = widthInch / 6;
			styleInited = false;
			initialWidth = Screen.width;
		}

		if (Input.GetKeyDown(KeyCode.C))
		{
			logs.Clear();
			cache.Clear();
			for (int i = 0; i < 5; ++i)
				logCount[i] = 0;
		}

		if (!string.IsNullOrEmpty(logFilename))
		{
			Logger.Log("Save log to file: " + logFilename);
			logFilename = string.Empty;
		}
	}

	bool _enabled = false;

    internal void OnEnable()
    {
        Application.RegisterLogCallback(HandleLog);
		_enabled = true;
    }

    internal void OnDisable()
    {
        Application.RegisterLogCallback(null);
		_enabled = false;
    }

	void OnDestroy()
	{
		WriteScriptHistory();
	}

	class Log
	{
		public LogType type;
		public string log;
		public string stackTrace;
	}

	private Queue<Log> logs = new Queue<Log>();
	private Queue<Log> cache = new Queue<Log>();
	Log lastLog;
	int[] logCount = new int[5];
	public int maxCount;
	[HideInInspector]
	public bool showLog = true;
	[HideInInspector]
	public bool showWarning = true;
	[HideInInspector]
	public bool showError = true;
	[HideInInspector]
	public bool frozen = false;
	[HideInInspector]
	public bool errorPause = false;
	public bool hide = true;
	public string[] presetScript;
	LinkedList<string> scriptHistory = new LinkedList<string>();
	bool showScriptHistory = false;

	string logFilename = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logString">错误信息</param>
    /// <param name="stackTrace">跟踪堆栈</param>
    /// <param name="type">错误类型</param>
    public void HandleLog(string logString, string stackTrace, LogType type)
    {
		if (frozen) return;
		if (!_enabled) return;

		System.DateTime now = System.DateTime.Now;
		string strLog = string.Format("({0:D2}:{1:D2}:{2:D2}.{3:D3}) {4}", now.Hour, now.Minute, now.Second, now.Millisecond, logString);
		lock (logs)
		{
			Log l = new Log();
			l.type = type;
			l.log = strLog;
			l.stackTrace = stackTrace.TrimEnd(new char[] { '\r', '\n' });
			if (!inLayouting)
				AddLog(l);
			else
				cache.Enqueue(l);
			lastLog = l;
		}

		if (errorPause && (type == LogType.Error || type == LogType.Exception || type == LogType.Assert))
			GameSystem.Instance.Pause = true;

    }

	public void SupplementLastLogStackTrace(string stackTrace)
	{
		if (lastLog != null)
			lastLog.stackTrace += stackTrace;
	}

	void AddLog(Log log)
	{
		while (logs.Count > maxCount)
		{
			logs.Dequeue();
			--logCount[(int)log.type];
		}
		logs.Enqueue(log);
		++logCount[(int)log.type];
	}

	GUIStyle styleVBtn;
	GUIStyle styleHBtn;
	GUIStyle styleEntryBtn;
	GUIStyle styleTextField;
	GUIStyle styleLabel;
	GUIStyle styleVSB;
	GUIStyle styleHSB;
	GUIStyle styleToggle;
	float viewWidth;
	public Texture2D tex;
	GUIStyle styleBox;
	GUILayoutOption littleBtnWidth;
	GUILayoutOption middleBtnWidth;
	bool styleInited = false;

	bool enableFunc;
	bool enableFilter = true;
    Vector2 m_scrollPos;
	List<string> m_filters = new List<string>();
	Log activeLog;
	string m_searchText = string.Empty;
	int m_searchHitIndex = 0;
	bool m_rewind = false;
	bool enableScript = false;
	string scriptText = string.Empty;

	bool inLayouting;

    internal void OnGUI()
    {
		if (!styleInited)
		{
			styleVBtn = new GUIStyle(GUI.skin.button);
			styleHBtn = new GUIStyle(GUI.skin.button);
			styleEntryBtn = new GUIStyle(GUI.skin.button);
			styleTextField = new GUIStyle(GUI.skin.textField);
			styleLabel = new GUIStyle(GUI.skin.label);
			styleVSB = new GUIStyle(GUI.skin.verticalScrollbar);
			styleHSB = new GUIStyle(GUI.skin.horizontalScrollbar);
			styleToggle = new GUIStyle(GUI.skin.button);
			styleBox = new GUIStyle(GUI.skin.box);

			int fontSize = (int)(0.10f * Screen.dpi * scaleFactor);
			styleVBtn.fixedWidth = 0.16f * Screen.dpi * scaleFactor;
			styleVBtn.fixedHeight = Screen.height;
			styleVBtn.fontSize = fontSize;
			styleHBtn.fixedHeight = 0.18f * Screen.dpi * scaleFactor;
			styleHBtn.fontSize = fontSize;
			styleEntryBtn.fontSize = fontSize;
			styleToggle.fixedHeight = 0.18f * Screen.dpi * scaleFactor;
			styleToggle.fontSize = fontSize;
			styleTextField.fixedHeight = 0.18f * Screen.dpi * scaleFactor;
			styleTextField.fontSize = fontSize;
			styleTextField.alignment = TextAnchor.MiddleLeft;
			styleLabel.fontSize = fontSize;
			styleLabel.wordWrap = false;
			styleLabel.alignment = TextAnchor.MiddleLeft;
			styleVSB.fixedWidth = 0.1f * Screen.dpi * scaleFactor;
			GUI.skin.verticalScrollbarThumb.fixedWidth = styleVSB.fixedWidth;
			styleHSB.fixedHeight = 0.1f * Screen.dpi * scaleFactor;
			GUI.skin.horizontalScrollbarThumb.fixedHeight = styleHSB.fixedHeight;
			styleBox.normal.background = tex;
			viewWidth = Screen.width * 0.9f;
			littleBtnWidth = GUILayout.Width(0.18f * Screen.dpi * scaleFactor);
			middleBtnWidth = GUILayout.Width(0.32f * Screen.dpi * scaleFactor);

			styleInited = true;
		}

		lock (logs)
		{
			if (Event.current.type == EventType.layout)
				inLayouting = true;

			GUILayout.BeginHorizontal();

			GUILayout.BeginVertical();
			if (GUILayout.Button(enableFunc ? "<" : ">", styleVBtn))
			{
				enableFunc = !enableFunc;
				//UICamera.currentCamera.GetComponent<UICamera>().enabled = !enableFunc;
				if (enableFunc)
					hide = false;
			}
			GUILayout.EndVertical();

			if (!hide)
			{
				Color tmp = GUI.color;
				GUI.color = new Color(0, 0, 0, enableFunc ? 0.9f : 0.5f);
				GUILayout.BeginVertical(styleBox, GUILayout.Width(viewWidth));
				GUI.color = tmp;


				bool toSearch = false;
				bool toSave = false;
				FileStream file = null;
				StreamWriter writer = null;
				if (enableFunc)
				{
					GUILayout.BeginHorizontal();
					if (GUILayout.Button("Add Filter", styleHBtn))
					{
						m_filters.Add(string.Empty);
					}
					enableFilter = GUILayout.Toggle(enableFilter, "Enable Filter", styleToggle);
					if (GUILayout.Button("Clear", styleHBtn))
					{
						logs.Clear();
						cache.Clear();
						for (int i = 0; i < 5; ++i)
							logCount[i] = 0;
					}
					frozen = GUILayout.Toggle(frozen, "Frozen", styleToggle);
					bool enableErrPause = GUILayout.Toggle(errorPause, "Error Pause", styleToggle);
					if (enableErrPause != errorPause)
					{
						if (!enableErrPause)
							GameSystem.Instance.Pause = false;
						errorPause = enableErrPause;
						PlayerPrefs.SetInt("LogErrorPause", errorPause ? 1 : -1);
					}
					enableScript = GUILayout.Toggle(enableScript, "Script", styleToggle);
					if (GUILayout.Button("Save", styleHBtn))
					{
						toSave = true;
						System.DateTime now = System.DateTime.Now;
						logFilename = string.Format("{0}/{1:D4}-{2:D2}-{3:D2}({4:D2}.{5:D2}.{6:D2}).log",
							Application.persistentDataPath, now.Year, now.Month, now.Day,
							now.Hour, now.Minute, now.Second);
						file = new FileStream(logFilename, FileMode.OpenOrCreate);
						writer = new StreamWriter(file);
					}
					if (GUILayout.Button("<", styleHBtn, littleBtnWidth))
					{
						hide = true;
						enableFunc = false;
						//UICamera.currentCamera.GetComponent<UICamera>().enabled = true;
					}
					GUILayout.EndHorizontal();

					if (enableFilter)
					{
						if (toSave) writer.Write("Filters: ");
						// 3 filter per line
						GUILayout.BeginHorizontal();
						for (int i = 0; i < m_filters.Count; ++i)
						{
							if (i / 3 > 0 && i % 3 == 0)
							{
								GUILayout.EndHorizontal();
								GUILayout.BeginHorizontal();
							}
							string filter = GUILayout.TextField(m_filters[i], styleTextField);
							if (toSave) writer.Write(filter + ", ");
							if (filter != m_filters[i])
							{
								m_filters[i] = filter;
								PlayerPrefs.SetString("LogFilter" + i, filter);
							}
							if (GUILayout.Button("X", styleHBtn, littleBtnWidth))
							{
								m_filters.RemoveAt(i);
								PlayerPrefs.DeleteKey("LogFilter" + i);
								return;
							}
						}
						GUILayout.EndHorizontal();
						if (toSave) writer.WriteLine();
						if (toSave) writer.WriteLine();
					}

					if (enableScript)
					{
						GUILayout.BeginHorizontal();
						float labelWidth = 0.27f * Screen.dpi * scaleFactor;
						float btnWidth = 0.2f * Screen.dpi * scaleFactor;
						GUILayout.Label("Script:", styleLabel,
							GUILayout.Width(labelWidth), GUILayout.Height(0.18f * Screen.dpi * scaleFactor));
						scriptText = GUILayout.TextField(scriptText, styleTextField, GUILayout.MaxWidth(viewWidth - labelWidth - btnWidth * 2));
						scriptText = scriptText.TrimEnd(new char[] { '\r', '\n', ' ' });
						bool exec = GUILayout.Button("E", styleHBtn, GUILayout.Width(btnWidth));
						if ((Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.Return) || exec)
						{
							//LuaScriptMgr.Instance.DoString(scriptText);
							AddScriptHistory(scriptText);
							scriptText = string.Empty;
						}
						showScriptHistory = GUILayout.Toggle(showScriptHistory, "H", styleHBtn, GUILayout.Width(btnWidth));
						GUILayout.EndHorizontal();
						if (showScriptHistory)
						{
							foreach (string script in scriptHistory)
							{
								if (GUILayout.Button(script, styleLabel, GUILayout.MaxWidth(viewWidth)))
									scriptText = script;
							}
						}
					}

					GUILayout.BeginHorizontal();
					GUI.color = showLog ? Color.green : Color.grey;
					bool enableLog = GUILayout.Toggle(showLog, "L" + logCount[(int)LogType.Log], styleToggle, middleBtnWidth);
					if (enableLog != showLog)
					{
						showLog = enableLog;
						PlayerPrefs.SetInt("LogEnableLog", showLog ? 1 : -1);
					}
					GUI.color = showWarning ? Color.yellow : Color.grey;
					bool enableWarning = GUILayout.Toggle(showWarning, "W" + logCount[(int)LogType.Warning], styleToggle, middleBtnWidth);
					if (enableWarning != showWarning)
					{
						showWarning = enableWarning;
						PlayerPrefs.SetInt("LogEnableWarning", showWarning ? 1 : -1);
					}
					GUI.color = showError ? Color.red : Color.grey;
					int count = logCount[(int)LogType.Error] + logCount[(int)LogType.Exception] + logCount[(int)LogType.Assert];
					bool enableError = GUILayout.Toggle(showError, "E" + count, styleToggle, middleBtnWidth);
					if (enableError != showError)
					{
						showError = enableError;
						PlayerPrefs.SetInt("LogEnableError", showError ? 1 : -1);
					}
					GUI.color = Color.white;
					GUILayout.Label("Find:", styleLabel,
						GUILayout.Width(0.22f * Screen.dpi * scaleFactor),
						GUILayout.Height(0.18f * Screen.dpi * scaleFactor));
					string searchText = GUILayout.TextField(m_searchText, styleTextField);
					if (searchText != m_searchText)
					{
						toSearch = true;
						m_searchText = searchText;
						if (!string.IsNullOrEmpty(m_searchText))
							m_searchHitIndex = 1;
					}
					if (GUILayout.Button("X", styleHBtn, littleBtnWidth))
					{
						m_searchText = string.Empty;
						return;
					}
					if (GUILayout.Button("Prev", styleHBtn, GUILayout.Width(0.5f * Screen.dpi * scaleFactor)))
					{
						--m_searchHitIndex;
						m_searchHitIndex = Mathf.Max(m_searchHitIndex, 0);
						toSearch = true;
					}
					if (GUILayout.Button("Next", styleHBtn, GUILayout.Width(0.5f * Screen.dpi * scaleFactor)))
					{
						++m_searchHitIndex;
						toSearch = true;
					}
					GUILayout.EndHorizontal();
				}

				if (m_rewind)
				{
					m_searchHitIndex = 1;
					toSearch = true;
					m_rewind = false;
				}

				bool searchHit = false;
				bool hasMatch = false;
				int searchHitIndex = 0;
				float totalHeight = 0;
				Vector2 scrollPos = GUILayout.BeginScrollView(m_scrollPos, styleHSB, styleVSB);
				if (!enableFunc)
				{
					m_scrollPos.x = scrollPos.x;
					m_scrollPos.y = float.MaxValue;
				}
				else
					m_scrollPos = scrollPos;
				foreach (Log l in logs)
				{
					if (l.type == LogType.Log)
					{
						GUI.color = Color.green;
						if (!showLog)
							continue;
					}
					else if (l.type == LogType.Warning)
					{
						GUI.color = Color.yellow;
						if (!showWarning)
							continue;
					}
					else if (l.type == LogType.Error || l.type == LogType.Exception || l.type == LogType.Assert)
					{
						GUI.color = Color.red;
						if (!showError)
							continue;
					}
					if ((l.type == LogType.Log || l.type == LogType.Warning) && !Filter(l.log))
						continue;
					if (!searchHit && !string.IsNullOrEmpty(m_searchText) && l.log.Contains(m_searchText))
					{
						++searchHitIndex;
						hasMatch = true;
						if (searchHitIndex == m_searchHitIndex)
						{
							searchHit = true;
							GUI.color = Color.magenta;
							if (toSearch)
								m_scrollPos.y = totalHeight - Screen.height * 0.4f;
						}
					}
					if (enableFunc)
					{
						GUILayout.BeginHorizontal();
						bool isActiveLog = (l == activeLog);
						if (GUILayout.Button(isActiveLog ? "↑" : "↓", styleEntryBtn, littleBtnWidth))
						{
							if (isActiveLog)
								activeLog = null;
							else
								activeLog = l;
						}
						GUILayout.Label(l.log, styleLabel);
						totalHeight += styleLabel.CalcHeight(new GUIContent(l.log), viewWidth) + styleLabel.margin.vertical / 2;
						GUILayout.EndHorizontal();
						if (l == activeLog)
						{
							GUILayout.Label(l.stackTrace, styleLabel);
							totalHeight += styleLabel.CalcHeight(new GUIContent(l.stackTrace), viewWidth) + styleLabel.margin.vertical / 2;
						}
					}
					else
					{
						GUILayout.Label(l.log, styleLabel);
						totalHeight += styleLabel.CalcHeight(new GUIContent(l.log), viewWidth) + styleLabel.margin.vertical / 2;
					}
					if (toSave) writer.WriteLine(l.log);
				}
				if (hasMatch && m_searchHitIndex > searchHitIndex)
					m_rewind = true;
				else if (!hasMatch)
					m_searchHitIndex = 0;
				GUILayout.EndScrollView();

				GUILayout.EndVertical();

				if (toSave)
				{
					writer.Flush();
					file.Flush();
				}
			}
			GUILayout.EndHorizontal();

			if (Event.current.type == EventType.repaint)
			{
				inLayouting = false;
				while (cache.Count > 0)
					AddLog(cache.Dequeue());
			}
		}
    }

	bool Filter(string log)
	{
		if (!enableFilter)
			return true;
		if (m_filters.Count == 0)
			return true;
		else
		{
			bool hasValid = false;
			bool contains = false;
			foreach (string filter in m_filters)
			{
				if (string.IsNullOrEmpty(filter))
					continue;
				else if (filter.StartsWith("~"))
				{
					string f = filter.Substring(1);
					if (!string.IsNullOrEmpty(f) && log.Contains(f))
						return false;
				}
				else
				{
					hasValid = true;
					if (log.Contains(filter))
						contains = true;
				}
			}
			return !hasValid || contains;
		}
	}

	void ReadScriptHistory()
	{
		scriptHistory.Clear();
		foreach (string s in presetScript)
			AddScriptHistory(s);
		int count = PlayerPrefs.GetInt("ScriptHistoryCount");
		for (int i = count - 1; i >= 0; --i)
		{
			string s = PlayerPrefs.GetString("ScriptHistory" + i);
			if (string.IsNullOrEmpty(s))
				continue;
			AddScriptHistory(s);
		}
	}

	void WriteScriptHistory()
	{
		PlayerPrefs.SetInt("ScriptHistoryCount", scriptHistory.Count);
		int i = 0;
		foreach (string s in scriptHistory)
		{
			if (string.IsNullOrEmpty(s))
				continue;
			PlayerPrefs.SetString("ScriptHistory" + i++, s);
		}
	}

	void AddScriptHistory(string script)
	{
		if (string.IsNullOrEmpty(script))
			return;
		scriptHistory.Remove(script);
		scriptHistory.AddFirst(script);
		while (scriptHistory.Count > 10)
			scriptHistory.RemoveLast();
	}
}
