using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private Vector2 moveSpot;

    [SerializeField]
    private float points;

    [SerializeField]
    private LootTable loots;

    [SerializeField]
    private float minX, maxX, minY, maxY;

    [SerializeField]
    private float startWaitTime; 

    private float waitTime;

    private float speed = 1f;

    public float MyMinX { get => minX; }
    public float MyMaxX { get => maxX; }
    public float MyMinY { get => minY; }
    public float MyMaxY { get => maxY; }
    public float MyPoints { get => points; }
    public LootTable MyLoots { get => loots; set => loots = value; }

    public override void Start()
    {
        base.Start();

        waitTime = startWaitTime;
        moveSpot = new Vector2(Random.Range(MyMinX, MyMaxX), Random.Range(MyMinY, MyMaxY));
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);

        if (transform.position.x > moveSpot.x)
        {
            Flip(1f);
        }
        else if (transform.position.x < moveSpot.x)
        {
            Flip(-1f);
        }

        if (Vector2.Distance(transform.position, moveSpot) < 0.2f)
        {
            if (waitTime <= 0)
            {
                moveSpot = new Vector2(Random.Range(MyMinX, MyMaxX), Random.Range(MyMinY, MyMaxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void GetLoots()
    {
        if (MyLoots != null)
        {
            GameObject current = MyLoots.LootDrop();

            if (current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }
    }
}
