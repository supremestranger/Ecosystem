using Leopotam.Ecs;
using UnityEngine;


namespace Client {
    sealed class CameraControlSystem : IEcsInitSystem, IEcsRunSystem {
        private CameraService _cameraService;
        private TimeService _timeService;
        private Config _config;

        private Vector2 _dragStartPos;
        private bool _dragging;
        private Plane _plane;
        private Vector2 _dragCurrentPos;

        public void Init() {
            _plane = new Plane(Vector3.back, Vector3.zero);
        }

        public void Run() {
            ControlZoom();
            ControlMovement();
        }

        private void ControlMovement() {
            if (Input.GetMouseButtonDown(0)) {
                var ray = _cameraService.Camera.ScreenPointToRay(Input.mousePosition);

                if (_plane.Raycast(ray, out var entry)) {
                    _dragStartPos = ray.GetPoint(entry);
                }

                _dragging = true;
            }

            if (Input.GetMouseButtonUp(0)) {
                _dragging = false;
            }

            if (_dragging) {
                var ray = _cameraService.Camera.ScreenPointToRay(Input.mousePosition);

                if (_plane.Raycast(ray, out var entry)) {
                    _dragCurrentPos = ray.GetPoint(entry);
                    _cameraService.ActualPos = (Vector2)_cameraService.GetPosition() + _dragStartPos - _dragCurrentPos;
                }
            }
            
            _cameraService.Camera.transform.position = Vector3.Lerp(_cameraService.GetPosition(), (Vector3)_cameraService.ActualPos - Vector3.forward, 12f * _timeService.UnscaledDeltaTime);
        }

        private void ControlZoom() {
            _cameraService.CurSize -= Input.mouseScrollDelta.y;
            _cameraService.CurSize = Mathf.Clamp(_cameraService.CurSize, _config.MinCameraSize, _config.MaxCameraSize);
            var size = _cameraService.Camera.orthographicSize;
            _cameraService.Camera.orthographicSize =
                Mathf.Lerp(size, _cameraService.CurSize, _config.CameraZoomSpeed * _timeService.UnscaledDeltaTime);
        }
    }
}