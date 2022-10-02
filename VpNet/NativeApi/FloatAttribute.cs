using System;

namespace VpNet.NativeApi
{
    internal enum FloatAttribute
    {
        AvatarX,
        AvatarY,
        AvatarZ,
        AvatarYaw,
        AvatarPitch,
        MyX,
        MyY,
        MyZ,
        MyYaw,
        MyPitch,
        ObjectX,
        ObjectY,
        ObjectZ,
        ObjectRotationX,
        ObjectRotationY,
        ObjectRotationZ,
        [Obsolete("See ObjectRotationX")] ObjectPitch = ObjectRotationX,
        [Obsolete("See ObjectRotationY")] ObjectYaw = ObjectRotationY,
        [Obsolete("See ObjectRotationZ")] ObjectRoll = ObjectRotationZ,
        ObjectRotationAngle,
        TeleportX,
        TeleportY,
        TeleportZ,
        TeleportYaw,
        TeleportPitch,
        ClickHitX,
        ClickHitY,
        ClickHitZ,
        JoinX,
        JoinY,
        JoinZ,
        JoinYaw,
        JoinPitch,
        InviteX,
        InviteY,
        InviteZ,
        InviteYaw,
        InvitePitch
    }
}
