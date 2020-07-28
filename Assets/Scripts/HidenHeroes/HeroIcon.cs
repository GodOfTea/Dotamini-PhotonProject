using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class HeroIcon : MonoBehaviour, IOnEventCallback
{
    [SerializeField] HidenHeroesCore core;
    public SpriteRenderer spriteRenderer;
    public Vector3 myPos;
    public Hero heroInfo;


    private void Start()
    {
        core = GameObject.FindGameObjectWithTag("HHCore").GetComponent<HidenHeroesCore>();
    }

    public void Spawn(Hero hero, Transform parent, Vector3 pos)
    {
        heroInfo = hero; 
        spriteRenderer.sprite = heroInfo.heroIcon;
        myPos = pos;
        Instantiate(gameObject, pos, Quaternion.identity, parent);
    }

    private void OnMouseDown()
    {
        bool isTarget = false;
        foreach (var target in core.targestList.killsTargets)
            if (heroInfo.name == target)
                isTarget = true;
        
        if (isTarget == true)
            { ShareState(true); }
        else
            { ShareState(false); }
    }

    private void ShareState(bool isTarget)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            RaiseEventOptions options = new RaiseEventOptions { Receivers = ReceiverGroup.All};
            SendOptions sendOptions = new SendOptions { Reliability = true };

            bool isFirstPlayer = PhotonNetwork.IsMasterClient ?
                    true : false;
            object[] data = new object[2];
            data[0] = isFirstPlayer;
            data[1] = heroInfo.index;

            if (isTarget == true)
            {
                PhotonNetwork.RaiseEvent(42, data, options, sendOptions);
            }
            else
            {
                PhotonNetwork.RaiseEvent(43, data, options, sendOptions);
            }
        }
    }

    private void KillTarget()
    {
        var contracts = core.targestList.contracts;

        foreach (var target in contracts)
            if (heroInfo.name == target.nameLabel.text)
            {
                target.SetContractAsComplete();
                break;
            }

        core.kills++;
        gameObject.SetActive(false);

        core.IsNewRound();
        Destroy(gameObject);
    }

    public void OnEvent(EventData photonEvent)
    {
        object[] data = (object[])photonEvent.CustomData; /* 0-bool, 1-int */

        if (heroInfo.index == (int)data[1])
        {
            switch (photonEvent.Code)
            {
                case 42:
                    core.UpdateScore((bool)data[0], 1);
                    KillTarget();
                    break;
                case 43:
                    core.UpdateScore((bool)data[0], -1);
                    break;
            }
        }
    }

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
