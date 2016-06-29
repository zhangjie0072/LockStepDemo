using System;
using UnityEngine;
using System.Collections;

public class solutionSectorDisplay : MonoBehaviour {

    public Rect startRect = new Rect(10, 10, 100, 50); // The rect the window is initially displayed at.
    public bool updateColor = true; // Do you want the color to change if the FPS gets low
    public bool allowDrag = true; // Do you want to allow the dragging of the FPS window
    public float frequency = 0.5F; // The update frequency of the fps
    public int nbDecimal = 1; // How many decimal do you want to display
    public bool isLockTest = false;
    private Color color = Color.white; // The color of the GUI, depending of the FPS ( R < 10, Y < 30, G >= 30 )
    private GUIStyle style; // The style the text will be displayed at, based en defaultSkin.label.

    private string sector = "";
    void Start()
    {
       
    }

    void Update()
    {
        if (GameSystem.Instance.mClient.mCurMatch != null &&
            GameSystem.Instance.mClient.mCurMatch.m_stateMachine != null &&
            GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState != null &&
            GameSystem.Instance.mClient.mCurMatch.m_stateMachine.m_curState.m_eState == MatchState.State.ePlaying)
        {
            GameSystem.Instance.shootSolutionManager.isLock = isLockTest;
            IM.Vector3 _rim = GameSystem.Instance.mClient.mCurMatch.mCurScene.mBasket.m_rim.center;
            IM.Vector3 _position = GameSystem.Instance.mClient.mCurMatch.mainRole.position;
            sector = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(_rim, _position).ToString();
        }
    }

    //IEnumerator FPS() 
    //{
    //    // Infinite loop executed every "frenquency" secondes.
    //    while (true)
    //    {
    //        if (GameSystem.Instance.mClient.mCurMatch != null)
    //        {
    //            Vector3 _rim = GameSystem.Instance.mClient.mCurMatch.mCurScene.mBasket.m_rim.center;
    //            Vector3 _position = GameSystem.Instance.mClient.mCurMatch.m_mainRole.position;
    //            sector = GameSystem.Instance.shootSolutionManager.CalcSectorIdx(_rim, _position).ToString();
    //        }
    //        yield return new WaitForSeconds(frequency);
    //    }
    //}

    void OnGUI()
    {
        // Copy the default label skin, change the color and the alignement
        if (style == null)
        {
            style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontSize = 20;
        }

        GUI.color = updateColor ? color : Color.white;
        startRect = GUI.Window(0, startRect, DoMyWindow, "");
    }

    void DoMyWindow(int windowID)
    {
        GUI.Label(new Rect(0, 0, startRect.width, startRect.height), sector, style);
        if (allowDrag) GUI.DragWindow(new Rect(0, 0, Screen.width, Screen.height));
    }
}
