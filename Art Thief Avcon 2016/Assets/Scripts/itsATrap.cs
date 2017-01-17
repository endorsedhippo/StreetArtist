using UnityEngine;
using System.Collections;

public class itsATrap : MonoBehaviour {


	public GameObject sprite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			Vector3 pos = new Vector3 (0, 5,0);
			Instantiate (sprite, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		if (other.tag == "Player 2")
		{
			Vector3 pos = new Vector3 (0, 5,0);
			Instantiate (sprite, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}


}
