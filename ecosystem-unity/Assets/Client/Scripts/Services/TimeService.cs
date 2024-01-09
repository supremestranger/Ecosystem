namespace Client {
    sealed class TimeService {
        public float DeltaTime;
        public float UnscaledDeltaTime;
        public float Time;
        public float GameTime; // виртуальное игровое время (день-ночь цикл).
        public Daytime Daytime;
    }
}