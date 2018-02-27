using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingPanel : MonoBehaviour {
	public void OnLoadingSceneEnd() {
		Debug.Log("on loading scene end ");
		// async.allowSceneActivation = false;
		SceneManager.LoadSceneAsync("game");
	}
}
