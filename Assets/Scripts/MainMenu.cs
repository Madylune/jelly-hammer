using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject quitButton;

    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            quitButton.SetActive(false);
        }
    }
}
