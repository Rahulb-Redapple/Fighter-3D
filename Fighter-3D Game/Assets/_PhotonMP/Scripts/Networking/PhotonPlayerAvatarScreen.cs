using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PhotonMultiplayer
{
    public class PhotonPlayerAvatarScreen : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private IconDataConfig _iconDataConfig;
        [SerializeField] private PhotonPlayerAvatarItem _photonPlayerAvatarItem;
        [SerializeField] private PhotonCustomProperties _photonCustomProperties;

        private List<PhotonPlayerAvatarItem> _photonPlayerAvatarItemList = new List<PhotonPlayerAvatarItem>();

        internal void Init()
        {
            PopulateAvatars();
        }

        private void PopulateAvatars()
        {
            if(!Equals(_photonPlayerAvatarItemList, null))
            {
                _photonPlayerAvatarItemList.ForEach(item => { Destroy(item.gameObject); });
                _photonPlayerAvatarItemList.Clear();
            }

            for(int i=0; i<_iconDataConfig.iconSpritesList.Count; i++)
            {
                PhotonPlayerAvatarItem avatarItem = Instantiate(_photonPlayerAvatarItem, _parent);
                avatarItem.Init(_iconDataConfig.iconSpritesList[i], _photonCustomProperties, i);
                _photonPlayerAvatarItemList.Add(avatarItem);
            }
        }
    }
}
