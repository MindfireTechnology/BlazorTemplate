using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mindfire.User;
using System;
using System.Threading.Tasks;
using Api = WebApi.Models;

namespace WebApi.Controllers.UserController;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
	protected IMapper Mapper { get; }
	protected IUserDataService UserDataService { get; }

	public UserController(IMapper mapper, IUserDataService userDataService)
	{
		Mapper = mapper;
		UserDataService = userDataService;
	}

	[HttpGet("get/{username}")]
	public async Task<ActionResult<Api.User>> GetUser([FromRoute] string username)
	{
		try
		{
			var user = await UserDataService.GetUser(userName: username);

			// for demonstration purposes only
			if (user == null)
			{
				user = await UserDataService.GetUser(userName: "TestUser");
				user!.UserName = username;
			}

			return Mapper.Map<Api.User>(user);
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}

	[HttpPut("address/update")]
	public async Task<ActionResult> UpdateAddress([FromBody] Api.Address address)
	{
		try
		{
			await UserDataService.UpdateAddress(Mapper.Map<Address>(address));
			return Ok();
		}
		catch (Exception ex)
		{
			return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
		}
	}
}
