using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Newtonsoft.Json;
using ProfileService.Contracts.Person;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Contracts.Person.Connections;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Contracts.Person.Skills;
using ProfileService.Models.Common;
using ProfileService.Models.Person;
using ProfileService.Repositories.Interfaces;
using ProfileService.Services.Interfaces;

namespace ProfileService.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _repository;
        private readonly ILookupInterestRepository _interestRepository;
        private readonly ILookupCategoryRepository _categoryRepository;
        private readonly ILookupSkillRepository _skillRepository;
        private readonly ILookupSchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository repository, IMapper mapper, ILookupInterestRepository interestRepository, ILookupCategoryRepository categoryRepository, ILookupSkillRepository skillRepository, ILookupSchoolRepository schoolRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _interestRepository = interestRepository;
            _categoryRepository = categoryRepository;
            _skillRepository = skillRepository;
            _schoolRepository = schoolRepository;
        }

        public async Task<SearchPersonResponse> SearchAsync(SearchPersonRequest request)
        {
            return await _repository.SearchAsync(request);
        }

        public async Task<GetPerson> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);

            // if (result == null) return new GetPerson();

            var person = _mapper.Map<GetPerson>(result);

            if (person != null)
            {
                person.Gender = result.Gender switch
                {
                    Gender.Female => "female",
                    Gender.Male => "male",
                    _ => "other"
                };
            }

            return person;
        }

        public async Task InsertAsync(NewPerson newPerson)
        {
            var person = new Person
            {
                Id = Guid.Parse(newPerson.UserId),
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
                UserId = Guid.Parse(newPerson.UserId)
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
        
        public async Task UpdateCoverPhotoAsync(UpdatePerson updatePerson)
        {
            var person = await _repository.GetByIdAsync(updatePerson.UserId);
            person.CoverPhoto = updatePerson.CoverPhoto;
            try
            {
                await _repository.UpdateAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAvatarAsync(UpdatePerson updatePerson)
        {
            var person = await _repository.GetByIdAsync(updatePerson.UserId);
            person.Avatar = updatePerson.Avatar;
            try
            {
                await _repository.UpdateAsync(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAsync(UpdatePerson updatePerson)
        {
            var person = await _repository.GetByIdAsync(updatePerson.Id);
            person.Bio = updatePerson.Bio;
            person.DateOfBirth = updatePerson.DateOfBirth;
            person.Gender = updatePerson.Gender switch
            {
                "male" => Gender.Male,
                "female" => Gender.Female,
                _ => Gender.Other
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
                var schoolId = award.School.Id;

                if (!schoolId.HasValue)
                {
                    schoolId = Guid.NewGuid();
                    await _schoolRepository.InsertAsync(new School
                    {
                        Id = schoolId.Value,
                        Name = award.School.Name
                    });
                }
                
                var model = _mapper.Map<PersonAward>(award);
                model.InstituteId = schoolId.Value;
                
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

        public async Task<NewPersonInterest> AddInterestAsync(NewPersonInterest model)
        {

            try
            {
                foreach (var interest in model.Interests)
                {
                    var interestId = interest.Id;
                    if (!interest.Id.HasValue)
                    {
                        interestId = Guid.NewGuid();
                        
                        await _interestRepository.InsertAsync(new Interest
                        {
                            Category = interest.Name,
                            Id = interestId.Value
                        });
                    }

                    if (interestId.HasValue)
                    {
                        await _repository.AddInterestAsync(new PersonInterest
                        {
                            PersonId = model.PersonId,
                            InterestId = interestId.Value
                        });
                    }
                }

                return model;

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

        public async Task DeleteInterestAsync(Guid interestId, Guid personId)
        {
            try
            {
                await _repository.DeleteInterestAsync(interestId, personId);
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

        public async Task<NewPersonSkill> AddSkillAsync(NewPersonSkill model)
        {
            try
            {
                var result = new NewPersonSkill
                {
                    PersonId = model.PersonId,
                    Skills = model.Skills.Where(q => q.Id.HasValue).ToList()
                };
                
                foreach (var skill in model.Skills)
                {
                    var skillId = skill.Id;
                    if (!skill.Id.HasValue)
                    {
                        skillId = Guid.NewGuid();

                        var toAdd = new Skill
                        {
                            Name = skill.Name,
                            Id = skillId.Value
                        };
                        await _skillRepository.InsertAsync(toAdd);
                        result.Skills.Add(new SkillViewModel
                        {
                            Id = toAdd.Id,
                            Name = toAdd.Name
                        });
                    }

                    if (skillId.HasValue)
                    {
                        await _repository.AddSkillAsync(new PersonSkill
                        {
                            PersonId = model.PersonId,
                            SkillId = skillId
                        });
                    }
                    
                }

                return result;

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

        public async Task DeleteSkillAsync(Guid skillId, Guid personId)
        {
            try
            {
                await _repository.DeleteSkillAsync(skillId, personId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPersonConnection>> GetConnectionsAsync(Guid personId)
        {
            var connections = await _repository.GetConnectionsAsync(personId);
            return _mapper.Map<IEnumerable<GetPersonConnection>>(connections);
        }

        public async Task<NewPersonConnection> AddConnectionAsync(NewPersonConnection connection)
        {
            try
            {
                var model = _mapper.Map<PersonConnection>(connection);
                await _repository.AddConnectionAsync(model);

                return _mapper.Map<NewPersonConnection>(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateConnectionAsync(UpdatePersonConnection connection)
        {
            try
            {
                var model = _mapper.Map<PersonConnection>(connection);
                await _repository.UpdateConnectionAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteConnectionAsync(Guid connectionId)
        {
            try
            {
                await _repository.DeleteConnectionAsync(connectionId);
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

        public async Task AddCategoryAsync(NewPersonCategory model)
        {
            try
            {
                foreach (var category in model.Categories)
                {
                    var categoryId = category.Id;
                    if (!category.Id.HasValue)
                    {
                        categoryId = Guid.NewGuid();
                        
                        await _categoryRepository.InsertAsync(new Category
                        {
                            Name = category.Name,
                            Id = categoryId.Value
                        });
                    }

                    if (categoryId.HasValue)
                    {
                        await _repository.AddCategoryAsync(new PersonCategory
                        {
                            PersonId = model.PersonId,
                            CategoryId = categoryId.Value
                        });
                    }
                    
                    
                }
                
                
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

        public async Task DeleteCategoryAsync(Guid categoryId, Guid personId)
        {
            try
            {
                await _repository.DeleteCategoryAsync(categoryId, personId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }
    }
}