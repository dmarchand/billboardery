using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dan.Service {
    public class GameService : MonoBehaviour {
        public static GameService Instance;

        public GameObject PlayerGameObject;

        private void Awake() {
            Instance = this;
        }
    }
}
