using UnityEngine;
using System.Collections;

public class DeathOverTime : MonoBehaviour {


		public float lifetime;

		void Start ()
		{
			Destroy (gameObject, lifetime);
		}
	}

