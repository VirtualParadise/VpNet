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
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace VpNet.Extensions
{
    public static class FileExtensions
    {
        public static string LoadTextFile(this string path)
        {
            using (var sr = new StreamReader(path, System.Text.Encoding.Unicode)) return sr.ReadToEnd();
        }       

        public static string LoadTextFile(this FileInfo path)
        {
            using (var sr = new StreamReader(path.FullName, System.Text.Encoding.Unicode)) return sr.ReadToEnd();
        }

        public static void SaveTextFile(this FileInfo path, string contents)
        {
            using (var sw = new StreamWriter(path.FullName, false,System.Text.Encoding.Unicode)) { sw.Write(contents); };
        }

        public static void SaveTextFile(this string contents, string path)
        {
            if (!(new FileInfo(path).Directory.Exists))
            {
                Directory.CreateDirectory(new FileInfo(path).Directory.FullName);
            }
            using (var sw = new StreamWriter(path,false, System.Text.Encoding.Unicode))
            {
                sw.Write(contents);
            };
        }

        public static void AppendTextFile(this string contents, string path)
        {
            if (!(new FileInfo(path).Directory.Exists))
            {
                Directory.CreateDirectory(new FileInfo(path).Directory.FullName);
            }
            using (var sw = GetUnlockedStreamWriter(path, true))
            {
                sw.Write(contents);
                return;
            }
        }

        private static StreamWriter GetUnlockedStreamWriter(string fileName,bool append)
        {
            while (true)
            {
                try
                {
                    return new StreamWriter(fileName,append, System.Text.Encoding.Unicode);
                }
                catch (IOException e)
                {
                    if (!IsFileLocked(e))
                        throw;
                    Thread.Sleep(100);
                }
            }
        }

        private static bool IsFileLocked(IOException exception)
        {
            int errorCode = Marshal.GetHRForException(exception) & ((1 << 16) - 1);
            return errorCode == 32 || errorCode == 33;
        }
    }
}
