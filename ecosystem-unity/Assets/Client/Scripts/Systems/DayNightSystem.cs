using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class DayNightSystem : IEcsRunSystem {
        private TimeService _timeService;
        private CameraService _cameraService;
        private Config _config;
        
        public void Run() {
            _timeService.GameTime += _timeService.DeltaTime;
            _timeService.GameTime %= 600f;
            if (_timeService.GameTime >= 300f && _timeService.Daytime == Daytime.Day) {
                _timeService.Daytime = Daytime.Night;
            }

            if (_timeService.GameTime < 300f && _timeService.Daytime == Daytime.Night) {
                _timeService.Daytime = Daytime.Day;
            }
            
            switch (_timeService.Daytime) {
                case Daytime.Day:
                    _cameraService.TargetBackgroundColor = Color.white;
                    break;
                case Daytime.Night:
                    _cameraService.TargetBackgroundColor = Color.black;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _cameraService.Camera.backgroundColor = Color.Lerp(_cameraService.Camera.backgroundColor,
                _cameraService.TargetBackgroundColor, 8f * _timeService.DeltaTime);
        }
    }
}