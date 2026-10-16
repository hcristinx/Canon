using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public SpawnManager spawnManager;

    private bool counted = false;

    public void NotifyDestroyed()
    {
        if (counted)
        {
            return;
        }

        counted = true;

        if (spawnManager != null)
        {
            spawnManager.EnemyDestroyed();
        }
    }

    void OnDestroy()
    {
        NotifyDestroyed();
    }
}