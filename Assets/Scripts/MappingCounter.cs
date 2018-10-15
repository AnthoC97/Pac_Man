using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingCounter : MonoBehaviour
{
	[Header("Listes")]
	[SerializeField] private List<Transform> Obstacles;
	[SerializeField] private List<Transform> Doors;
	private int[,] EtatCase;
	[Header("Taille Map")]
	[SerializeField] private int x;
	[SerializeField] private int z;
	
	// Use this for initialization
	void Start ()
	{
		//Initialisation du statut à 0 (passage possible)
		EtatCase = new int[x,z];
		for (int i = 0; i < x; i++)
		{
			for (int j = 0; j < z; j++)
			{
				EtatCase[i,j] = 0;
			}
		}
		//Statut des obstacles mis à 1
		foreach (Transform cpt in Obstacles)
		{
			EtatCase[(int)cpt.position.x,(int)cpt.position.z] = 1;
		}
		//Statut des Portails mis à 2
		foreach (Transform cpt in Doors)
		{
			EtatCase[(int)cpt.position.x,(int)cpt.position.z] = 2;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
