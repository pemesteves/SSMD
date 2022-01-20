using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private const string HeartBeatBang = "heart-beat", HealthValue = "health-value";

    [SerializeField] private LibPdInstance heartBeatInstance = null;

    [SerializeField] private float maxHealth = 100f;

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
            currentHealth -= 5f;
            UpdateHeartBeat();
        }
    }

    private void UpdateHeartBeat()
    {
        heartBeatInstance.SendFloat(HealthValue, currentHealth / maxHealth);
    }

    private void OnDestroy()
    {
        UpdateHeartBeat();
        heartBeatInstance.SendBang(HeartBeatBang);
    }
}
