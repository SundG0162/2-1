using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoSingleton<PlayerManager>
{
    public Player Player { get; private set; }
    public Transform PlayerTrm { get; private set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
        PlayerTrm = Player.transform;
    }
}
