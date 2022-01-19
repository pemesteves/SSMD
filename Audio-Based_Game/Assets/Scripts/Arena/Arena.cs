using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private Transform arenaNoiseTransform = null;
    [SerializeField] private LayerMask arenaMask;

    [SerializeField] private float arenaRadius = 50;

    private void FixedUpdate()
    {
        if (Player.instance.transform.position == transform.position) return;

        Vector3 direction = (Player.instance.transform.position - transform.position).normalized;
        direction.y = 0;

        if (!Physics.Raycast(transform.position, direction, out RaycastHit hit, arenaRadius, arenaMask)) return;

#if UNITY_EDITOR
        Debug.DrawLine(transform.position, hit.point, Color.yellow);
#endif

        arenaNoiseTransform.position = hit.point;
    }
}