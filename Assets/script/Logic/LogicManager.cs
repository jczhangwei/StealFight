using System;
using UnityEngine;

namespace stealFight {

    public class LogicManager : MonoBehaviour {
        // Use this for initialization

        private LogicManager instance;

        private void Awake() {
            DontDestroyOnLoad(gameObject);
        }

        void Start() {
            Debug.Log("start system manager");
            InitAllSystems();
        }

        // Update is called once per frame
        void Update() {
        }

        void InitAllSystems() {
            MainLogic.Instance.Init();
            NetLogic.Instance.Init();
            NotificationLogic.Instance.Init();
        }
    }

}