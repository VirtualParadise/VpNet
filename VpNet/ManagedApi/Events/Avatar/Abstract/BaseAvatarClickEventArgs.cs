using System;
using VpNet.Interfaces;

namespace VpNet.Abstract
{
    [Serializable]
    public abstract class BaseAvatarClickEventArgs<TAvatar> : TimedEventArgs, IAvatarClickEventArgs<TAvatar>
        where TAvatar : class, IAvatar, new()
    {
        public virtual TAvatar Avatar { get; set; }
        public virtual TAvatar ClickedAvatar { get; set; }
        public virtual Vector3 WorldHit { get; set; }

        protected BaseAvatarClickEventArgs(TAvatar avatar, TAvatar clickedAvatar, Vector3 worldHit)
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
