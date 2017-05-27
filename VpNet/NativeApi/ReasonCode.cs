#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2016 CUBE3 (Cit:36)

    VPNET is free software: you can redistribute it and/or modify it under the terms of the 
    GNU Lesser General Public License (LGPL) as published by the Free Software Foundation, either
    version 2.1 of the License, or (at your option) any later version.

    VPNET is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even
    the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the LGPL License
    for more details.

    You should have received a copy of the GNU Lesser General Public License (LGPL) along with VPNET.
    If not, see <http://www.gnu.org/licenses/>. 
*/
#endregion

using System;

namespace VpNet.NativeApi
{
    /// <summary>
    /// Reason Codes
    /// </summary>
    public enum ReasonCode
    {
        /// <summary>
        /// Operation Successfull
        /// </summary>
        Success = 0,
        /// <summary>
        /// Incorrect API Version
        /// </summary>
        VersionMismatch=1,
        /// <summary>
        /// Instance not initalized
        /// </summary>
        [Obsolete]
        NotInitialized=2,
        /// <summary>
        /// Instance already initialized
        /// </summary>
        [Obsolete]
        AlreadyInitialized=3,
        /// <summary>
        /// String too long
        /// </summary>
        StringTooLong=4,
        /// <summary>
        /// Invalid password
        /// </summary>
        InvalidPassword=5,
        /// <summary>
        /// World not found
        /// </summary>
        WorldNotFound=6,
        /// <summary>
        /// World login error
        /// </summary>
        WorldLoginError=7,
        /// <summary>
        /// Not in world
        /// </summary>
        NotInWorld=8,
        /// <summary>
        /// Connection error
        /// </summary>
        ConnectionError=9,
        /// <summary>
        /// No instance
        /// </summary>
        NoInstance=10,
        /// <summary>
        /// Not immplemented
        /// </summary>
        NotImplemented=11,
        /// <summary>
        /// No such attribute available
        /// </summary>
        NoSuchAttribute=12,
        /// <summary>
        /// Operation not allowed
        /// </summary>
        NotAllowed=13,
        /// <summary>
        /// Universe database error
        /// </summary>
        DatabaseError=14,
        /// <summary>
        /// No such user exists
        /// </summary>
        NoSuchUser=15,
        /// <summary>
        /// Timeout
        /// </summary>
        Timeout=16,
        /// <summary>
        /// Not in universe
        /// </summary>
        NotInUniverse=17,
        /// <summary>
        /// Invalid arguments
        /// </summary>
        InvalidArguments = 18,
        /// <summary>
        /// Used when querying an object by an unknown ID.
        /// </summary>
        ObjectNotFound = 19,
        /// <summary>
        /// Unknown error
        /// </summary>
        UnknownError = 20,
        /// <summary>
        /// The recursive wait
        /// </summary>
        RecursiveWait = 21,
        /// <summary>
        /// The join declined
        /// </summary>
        JoinDeclined = 22,
        /// <summary>
        /// The secure connection required
        /// </summary>
        SecureConnectionRequired = 23,
        /// <summary>
        /// The handshake failed
        /// </summary>
        HandshakeFailed = 24,
        /// <summary>
        /// The verifciation failed
        /// </summary>
        VerifciationFailed = 25,
        /// <summary>
        /// The no such session
        /// </summary>
        NoSuchSession = 26
    }
}
