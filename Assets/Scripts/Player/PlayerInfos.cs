using System.Collections;
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

    public void SetName()
    {
        PhotonNetwork.NickName = usernameInput.text;

        usernameInput.gameObject.SetActive(false);
        userNameBtn.gameObject.SetActive(false);

        usernameOutput.text = PhotonNetwork.NickName;
        usernameOutput.gameObject.SetActive(true);
    }
}
