using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Fighting3D
{
    public abstract class EssentialConfigScriptableObject : ScriptableObject
    {
        internal virtual void Init() { }
    }

    [CreateAssetMenu(fileName = nameof(EssentialConfigData), menuName = "Scriptable Objects/" + nameof(EssentialConfigData))]
    public class EssentialConfigData : ScriptableObject
    {
        [SerializeField] private List<EssentialConfigScriptableObject> _essentialConfigList = new List<EssentialConfigScriptableObject>();

#if UNITY_EDITOR
        public List<EssentialConfigScriptableObject> EssentialConfigList => _essentialConfigList;
#endif

        private Dictionary<Type, EssentialConfigScriptableObject> _essentialConfigsCollection = new Dictionary<Type, EssentialConfigScriptableObject>();

        public void Init()
        {
            for (int i = 0; i < _essentialConfigList.Count; i++)
            {
                _essentialConfigsCollection.Add(_essentialConfigList[i].GetType(), _essentialConfigList[i]);
                _essentialConfigList[i].Init();
            }
        }

        public T AccessConfig<T>() where T : EssentialConfigScriptableObject
        {
            if (_essentialConfigsCollection.ContainsKey(typeof(T)))
            {
                return (T)_essentialConfigsCollection[typeof(T)];
            }
            return default;
        }

        public void Cleanup()
        {
            _essentialConfigsCollection?.Clear();
        }
    }
}
