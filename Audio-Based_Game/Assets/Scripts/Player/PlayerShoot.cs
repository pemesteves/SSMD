using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private const string ShootPdBang = "trigger-shoot", FireButton = "Fire",
        EnemyLayer = "Enemy", ArenaLayer = "Arena";

    [SerializeField] private float shootingDistance = 150f;
    [SerializeField] private float timeBetweenShots = 1f;
    [SerializeField] private LayerMask shootingLayerMask;
    [SerializeField] private LibPdInstance shootInstance = null;
    [SerializeField] private Range<float> bulletDamage;

    [SerializeField] private Vector3 boxHalfExtents = Vector3.one;

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
        
        //if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, shootingDistance, shootingLayerMask))
        if (Physics.BoxCast(transform.position, boxHalfExtents, transform.forward, out RaycastHit hit, Quaternion.identity, shootingDistance, shootingLayerMask))
        {
            ExtDebug.DrawBoxCastBox(transform.position, boxHalfExtents, Quaternion.identity, transform.forward, hit.distance, Color.green);
            GameObject target = hit.collider.gameObject;
            int layer = target.layer;

            if (LayerMask.NameToLayer(EnemyLayer).Equals(layer))
            {
                target.GetComponent<Enemy>().Damage(Random.Range(bulletDamage.min, bulletDamage.max));
            }
            else if (LayerMask.NameToLayer(ArenaLayer).Equals(layer))
            {

            }
        }
    }
}