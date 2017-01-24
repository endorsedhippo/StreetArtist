using UnityEngine;
using System.Collections;

public class LoadNextLevel : MonoBehaviour {

    public bool randomLevel;
    public float timeUntilNextLevel;
    public string[] sceneNames;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (timeUntilNextLevel > 0)
            timeUntilNextLevel -= Time.deltaTime;
        else if (randomLevel)
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNames[Random.Range(0, sceneNames.Length)]); //load random level from list
	}
}
