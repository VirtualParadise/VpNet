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

using System.Xml.Serialization;

namespace VpNet
{
    /// <summary>
    /// Default Vp Exception implementation. Throws an Exception if an RC code is not 0.
    /// </summary>
    [XmlRoot("Rc", Namespace = Global.XmlnsRc)]
    public class RcDefault : Abstract.BaseRc
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RcDefault"/> class.
        /// </summary>
        public RcDefault(){}

        private int _rc;

        /// <summary>
        /// Gets or sets the rc.
        /// </summary>
        /// <value>
        /// The rc.
        /// </value>
        public override int Rc
        {
            get
            {
                {return _rc;}
            }
            set
            {
                _rc = value;
                if (!IsHandledByEventSubsription && value != 0 && !IgnoreExceptions)
                    throw Exception; 
                base.Rc = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RcDefault"/> class.
        /// </summary>
        /// <param name="rc">The rc.</param>
        public RcDefault(int rc) : base(rc)
        {
            if (rc != 0)
                throw Exception;
        }
    }
}
