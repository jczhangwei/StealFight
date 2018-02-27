using UnityEngine;
using UnityEngine.SceneManagement;

namespace stealFight {

    public class MainUI : MonoBehaviour {
        public void OnBtnScene() {
            MainView.getInstance().ToScene(SceneResource.game);
        }
    }

}