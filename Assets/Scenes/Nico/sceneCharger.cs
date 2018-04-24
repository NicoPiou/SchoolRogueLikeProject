using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneCharger : MonoBehaviour
{

	private int levelNumber;

	public string testActivateNewScene;


	void Update()
	{
			
		if(Input.GetButton(testActivateNewScene)){
			levelNumber = Random.Range (1, 3);
			Application.LoadLevel ("Room" + levelNumber);


	
}
}
}


