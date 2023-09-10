﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Sendeo.OnlineShop.Product.Api.Filters.Authentication
{
	public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		readonly Domain.Services.IAuthenticationService _authenticationService;

		public BasicAuthenticationHandler(Domain.Services.IAuthenticationService authenticationService, IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
		{
			_authenticationService = authenticationService;
		}

		protected override async Task<AuthenticateResult> HandleAuthenticateAsync() 
		{ 
			string username = string.Empty;
			
			try 
			{ 
				var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]); 
				
				var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
				
				username = credentials.FirstOrDefault(); 
				var password = credentials.LastOrDefault(); 
				
				if (!_authenticationService.ValidateCredentials(username, password)) throw new ArgumentException("Invalid credentials"); 
			}
			catch (Exception ex)
			{
				return AuthenticateResult.Fail($"Authentication failed: {ex.Message}");
			} 
			
			var claims = new[] { new Claim(ClaimTypes.Name, username) };
			
			var identity = new ClaimsIdentity(claims, Scheme.Name); 
			
			var principal = new ClaimsPrincipal(identity); 
			
			var ticket = new AuthenticationTicket(principal, Scheme.Name); 
			
			return AuthenticateResult.Success(ticket); }
	}
}
