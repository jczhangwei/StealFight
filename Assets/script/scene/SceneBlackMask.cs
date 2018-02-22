using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBlackMask : MonoBehaviour {

	public bool is_exit_complete = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void OnExitComplte () {
		is_exit_complete = true;
	}
}