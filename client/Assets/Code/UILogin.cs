using UnityEngine;
using System.Collections;

public class UILogin : MonoBehaviour
{
    string ip = "192.168.1.75";
    string port = "9802";
    string accName = "1";
    bool toLogin;

    void Start()
    {
        NetworkManager.Instance.onServerConnected += OnServerConnected;
    }

    // Update is called once per frame
    void Update()
    {
        if (toLogin)
        {
            NetworkManager.Instance.ConnectToGS(ip, int.Parse(port));
            toLogin = false;
        }
    }

    void OnServerConnected(NetworkConn.Type type)
    {
        if (type == NetworkConn.Type.eGameServer)
        {
            Logger.Log("Server connected.");
            GameMsgSender.SendEnterPlat();
            Object.Destroy(gameObject);
        }
    }

    internal void OnGUI()
    {
        //if (Event.current.type == EventType.layout)
        {
			int fontSize = (int)(0.10f * Screen.dpi);
            GUIStyle style = new GUIStyle(GUI.skin.textField);
			style.fixedHeight = 0.18f * Screen.dpi;
			style.fontSize = fontSize;
			style.alignment = TextAnchor.MiddleLeft;

            GUILayout.BeginHorizontal(GUILayout.Width(Screen.dpi * 1.2f));
            GUILayout.Label("   IP:");
            GUILayout.TextField(ip, GUILayout.Width(Screen.dpi * 1f));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal(GUILayout.Width(Screen.dpi * 1.2f));
            GUILayout.Label("Port:");
            GUILayout.TextField(port, GUILayout.Width(Screen.dpi * 1f));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Name:");
            accName = GUILayout.TextField(accName, 10, GUILayout.Width(Screen.dpi * 1f));
            if (GUILayout.Button("Login"))
            {
                GameSystem.Instance.AccountID = uint.Parse(accName);
                toLogin = true;
            }
            GUILayout.EndHorizontal();
        }
    }
}
