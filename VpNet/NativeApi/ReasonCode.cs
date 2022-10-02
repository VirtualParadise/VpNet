using System;

namespace VpNet.NativeApi
{
    /// <summary>
    ///     An enumeration of native return codes.
    /// </summary>
    /// <value>This enumeration directly corresponds to the <c>VPReturnCode</c> enumeration in the native SDK.</value>
    public enum ReasonCode
    {
        /// <summary>
        ///     The operation was successful.
        /// </summary>
        Success = 0,
        
        /// <summary>
        ///     The versions of the API and SDK do not match.
        /// </summary>
        VersionMismatch,
        
        /// <summary>
        ///     The instance has not yet been initialized.
        /// </summary>
        [Obsolete("This value is no longer returned by the native SDK.")]
        NotInitialized,
        
        /// <summary>
        ///     The instance has already been initialized.
        /// </summary>
        [Obsolete("This value is no longer returned by the native SDK.")]
        AlreadyInitialized,
        
        /// <summary>
        ///     The specified string value is too long.
        /// </summary>
        StringTooLong,
        
        /// <summary>
        ///     The specified username and password constitute an invalid login request.
        /// </summary>
        InvalidLogin,
        
        /// <summary>
        ///     The specified world was not found.
        /// </summary>
        WorldNotFound,
        
        /// <summary>
        ///     There was an error logging into the world.
        /// </summary>
        WorldLoginError,
        
        /// <summary>
        ///     A world request was made while not being connected to a world server. 
        /// </summary>
        NotInWorld,
        
        /// <summary>
        ///     An error occured in connection.
        /// </summary>
        ConnectionError,
        
        /// <summary>
        ///     An operation was attempted when no instance was created.
        /// </summary>
        [Obsolete("This value is no longer returned by the native SDK.")]
        NoInstance,
        
        /// <summary>
        ///     The requested operation is not implemented.
        /// </summary>
        NotImplemented,
        
        /// <summary>
        ///     The requested attribute does not exist.
        /// </summary>
        NoSuchAttribute,
        
        /// <summary>
        ///     The requested operation is not allowed.
        /// </summary>
        NotAllowed,
        
        /// <summary>
        ///     An error occurred in the Universe database.
        /// </summary>
        DatabaseError,
        
        /// <summary>
        ///     The user does not exist.
        /// </summary>
        NoSuchUser,
        
        /// <summary>
        /// Timeout
        /// </summary>
        Timeout,
        
        /// <summary>
        ///     The instance is not connected to a universe. 
        /// </summary>
        NotInUniverse,
        
        /// <summary>
        ///     A function was called with invalid arguments.
        /// </summary>
        InvalidArguments,
        
        /// <summary>
        ///     The queried ID does not belong to an object.
        /// </summary>
        ObjectNotFound,
        
        /// <summary>
        ///     An unknown error occurred.
        /// </summary>
        UnknownError,
        
        /// <summary>
        ///     <c>vp_wait</c> was called recursively.
        /// </summary>
        [Obsolete("This value is not returned by the managed SDK.")]
        RecursiveWait,
        
        /// <summary>
        ///     The join request was declined.
        /// </summary>
        JoinDeclined,
        
        /// <summary>
        ///     A secure connection is required for the operation.
        /// </summary>
        SecureConnectionRequired,
        
        /// <summary>
        ///     An error occurred when attempting to initiate a secure handshake.
        /// </summary>
        HandshakeFailed,
        
        /// <summary>
        ///     Verification failed.
        /// </summary>
        VerificationFailed,
        
        /// <summary>
        ///     The queried session does not belong to an avatar.
        /// </summary>
        NoSuchSession,
        
        /// <summary>
        ///     The operation is not supported.
        /// </summary>
        NotSupported,
        
        /// <summary>
        ///     The invite request was declined.
        /// </summary>
        InviteDeclined,
        
        /// <summary>
        ///     The created or modified object was placed beyond the bounds of the world.
        /// </summary>
        OutOfBounds
    }
}
