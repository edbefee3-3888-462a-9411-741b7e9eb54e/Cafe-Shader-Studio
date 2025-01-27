﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BfresLibrary;
using GLFrameworkEngine;

namespace BfresEditor
{
    public class TextureSRTAnimFolder : SubSectionBase
    {
        public override string Header => "Texture SRT Animations";

        public TextureSRTAnimFolder(BFRES bfres, ResFile resFile, ResDict<MaterialAnim> resDict)
        {
            foreach (MaterialAnim anim in resDict.Values)
            {
                var node = new BfresNodeBase(anim.Name);
                AddChild(node);

                var fsha = new BfresMaterialAnim(anim, resFile.Name);
                node.Tag = fsha;
                bfres.MaterialAnimations.Add(fsha);
            }
        }
    }
}
