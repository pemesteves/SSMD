using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private const string ShootPdBang = "trigger-shoot", FireButton = "Fire",
        EnemyLayer = "Enemy", ArenaLayer = "Arena";

    [SerializeField] private float shootingDistance = 150f;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private LayerMask shootingLayerMask;
    [SerializeField] private LibPdInstance shootInstance = null;

    private float timeSinceLastShot = 0f;
    private bool isShooting = false;

    private void Update()
    {
        if (Input.GetButtonDown(FireButton))
        {
            Shoot();
            timeSinceLastShot = 0f;
            isShooting = true;
        }
        else if (Input.GetButtonUp(FireButton)) isShooting = false;
        
        if (isShooting && timeSinceLastShot >= timeBetweenShots)
        {
            Shoot();
            timeSinceLastShot -= timeBetweenShots;
        }

        timeSinceLastShot += Time.deltaTime;
    }

    private void Shoot()
    {
        shootInstance.SendBang(ShootPdBang);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, shootingDistance, shootingLayerMask))
        {
            int layer = hit.collider.gameObject.layer;

            if (LayerMask.NameToLayer(EnemyLayer).Equals(layer))
            {

            }
            else if (LayerMask.NameToLayer(ArenaLayer).Equals(layer))
            {

            }
        }
    }
}
