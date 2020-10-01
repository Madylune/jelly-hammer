using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class MyPlayer : Character
{
    private static MyPlayer instance;

    public static MyPlayer MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<MyPlayer>();
            }
            return instance;
        }
    }

    public float MyScore { get => score; set => score = value; }
    public int MyDiamonds { get => diamonds; set => diamonds = value; }
    public int MyBombs { get => bombs; set => bombs = value; }
    public int MyAttackDamage { get => attackDamage;  }
    public Rigidbody2D MyRb { get => rb; set => rb = value; }
    public float MyMoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField]
    private float moveSpeed = 3f;

    [SerializeField]
    private int attackDamage = 50;

    [SerializeField]
    private float score;

    [SerializeField]
    private int diamonds;

    [SerializeField]
    private int bombs;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Text usernameText;

    private PhotonView photonView;

    public override void Start()
    {
        photonView = GetComponent<PhotonView>();
        MyName = photonView.Owner.NickName;

        base.Start();
    }

    private void Update()
    {
        usernameText.text = MyName;
    }

    public void EarnPoints(float _points)
    {
        MyScore += _points;
    }

    public void AnimateWalk(Vector2 move)
    {
        if (move.x == 0 && move.y == 0)
        {
            animator.SetBool("IsWalking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Loot")
        {
            string collisionName = collision.GetComponent<Loot>().lootName;
            if (collisionName == "Diamond")
            {
                MyDiamonds++;
            }
            if (collisionName == "Bomb")
            {
                MyBombs++;
            }
            Destroy(collision.gameObject);
        }
    }
}
