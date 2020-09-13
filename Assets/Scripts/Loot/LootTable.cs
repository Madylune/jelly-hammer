using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    public Loot[] loots;

    public GameObject LootDrop()
    {
        int cumProb = 0;
        int currentProb = Random.Range(0, 100);

        for (int i = 0; i < loots.Length; i++)
        {
            cumProb += loots[i].lootChance;

            if (currentProb <= cumProb)
            {
                return loots[i].lootObject;
            }
        }

        return null;
    }
}
