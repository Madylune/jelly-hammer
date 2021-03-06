﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerInfos : MonoBehaviour
{
    [SerializeField]
    private InputField usernameInput;

    [SerializeField]
    private Button userNameBtn;

    [SerializeField]
    private Text usernameOutput;

    [SerializeField]
    private Button[] characterButtons;

    private string characterName;

    private readonly string selectedCharacter = "SelectedCharacter";

    private ExitGames.Client.Photon.Hashtable _playerCustomProps = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        characterName = PlayerPrefs.GetString(selectedCharacter);
        foreach (Button character in characterButtons)
        {
            if (character.name == characterName)
            {
                character.Select();
                character.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        foreach (Button character in characterButtons)
        {
            if (character.name == characterName)
            {
                character.transform.GetChild(0).gameObject.SetActive(true);
                PhotonNetwork.SetPlayerCustomProperties(_playerCustomProps);
                _playerCustomProps["PlayerSprite"] = characterName;
            }
            else
            {
                character.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void OnSelectCharacter(string name)
    {
        characterName = name;
        PlayerPrefs.SetString(selectedCharacter, name);

        PhotonNetwork.SetPlayerCustomProperties(_playerCustomProps);
        _playerCustomProps["PlayerSprite"] = name;
    }

    public void OnInputChange()
    {
        if (usernameInput.text.Length >= 3)
        {
            userNameBtn.interactable = true;
        }
        else
        {
            userNameBtn.interactable = false;
        }
    }

    public void OnSubmitCharacter()
    {
        PhotonNetwork.NickName = usernameInput.text;

        usernameInput.gameObject.SetActive(false);
        userNameBtn.gameObject.SetActive(false);

        usernameOutput.text = PhotonNetwork.NickName;
        usernameOutput.gameObject.SetActive(true);

        NetworkManager.MyInstance.StartGame();
    }
}
