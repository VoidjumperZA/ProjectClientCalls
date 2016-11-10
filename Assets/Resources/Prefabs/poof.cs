using UnityEngine;
using System.Collections;

public class poof : MonoBehaviour {

    public GameObject poofParticles;

    void Start ()
    {
        poofParticles.GetComponent<ParticleSystem>().Stop();
    }

	void OnTriggerEnter (Collider col) {
        if (col.gameObject.tag == "Player")
        {
            poofParticles.GetComponent<ParticleSystem>().Play();
        }
	
	}
	
}
