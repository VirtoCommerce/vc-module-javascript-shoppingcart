using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Optimization;
using AutoMapper;
using Microsoft.Practices.Unity;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Security;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Services;
using VirtoCommerce.JavaScriptShoppingCart.Crawling;
using VirtoCommerce.JavaScriptShoppingCart.Crawling.Mapping;
using VirtoCommerce.JavaScriptShoppingCart.Data.Services;
using VirtoCommerce.JavaScriptShoppingCart.Web.Bundles;
using VirtoCommerce.JavaScriptShoppingCart.Web.Mappings;
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

        public override void Initialize()
        {
            base.Initialize();

            _container.RegisterInstance<ICrawlingConfiguration>(new DefaultCrawlingConfiguration(PredefinedMappings.DefaultMapping));
            _container.RegisterType<ICrawler, DefaultCrawler>();
            _container.RegisterType<ICartBuilder, CartBuilder>();

            var configuration = new MapperConfiguration(configurationExpression =>
            {
                configurationExpression.AddProfile(new MappingProfile());
            });

#pragma warning disable S125 // Could be uncommented when debugging mappings

            // configuration.AssertConfigurationIsValid();
#pragma warning restore S125

            var mapper = configuration.CreateMapper();

            _container.RegisterInstance(mapper);
        }

        public override void PostInitialize()
        {
            const string moduleName = "VirtoCommerce.JavaScriptShoppingCart";

            var moduleCatalog = _container.Resolve<IModuleCatalog>();
            var moduleInfos = moduleCatalog.Modules.OfType<ManifestModuleInfo>();
            var module = moduleInfos.FirstOrDefault(moduleInfo => moduleInfo.ModuleName == moduleName);
            if (module != null)
            {
                var mappedPath = HostingEnvironment.MapPath("~/Modules");
                var moduleRelativePath = "~/Modules" + module.FullPhysicalPath.Replace(mappedPath, string.Empty).Replace("\\", "/");

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
            else
            {
                // idle
            }

            InitializeSecurity();
        }

        private static void InitializeRole(IRoleManagementService roleManagementService, Role role, IEnumerable<Permission> permissions)
        {
            var searchRoleResponse = roleManagementService.SearchRoles(
                new RoleSearchRequest
                {
                    Keyword = role.Name,
                });

            var predefinedPermissions = PredefinedPermissions.Permissions;
            var securityCallApi = PredefinedPermissions.SecurityCallApi;
            var permissionsQuery = predefinedPermissions.Where(permission => permission.Id == securityCallApi);

            var defaultRole = searchRoleResponse.Roles.FirstOrDefault() ?? role;
            var defaultPermissions = permissions ?? Enumerable.Empty<Permission>();

            defaultRole.Permissions = permissionsQuery.AsEnumerable().Concat(defaultPermissions).ToArray();
            roleManagementService.AddOrUpdateRole(defaultRole);
        }

        private void InitializeSecurity()
        {
            var roleManagementService = _container.Resolve<IRoleManagementService>();
            var securityService = _container.Resolve<ISecurityService>();

            var permissions = securityService.GetAllPermissions();
            var allPermissions = SecurityConstants.Permissions.AllPermissions;
            var filteredPermissions = permissions.Where(permission => allPermissions.Contains(permission.Id));

            InitializeRole(roleManagementService, SecurityConstants.JsShoppingCartUser, filteredPermissions);
        }
    }
}
