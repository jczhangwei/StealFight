﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace stealFight {

    public class MainUI : MonoBehaviour {
        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }

        public void OnBtnScene() {
            MainView.getInstance().ToScene(SceneResource.game);
        }
    }

}