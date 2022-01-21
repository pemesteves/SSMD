using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent agent = null;

    private const string FootstepsPdBang = "trigger-footstep", LoadZombieFileBang = "load-file", PlayZombieBang = "trigger-zombie";
    [Header("Footsteps")]
    [SerializeField] private float timeBetweenFootstep = 0.01f;
    [SerializeField] private LibPdInstance footstepsInstance = null;
    [SerializeField] private AudioSource footstepsAudioSource = null;
    [SerializeField] private float panValue = 1f;

    [Header("Voice")]
    [SerializeField] private LibPdInstance voiceInstance = null;

    private bool panRight = true;
    private float footstepsTime = 0;

    private void Start()
    {
        voiceInstance.SendBang(LoadZombieFileBang);
        voiceInstance.SendBang(PlayZombieBang);
    }

    private void Update()
    {
        agent.SetDestination(Player.instance.transform.position);

        if (agent.speed == 0)
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
    }

    private void PlayFootsteps()
    {
        footstepsAudioSource.panStereo = panRight ? panValue : -panValue;
        panRight = !panRight;
        footstepsInstance.SendBang(FootstepsPdBang);
    }

    private void OnDestroy() => voiceInstance.SendBang(PlayZombieBang);
}
