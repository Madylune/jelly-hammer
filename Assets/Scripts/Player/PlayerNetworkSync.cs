using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerNetworkSync : MonoBehaviourPun, IPunObservable
{
    private Vector3 _playerPos;
    private SpriteRenderer _spriteRenderer;
    private PhotonView _photonView;
    private MyPlayer _myPlayer;

    private ExitGames.Client.Photon.Hashtable _playerCustomProps = new ExitGames.Client.Photon.Hashtable();

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _photonView = GetComponent<PhotonView>();
        _myPlayer = GetComponent<MyPlayer>();

        if (_photonView.IsMine)
        {
            _photonView.RPC("ShowGameInformation", RpcTarget.All, _myPlayer.MyName + " is entered into the party !");
        }
    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, this._playerPos, Time.deltaTime * 5);
        }

        if (photonView.IsMine)
        {
            UpdateScore(_myPlayer.MyScore);
        }
    }

    private void UpdateScore(float _score)
    {
        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProps);
        _playerCustomProps["PlayerScore"] = _score;
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

    [PunRPC]
    void ShowGameInformation(string _message)
    {
        UIManager.MyInstance.WriteGameInformation(_message);
    }
}
