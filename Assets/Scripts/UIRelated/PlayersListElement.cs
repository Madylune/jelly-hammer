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

    [SerializeField]
    private Sprite[] sprites;

    private Player player;
    public Player Player { get => player; set => player = value; }

    public void SetPlayerInfo(Player _player)
    {
        Player = _player;
        playerName.text = _player.NickName;

        string _playerSprite = (string)player.CustomProperties["PlayerSprite"];
        Sprite _sprite = (_playerSprite == "Panda") ? sprites[0] : sprites[1];
        playerImage.sprite = _sprite;
    }
}
