using System.Collections.Generic;
using fogs.proto.msg;
using UnityEngine;

public class GameView : Singleton<GameView>
{
    public Dictionary<uint, GameObject> playerModels { get; private set; }

    public void Initialize()
    {
        playerModels = new Dictionary<uint, GameObject>();

        var playerInfos = Game.Instance.playerInfos;
        if (playerInfos != null)
        {
            Object prefab = Resources.Load("Prefab/FootballPlayer");
            foreach (var info in playerInfos)
            {
                GameObject go = GameObject.Instantiate(prefab) as GameObject;
                go.name = "Player_" + info.Value.ID;
                SetModelByInfo(go, info.Value);
                playerModels.Add(info.Key, go);
            }
        }
    }

    void SetModelByInfo(GameObject go, PlayerInfo info)
    {
        Vector3 newPosition = (Vector3)info.moveCtrl.position;
        float velocity = ((Vector3)info.moveCtrl.velocity).magnitude;
        float towardsVelocity = Mathf.Clamp(velocity, 1, float.MaxValue);
        go.transform.position = Vector3.MoveTowards(go.transform.position, newPosition, towardsVelocity * Time.deltaTime);
        if (!Mathf.Approximately(velocity, 0f))
            go.transform.forward = (Vector3)info.moveCtrl.forward;
        Animator animator = go.GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetFloat("velocity", velocity);
            animator.SetInteger("action", (int)info.state);
        }
        MeshRenderer renderer = go.GetComponent<MeshRenderer>();
        if (renderer == null)
            return;
        uint state = info.state;
        switch (state)
        {
            case 0:
                renderer.material.color = Color.white;
                break;
            case 1:
                renderer.material.color = Color.red;
                break;
            case 2:
                renderer.material.color = Color.green;
                break;
            case 3:
                renderer.material.color = Color.blue;
                break;
        }
    }

    public void Update()
    {
        var playerInfos = Game.Instance.playerInfos;
        if (playerInfos != null)
        {
            foreach (var info in playerInfos)
            {
                GameObject go = playerModels[info.Value.ID];
                SetModelByInfo(go, info.Value);
            }
        }
    }

}
