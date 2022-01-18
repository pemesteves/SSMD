using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f, rotateSpeed = 1f;

    [Header("Sound")]
    private const string FootstepsPdBang = "trigger-footstep";
    [SerializeField] private float timeBetweenFootstep = 0.01f;
    [SerializeField] private LibPdInstance footstepsInstance = null;
    [SerializeField] private AudioSource footstepsAudioSource = null;
    private float footstepsTime = 0;
    private bool panRight = true;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal"), y = Input.GetAxis("Vertical");

        if (y == 0)
        {
            footstepsTime = -1;
        }
        else if (footstepsTime < 0)
        {
            PanAudioSource();
            footstepsInstance.SendBang(FootstepsPdBang);
            footstepsTime = 0;
        }
        else if (footstepsTime > timeBetweenFootstep)
        {
            PanAudioSource();
            footstepsInstance.SendBang(FootstepsPdBang);
            footstepsTime -= timeBetweenFootstep;
        }

        footstepsTime += Time.deltaTime;

        transform.position += transform.forward * y * speed;
        transform.rotation *= Quaternion.Euler(0, x * rotateSpeed, 0);
    }

    private void PanAudioSource()
    {
        /*footstepsAudioSource.panStereo = panRight ? 1 : -1;
        panRight = !panRight;*/
    }
}