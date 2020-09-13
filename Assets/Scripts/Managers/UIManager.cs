using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private Image avatar;

    [SerializeField]
    private Text username, score, diamonds, bombs;

    private void Start()
    {
        username.text = Player.MyInstance.MyName.ToUpper();
        avatar.sprite = Player.MyInstance.MySprite;
        score.text = Player.MyInstance.MyScore.ToString() + " pts";
        diamonds.text = Player.MyInstance.MyDiamonds.ToString();
        bombs.text = Player.MyInstance.MyBombs.ToString();
    }

    private void Update()
    {
        score.text = Player.MyInstance.MyScore.ToString() + " pts";
        diamonds.text = Player.MyInstance.MyDiamonds.ToString();
        bombs.text = Player.MyInstance.MyBombs.ToString();
    }

}
