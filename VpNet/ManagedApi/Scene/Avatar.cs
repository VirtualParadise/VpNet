using System;
using System.Xml.Serialization;

namespace VpNet
{
    [Serializable]
    public class Avatar : Abstract.BaseAvatar
    {
        public Avatar(int userId, string name,int session,int avatarType, Vector3 position, Vector3 rotation)
            :base(userId, name,session,avatarType,position,rotation){}

        public Avatar() { }
    }
}
