using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f, rotateSpeed = 1f;
    [SerializeField] private Rigidbody _rb = null;

    private const string FootstepsPdBang = "trigger-footstep";
    [Header("Sound")]
    [SerializeField] private float timeBetweenFootstep = 0.01f;
    [SerializeField] private LibPdInstance footstepsInstance = null;
    [SerializeField] private AudioSource footstepsAudioSource = null;
    [SerializeField] private float panValue = 1f;
    private bool panRight = true;
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
            PlayFootsteps();
            footstepsTime = 0;
        }
        else if (footstepsTime > timeBetweenFootstep)
        {
            PlayFootsteps();
            footstepsTime -= timeBetweenFootstep;
        }

        footstepsTime += Time.deltaTime;

        _rb.velocity = transform.forward * y * speed;
        transform.rotation *= Quaternion.Euler(0, x * rotateSpeed, 0);
    }

    private void PlayFootsteps()
    {
        footstepsAudioSource.panStereo = panRight ? panValue : -panValue;
        panRight = !panRight;
        footstepsInstance?.SendBang(FootstepsPdBang);
    }
}