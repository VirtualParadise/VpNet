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

namespace VpNet.Interfaces
{
    public interface IChatFunctions<out TRc, in TAvatar, in TColor, TVector3> 
        where TRc : class, IRc, new()
        where TColor : class, IColor, new()
        where TVector3 : struct, IVector3
        where TAvatar : class, IAvatar<TVector3>, new()
        
    {
        TRc Say(string message);
        TRc Say(string format, params object[] arg);

        TRc ConsoleMessage(TAvatar targetAvatar, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0);
        TRc ConsoleMessage(int targetSession, string name, string message, TextEffectTypes effects = 0, byte red = 0, byte green = 0, byte blue = 0);
        TRc ConsoleMessage(TAvatar targetAvatar, string name, string message, TColor color, TextEffectTypes effects = 0);
        TRc ConsoleMessage(int targetSession, string name, string message, TColor color, TextEffectTypes effects = 0);
        TRc ConsoleMessage(string name, string message, TColor color, TextEffectTypes effects = 0);
        TRc ConsoleMessage(string message, TColor color, TextEffectTypes effects = 0);
        TRc ConsoleMessage(string message);
    }
}
