using UnityEngine;
using System.Collections;

public class speedBoost : MonoBehaviour 
{
	public float speedBonus;
	/*
	public GameObject player;
	PlatformerCharacter2D playerScript;

	void Start()
	{
		player.GetComponent<BoxCollider2D>().enabled = (true);
		playerScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlatformerCharacter2D> ();
	}
	*/

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("Trigger entered");
		PlatformerCharacter2D collidedPlayerScript = other.GetComponent<PlatformerCharacter2D> ();
		if (collidedPlayerScript != null) 
		{
			Debug.Log ("There is a PlatformerCharacter2D component on the collided object " + other.gameObject);
			StartCoroutine (BoostPlayer (collidedPlayerScript));
		}
		
		/*if (other.gameObject.tag == "Player") 
		{
			
			StartCoroutine (MyCoroutine ());
		}*/

	}

	IEnumerator BoostPlayer(PlatformerCharacter2D movementScript)
	{
		movementScript.m_MaxSpeed += speedBonus;
		GetComponent<BoxCollider2D> ().enabled = false;
		yield return new WaitForSeconds (3);
		movementScript.m_MaxSpeed -= speedBonus;
	}

	/*IEnumerator MyCoroutine()
	{
		playerScript.m_MaxSpeed += speedBonus;
		player.GetComponent<BoxCollider2D>().enabled = (false);
		yield return new WaitForSeconds (3);
		playerScript.m_MaxSpeed -= speedBonus;

	}*/
}