

using UnityEngine;

namespace UI.Menus.States
{
    internal class Gameplay : IState
    {
        private MapSelector _mapSelector;

        public Gameplay(MapSelector mapSelector)
        {
            _mapSelector = mapSelector;
        }

        public void Enter() { }

        public void Exit() { }

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuManager.Instance.SetState(new Pause(_mapSelector));
            }
        }
    }
}
