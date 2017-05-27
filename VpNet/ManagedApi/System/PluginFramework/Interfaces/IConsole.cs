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
using VpNet.PluginFramework.Interfaces.IConsoleDelegate;

namespace VpNet.PluginFramework.Interfaces
{
    namespace IConsoleDelegate
    {
        public delegate string GetPrompt();
        public delegate void ParseCommandLineDelegate(string commandLine);
    }

    public interface IConsole
    {

        GetPrompt GetPromptTarget { get; set; }
        ParseCommandLineDelegate ParseCommandLine { get; set; }
        ConsoleColor BackgroundColor { get; set; }
        bool IsPromptEnabled { get; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        string Title { get; set; }

        void ReadLine();
        void WriteLine(ConsoleMessageType type, string text);
        void WriteLine(string text);
        void Write(ConsoleMessageType type, string text);
        void Write(string text);
        void RevertPrompt();

        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();
    }
}