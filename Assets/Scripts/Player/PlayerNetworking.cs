using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviourPun, IPunObservable
{
    public MonoBehaviour[] scriptsToIgnore;

    private PhotonView pv;

    private MyPlayer myPlayer;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            myPlayer = GetComponent<MyPlayer>();
            myPlayer.MyName = PhotonNetwork.NickName;
        }
        else
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
            myPlayer = GetComponent<MyPlayer>();
            myPlayer.MyName = pv.Owner.NickName;
        }
    }

    private void FixedUpdate()
    {
        if (!pv.IsMine)
        {
            myPlayer.SmoothMovement();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else if (stream.IsReading)
        {
            myPlayer.MySmoothMove = (Vector3) stream.ReceiveNext();
        }
    }
}
