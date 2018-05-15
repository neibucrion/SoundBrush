using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Theremin : MonoBehaviour {
	public GameObject controllerRight;
	public GameObject controllerLeft;
	public ParticleSystem particuleRight;
	public ParticleSystem particuleLeft;
	public GameObject pitchRef;
	public GameObject volRef;
	public AudioSource note;
	public float amplitudePitch;
	public float distorsion = 1.5f;
	public float distorsionVolume = 1f;
	public float amplitudeVolume;
	public float limiteParticules = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diffPitch = controllerRight.transform.position - pitchRef.transform.transform.position;
		diffPitch.y = 0;
		float distancePitch = diffPitch.magnitude;
		float rapportPitch = distancePitch / amplitudePitch;
		note.pitch = 1f-(rapportPitch*distorsion);
		note.pitch = Mathf.Clamp (note.pitch, 0f, 3f);
		trouveCouleur (rapportPitch, particuleRight);

		Vector3 diffVolume = controllerLeft.transform.position - volRef.transform.position;
		float rappVolume = diffVolume.y / amplitudeVolume;
		note.volume = 1f * (rappVolume*distorsionVolume);
		trouveCouleur (rappVolume, particuleLeft);		
	}

	void trouveCouleur(float rapport, ParticleSystem particules)
	{
		Color couleur = new Color (1f-rapport, rapport , 0f);
		couleur.a = 1f;
		if (rapport > limiteParticules) 
		{
			couleur.a = 0f;
		}
		var main = particules.main;
		main.startColor = couleur;
	}
}
