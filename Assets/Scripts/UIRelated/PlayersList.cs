using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersList : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayersListElement playerPrefab;

    private List<PlayersListElement> playersList = new List<PlayersListElement>();

    private void Awake()
    {
        GetCurrentRoomPlayers();
    }

    private void GetCurrentRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerIntoList(playerInfo.Value);
        }
    }

    private void AddPlayerIntoList(Player player)
    {
        PlayersListElement element = Instantiate(playerPrefab, transform);

        if (element != null)
        {
            element.SetPlayerInfo(player);
            playersList.Add(element);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = playersList.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(playersList[index].gameObject);
            playersList.RemoveAt(index);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerIntoList(newPlayer);
    }
}
