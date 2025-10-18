﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAspNetUdemy.Data.VO;
using RestWithASPNETUdemy.Business;

namespace RestAspNetUdemy.Controllers
{
	[ApiVersion("1")]
	[ApiController]
	[Route("api/[controller]/v{version:apiVersion}")]

	public class AuthController : ControllerBase
	{
		private ILoginBusiness _loginBusiness;

		public AuthController(ILoginBusiness loginBusiness)
		{
			_loginBusiness = loginBusiness;
		}

		[HttpPost]
		[Route("signin")]
		public IActionResult Signin([FromBody] UserVO user)
		{
			if (user == null) return BadRequest("Invalid client request");
			var token = _loginBusiness.ValidateCredentials(user);

			if (token == null) return Unauthorized();
			return Ok(token);
		}
	}
}
