using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Client {
    sealed class Config {
        public float MinCameraSize;
        public float MaxCameraSize;
        public float CameraZoomSpeed;
        public Color DayColor;
        
        // units
        public float UnitMaxSeeAhead;
        public UnitView UnitPrefab;
    }
}