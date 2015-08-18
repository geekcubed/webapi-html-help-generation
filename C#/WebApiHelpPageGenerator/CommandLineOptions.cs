using CommandLine;
using CommandLine.Text;
using CommandLine.Infrastructure;

namespace WebApiHelpPageGenerator
{
    internal class CommandLineOptions
    {
        [Option('p', null, Required = true, HelpText = "Path to the assembly where the Web APIs are defined.")]
        public string AssemblyPath { get; set; }

        [Option('d', null, Required = false, HelpText = "Path to the XML documentation file for the Web API assembly.")]
        public string XmlDocPath { get; set; }

        [Option('o', null, Required = false, HelpText = "Folder path to save generated documentation.")]
        public string OutputPath { get; set; }

        [OptionArray('r', null, DefaultValue = new string[0], HelpText = "Additional assembly references.")]
        public string[] References { get; set; }

        [Option('e', null, HelpText = "Path to the assembly where the extensions are defined.")]
        public string ExtensionAssemblyPath { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}