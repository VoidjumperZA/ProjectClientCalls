using UnityEngine;
using System.Collections;

public class PlayerAudio : MonoBehaviour {

    public AudioClip musicSound;
    private AudioSource musicSoundSource;

    public AudioClip darknessSound;
    private AudioSource darknessSoundSource;
    void Awake()
    {
        musicSoundSource = gameObject.AddComponent<AudioSource>();
        musicSoundSource.playOnAwake = true;
        musicSoundSource.clip = musicSound;
        musicSoundSource.loop = true;

        darknessSoundSource = gameObject.AddComponent<AudioSource>();
        darknessSoundSource.playOnAwake = true;
        darknessSoundSource.clip = darknessSound;
        darknessSoundSource.loop = true;

    }
    // Use this for initialization
    void Start () {

        musicSoundSource.Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
