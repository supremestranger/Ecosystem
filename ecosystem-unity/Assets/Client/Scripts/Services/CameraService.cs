using UnityEngine;

namespace Client {
    sealed class CameraService {
        public Camera Camera;
        public float CurSize;
        public Vector2 ActualPos;
        public Color TargetBackgroundColor;

        public Vector3 GetPosition() {
            return Camera.transform.position;
        }
    }
}