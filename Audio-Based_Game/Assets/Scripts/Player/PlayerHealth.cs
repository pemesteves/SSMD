using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string HeartBeatBang = "heart-beat", HealthValue = "health-value";

    [SerializeField] private LibPdInstance heartBeatInstance = null;
    [SerializeField] private float maxHealth = 100f;

    [SerializeField] private GameObject scream = null;

    private float currentHealth;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartBeat();
        heartBeatInstance.SendBang(HeartBeatBang);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Damage(5);
        }
    }

    private void Damage(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        UpdateHeartBeat();

        if (currentHealth > 0) return;
        heartBeatInstance.SendBang(HeartBeatBang);
        Instantiate(scream, transform);
        Player.instance.Movement.enabled = false;
        Player.instance.Shoot.enabled = false;
    }

    private void UpdateHeartBeat() => heartBeatInstance.SendFloat(HealthValue, currentHealth / maxHealth);
}