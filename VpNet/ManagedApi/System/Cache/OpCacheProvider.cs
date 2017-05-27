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
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace VpNet.Cache
{
    public delegate void ModelDataDelegate(ModelData data);

    public class OpCacheProvider  
       
    {
        private readonly string _objectPath;
        private readonly string _localPath;
        private readonly string _modelPath;
        private readonly string _remoteModelPath;
        public Dictionary<string, Task> _tasks;
        private Dictionary<string, string> _modelData; 

        public OpCacheProvider(string objectPath, string localPath)
        {
            _tasks = new Dictionary<string, Task>();
            _objectPath = objectPath;
            _localPath = localPath;
            _modelPath = Path.Combine(localPath, "models");
            _remoteModelPath = _objectPath + "/models/";
            _modelData = new Dictionary<string, string>();
            if (!Directory.Exists(_modelPath))
            {
                Directory.CreateDirectory(_modelPath);
            }
        }

        public Task GetModelDataAsync(string name, ModelDataDelegate callback)
        {
            name = Path.GetFileNameWithoutExtension(name);
            Task t = null;
            if (_modelData.ContainsKey(name))
            {
                callback(new ModelData() { Data = _modelData[name], Name = name });
            }
            else
            {
                t = Task.Factory.StartNew(() => Download(name, callback, t));

            }
            return t;
        }

        private void Download(string name, ModelDataDelegate callback, Task task)
        {
            Task conflictingTask = null;
            lock (_tasks)
            {
                if (!_tasks.ContainsKey(name))
                {
                    _tasks.Add(name, task);
                }
                else
                {
                    if (_tasks.ContainsKey(name) && _tasks[name].Id != task.Id)
                    {
                        conflictingTask = _tasks[name];
                    }
                }
            }
            if (conflictingTask != null)
            {
                Debug.WriteLine("Waiting for other task to finish.");
                if (!conflictingTask.IsCompleted)
                 conflictingTask.Wait();

            }
            var model = Path.Combine(_modelPath, Path.GetFileNameWithoutExtension(name) + ".zip");
            if (File.Exists(model))
            {
                Debug.WriteLine("Reading and unzipping model from file system.");
                var zip = ZipStorer.Open(model, FileAccess.Read);
                var dir = zip.ReadCentralDir();
                if (dir.Count == 0)
                {
                    lock (_tasks)
                    {
                        _tasks.Remove(name);
                    }
                    callback(new ModelData
                                 {Data = string.Empty, Exception = new Exception("No such model found."), Name = name});
                }
                using (var mem = new MemoryStream())
                {
                    zip.ExtractFile(dir[0], mem);
                    zip.Close();
                    mem.Position = 0;
                    using (var sr = new StreamReader(mem))
                    {
                        var data = sr.ReadToEnd();
                        lock (_tasks)
                        {
                            _tasks.Remove(name);
                        }
                        if (!_modelData.ContainsKey(name))
                            _modelData.Add(name,data);
                        callback(new ModelData { Data = data, Name = name });
                    }
                }
                //task.Dispose();
                return;
            }

            
            Debug.WriteLine("Downloading model from internet.");
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFile(
                                           Path.Combine(_remoteModelPath,
                                                        Path.GetFileNameWithoutExtension(name) + ".zip"),model);
                    // recurse to read the file which was saved to harddisk.
                    lock (_tasks)
                    {
                        _tasks.Remove(name);
                    }
                    Download(name, callback,task);
                   
                }
                catch (Exception ex)
                {
                    lock (_tasks)
                    {

                        _tasks.Remove(name);
                    }
                    callback(new ModelData{Name=name, Data=string.Empty,Exception=ex});
                }
           }
            //task.Dispose();
        }
    }

    public class ModelData
    {
        public string Data { get; internal set; }
        public string Name { get; internal set; }
        public Exception Exception { get; internal set; }

        public bool HasException
        {
            get { return Exception != null; }
        }
    }
}
