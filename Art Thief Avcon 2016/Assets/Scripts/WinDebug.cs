using UnityEngine;
using System.Collections;

public class WinDebug : MonoBehaviour {

    int playerNumber;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Player")
		{
			Debug.Log ("Player wins");
            playerNumber = col.GetComponent<PlayerController>().playerNumber;
			StartCoroutine (Coroutine ());
		}
	}

	IEnumerator Coroutine()
	{
		Time.timeScale = 0.3f;
		yield return new WaitForSeconds (1);
		Time.timeScale = 1;
		Application.LoadLevel ("P" + playerNumber + "_FinishScreen");
	}


}
