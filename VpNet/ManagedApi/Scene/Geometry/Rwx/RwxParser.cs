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
using System.Globalization;
using System.Linq;
using VpNet.Extensions;

namespace VpNet.Geometry.Rwx
{
    public static class RwxParser
    {
        private static char[] split = new[] { ' ', '\t' };

        public static RwxModel Parse(string modelData)
        {
            var rwx = new RwxModel();
            rwx.SourceFile = "unit-test.rwx";
            RwxClump a = null;
            RwxClump proto = null;
            RwxClump model = null;
            string protoName = null;
            bool isProto = false;

            foreach (string line in modelData.ToLower().Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.Trim().StartsWith("#"))
                    continue;
                var items = line.Split(split, StringSplitOptions.RemoveEmptyEntries);
                switch (items[0])
                {
                    case "triangle":
                        a.Indices.Add(int.Parse(items[1], CultureInfo.InvariantCulture));
                        a.Indices.Add(int.Parse(items[2], CultureInfo.InvariantCulture));
                        a.Indices.Add(int.Parse(items[3], CultureInfo.InvariantCulture));
                        break;
                    case "quad":
                        for (int i = 1; i < items.Length; i++)
                        {
                            a.Indices.Add(int.Parse(items[i], CultureInfo.InvariantCulture));
                        }
                        break;
                    //case "modelbegin":
                    //    break;
                    case "addmaterialmode":
                        if (items[1] == "double")
                            if (model == null)
                                // global double material.
                                rwx.IsDouble = true;
                            else
                            {
                                model.Material.IsDouble = true;
                            }
                        break;
                    case "protobegin":
                        proto = new RwxClump();
                        protoName = items[1];
                        proto.Name = items[1];
                        a = proto;
                        break;
                    case "surface":
                        a.Material.Ambient = double.Parse(items[1], CultureInfo.InvariantCulture);
                        a.Material.Diffuse = double.Parse(items[2], CultureInfo.InvariantCulture);
                        a.Material.Specular = double.Parse(items[3], CultureInfo.InvariantCulture);
                        break;
                    case "texturemodes":
                        for (int i = 1; i < items.Length; i++)
                        {
                            switch (items[i])
                            {
                                case "lit":
                                    a.Material.IsTextureLit = true;
                                    break;
                                case "foreshorten":
                                    a.Material.IsTextureForeshorten = true;
                                    break;
                                case "filter":
                                    a.Material.IsTextureFilter = true;
                                    break;
                            }

                        }
                        break;
                    case "translate":
                        a.Transforms.Add(new RwxTranslate() { Translate = new Vector3(items[1], items[2], items[3]) });
                        break;
                    case "transform":
                        a.Transforms.Add(new RwxTransform { Matrix = new Matrix(items.Skip(1).Take(16).ToArray()) });
                        break;
                    case "vertex":
                        a.Vertices.Add(double.Parse(items[1], CultureInfo.InvariantCulture));
                        a.Vertices.Add(double.Parse(items[2], CultureInfo.InvariantCulture));
                        a.Vertices.Add(double.Parse(items[3], CultureInfo.InvariantCulture));
                        a.Uvs.Add(double.Parse(items[5], CultureInfo.InvariantCulture));
                        a.Uvs.Add(double.Parse(items[6], CultureInfo.InvariantCulture));
                        a.Uvs.Add(0);
                        break;
                    case "color":
                        if (items.Length > 3)
                            a.Material.Color = new Vector3(items[1], items[2], items[3]);
                        break;
                    case "opacity":
                        if (items.Length > 1)
                            a.Material.Opacity = (double.Parse(items[1], CultureInfo.InvariantCulture));
                        break;
                    case "texture":
                        if (items.Length > 1)
                        {
                            a.Material.Texture = items[1];
                        }
                        break;
                    case "lightsampling":
                        if (items.Length > 1)
                        {
                            a.Material.IsVertexLighting = (items[1] == "vertex");
                        }
                        break;
                    case "protoend":
                        rwx.Prototypes.Add(protoName, proto);
                        proto = null;
                        break;
                    case "clumpbegin":
                        model = new RwxClump();
                        a = model;
                        break;
                    case "clumpend":
                        rwx.Models.Add(model);
                        model = null;
                        break;
                    case "ambient":
                        if (items.Length > 1)
                            a.Material.Ambient = (double.Parse(items[1], CultureInfo.InvariantCulture));
                        break;
                    case "diffuse":
                        if (items.Length > 1)
                            a.Material.Diffuse = (double.Parse(items[1], CultureInfo.InvariantCulture));
                        break;
                    case "rotate":
                        if (items.Length > 4)
                            a.AddTransform(new Vector3(items[1], items[2], items[3]), double.Parse(items[4], CultureInfo.InvariantCulture));
                        break;
                    case "scale":
                        if (items.Length > 3)
                            a.AddScale(new Vector3(items[1], items[2], items[3]));
                        break;
                    case "protoinstance":
                        if (items.Length > 1)
                        {
                            var c = rwx.Prototypes[items[1]].Copy();
                            c.Transforms.AddRange(a.Transforms.Copy());
                            rwx.Models.Add(c);
                        }
                        break;
                    case "modelend":
                        break;
                    default:
                        break;

                }
            }
            foreach (var m in rwx.Models)
            {
                int ij = 0;
                if (m.Name == string.Empty)
                {
                    m.Name = "unnamed_" + ij;
                    ij++;
                }
            }
            return rwx;
        }
    }
}
