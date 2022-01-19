using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f, rotateSpeed = 1f;
    [SerializeField] private Rigidbody _rb = null;

    [Header("Sound")]
    private const string FootstepsPdBang = "trigger-footstep";
    [SerializeField] private float timeBetweenFootstep = 0.01f;
    [SerializeField] private LibPdInstance footstepsInstance = null;
    private float footstepsTime = 0;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal"), y = Input.GetAxis("Vertical");

        if (y == 0)
        {
            footstepsTime = -1;
        }
        else if (footstepsTime < 0)
        {
            footstepsInstance.SendBang(FootstepsPdBang);
            footstepsTime = 0;
        }
        else if (footstepsTime > timeBetweenFootstep)
        {
            footstepsInstance.SendBang(FootstepsPdBang);
            footstepsTime -= timeBetweenFootstep;
        }

        footstepsTime += Time.deltaTime;

        _rb.velocity = transform.forward * y * speed;
        transform.rotation *= Quaternion.Euler(0, x * rotateSpeed, 0);
    }
}