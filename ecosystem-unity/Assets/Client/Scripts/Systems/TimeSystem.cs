using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class TimeSystem : IEcsRunSystem {
        private TimeService _timeService;
        
        public void Run() {
            _timeService.DeltaTime = Time.deltaTime;
            _timeService.Time = Time.time;
            _timeService.UnscaledDeltaTime = Time.unscaledDeltaTime;
        }
    }
}