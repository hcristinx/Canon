using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemigos")]
    public GameObject enemy;

    [Header("Puntos de aparición")]
    public Transform[] spawnPoints;

    [Header("Configuración")]
    public float spawnRate = 2.5f;
    public int maxEnemies = 5;

    private float nextSpawnTime;
    private int enemiesInScene = 0;

    void Update()
    {
        // Comprobar si ya podemos generar otro enemigo
        if (Time.time >= nextSpawnTime &&
            enemiesInScene < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        // Comprobar que exista el prefab y haya puntos disponibles
        if (enemy == null || spawnPoints.Length == 0)
        {
            return;
        }

        // Elegir un punto de aparición al azar
        int randomIndex = Random.Range(0, spawnPoints.Length);

        Transform selectedSpawnPoint = spawnPoints[randomIndex];

        // Crear el enemigo
        GameObject newEnemy = Instantiate(enemy,selectedSpawnPoint.position,selectedSpawnPoint.rotation
        );

        // Aumentar el contador
        enemiesInScene++;

        // Detectar cuándo se destruye el enemigo
        EnemyCounter counter = newEnemy.GetComponent<EnemyCounter>();

        if (counter != null)
        {
            counter.spawnManager = this;
        }
    }

    public void EnemyDestroyed()
    {
        enemiesInScene--;

        if (enemiesInScene < 0)
        {
            enemiesInScene = 0;
        }
    }
}