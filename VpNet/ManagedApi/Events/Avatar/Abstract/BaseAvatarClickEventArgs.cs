using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatarClickEventArgs : TimedEventArgs, IAvatarClickEventArgs
    {
        public virtual IAvatar Avatar { get; set; }
        public virtual IAvatar ClickedAvatar { get; set; }
        public virtual Vector3 WorldHit { get; set; }

        protected BaseAvatarClickEventArgs(IAvatar avatar, IAvatar clickedAvatar, Vector3 worldHit)
        {
            Avatar = avatar;
            ClickedAvatar = clickedAvatar;
            WorldHit = worldHit;
        }

        protected BaseAvatarClickEventArgs()
        {
        }
    }

}
