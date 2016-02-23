using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwitchScenes : MonoBehaviour {

	public void loadSceneFromButton()
	{
		SceneManager.LoadScene ("Scene");
	}
}
