using System;
using UnityEngine;

namespace Client {
    sealed class SeaView : MonoBehaviour {
        public Transform Transform;

        private void Awake() {
            Transform = transform;
        }
    }
}