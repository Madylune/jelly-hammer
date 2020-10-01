using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSync : MonoBehaviourPun, IPunObservable
{
    private Vector3 _playerPos;
    private SpriteRenderer _spriteRenderer;
    private PhotonView _photonView;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, this._playerPos, Time.deltaTime * 5);
        }
    }

    // Handle others players movement and get them smooth
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) //Send to others our data
        {
            stream.SendNext(transform.position);
            stream.SendNext(_spriteRenderer.flipX);
        }
        else // network players, receive data
        {
            this._playerPos = (Vector3)stream.ReceiveNext();
            this._spriteRenderer.flipX = (bool)stream.ReceiveNext();
        }
    }
}
