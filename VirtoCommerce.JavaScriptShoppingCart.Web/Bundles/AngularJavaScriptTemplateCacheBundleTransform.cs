using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Optimization;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Bundles
{
    public class AngularJavaScriptTemplateCacheBundleTransform : IBundleTransform
    {
        private readonly string _moduleName;

        public AngularJavaScriptTemplateCacheBundleTransform(string moduleName)
        {
            _moduleName = moduleName;
        }

        public void Process(BundleContext context, BundleResponse response)
        {
            var strBundleResponse = new StringBuilder();

            foreach (var file in response.Files)
            {
                if (file.IncludedVirtualPath.EndsWith(".js"))
                {
                    var absFile = HttpContext.Current.Server.MapPath(file.IncludedVirtualPath);
                    var content = File.ReadAllText(absFile);
                    strBundleResponse.AppendLine(content);
                }
            }

            strBundleResponse.AppendFormat(@"angular.module('{0}').run(['$templateCache',function(t){{", _moduleName);

            foreach (var file in response.Files)
            {
                if (!file.IncludedVirtualPath.EndsWith(".js"))
                {
                    var absFile = HttpContext.Current.Server.MapPath(file.IncludedVirtualPath);
                    var content = File.ReadAllText(absFile).Replace("\r\n", string.Empty).Replace("\n", string.Empty).Replace("'", "\\'");
                    strBundleResponse.AppendFormat(@"t.put('{0}','{1}');", file.VirtualFile.Name, content);
                }
            }

            strBundleResponse.Append(@"}]);");

            response.Files = new List<BundleFile>();
            response.Content = strBundleResponse.ToString();
            response.ContentType = "text/javascript";
        }
    }
}
