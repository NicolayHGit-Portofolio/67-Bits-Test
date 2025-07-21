using UnityEngine;

public class BodiesCollector : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player == null) return;
        int enemiesCollected = player.Stack.CollectEnemies(transform);
        if (enemiesCollected == 0) return;

        GameManager.Instance.EnemiesCollected(enemiesCollected);
    }
}
