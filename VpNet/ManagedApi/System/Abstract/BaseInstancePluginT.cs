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

using VpNet.Interfaces;
using VpNet.PluginFramework;
using VpNet.PluginFramework.Interfaces;

namespace VpNet.Abstract
{

    public abstract class BaseInstancePluginT<TWorld> : IInstancePlugin<TWorld>, IPlugin
        where TWorld: class, IWorld, new()
    {
        public abstract void InitializePlugin(BaseInstanceEvents<TWorld> baseInstance);

        public IConsole Console { get; set; }
        public abstract PluginDescription Description {get;}

        public abstract void Unload();

        protected BaseInstancePluginT(){}

        #region Implementation of IDisposable

        ~BaseInstancePluginT()
        {
            
        }

        public virtual bool HandleConsoleInput(string input)
        {
            return false;
        }

        public virtual void Dispose()
        {

        }

        #endregion
    }

}
