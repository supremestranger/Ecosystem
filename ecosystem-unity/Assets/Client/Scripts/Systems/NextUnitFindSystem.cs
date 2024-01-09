using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class NextUnitFindSystem : IEcsRunSystem {
        private CameraService _cameraService;
        private UnitService _unitService;
        private int _currentUnit;
        
        public void Run() {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                var entity = _unitService.Units[_currentUnit];
                _currentUnit++;
                _currentUnit %= _unitService.UnitCount;

                ref var unit = ref entity.Get<Unit>();
                _cameraService.ActualPos = unit.Position;
            }
        }
    }
}