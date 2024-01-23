using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public List<AudioClip> footsteps;
    private AudioSource footstepSource;
    // Start is called before the first frame update
    void Start()
    {
        footstepSource = GetComponent<AudioSource>();
    }

    void PlayFootstep()
    {
        AudioClip clip = null;
        clip = footsteps[Random.Range(0, footsteps.Count)];
        footstepSource.clip = clip;
        footstepSource.volume = Random.Range(0.05f, 0.1f);
        footstepSource.pitch = Random.Range(0.8f, 1.2f);
        footstepSource.Play();
    }
}
