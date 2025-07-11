using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Fighting3D
{
    [CreateAssetMenu(fileName = nameof(HitMappingConfig), menuName = "Scriptable Objects/" + nameof(HitMappingConfig))]
    public class HitMappingConfig : EssentialConfigScriptableObject
    {
        [SerializeField] private List<HitMap> _hitMapsList = new List<HitMap>();
        internal ReadOnlyCollection<HitMap> HitMapsCollection => _hitMapsList.AsReadOnly();

        internal Dictionary<PunchType, List<HitType>> HitMapsCollectionDict = new Dictionary<PunchType, List<HitType>>();

        internal override void Init()
        {
            base.Init();
            for (int i = 0; i < _hitMapsList.Count; i++)
            {
                HitMapsCollectionDict.Add(_hitMapsList[i].PunchType, _hitMapsList[i].hitTypesCollection.ToList());
            }
        }
    }

    [Serializable]
    public class HitMap
    {
        [SerializeField] private PunchType _punchType;
        [SerializeField] private List<HitType> _hitTypesList = new List<HitType>();

        internal PunchType PunchType => _punchType;
        internal ReadOnlyCollection<HitType> hitTypesCollection => _hitTypesList.AsReadOnly();
    }
}
