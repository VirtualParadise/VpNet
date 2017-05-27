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

namespace VpNet.Interfaces
{
    public interface IUserAttributes
    {
        /// <summary>
        /// Gets or sets the id of the user, not to be mistaken with session.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        int Id { get; set; }
        /// <summary>
        /// Gets or sets the registration date of the user.
        /// </summary>
        /// <value>
        /// The registration date.
        /// </value>
        DateTime RegistrationDate { get; set; }
        /// <summary>
        /// Gets or sets the online time spend by the user in the universe.
        /// </summary>
        /// <value>
        /// The online time.
        /// </value>
        TimeSpan OnlineTime { get; set; }
        /// <summary>
        /// Gets or sets the last login of the user in the universe.
        /// </summary>
        /// <value>
        /// The last login.
        /// </value>
        DateTime LastLogin { get; set; }
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        string Email { get; set; }

    }
}
