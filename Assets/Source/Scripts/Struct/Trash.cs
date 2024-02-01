using UnityEngine;

namespace BuilderStory
{
    public class Trash : MonoBehaviour, IBuildable
    {
        [SerializeField] private Transform _trashPoint;

        public bool IsBuilding { get; private set; } = true;

        public bool IsBuilt()
        {
            return false;
        }

        public bool TryGetBuildMaterial(out BuildMaterial buildMaterial)
        {
            buildMaterial = null;
            return true;
        }

        public bool CouldPlaceMaterial(ILiftable material)
        {
            return true;
        }

        public bool TryPlaceMaterial(ILiftable liftable, float placeDuration, out Transform destination)
        {
            destination = _trashPoint;
            return true;
        }

        public Transform GetMaterialPoint(ILiftable material)
        {
            return _trashPoint;
        }
    }
}
