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
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using VpNet.CommandLine;
using VpNet.CommandLine.Attributes;

namespace VpNet.ManagedApi.System.CommandLine
{
    public class CommandLineParser<TExecutionContext>
    {
        private string[] _args;

        public IParsableCommand<TExecutionContext> Parse(string commandLine)
        {
            return Parse(commandLine, Assembly.GetCallingAssembly());
        }

        public IParsableCommand<TExecutionContext> Parse(string commandLine, Assembly assembly)
        {
            IParsableCommand<TExecutionContext> cmd = null;
            // convert string[] args to simulate console input type style
            _args = (from object match in Regex.Matches(commandLine, @"([^\s]*""[^""]+""[^\s]*)|\w+") select match.ToString()).ToArray();
            foreach (var type  in from types in assembly.GetTypes() 
                                  from @interface in types.GetInterfaces() 
                                  where @interface.Name == typeof(IParsableCommand<TExecutionContext>).Name select types)
            {
                var b = type.GetCustomAttributes(typeof (CommandAttribute), false);
                if (b.Length == 1)
                {
                    var a = (CommandAttribute) b[0];
                    if (a.Literal == _args[0].ToLower())
                    {
                        // process the command.
                        cmd = (IParsableCommand<TExecutionContext>)Activator.CreateInstance(type);
                        foreach (var prop in type.GetProperties())
                        {
                            var p = prop.GetCustomAttributes(typeof(CommandLineAttribute), true);
                            if (p.Length == 1)
                            {
                                if ((p[0].GetType() == typeof(BoolFlagAttribute)))
                                {
                                    var boolFlag = (BoolFlagAttribute)p[0];
                                    if (boolFlag.ArgumentIndex == -1)
                                    {
                                        if (_args.Contains(((BoolFlagAttribute)p[0]).True))
                                        {
                                            prop.SetValue(cmd, true, null);
                                            continue;
                                        }
                                        else if (_args.Contains(((BoolFlagAttribute)p[0]).False))
                                        {
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        prop.SetValue(cmd, _args[boolFlag.ArgumentIndex] == boolFlag.True, null);
                                        continue;
                                    }
                                }
                                else if ((p[0].GetType() == typeof(NamedFlagAttribute)))
                                {
                                    var namedFlag = (NamedFlagAttribute) p[0];
                                    if (namedFlag.ArgumentIndex == -1)
                                    {
                                        if (_args.Contains(namedFlag.Prefix + namedFlag.Literal))
                                        {
                                            prop.SetValue(cmd, true, null);
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        if (namedFlag.ArgumentIndex < _args.Length)
                                        {

                                            if (_args[namedFlag.ArgumentIndex] == namedFlag.Prefix + namedFlag.Literal)
                                            {
                                                prop.SetValue(cmd, true, null);
                                                continue;
                                            }
                                        }
                                    }
                                }
                                else if ((p[0].GetType() == typeof(LiteralAttribute)))
                                {
                                    try { 
                                        var LiteralAttribute = (LiteralAttribute)p[0];
                                    //if (LiteralAttribute.ArgumentIndex==-1)
                                    //    throw new Exception("Literal attributes need to contain an argument index.");
                                        if (prop.PropertyType == typeof (Int32))
                                        {
                                            prop.SetValue(cmd, int.Parse(_args[LiteralAttribute.ArgumentIndex].Trim('"')), null);
                                        }
                                        else
                                        {
                                            prop.SetValue(cmd, _args[LiteralAttribute.ArgumentIndex].Trim('"'), null);

                                        }
                                    }
                                    catch (IndexOutOfRangeException ex)
                                    {
                                        
                                       // throw new Exception("Command needs more arguments.");
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }

            return cmd;
        }

        public bool Parse(string[] args)
        {
            _args = args;
            return true;
        }
    }
}
