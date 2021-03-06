﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using Umbraco.Core;
using Umbraco.Core.Models;
using TypeCast.Extensions;
using TypeCast.Attributes;
using TypeCast.Core.Resolver;
using TypeCast.Core.Modules;
using System.Reflection;
using Umbraco.Core.Services;
using TypeCast.Exceptions;

namespace TypeCast.Core.Modules
{
    public class TemplateModule : ITemplateModule
    {
        private IDocumentTypeModule _documentTypeModule;
        private IFileService _fileService;
        private IContentTypeService _contentTypeService;

        public TemplateModule(IDocumentTypeModule documentTypeModule, IFileService fileService, IContentTypeService contentTypeService)
        {
            _fileService = fileService;
            _contentTypeService = contentTypeService;
            _documentTypeModule = documentTypeModule;
        }
        public void Initialise(IEnumerable<Type> classes)
        {
            foreach (var t in classes)
            {
                RegisterTemplates(t);
            }
        }

        private object _lock = new object();

        /// <summary>
        /// Registers the specified templates for the given doctype. Creates a basic default cshtml file if none exists at the specified path.
        /// </summary>
        public void RegisterTemplates(Type docType)
        {
            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Passive)
            {
                return;
            }

            DocumentTypeRegistration reg;
            if (_documentTypeModule.TryGetDocumentType(docType, out reg))
            {
                var attributes = docType.GetCodeFirstAttributes<TemplateAttribute>();
                var type = _contentTypeService.GetContentType(reg.Alias);
                List<ITemplate> templateList = new List<ITemplate>();
                ITemplate defaultTemplate = null;

                foreach (var attribute in attributes)
                {
                    if (templateList.Any(x => x.Alias == attribute.TemplateAlias))
                    {
                        throw new CodeFirstException("Duplicate template aliases specified on " + docType.FullName);
                    }
                    var template = ConfigureTemplate(docType, ref defaultTemplate, attribute);
                    templateList.Add(template);
                }

                if (defaultTemplate != null)
                {
                    type.SetDefaultTemplate(defaultTemplate);
                }
                type.AllowedTemplates = templateList;
                _contentTypeService.Save(type);
            }
            else
            {
                throw new CodeFirstException(docType.Name + " is not a registered document type. [Template] can only be applied to document types.");
            }
        }

        private ITemplate ConfigureTemplate(Type docType, ref ITemplate defaultTemplate, TemplateAttribute attribute)
        {
            var template = _fileService.GetTemplates().FirstOrDefault(x => string.Equals(x.Alias, attribute.TemplateAlias, StringComparison.InvariantCultureIgnoreCase));
            if (template == null)
            {
                template = CreateTemplate(attribute);
            }
            if (attribute.IsDefault)
            {
                if (defaultTemplate == null)
                {
                    defaultTemplate = template;
                }
                else
                {
                    throw new CodeFirstException("More than one default template specified for " + docType.FullName);
                }
            }
            if (attribute.TemplateName != template.Name)
            {
                var t = new umbraco.cms.businesslogic.template.Template(template.Id);
                t.Text = attribute.TemplateName;
                t.Save(); 
                template = _fileService.GetTemplates().FirstOrDefault(x => x.Alias == attribute.TemplateAlias); //re-get the template to pick up the changes
            }
            return template;
        }

        private ITemplate CreateTemplate(TemplateAttribute attribute)
        {
            var absolutePath = GetAbsoluteTemplatePath(attribute);

            var template = new Template(attribute.TemplateName, attribute.TemplateAlias);

            lock (_lock)
            {
                if (System.IO.File.Exists(HostingEnvironment.MapPath(absolutePath)))
                {
                    //get the existing content from the file so it isn't overwritten when we save the template.
                    template.Content = System.IO.File.ReadAllText(HostingEnvironment.MapPath(absolutePath));
                }
                else
                {
                    //TODO get this from a file resource containing a default view
                    var content = string.Empty;
                    content = "@inherits TypeCast.Views.UmbracoDocumentViewPage<" + attribute.DecoratedTypeFullName + ">" + Environment.NewLine + Environment.NewLine;

                    template.Content = content;
                }
                
                _fileService.SaveTemplate(template);
            }
            return template;
        }


        private string GetAbsoluteTemplatePath(TemplateAttribute attribute)
        {
            return $"~/Views/{attribute.TemplateAlias.ToPascalCase()}.cshtml";
        }
    }

}

namespace TypeCast.Extensions
{
    public static class TemplateModuleExtensions
    {
        public static void AddDefaultTemplateModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<ITemplateModule>(new TemplateModuleFactory());
        }
    }
}