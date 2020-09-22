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
        if (MyPlayer.MyInstance != null)
        {
            username.text = MyPlayer.MyInstance.MyName.ToUpper();
            avatar.sprite = MyPlayer.MyInstance.MySprite;
            score.text = MyPlayer.MyInstance.MyScore.ToString() + " pts";
            diamonds.text = MyPlayer.MyInstance.MyDiamonds.ToString();
            bombs.text = MyPlayer.MyInstance.MyBombs.ToString();
        }
    }

    private void Update()
    {
        if (MyPlayer.MyInstance != null)
        {
            username.text = MyPlayer.MyInstance.MyName.ToUpper();
            score.text = MyPlayer.MyInstance.MyScore.ToString() + " pts";
            diamonds.text = MyPlayer.MyInstance.MyDiamonds.ToString();
            bombs.text = MyPlayer.MyInstance.MyBombs.ToString();
        }
    }

}
