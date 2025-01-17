using System.Collections.Generic;
using BuilderStory.BuildingMaterial;
using UnityEngine;

namespace BuilderStory.Config.BuildMaterial
{
    [CreateAssetMenu(fileName = "BuildMaterialMap", menuName = "BuilderStory/BuildMaterialMap", order = 0)]
    public class BuildMaterialMap : ScriptableObject
    {
        [SerializeField] private BuildMaterialMapStructure[] _buildMaterialMapStructures;

        private Dictionary<MaterialType, BuildMaterialMapStructure> _buildMaterialMap
            = new Dictionary<MaterialType, BuildMaterialMapStructure>();

        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            _buildMaterialMap.Clear();
        }

        public Material GetMaterial(MaterialType type)
        {
            return _buildMaterialMap[type].BuildMaterial;
        }

        private void Init()
        {
            foreach (var buildMaterialMapStructure in _buildMaterialMapStructures)
            {
                if (_buildMaterialMap.ContainsKey(buildMaterialMapStructure.Type))
                {
                    Debug.LogError($"Material with type {buildMaterialMapStructure.Type} already exists");
                    continue;
                }

                _buildMaterialMap.Add(buildMaterialMapStructure.Type, buildMaterialMapStructure);
            }
        }
    }
}
