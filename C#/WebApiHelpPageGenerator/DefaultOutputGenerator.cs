﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Web.Http.Description;
using WebApiHelpPage;
using WebApiHelpPage.Models;

namespace WebApiHelpPageGenerator
{
    public class DefaultOutputGenerator : IOutputGenerator
    {
        private const string fileName = "Index.html";
        public string basePath { get; set; }


        public DefaultOutputGenerator()
        {
            basePath = Path.Combine(Environment.CurrentDirectory, "HtmlHelp");
        }

        public DefaultOutputGenerator(string outputPath) : base()
        {
            if (!string.IsNullOrEmpty(outputPath)) {
                this.basePath = outputPath;
            }
        }

        public void GenerateIndex(Collection<ApiDescription> apis)
        {
            Index indexTemplate = new Index
            {
                Model = apis,
                ApiLinkFactory = apiName =>
                {
                    return apiName + ".html";
                }
            };
            string helpPageIndex = indexTemplate.TransformText();
            WriteFile(fileName, helpPageIndex);
        }

        public void GenerateApiDetails(HelpPageApiModel apiModel)
        {
            Api apiTemplate = new Api
            {
                Model = apiModel,
                HomePageLink = fileName
            };
            string helpPage = apiTemplate.TransformText();
            WriteFile(apiModel.ApiDescription.GetFriendlyId() + ".html", helpPage);
        }

        private void WriteFile(string fileName, String pageContent)
        {
            Console.WriteLine("writing file: {0}", fileName);
            if (!Directory.Exists(basePath))
                Directory.CreateDirectory(basePath);
            File.WriteAllText(Path.Combine(basePath, fileName), pageContent);
        }
    }
}