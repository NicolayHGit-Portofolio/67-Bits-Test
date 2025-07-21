using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSpot : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;

        player.PlayerLevelUp();
    }
}
