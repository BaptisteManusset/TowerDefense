using ScriptableVariable.Unite2017.Variables;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class Destination : MonoBehaviour
{
    [SerializeField] FloatVariable vie;
    [SerializeField] LayerMask layer;
    [SerializeField] UnityEvent DamageEvent;
    [SerializeField] UnityEvent DeathEvent;
    [SerializeField] UnityEvent BeforeGameOver;
    [SerializeField] [Tooltip("Activer les degas fait au CORE")] bool enable = true;
    AudioSource audio;

    [Header("Audio")] [SerializeField] AudioClip audioDamage;
    [SerializeField] AudioClip audioDeath;
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {


        Mind ennemy = other.GetComponent<Mind>();

        if (vie.Value <= 0)
        {
            Destroy(other.gameObject);
            return;
        }

        if (!ennemy) return;

        vie.ApplyChange(-ennemy.damageToTower);

        if (enable)
        {
            if (vie.Value <= 0)
            {
                DeathEvent.Invoke();
                audio.clip = audioDeath;
                audio.Play();
            }
            else
            {
                DamageEvent.Invoke();
                audio.PlayOneShot(audioDamage);
            }
        }

        Destroy(other.gameObject);
    }
}
