using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace PhotonMultiplayer
{
    [CreateAssetMenu(fileName = nameof(IconDataConfig), menuName = "Scriptable Objects/" + nameof(IconDataConfig))]
    public class IconDataConfig : ScriptableObject
    {
        [SerializeField] private List<Sprite> _iconSpriteList = new List<Sprite>();

        internal ReadOnlyCollection<Sprite> iconSpritesList => _iconSpriteList.AsReadOnly();
    }
}
