using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string HeartBeatBang = "heart-beat", HealthValue = "health-value";

    [SerializeField] private LibPdInstance heartBeatInstance = null;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private Vector3 damageRadius = Vector3.one;
    [SerializeField] private float enemyDamagePerSecond = 1f, healthRecoverPerSecond = .5f;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] private GameObject scream = null;

    private float currentHealth;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartBeat();
        heartBeatInstance.SendBang(HeartBeatBang);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, damageRadius);
    }

    private void Update()
    {
        if (currentHealth <= 0) return;

        Collider[] _enemies = Physics.OverlapBox(transform.position, damageRadius, Quaternion.identity, enemyLayerMask);

        if (_enemies.Length > 0) Damage(_enemies.Length * enemyDamagePerSecond * Time.deltaTime);
        else Recover();

        Debug.Log(currentHealth);
    }

    private void Damage(float damage)
    {
        if (currentHealth <= 0) return;

        SetCurrentHealth(currentHealth - damage);

        if (currentHealth > 0) return;

        heartBeatInstance.SendBang(HeartBeatBang);
        Instantiate(scream, transform);
        Player.instance.Movement.enabled = false;
        Player.instance.Shoot.enabled = false;
    }

    private void Recover()
    {
        if (currentHealth <= 0) return;

        SetCurrentHealth(currentHealth + healthRecoverPerSecond * Time.deltaTime);
    }

    private void SetCurrentHealth(float healthValue)
    {
        currentHealth = Mathf.Clamp(healthValue, 0, maxHealth);
        UpdateHeartBeat();
    }

    private void UpdateHeartBeat() => heartBeatInstance.SendFloat(HealthValue, currentHealth / maxHealth);
}