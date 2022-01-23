using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent agent = null;

    private const string FootstepsPdBang = "trigger-footstep", LoadZombieFileBang = "load-file", PlayZombieBang = "trigger-zombie", DeathBang = "trigger-death";
    [Header("Footsteps")]
    [SerializeField] private float timeBetweenFootstep = 0.01f;
    [SerializeField] private LibPdInstance footstepsInstance = null;
    [SerializeField] private AudioSource footstepsAudioSource = null;
    [SerializeField] private float panValue = 1f;

    [Header("Voice")]
    [SerializeField] private LibPdInstance voiceInstance = null;

    [Header("Health")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private Collider cldr = null;
    [SerializeField] private float timeToDie = 1f;

    private float currentHealth;

    private bool panRight = true, stopUpdate = false;
    private float footstepsTime = 0;

    private void Start()
    {
        voiceInstance.SendBang(LoadZombieFileBang);
        voiceInstance.SendBang(PlayZombieBang);
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (stopUpdate) return;

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

    public void Damage(float d)
    {
        currentHealth -= d;
        if (currentHealth <= 0)
        {
            voiceInstance.SendBang(PlayZombieBang);
            voiceInstance.SendBang(DeathBang);
            cldr.enabled = false;
            stopUpdate = true;
            agent.isStopped = true;
            StartCoroutine(KillEnemy());
        }
    }

    private IEnumerator KillEnemy()
    {
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }

    private void PlayFootsteps()
    {
        footstepsAudioSource.panStereo = panRight ? panValue : -panValue;
        panRight = !panRight;
        footstepsInstance.SendBang(FootstepsPdBang);
    }
}
