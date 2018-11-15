using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using TypeCast.Attributes;
using TypeCast.Core.Modules.PartialViewModule;
using TypeCast.Core.Resolver;
using TypeCast.Extensions;
using Umbraco.Core.Models;
using Umbraco.Core.Services;

namespace TypeCast.Core.Modules.PartialViewModule
{
    public class PartialViewModule : ICodeFirstEntityModule
    {
        private IDocumentTypeModule _documentTypeModule;
        private IFileService _fileService;
        private IContentTypeService _contentTypeService;

        public PartialViewModule(IDocumentTypeModule documentTypeModule, IFileService fileService, IContentTypeService contentTypeService)
        {
            _fileService = fileService;
            _contentTypeService = contentTypeService;
            _documentTypeModule = documentTypeModule;
        }

        public void Initialise(IEnumerable<Type> classes)
        {
            foreach(var t in classes)
            {
                CreatePartialView(t);
            }
        }

        private object _lock = new object();

        private void CreatePartialView(Type docType)
        {
            if (CodeFirstManager.Current.Features.InitialisationMode == InitialisationMode.Passive)
            {
                return;
            }

            DocumentTypeRegistration reg;
            if (_documentTypeModule.TryGetDocumentType(docType, out reg))
            {
                var attributes = docType.GetCodeFirstAttributes<PartialViewAttribute>();

                foreach(var attribute in attributes)
                {
                    var absolutePath = attribute.GetAbsolutePartialViewPath();
                    var partialView = new PartialView(attribute.GetRelativePartialViewPath());

                    lock (_lock)
                    {
                        if (System.IO.File.Exists(HostingEnvironment.MapPath(absolutePath)))
                        {
                            partialView.Content = System.IO.File.ReadAllText(HostingEnvironment.MapPath(absolutePath));
                        }
                        else
                        {
                            var content = $"@model {attribute.DecoratedTypeFullName}{Environment.NewLine}{Environment.NewLine}";
                            partialView.Content = content;
                        } 

                        _fileService.SavePartialView(partialView);
                    }
                }                
            }
        }      
    }
}

namespace TypeCast.Extensions
{
    public static class PartialViewModuleExtensions
    {
        public static void AddDefaultPartialViewModule(this CodeFirstModuleResolver resolver)
        {
            resolver.RegisterModule<ICodeFirstEntityModule>(new PartialViewModuleFactory());
        }
    }
}