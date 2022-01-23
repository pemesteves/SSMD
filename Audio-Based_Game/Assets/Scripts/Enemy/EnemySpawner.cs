using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Singleton
    public static EnemySpawner instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject enemyPrefab = null;

    [SerializeField] private float arenaSize = 50f;
    [SerializeField] private float minDistanceToPlayer = 15f;
    [SerializeField] private int triesToSpawn = 15;

    private int numEnemiesPerWave = 1, numEnemiesLeft;

    private void Start() => SpawnEnemies();

    private void SpawnEnemies()
    {
        float sqrRadius = minDistanceToPlayer * minDistanceToPlayer;
        numEnemiesLeft = 0;
        float yPos = enemyPrefab.transform.position.y;
        for (int i = 0; i < numEnemiesPerWave; i++)
        {
            for (int j = 0; j < triesToSpawn; j++)
            {
                Vector3 enemyPos = new Vector3(Random.Range(-arenaSize, arenaSize), yPos, Random.Range(-arenaSize, arenaSize));
                if ((Player.instance.transform.position - enemyPos).sqrMagnitude < sqrRadius) continue;

                Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
                numEnemiesLeft++;
                break;
            }
        }

        numEnemiesPerWave = numEnemiesLeft + 1;
    }

    public void RegisterEnemyDeath()
    {
        numEnemiesLeft--;
        if (numEnemiesLeft <= 0) SpawnEnemies();
    }
}