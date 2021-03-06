﻿using System.Xml;
using FubuCsProjFile.MSBuild;
using FubuCore;

namespace FubuCsProjFile
{
    public class AssemblyReference : ProjectItem
    {
        private const string HintPathAtt = "HintPath";

        public AssemblyReference() : base("Reference")
        {
        }

        public AssemblyReference(string assemblyName) : base("Reference", assemblyName)
        {
        }

        public AssemblyReference(string assemblyName, string hintPath) : this(assemblyName)
        {
            HintPath = hintPath;
        }

        public string HintPath { get; set; }
        public string FusionName { get; set; }
        public string DisplayName { get; set; }
        public bool? SpecificVersion { get; set; }
        public bool? Private { get; set; }
        public string Aliases { get; set; }

        internal override MSBuildItem Configure(MSBuildItemGroup @group)
        {
            var item = base.Configure(@group);
            if (HintPath.IsNotEmpty())
            {
                item.SetMetadata(HintPathAtt, HintPath);
            }

            if (FusionName.IsNotEmpty())
            {
                item.SetMetadata("FusionName", FusionName);
            }

            if (Aliases.IsNotEmpty())
            {
                item.SetMetadata("Aliases", Aliases);
            }

            if (DisplayName.IsNotEmpty())
            {
                item.SetMetadata("Name", DisplayName);
            }

            if (SpecificVersion.HasValue)
            {
                item.SetMetadata("SpecificVersion", SpecificVersion.Value.ToString().ToLower());
            }

            if (Private.HasValue)
            {
                item.SetMetadata("Private", Private.Value.ToString().ToLower());
            }

            return item;
        }


        internal override void Read(MSBuildItem item)
        {
            base.Read(item);


            HintPath = item.HasMetadata(HintPathAtt) ? item.GetMetadata(HintPathAtt) : null;
            FusionName = item.HasMetadata("FusionName") ? item.GetMetadata("FusionName") : null;
            Aliases = item.HasMetadata("Aliases") ? item.GetMetadata("Aliases") : null;
            DisplayName = item.HasMetadata("Name") ? item.GetMetadata("Name") : null;

            if (item.HasMetadata("SpecificVersion"))
            {
                SpecificVersion = bool.Parse(item.GetMetadata("SpecificVersion"));
            }

            if (item.HasMetadata("Private"))
            {
                Private = bool.Parse(item.GetMetadata("Private"));
            }
        }
    }
}