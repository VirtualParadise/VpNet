#region Copyright notice
/*
____   ___.__         __               .__    __________                        .__.__                
\   \ /   |__________/  |_ __ _______  |  |   \______   _____ ____________    __| _|__| ______ ____   
 \   Y   /|  \_  __ \   __|  |  \__  \ |  |    |     ___\__  \\_  __ \__  \  / __ ||  |/  ____/ __ \  
  \     / |  ||  | \/|  | |  |  // __ \|  |__  |    |    / __ \|  | \// __ \/ /_/ ||  |\___ \\  ___/  
   \___/  |__||__|   |__| |____/(____  |____/  |____|   (____  |__|  (____  \____ ||__/____  >\___  > 
                                     \/                      \/           \/     \/        \/     \/  
    This file is part of VPNET Version 1.0

    Copyright (c) 2012-2013 CUBE3 (Cit:36)

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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq.Expressions;
using VpNet.Abstract;

namespace VpNet.PluginFramework
{
    public class PluginDiscovery
    {
        private DirectoryCatalog _catalog;
        private CompositionContainer _container;
        private CompositionBatch _batch;

        [ImportMany(typeof(BaseInstancePlugin))]
        public List<BaseInstancePlugin> Plugins;

        public PluginDiscovery()
        {
            _catalog = new DirectoryCatalog(".", "*.dll");
            _container = new CompositionContainer(_catalog);
            _batch = new CompositionBatch();
            _batch.AddPart(this);
            _container.Compose(_batch);
        }


        //public static IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> ListPlugins()
        //{
        //   // Expression<Func<ExportDefinition, bool>> constraint = (ExportDefinition exportDefinition) => exportDefinition.ContractName == CompositionServices.GetContractName(typeof(MyExport));
        //    var importDefinition = new ContractBasedImportDefinition("VpNet.Abstract.BaseInstancePlugin", null, null, ImportCardinality.ZeroOrMore, true, false, CreationPolicy.Any);
        //    return _catalog.GetExports(importDefinition);
        //}
    }
}
