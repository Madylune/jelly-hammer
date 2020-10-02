using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PlayersListElement : MonoBehaviour
{
    [SerializeField]
    private Text playerName, playerScore;

    [SerializeField]
    private Image playerImage;

    [SerializeField]
    private Sprite[] sprites;

    private Player player;
    public Player Player { get => player; set => player = value; }

    private void Update()
    {
        float _score = (float)player.CustomProperties["PlayerScore"];
        playerScore.text = _score.ToString() + " pts";
    }

    public void SetPlayerInfo(Player _player)
    {
        Player = _player;
        playerName.text = _player.NickName;

        string _playerSprite = (string)player.CustomProperties["PlayerSprite"];
        Sprite _sprite = (_playerSprite == "Panda") ? sprites[0] : sprites[1];
        playerImage.sprite = _sprite;

        switch (_player.ActorNumber)
        {
            case 1:
                gameObject.GetComponent<Image>().color = new Color32(57, 80, 246, 255); //Blue
                break;
            case 2:
                gameObject.GetComponent<Image>().color = new Color32(241, 62, 73, 255); //Red
                break;
            case 3:
                gameObject.GetComponent<Image>().color = new Color32(241, 199, 62, 255); //Yellow
                break;
            case 4:
                gameObject.GetComponent<Image>().color = new Color32(80, 181, 100, 255); //Green
                break;
            default:
                break;
        }
    }
}
