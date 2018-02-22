using UnityEngine;

namespace stealFight {

    public class ViewManager : MonoBehaviour {
        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }
    }

}