using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class TimeControlSystem : IEcsRunSystem {
        private Ui _ui;
        
        public void Run() {
            if (Input.GetKeyDown(KeyCode.F)) {
                Time.timeScale = 1f;
                _ui.TimeScaleLabel.text = "1x";
            }

            if (Input.GetKeyDown(KeyCode.A)) {
                var add = 1f;
                if (Input.GetKey(KeyCode.LeftShift)) {
                    add = 10f;
                }
                Time.timeScale = Mathf.Max(0f, Time.timeScale - add);
                _ui.TimeScaleLabel.text = $"{Time.timeScale}x";
            }
            
            if (Input.GetKeyDown(KeyCode.D)) {
                var add = 1f;
                if (Input.GetKey(KeyCode.LeftShift)) {
                    add = 10f;
                }
                Time.timeScale += add;
                _ui.TimeScaleLabel.text = $"{Time.timeScale}x";
            }
        }
    }
}