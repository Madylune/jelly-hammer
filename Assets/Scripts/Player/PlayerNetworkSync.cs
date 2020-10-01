using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSync : MonoBehaviourPun, IPunObservable
{
    private Vector3 playerPos;
    //private Quaternion playerRot;
    private PhotonView _photonView;

    private void Start()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, this.playerPos, Time.deltaTime * 5);
            //transform.rotation = Quaternion.Lerp(transform.rotation, this.playerRot, Time.deltaTime * 5);
        }
    }

    // See others players movement + smooth move
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            //stream.SendNext(transform.rotation);
        }
        else
        {
            this.playerPos = (Vector3)stream.ReceiveNext();
            //this.playerRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
