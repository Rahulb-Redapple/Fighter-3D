using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PhotonMultiplayer
{
    public class PhotonPlayerAvatarItem : MonoBehaviour
    {
        [SerializeField] private Image _avatarImage;

        private PhotonCustomProperties _photonCustomProperties;

        private int _avatarIndex = -1;  

        internal void Init(Sprite sprite, PhotonCustomProperties photonCustomProperties, int index)
        {
            _photonCustomProperties = photonCustomProperties;
            _avatarImage.sprite = sprite;
            _avatarIndex = index;
        }

        public void OnClick()
        {
            _photonCustomProperties.SetProfileIcon(_avatarIndex);
        }
    }
}
