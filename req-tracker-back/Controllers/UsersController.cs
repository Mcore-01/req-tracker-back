﻿using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using req_tracker_back.Services;
using req_tracker_back.ViewModels;
using LoginRequest = req_tracker_back.RequestModels.LoginRequest;

namespace req_tracker_back.Controllers
{
    [Route("RT/[controller]")]
    [ApiController]
    public class UsersController(UsersService userService) : ControllerBase
    {
        private readonly UsersService _userService = userService;
        [HttpGet]
        public ActionResult<IEnumerable<ViewUserDTO>> GetAll([FromQuery] string? filter)
        {
            try
            {
                return Ok(_userService.GetAll(filter));
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
