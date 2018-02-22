using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace stealFight {

    public class SceneBase : MonoBehaviour {
        // Use this for initialization
        public void Start() {
            var system = GameObject.FindWithTag("SystemObject");
            if (!system) {
                Instantiate(Resources.Load<GameObject>("prefab/System"));
            }

            var view = GameObject.FindWithTag("ViewObject");
            if (!view) {
                Instantiate(Resources.Load<GameObject>("prefab/View"));
            }
        }

        // Update is called once per frame
        public void Update() {
        }
    }

}