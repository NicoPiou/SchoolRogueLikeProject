using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneCharger : MonoBehaviour
{

	private int levelNumber;

	public int randomRangeStart;
	public int randomRangeEnd;


	public string testActivateNewScene;


	void Update()
	{
			
		if(Input.GetButton(testActivateNewScene)){
			levelNumber = Random.Range (randomRangeStart, randomRangeEnd);
			Application.LoadLevel ("Room" + levelNumber);


	
}
}
}


