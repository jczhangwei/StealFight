using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace stealFight {

    public class GameSceneUI : MonoBehaviour {
        private CanvasScaler canvas_scaler = null;
        public Text text = null;

        // Use this for initialization
        void Start() {
            canvas_scaler = GetComponent<CanvasScaler>();

            NotificationLogic.Instance.Register(NotificationEvent.DirControllInfo,
                delegate(NotificationEvent @event, System.Object[] @params) {
                    if (@params.Length >= 2) {
                        var direction = (Vector2)@params[0];
                        var distance = (float)@params[1];

                        text.text = direction + " " + distance;
                    }
                }, text);
        }

        // Update is called once per frame
        void Update() {
        }

        public void OnBtnMainScene() {
            MainView.getInstance().next_scene = "main_scene";
            SceneManager.LoadScene("loading_scene");
        }

        public void OnBtnConnect() {
            NetLogic.Instance.Connect();
        }

        public void OnBtnCut() {
            NetLogic.Instance.Connect();
        }
    }

}