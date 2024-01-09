using System;
using UnityEngine;

namespace Client {
    public class ObstacleView : MonoBehaviour {
        public Transform Transform;

        private void Awake() {
            Transform = transform;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawLine(transform.position, transform.position + transform.right * transform.localScale.x);
        }
    }
}