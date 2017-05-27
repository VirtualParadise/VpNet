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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VpNet.Extensions;
using VpNet.ManagedApi.System.PluginFramework;
using VpNet.PluginFramework.Interfaces;

namespace VpNet.PluginFramework
{
    /// <summary>
    /// Hot swappable plugin framework.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HotSwapPlugins<T>
        where T : class, IPlugin
    {
        private readonly string _pluginPath;
        private List<Assembly> _assemblies;
        private List<T> _instances;
        private FileSystemWatcher _watcher;
        private List<T> _activePlugins; 

        public HotSwapPlugins()
        {
            _pluginPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _instances = new List<T>();
            _activePlugins = new List<T>();
            _assemblies = new List<Assembly>();
            Discover();
        }

        public void Activate(T plugin)
        {
            if (!_activePlugins.Contains(plugin))
                _activePlugins.Add(plugin);
        }

        public void SaveConfiguration(string path)
        {
            _activePlugins.Select(plugin => plugin.Description).ToList().Serialize(path);
        }

        public List<T> ActivePlugins()
        {
            return _activePlugins.ToList();
        }

        public void Deactivate(T plugin)
        {
            if (_activePlugins.Contains(plugin))
            {
                _activePlugins.Remove(plugin);
            }
            plugin.Console = new NullConsole(); 
            plugin.Unload();
            plugin.Dispose();
        } 

        public List<T> Instances
        {
            get { return _instances; }
        }

        private void Discover()
        {
            Assembly assembly;
            DirectoryInfo di = new DirectoryInfo(_pluginPath);
            var files = di.GetFiles();
            foreach (var file in files)
            {
                byte[] bytes = File.ReadAllBytes(file.FullName);
                try
                {
                    assembly = Assembly.Load(bytes);
                }
                catch
                {
                    continue;
                }
                bool isAdded = false;
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (!type.IsClass || type.IsNotPublic) continue;
                        if (type.BaseType == typeof(T))
                        {
                            if (!isAdded)
                            {
                                _assemblies.Add(assembly);
                            }
                            _instances.Add(Activator.CreateInstance(type) as T);
                            isAdded = true;
                        }
                    }
                }
                catch (ReflectionTypeLoadException ex)
                {
                    continue;
                }
            }
            _watcher = new FileSystemWatcher(_pluginPath, "*.dll");
            _watcher.Changed += new FileSystemEventHandler(WatcherChanged);
            _watcher.EnableRaisingEvents = true;
        }

        private int _changed = 0;

        private void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
                return;

            _changed++; // somehow watcher gets called twice.
            if (_changed == 1)
                return;

            _changed = 0;
            System.Threading.Thread.Sleep(500);
            Assembly assembly;
            byte[] bytes = File.ReadAllBytes(e.FullPath);
            try
            {
                assembly = Assembly.Load(bytes);
            }
            catch
            {
                // not a valid .net dll.
                return;
            }
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsNotPublic) continue;
                if (type.BaseType == typeof(T))
                {
                    var newInstance = Activator.CreateInstance(type) as T;
                    foreach (var instance in _instances.FindAll(p=>p.Description.Name == newInstance.Description.Name))
                    {
                        if (_activePlugins.Contains(instance))
                        {
                            _activePlugins.Remove(instance);
                            instance.Unload();
                            _instances.Remove(instance);
                            _instances.Add(newInstance);
                            instance.Dispose();
                            OnPluginUnloaded(this, new PluginUnloadedArguments<T>()
                            {
                                NewInstance = newInstance,
                            });
                        }
                        else
                        {
                            _instances.Remove(instance);
                            _instances.Add(newInstance);
                            instance.Dispose();
                        }
                    }
                }
            }
            _assemblies.RemoveAll(p => p.FullName == assembly.FullName);
            _assemblies.Add(assembly);
        }

        public void Dispose()
        {
            _watcher.EnableRaisingEvents = false;
            _watcher.Dispose();
        }

        public delegate void PluginUnloadedDelegate(HotSwapPlugins<T> sender, PluginUnloadedArguments<T> args);
        /// <summary>
        /// Occurs when an active plugin got unloaded and replaced by a newer version of the plugin.
        /// </summary>
        public event PluginUnloadedDelegate OnPluginUnloaded;

        public List<PluginDescription> LoadConfiguration(string path)
        {
            return SerializableExtensions.Deserialize<List<PluginDescription>>(path);
        }
    }

    public class PluginUnloadedArguments<T>
    {
        public T NewInstance { get; set; }
    }
}
