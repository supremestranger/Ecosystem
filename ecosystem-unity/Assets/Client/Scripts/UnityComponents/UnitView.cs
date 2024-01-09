using System;
using Leopotam.Ecs;
using UnityEditor;
using UnityEngine;

namespace Client {
    sealed class UnitView : MonoBehaviour {
        public Transform Transform;
        public EcsEntity Entity;
        public UnitType UnitType;

        private void Awake() {
            Transform = transform;
        }

        public void SetPos(Vector2 newPos) {
            var delta = (newPos - (Vector2)Transform.position).normalized;
            if (delta != Vector2.zero) {
                Transform.up = delta;
                Transform.position = (Vector3)newPos;
            }
        }
    }
}