using System;
using System.Reflection;

namespace EAP_C2007L_NguyenDucHuy.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}