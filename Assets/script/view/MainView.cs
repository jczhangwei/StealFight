using System;
using UnityEngine.SceneManagement;

namespace stealFight {

    public enum SceneResource {
        main,
        game,
        loading,
    }

    public class MainView {
        public static MainView instance;

        public String next_scene;

        public static MainView getInstance() {
            return instance ?? (instance = new MainView());
        }

        public void ToScene(SceneResource target_scene) {
            next_scene = SceneName(target_scene);
            SceneManager.LoadScene("loading");
        }

        string SceneName(SceneResource target_scene) {
            return Enum.GetName(typeof(SceneResource), target_scene);
        }
    }

}