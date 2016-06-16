using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerMoveCollider
{
    private Player m_Owner;
    IM.Number bodyWidth = new IM.Number(0,400);
    public PlayerMoveCollider(Player player)
    {
        m_Owner = player;
    }
    public IM.Vector3 AdjustDeltaMove(IM.Vector3 deltaMove)
    {

        PlayerManager pm = GameSystem.Instance.mClient.mPlayerManager;
        foreach (Player player in pm.m_Players)
        {
            if (m_Owner == player)
                continue;

            IM.Number fDis = GameUtils.HorizonalDistance(m_Owner.position, player.position);
            //IM.Number radius1 = (new IM.Number(1, 500) + GlobalConst.CHARACTER_SKIN_WIDTH) * m_Owner.scale.z;
            //IM.Number radius2 = (new IM.Number(1, 500) + GlobalConst.CHARACTER_SKIN_WIDTH) * player.scale.z;
            if (fDis < bodyWidth * 2)
            {
                IM.Vector3 fDirToTarget = GameUtils.HorizonalNormalized(player.position, m_Owner.position);
                IM.Number preSpeed = deltaMove.magnitude;
                IM.Vector3 deltaMoveDir = deltaMove.normalized;
                IM.Vector3 tempDir = IM.Vector3.CrossAndNormalize(deltaMoveDir, fDirToTarget);
                IM.Vector3 realDir = IM.Vector3.CrossAndNormalize(fDirToTarget, tempDir);
                IM.Number angle = IM.Vector3.AngleRad(deltaMoveDir, fDirToTarget);
                if (angle > IM.Math.HALF_PI)
                    break; 
                IM.Number realSpeed = IM.Math.Sin(angle) * preSpeed;
                if (IM.Number.Approximately(realSpeed,IM.Number.zero))
                    break;
                deltaMove = realDir * realSpeed;
                break;
            }
        }
        return deltaMove;
    }
} 
