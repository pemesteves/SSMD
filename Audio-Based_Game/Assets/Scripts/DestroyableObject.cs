using System.Collections;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField] private float timeToDestroy = 1f;

    private void Start() => StartCoroutine(Destroy());

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}