using UnityEngine;

public class Player : MonoBehaviour
{
    #region Singleton
    public static Player instance;

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

    public PlayerMovement Movement { get; set; }
    public PlayerShoot Shoot { get; set; }
    public PlayerHealth Health { get; set; }

    private void Start()
    {
        Movement = GetComponent<PlayerMovement>();
        Shoot = GetComponent<PlayerShoot>();
        Health = GetComponent<PlayerHealth>();
    }
}