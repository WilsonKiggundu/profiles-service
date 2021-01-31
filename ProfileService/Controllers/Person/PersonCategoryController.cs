using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Controllers.Common;
using ProfileService.Models.Common;
using ProfileService.Models.Person;
using ProfileService.Services.Interfaces;

namespace ProfileService.Controllers.Person
{
    /// <summary>
    /// PersonCategory controller
    /// </summary>
    [Route("api/person/categories")]
    public class PersonCategoryController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonCategoryController> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="personService"></param>
        public PersonCategoryController(IPersonService personService, ILogger<PersonCategoryController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        /// <summary>
        /// SEARCH persons
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<GetPersonCategory>> Get(Guid personId)
        {
            var categories = await _personService.GetCategoriesAsync(personId);
            _logger.LogInformation(JsonConvert.SerializeObject(categories));
            return categories;
        }

        /// <summary>
        /// CREATE a person category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ICollection<PersonCategory>> Create(AddPersonCategory model)
        {
            try
            {
                var response = new List<PersonCategory>();

                foreach (var category in model.Categories)
                {
                    var result 
                        = await _personService.AddCategoryAsync(category, model.PersonId);
                    
                    response.Add(new PersonCategory
                    {
                        
                        PersonId = model.PersonId,
                        CategoryId = result.Id,
                        Category = new Category
                        {
                            Id = result.Id,
                            Name = result.Name
                        }
                    });
                }
                
                return response;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
        
        /// <summary>
        /// UPDATE a person category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<UpdatePersonCategory> Update([FromBody] UpdatePersonCategory category)
        {
            try
            {
                await _personService.UpdateCategoryAsync(category);
                return category;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// DELETE a person category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="personId"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid categoryId, Guid personId)
        {
            try
            {
                await _personService.DeleteCategoryAsync(categoryId, personId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}