using System.Collections.Generic;
using Leopotam.Ecs;

namespace Client {
    // этот сервис является кешем для юнитов для быстрого поиска следующего юнита
    sealed class UnitService {
        public readonly List<EcsEntity> Units;
        public int UnitCount;

        public UnitService() {
            Units = new();
        }
    }
}