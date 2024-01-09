using System.Collections.Generic;

namespace Client {
    sealed class EnvironmentService {
        public List<Circle> Obstacles;
        public List<Circle> Seas;

        public EnvironmentService() {
            Obstacles = new();
            Seas = new();
        }
    }
}