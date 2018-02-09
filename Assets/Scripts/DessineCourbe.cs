using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DessineCourbe : MonoBehaviour {

	public GameObject controller;
	public GameObject point;
	public float granularite = 0.1f;
    public float hauteurNoir = 2f;
    public float hauteurBleu = 1.7f;
    public float hauteurVert = 1.5f;
    public float hauteurRouge = 1.3f;
    public float hauteurBlanc = 1f;
    public float distanceCouleur = 0.2f;

    private bool dessinActif = false;
	private Vector3 depart;

    // Use this for initialization
    void Start()
    {
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("TriggerLeft")) {
			depart = controller.transform.position;
			dessinActif = true;
		} else if (Input.GetButtonUp ("TriggerLeft")) {
            dessineSpheres();
			dessinActif = false;
		}

		if (dessinActif) {
			dessineSpheres ();
		}		
	}

	void dessineSpheres()
	{
		Vector3 finTrait = controller.transform.position;
		Vector3 diffTrait = finTrait - depart;
		if (diffTrait.magnitude > granularite) {
			int nombreIterations = (int)(diffTrait.magnitude/granularite);
			for (int iter = 0; iter < nombreIterations; iter++) {
				float longueur = granularite * iter;
				Vector3 posPoint = depart + (diffTrait.normalized*longueur);
				GameObject sphere = Instantiate(point,posPoint, Quaternion.identity);
                colorieSphere(sphere);
			}
			depart = finTrait;
		}
	}

    void colorieSphere(GameObject sphere)
    {
        float hauteurSphere = sphere.transform.position.y;
        Color couleur = new Color();
        if (hauteurSphere >= hauteurNoir)
        {
            couleur = Color.black;
        }
        else if (hauteurSphere < hauteurNoir && hauteurSphere >= hauteurBleu)
        {
            float valeurNoir = (hauteurNoir - hauteurSphere) / distanceCouleur;
            couleur = new Color(0f, 0f, valeurNoir);
        }
        else if (hauteurSphere < hauteurBleu && hauteurSphere >= hauteurVert)
        {
            float valeurVert = 1f - ((hauteurSphere - hauteurVert) / distanceCouleur);
            float valeurBleu = 1f - valeurVert;            
            couleur = new Color(0f, valeurVert, valeurBleu);
        }
        else if (hauteurSphere < hauteurVert && hauteurSphere >= hauteurRouge)
        {
            float valeurRouge = 1f - ((hauteurSphere - hauteurRouge) / distanceCouleur);
            float valeurVert = 1f - valeurRouge;
            couleur = new Color( valeurRouge, valeurVert,0f);
        }
        else if (hauteurSphere < hauteurRouge && hauteurSphere >= hauteurBlanc)
        {
            float valeurBlanc = 1f - ((hauteurSphere - hauteurBlanc) / distanceCouleur);
            couleur = new Color(1.0f, valeurBlanc, valeurBlanc);
        }
        else
        {
            couleur = Color.white;
        }
        Renderer sphereRend = sphere.GetComponent<Renderer>();
        Material materiel = new Material(Shader.Find("Specular"));
        materiel.color = couleur;
        sphereRend.material = materiel;
    }
}
