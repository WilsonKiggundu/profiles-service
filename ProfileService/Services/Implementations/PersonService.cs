using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ProfileService.Contracts.Person;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Contracts.Person.Skills;
using ProfileService.Models.Person;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ICollection<GetPerson>> SearchAsync(Guid? exclude = null)
        {
            var results = await _repository.SearchAsync(exclude);
            return _mapper.Map<ICollection<GetPerson>>(results);
        }

        public async Task<GetPerson> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            // if (result == null) return new GetPerson();
            
            var person = _mapper.Map<GetPerson>(result);
            // person.Gender = result.Gender switch
            // {
            //     Gender.Female => "female",
            //     Gender.Male => "male",
            //     _ => "other"
            // };
            
            return person;


        }

        public async Task InsertAsync(NewPerson newPerson)
        {
            var person = new Person
            {
                Id = newPerson.UserId,
                Firstname = newPerson.FirstName,
                Lastname = newPerson.LastName,
                Bio = newPerson.Bio,
                Gender = newPerson.Gender switch    
                {
                    "male" => Gender.Male,
                    "female" => Gender.Female,
                    //"other" => Gender.Other,
                    _ => Gender.Female
                },
                DateOfBirth = newPerson.DateOfBirth,
                UserId = newPerson.UserId
            };
            
            try
            {
                await _repository.InsertAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdatePerson updatePerson)
        {
            var person = new Person
            {
                Id = updatePerson.Id,
                Firstname = updatePerson.FirstName,
                Lastname = updatePerson.LastName,
                Bio = updatePerson.Bio,
                Gender = updatePerson.Gender switch    
                {
                    "male" => Gender.Male,
                    "female" => Gender.Female,
                    _ => Gender.Female
                },
                DateOfBirth = updatePerson.DateOfBirth,
                UserId = updatePerson.UserId
            };
            try
            {
                await _repository.UpdateAsync(person);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                await _repository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPersonAward>> GetAwardsAsync(Guid personId)
        {
            var awards = await _repository.GetAwardsAsync(personId);
            return _mapper.Map<IEnumerable<GetPersonAward>>(awards);
        }

        public async Task AddAwardAsync(NewPersonAward award)
        {
            try
            {
                var model = _mapper.Map<PersonAward>(award);
                await _repository.AddAwardAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAwardAsync(UpdatePersonAward award)
        {
            try
            {
                var model = _mapper.Map<PersonAward>(award);
                await _repository.UpdateAwardAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAwardAsync(Guid awardId)
        {
            try
            {
                await _repository.DeleteAwardAsync(awardId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<ICollection<GetPersonInterest>> GetInterestsAsync(Guid personId)
        {
            var interests = await _repository.GetInterestsAsync(personId);
            return _mapper.Map<ICollection<GetPersonInterest>>(interests);
        }

        public async Task AddInterestAsync(NewPersonInterest interest)
        {
            try
            {
                var model = _mapper.Map<PersonInterest>(interest);
                await _repository.AddInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateInterestAsync(UpdatePersonInterest interest)
        {
            try
            {
                var model = _mapper.Map<PersonInterest>(interest);
                await _repository.UpdateInterestAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteInterestAsync(Guid interestId)
        {
            try
            {
                await _repository.DeleteInterestAsync(interestId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPersonSkill>> GetSkillsAsync(Guid personId)
        {
            var skills = await _repository.GetSkillsAsync(personId);
            return _mapper.Map<IEnumerable<GetPersonSkill>>(skills);
        }

        public async Task AddSkillAsync(NewPersonSkill skill)
        {
            try
            {
                var model = _mapper.Map<PersonSkill>(skill);
                await _repository.AddSkillAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateSkillAsync(UpdatePersonSkill skill)
        {
            try
            {
                var model = _mapper.Map<PersonSkill>(skill);
                await _repository.UpdateSkillAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteSkillAsync(Guid skillId)
        {
            try
            {
                await _repository.DeleteSkillAsync(skillId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPersonCategory>> GetCategoriesAsync(Guid personId)
        {
            var categories = await _repository.GetCategoriesAsync(personId);
            return _mapper.Map<IEnumerable<GetPersonCategory>>(categories);
        }

        public async Task AddCategoryAsync(NewPersonCategory award)
        {
            try
            {
                var model = _mapper.Map<PersonCategory>(award);
                await _repository.AddCategoryAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateCategoryAsync(UpdatePersonCategory award)
        {
            try
            {
                var model = _mapper.Map<PersonCategory>(award);
                await _repository.UpdateCategoryAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteCategoryAsync(Guid awardId)
        {
            try
            {
                await _repository.DeleteCategoryAsync(awardId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}