using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Get the list of all persons - User authentication is required
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetList()
        {
            var result = _personService.GetList();

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Get a list of the persons from specified city - User authentication is required
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        [HttpGet("getlistbycity")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetListByCity(String city)
        {
            var result = _personService.GetListByCity(city);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Get the person information by its id - User authentication is required
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Get(int id)
        {
            var result = _personService.GetById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Add a new person to database - Admin authentication is required
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [Authorize(Roles = "Admin")]
        public IActionResult Add(Person person)
        {
            var result = _personService.Add(person);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Delete a person from database - Admin authentication is required
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(Person person)
        {
            var result = _personService.Delete(person);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }

        /// <summary>
        /// Update a person on database - Admin authentication is required
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [HttpPost("update")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(Person person)
        {
            var result = _personService.Update(person);

            if (result.Success)
            {
                return Ok(result.Message);
            }
            else
            {
                return BadRequest(result.Message);
            }
        }
    }
}
