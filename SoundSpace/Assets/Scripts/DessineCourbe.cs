using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Draws a red bezier curve from (0,0,0) to the transform's position



public class DessineCourbe : MonoBehaviour {

	public GameObject controller;
	public GameObject point;
	public float granularite = 0.1f;

	private bool dessinActif = false;
	private Vector3 depart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("TriggerLeft")) {
			depart = controller.transform.position;
			dessinActif = true;

		} else if (Input.GetButtonUp ("TriggerLeft")) {
			dessineTrait ();
			dessinActif = false;
		}

		if (dessinActif) {
			dessineTrait ();

		}
		
	}

	void dessineTrait()
	{
		Vector3 finTrait = controller.transform.position;
		Vector3 diffTrait = finTrait - depart;
		if (diffTrait.magnitude > granularite) {
			int nombreIterations = (int)(diffTrait.magnitude/granularite);
			for (int iter = 0; iter < nombreIterations; iter++) {
				float longueur = granularite * iter;
				Vector3 posPoint = depart + (diffTrait.normalized*longueur);
				Instantiate(point,posPoint, Quaternion.identity);
			}
			depart = finTrait;


			/*Vector3 posTrait = depart + (diffTrait / 2f);
			GameObject traitCree  = Instantiate (trait, posTrait, Quaternion.LookRotation (diffTrait));
			float echelleZ = diffTrait.magnitude;
			Vector3 echelleActuelle = traitCree.transform.localScale;
			echelleActuelle.z = echelleZ;
			traitCree.transform.localScale = echelleActuelle;
			depart = finTrait;*/
		}

	}
}
