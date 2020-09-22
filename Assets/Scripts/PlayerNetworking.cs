using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworking : MonoBehaviourPun, IPunObservable
{
    public MonoBehaviour[] scriptsToIgnore;

    private PhotonView pv;

    private MyPlayer otherPlayer;

    void Start()
    {
        pv = GetComponent<PhotonView>();
        if (!pv.IsMine)
        {
            Debug.Log("ici");
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
            otherPlayer.GetComponent<MyPlayer>();
            otherPlayer.SmoothMovement();
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
            otherPlayer.MySmoothMove = (Vector3) stream.ReceiveNext();
        }
    }
}
