using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInformation : MonoBehaviour
{
    [SerializeField]
    private float duration;

    [SerializeField]
    private Text text;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
