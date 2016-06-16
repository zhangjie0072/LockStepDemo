using UnityEngine;
using System.Collections;

public enum 动作控制类型枚举 { 切换Bool, 打开Bool, 关闭Bool, 设置Int, 修正Int, Trigger }
public class AnimatorContrl : MonoBehaviour
{
    public Animator 动作控制器;
    public string 参数名;
    public 动作控制类型枚举 类型;
    public int Int操作值;

    public void 操作()
    {
		动作控制器.enabled = true;
        switch (类型)
        {
            case 动作控制类型枚举.切换Bool:
                动作控制器.SetBool(参数名, !动作控制器.GetBool(参数名)); break;
            case 动作控制类型枚举.打开Bool:
                动作控制器.SetBool(参数名, true); break;
            case 动作控制类型枚举.关闭Bool:
                动作控制器.SetBool(参数名, false); break;
            case 动作控制类型枚举.Trigger:
                动作控制器.SetTrigger(参数名); break;
            case 动作控制类型枚举.设置Int:
                动作控制器.SetInteger(参数名, Int操作值); break;
            case 动作控制类型枚举.修正Int:
                动作控制器.SetInteger(参数名, 动作控制器.GetInteger(参数名) + Int操作值); break;
        }
    }

}
