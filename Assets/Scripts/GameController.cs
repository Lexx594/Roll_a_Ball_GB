using Maze.Interface;
using UnityEngine;

namespace Maze
{
    public sealed class GameController : MonoBehaviour
    {
        private InteractiveObject[] _interactiveObjects;
        private void Start()
        {
            _interactiveObjects = FindObjectsOfType<InteractiveObject>();
        }
        private void Update()
        {
            for (var i = 0; i < _interactiveObjects.Length; i++)
            {
                var interactiveObject = _interactiveObjects[i];
                if (interactiveObject == null)
                {
                    continue;
                }
                if (interactiveObject is IFlay flay)
                {
                    flay.Flay();
                }
                if (interactiveObject is IFlicker flicker)
                {
                    flicker.Flicker();
                }
                if (interactiveObject is IRotator rotation)
                {
                    rotation.Rotation();
                }
            }
        }
    }
}
