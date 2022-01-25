using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private const string ShootPdBang = "trigger-shoot", ReloadPdBang = "trigger-reload", FireButton = "Fire",
        EnemyLayer = "Enemy", ArenaLayer = "Arena";

    [SerializeField] private float shootingDistance = 150f;
    [SerializeField] private float timeBetweenShots = 1f, reloadingTime = 1f;
    [SerializeField] private LayerMask shootingLayerMask;
    [SerializeField] private LibPdInstance shootInstance = null;
    [SerializeField] private Range<float> bulletDamage;
    [SerializeField] private int bulletsPerClip = 100;

    [SerializeField] private Vector3 boxHalfExtents = Vector3.one;

    private float timeSinceLastShot = 0f;
    private bool isShooting = false;
    private int bulletsLeftInClip = 0;

    private void Start() => bulletsLeftInClip = bulletsPerClip;

    private void Update()
    {
        if (bulletsLeftInClip <= 0) return;

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

        bulletsLeftInClip--;
        if (bulletsLeftInClip == 0) StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        isShooting = false;
        shootInstance.SendBang(ReloadPdBang);
        yield return new WaitForSeconds(reloadingTime);

        bulletsLeftInClip = bulletsPerClip;
    }
}