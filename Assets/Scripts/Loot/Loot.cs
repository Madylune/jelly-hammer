﻿using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Loot : MonoBehaviourPun
{
    public GameObject lootObject;
    public int lootChance;
    public string lootName;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (lootName == "Diamond")
            {
                collision.GetComponent<MyPlayer>().MyDiamonds++;
            }
            if (lootName == "Bomb")
            {
                collision.GetComponent<MyPlayer>().MyBombs++;
            }

            Destroy(gameObject);
            
        }
    }
}
