using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class UnitLookForPartnerSystem : IEcsRunSystem {
        private EcsFilter<Unit, LookingForPartner> _units;
        private EcsWorld _world;
        
        public void Run() {
            EcsEntity first = default;
            
            foreach (var i in _units) {
                ref var entity = ref _units.GetEntity(i);
                
                if (first == default) {
                    first = entity;
                    continue;
                }

                ref var arriving1 = ref first.Get<Follow>();
                ref var arriving2 = ref entity.Get<Follow>();

                ref var busy1 = ref first.Get<Busy>();
                ref var busy2 = ref entity.Get<Busy>();

                arriving1.Target = entity;
                arriving2.Target = first;

                ref var reproduction = ref _world.NewEntity().Get<Reproduction>();
                reproduction.First = first;
                reproduction.Second = entity;
                
                first = default;
            }
        }
    }
}