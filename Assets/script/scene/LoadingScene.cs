using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace stealFight {

    public class LoadingScene : SceneBase {
        public Slider slider;
        public Animator hiding_mask;
        public SceneBlackMask scene_black_mask;
        public Button any_button;

        private AsyncOperation async;

        // Use this for initialization
        new void Start() {
            base.Start();

            var next_scene = MainView.getInstance().next_scene;
            if (next_scene == null) {
                next_scene = "game";
            }
            if (next_scene != null) {
                StartCoroutine(LoadingNewScene(next_scene));
                MainView.getInstance().next_scene = null;
            }
            any_button.onClick.AddListener(delegate() {
                hiding_mask.SetTrigger("exit");
            });
        }

        // Update is called once per frame
        new void Update() {
            if (async != null) {
                slider.value = async.progress * 1.1111111111111111111f;
            }
        }

        IEnumerator LoadingNewScene(string scene_name) {
            async = SceneManager.LoadSceneAsync(scene_name);
            async.allowSceneActivation = false;
            async.completed += delegate(AsyncOperation ao) {
                hiding_mask.SetTrigger("enter");
            };

            if (async.isDone) {
                hiding_mask.SetTrigger("enter");
            }

            while (!async.isDone) {
                // Debug.Log("loaading ....................");
                yield return null;
            }
        }

        public void OnLoadingSceneEnd() {
            Debug.Log("on loading scene end ");
        }
    }

}