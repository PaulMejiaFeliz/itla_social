using AutoMapper;
using ItlaSocial.Models;
using ItlaSocial.Models.ApplicationViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItlaSocial.Controllers.Api
{
    [Route("api/publications")]
    [Authorize]
    public class PublicationController: Controller
    {
        private IDbRepository _repository;

        public PublicationController(IDbRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var publications = await _repository.GetPublicationsAsync(User);

                var result = Ok(Mapper.Map<IEnumerable<PublicationViewModel>>(publications));

                return result;
            }
            catch (Exception e)
            {
                
            }
            return BadRequest("Failed to get the publications");
        }

    }
}
