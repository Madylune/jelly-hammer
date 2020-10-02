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

    [SerializeField]
    private CanvasGroup menuPanel;

    [SerializeField]
    private GameObject gameInformation, canvas;

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

        HandleInputs();
    }

    private void HandleInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel(menuPanel);
        }
    }

    public void TogglePanel(CanvasGroup canvasGroup)
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1;
        canvasGroup.blocksRaycasts = canvasGroup.alpha > 0 ? true : false;
    }

    public void WriteGameInformation(string _message)
    {
        Text gameInfoText = Instantiate(gameInformation, canvas.transform).GetComponent<Text>();
        gameInfoText.text = _message;
    }
}
