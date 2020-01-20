using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VirtoCommerce.Domain.Customer.Model;
using VirtoCommerce.Domain.Customer.Services;
using VirtoCommerce.JavaScriptShoppingCart.Core.Model.Security;
using VirtoCommerce.Platform.Core.Security;

namespace VirtoCommerce.JavaScriptShoppingCart.Web.Controllers.Api
{
    [RoutePrefix("jscart/api/security")]
    [AllowAnonymous]
    public class JsCartSecurityController : ApiController
    {
        private readonly ISecurityService _securityService;
        private readonly IPasswordCheckService _passwordCheckService;
        private readonly IRoleManagementService _roleManagementService;
        private readonly IMemberService _memberService;

        public JsCartSecurityController(ISecurityService securityService, IPasswordCheckService passwordCheckService, IRoleManagementService roleManagementService, IMemberService memberService)
        {
            _securityService = securityService;
            _passwordCheckService = passwordCheckService;
            _roleManagementService = roleManagementService;
            _memberService = memberService;
        }

        [HttpPost]
        [Route("registerUser")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> RegisterUser(ApplicationUserExtended user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var contact = new Contact
            {
                Name = user.UserName,
                Emails = new List<string>() { user.Email },
                FullName = user.UserName
            };

            _memberService.SaveChanges(new Member[] { contact });

            user.MemberId = contact.Id;

            var role = _roleManagementService.GetRole(SecurityConstants.JsShoppingCartUser.Id);

            if (role == null)
            {
                throw new ArgumentNullException($"Role {SecurityConstants.JsShoppingCartUser.Name} not found");
            }

            user.Roles = new[] { role };

            var result = await _securityService.CreateAsync(user);

            return Content(result.Succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest, result);
        }

        /// <summary>
        /// Get current user details.
        /// </summary>
        [HttpGet]
        [Route("currentuser")]
        [ResponseType(typeof(ApplicationUserExtended))]
        public async Task<IHttpActionResult> GetCurrentUser()
        {

            var user = await _securityService.FindByNameAsync(User.Identity.Name, UserDetails.Full);
            return Ok(user);
        }

        [HttpPost]
        [Route("validatepassword")]
        [ResponseType(typeof(PasswordValidationResult))]
        public async Task<IHttpActionResult> ValidatePasswordAsync([FromBody] string password)
        {
            var result = await _passwordCheckService.ValidatePasswordAsync(password);
            return Ok(result);
        }
    }
}