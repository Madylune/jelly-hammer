using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    private readonly string selectedCharacter = "SelectedCharacter";

    public string MyCharacter { get; set; }

    [SerializeField]
    private Transform[] spawnPoints;

    private void Start()
    {
        string characterName;
        characterName = PlayerPrefs.GetString(selectedCharacter);
        MyCharacter = characterName;

        PhotonNetwork.Instantiate("Prefabs/" + characterName, transform.position, Quaternion.identity);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
