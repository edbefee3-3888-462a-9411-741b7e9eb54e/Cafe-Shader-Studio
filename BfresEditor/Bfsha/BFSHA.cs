﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Toolbox.Core.ViewModels;
using Toolbox.Core;
using Toolbox.Core.IO;
using BfshaLibrary;

namespace BfresEditor
{
    public class BFSHA : NodeBase, IFileFormat
    {
        public bool CanSave { get; set; } = false;

        public string[] Description { get; set; } = new string[] { "BFRES" };
        public string[] Extension { get; set; } = new string[] { "*.bfres" };

        public File_Info FileInfo { get; set; }

        public bool Identify(File_Info fileInfo, Stream stream)
        {
            using (var reader = new FileReader(stream, true)) {
                return reader.CheckSignature(4, "FSHA");
            }
        }

        public BfshaFile BfshaFile;

        public void Load(Stream stream)
        {
            BfshaFile = new BfshaFile(stream);

            this.Header = BfshaFile.Name;
            foreach (var model in BfshaFile.ShaderModels.Values) {
                AddChild(LoadShaderModel(model));
            }
            Tag = this;
        }

        public void Save(Stream stream)
        {

        }

        private NodeBase LoadShaderModel(ShaderModel shaderModel)
        {
            var node = new NodeBase(shaderModel.Name);
            node.Tag = new ShaderModelWrapper(shaderModel);
            return node;
        }

        public static byte[] CreateGX2Shader(ShaderModel shaderModel, int programIndex)
        {
            var program = shaderModel.GetShaderProgram(programIndex);

            var mem = new MemoryStream();
            using (var writer = new FileWriter(mem))
            {
              //  writer.Write(program.gx);
            }
            return mem.ToArray();
        }
    }
}
