using UnityEngine;
using System.Collections;

public class LoadLevel1 : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartButton ()
	{
		Time.timeScale = 1;
		Application.LoadLevel ("Scene 012");
	}
}
