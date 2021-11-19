using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Core.Utilities.Cache;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private IPersonService _personService;
        private IRedisService _redisService;
        private bool redisConnection  = false;

        public PersonsController(IPersonService personService, IRedisService redisService)
        {
            _personService = personService;
            _redisService = redisService;
            redisConnection = _redisService.CheckConnection();
        }

        /// <summary>
        /// Get the list of all persons - User authentication is required
        /// </summary>
        /// <returns></returns>
        [HttpGet("getall")]
        //[Cached(600)]
        [Authorize(Roles = "User, Admin")]
        public IActionResult GetList()
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (redisConnection && _redisService.IsKeyExist("getall"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                var RedisResult = _redisService.GetList<List<Person>>("getall");

                watch.Stop();
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");

                return Ok(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            var result = _personService.GetList();

            watch.Stop();
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            if (result.Success)
            {
                if (redisConnection)
                {
                    _redisService.StoreList<List<Person>>("getall", result.Data, TimeSpan.MaxValue);
                }
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
        //[Cached(600)]
        [Authorize(Roles = "User,Admin")]
        public IActionResult GetListByCity(String city)
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (_redisService.IsKeyExist($"getlistbycity-{city}"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                var RedisResult = _redisService.GetList<List<Person>>($"getlistbycity-{city}");

                watch.Stop();
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");

                return Ok(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            var result = _personService.GetListByCity(city);

            watch.Stop();
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            if (result.Success)
            {
                _redisService.StoreList<List<Person>>($"getlistbycity-{city}", result.Data, TimeSpan.MaxValue);
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
        [Cached(600)]
        [Authorize(Roles = "User,Admin")]
        public IActionResult Get(int id)
        {
            var watch = new System.Diagnostics.Stopwatch();  // To calculate execution time

            if (_redisService.IsKeyExist($"getbyid-{id}"))
            {
                watch.Start();

                Console.WriteLine("Using Redis...");
                var RedisResult = _redisService.GetList<Person>($"getbyid-{id}");

                watch.Stop();
                Console.WriteLine($"Execution time with Redis: {watch.ElapsedMilliseconds} ms");

                return Ok(RedisResult);
            }

            watch.Start();

            Console.WriteLine("Not using Redis...");
            var result = _personService.GetById(id);

            watch.Stop();
            Console.WriteLine($"Execution time without Redis: {watch.ElapsedMilliseconds} ms");

            if (result.Success)
            {
                _redisService.StoreList<Person>($"getbyid-{id}", result.Data, TimeSpan.MaxValue);
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
