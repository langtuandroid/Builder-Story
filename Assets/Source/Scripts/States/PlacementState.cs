using System.Collections;
using BuilderStory.Lifting;
using BuilderStory.Struct;
using UnityEngine;

namespace BuilderStory.States
{
    public class PlacementState : IBehaviour
    {
        private static readonly int Placement = Animator.StringToHash("Pickup");

        private readonly Animator _animator;
        private readonly float _placementDistance;
        private readonly Lift _lift;
        private readonly LayerMask _layerMask;

        private IBuildable _buildable;

        private Coroutine _placing;

        public PlacementState(
            Animator animator,
            Lift lift,
            float placementDistance,
            LayerMask layerMask)
        {
            _animator = animator;
            _lift = lift;
            _placementDistance = placementDistance;
            _layerMask = layerMask;
        }

        public void Enter()
        {
            _placing = _lift.StartCoroutine(Place());
        }

        public void Exit()
        {
            if (_placing != null)
            {
                _lift.StopCoroutine(_placing);
            }
        }

        public bool IsReady()
        {
            if (_lift.IsEmpty || _lift.IsLifting)
            {
                return false;
            }

            var colliders = Physics.OverlapSphere(
                _lift.transform.position,
                _placementDistance,
                _layerMask);

            if (colliders.Length == 0)
            {
                return false;
            }

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out IBuildable buildable) == false)
                {
                    continue;
                }

                if (buildable.IsBuilt())
                {
                    continue;
                }

                if (buildable.CouldPlaceMaterial(_lift.LastLiftable))
                {
                    _buildable = buildable;
                    return true;
                }
            }

            return false;
        }

        public void Update()
        {
        }

        private IEnumerator Place()
        {
            var delay = new WaitForSeconds(_lift.Duration);

            while (_lift.IsEmpty == false)
            {
                if (_buildable.TryPlaceMaterial(_lift.LastLiftable, _lift.Duration, out Transform destination) == false)
                {
                    break;
                }

                _animator.SetTrigger(Placement);
                _lift.Place(_lift.LastLiftable, destination);

                yield return delay;
            }
        }
    }
}
