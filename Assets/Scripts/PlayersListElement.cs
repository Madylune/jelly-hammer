using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayersListElement : MonoBehaviour
{
    [SerializeField]
    private Text playerName;

    [SerializeField]
    private Image playerImage;

    private Player player;
    public Player Player { get => player; set => player = value; }

    public void SetPlayerInfo(Player _player)
    {
        Player = _player;
        playerName.text = _player.NickName;
        //playerImage.sprite = player.CustomProperties.
    }
}
