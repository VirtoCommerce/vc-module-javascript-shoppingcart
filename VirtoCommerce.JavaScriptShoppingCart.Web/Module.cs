﻿using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Optimization;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Security;
using VirtoCommerce.JavaScriptShoppingCart.Web.Bundles;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security;
using Role = VirtoCommerce.Platform.Core.Security.Role;

namespace VirtoCommerce.JavaScriptShoppingCart.Web
{
    public class Module : ModuleBase
    {
        private readonly IUnityContainer _container;

        public Module(IUnityContainer container)
        {
            _container = container;
        }

        #region IModule Members

        public override void PostInitialize()
        {
            var moduleCatalog = _container.Resolve<IModuleCatalog>();
            var javaScriptShoppingCartModule = moduleCatalog.Modules.OfType<ManifestModuleInfo>().FirstOrDefault(x => x.ModuleName == "VirtoCommerce.JavaScriptShoppingCart");
            if (javaScriptShoppingCartModule != null)
            {
                var moduleRelativePath = "~/Modules" + javaScriptShoppingCartModule.FullPhysicalPath.Replace(HostingEnvironment.MapPath("~/Modules"), string.Empty).Replace("\\", "/");
                var cssBundle = new Bundle("~/styles/vc-shopping-cart", new CssMinify())
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Content"), "*.css", true);
                BundleTable.Bundles.Add(cssBundle);

                var partialBundle = new AngularJavaScriptBundle("virtoCommerce.cartModule", "~/scripts/vc-shopping-cart")
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/cart"), "*.js", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/security"), "*.js", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout"), "*.js", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout"), "*.tpl.html", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout-modal"), "*.js", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/checkout-modal"), "*.tpl.html", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/cart"), "*.tpl.html", true)
                    .IncludeDirectory(Path.Combine(moduleRelativePath, "Scripts/services"), "*.js", true);
                BundleTable.Bundles.Add(partialBundle);
            }

            InitializeSecurity();
        }

        private void InitializeSecurity()
        {
            var roleManagementService = _container.Resolve<IRoleManagementService>();
            var securityService = _container.Resolve<ISecurityService>();

            var allPermissions = securityService.GetAllPermissions().Where(x => SecurityConstants.Permissions.AllPermissions.Contains(x.Id));

            InitializeRole(roleManagementService, SecurityConstants.JsShoppingCartUser, allPermissions);

        }

        private void InitializeRole(IRoleManagementService roleManagementService, Role jsShoppingCartRole, IEnumerable<Permission> permissions)
        {
            var role = roleManagementService.SearchRoles(new RoleSearchRequest { Keyword = jsShoppingCartRole.Name }).Roles.FirstOrDefault() ?? new Role { Id = jsShoppingCartRole.Id, Name = jsShoppingCartRole.Name, Description = jsShoppingCartRole.Description };
            var callApiPermission = PredefinedPermissions.Permissions.Where(p => p.Id == PredefinedPermissions.SecurityCallApi).ToArray();
            role.Permissions = callApiPermission.Concat(permissions ?? Enumerable.Empty<Permission>()).ToArray();
            roleManagementService.AddOrUpdateRole(role);
        }

        #endregion
    }
}