using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProfileService.Contracts.Lookup.Category;
using ProfileService.Contracts.Person;
using ProfileService.Contracts.Person.Awards;
using ProfileService.Contracts.Person.Categories;
using ProfileService.Contracts.Person.Connections;
using ProfileService.Contracts.Person.Contact;
using ProfileService.Contracts.Person.Interests;
using ProfileService.Contracts.Person.Skills;
using ProfileService.Helpers;
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
        private readonly ILogger<PersonService> _logger;
        private readonly IContactRepository _contactRepository;
        private readonly IDeviceService _deviceService;
        private readonly IWebNotification _notification;
        private readonly IConfiguration _configuration;

        public PersonService(IPersonRepository repository, IMapper mapper, ILookupInterestRepository interestRepository,
            ILookupCategoryRepository categoryRepository, ILookupSkillRepository skillRepository,
            ILookupSchoolRepository schoolRepository, ILogger<PersonService> logger, IContactRepository contactRepository, IDeviceService deviceService, IWebNotification notification, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _interestRepository = interestRepository;
            _categoryRepository = categoryRepository;
            _skillRepository = skillRepository;
            _schoolRepository = schoolRepository;
            _logger = logger;
            _contactRepository = contactRepository;
            _deviceService = deviceService;
            _notification = notification;
            _configuration = configuration;
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

        public async Task<NewPerson> InsertAsync(NewPerson newPerson)
        {
            var person = new Person
            {
                Id = Guid.Parse(newPerson.UserId),
                Firstname = newPerson.FirstName,
                Lastname = newPerson.LastName,
                Bio = newPerson.Bio,
                Avatar = newPerson.Avatar,
                CoverPhoto = newPerson.CoverPhoto,
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
                
                var devices 
                    = await _deviceService.SearchAsync(person.Id.ToString());
                
                _notification.Send(devices, new NotificationPayload
                {
                    Title = $"{person.Firstname} {person.Lastname}" + " joined My Village",
                    Icon = person.Avatar,
                    Date = DateTime.UtcNow,
                    
                    Data = new
                    {
                        profileId = person.Id,
                        baseUrl = _configuration.GetSection("MyVillageBaseUrl").Get<string>()
                    },
                    
                    Options = new NotificationOptions
                    {
                        Actions = new List<NotificationAction>
                        {
                            new NotificationAction
                            {
                                Action = "view-profile",
                                Title = "View profile"
                            }
                        },
                        
                        // Body = comment.Details,
                        Tag = person.Id.ToString(),
                        Icon = person.Avatar,
                    }
                });
                
                return _mapper.Map<NewPerson>(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<UpdatePerson> UpdateCoverPhotoAsync(UpdatePerson updatePerson)
        {
            var person = await _repository.GetByIdAsync(updatePerson.UserId);
            person.CoverPhoto = updatePerson.CoverPhoto;
            try
            {
                await _repository.UpdateAsync(person);
                return _mapper.Map<UpdatePerson>(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<UpdatePerson> UpdateAvatarAsync(UpdatePerson updatePerson)
        {
            var person = await _repository.GetByIdAsync(updatePerson.UserId);
            person.Avatar = updatePerson.Avatar;
            try
            {
                await _repository.UpdateAsync(person);
                return _mapper.Map<UpdatePerson>(person);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<GetPersonContact>> GetContactsAsync(Guid personId)
        {
            var contacts = await _repository.GetContactsAsync(personId);
            return _mapper.Map<IEnumerable<GetPersonContact>>(contacts);
        }

        public async Task<PersonContact> AddContactAsync(NewPersonContact contact)
        {
            try
            {
                var contactId = Guid.NewGuid();
                
                var personContact = new PersonContact
                {
                    PersonId = contact.BelongsTo,
                    ContactId = contactId,
                    Contact = new Contact
                    {
                        Id = contactId,
                        Category = contact.Category,
                        Details = contact.Details,
                        Type = contact.Type,
                        Value = contact.Value,
                        BelongsTo = contact.BelongsTo,
                    }
                };

                await _repository.AddContactAsync(personContact);
                
                return personContact;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<PersonContact> UpdateContactAsync(UpdatePersonContact contact)
        {
            try
            {
                _logger.LogInformation(JsonConvert.SerializeObject(contact, Formatting.Indented));
                
                var contactEntity = await _contactRepository.GetByIdAsync(contact.Id);

                contactEntity.Category = contact.Category;
                contactEntity.Details = contact.Details;
                contactEntity.Type = contact.Type;
                // contactEntity.BelongsTo = contact.BelongsTo;
                contactEntity.Value = contact.Value;
                
                await _contactRepository.UpdateAsync(contactEntity);
                
                return new PersonContact
                {
                    PersonId = contact.BelongsTo,
                    ContactId = contact.Id,
                    Contact = new Contact
                    {
                        Category = contact.Category,
                        Details = contact.Details,
                        Id = contact.Id,
                        Type = contact.Type,
                        Value = contact.Value,
                        BelongsTo = contact.BelongsTo
                    }
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteContactAsync(Guid contactId, Guid belongsTo)
        {
            try
            {
                await _repository.DeleteContactAsync(contactId, belongsTo);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task<IEnumerable<PersonStack>> GetStackAsync(Guid personId)
        {
            return await _repository.GetStackAsync(personId);
        }

        public async Task AddStackAsync(PersonStack stack)
        {
            await _repository.AddStackAsync(stack);
        }

        public async Task UpdateStackAsync(PersonStack stack)
        {
            await _repository.UpdateStackAsync(stack);
        }

        public async Task DeleteStackAsync(Guid stackId, Guid personId)
        {
            await _repository.DeleteStackAsync(stackId, personId);
        }

        public async Task<IEnumerable<PersonProject>> GetProjectsAsync(Guid personId)
        {
            return await _repository.GetProjectsAsync(personId);
        }

        public async Task AddProjectAsync(PersonProject stack)
        {
            await _repository.AddProjectAsync(stack);
        }

        public async Task UpdateProjectAsync(PersonProject stack)
        {
            await _repository.UpdateProjectAsync(stack);
        }

        public async Task DeleteProjectAsync(Guid projectId, Guid personId)
        {
            await _repository.DeleteProjectAsync(projectId, personId);
        }

        public async Task<IEnumerable<PersonEmployment>> GetEmploymentAsync(Guid personId)
        {
            return await _repository.GetEmploymentAsync(personId);
        }

        public async Task AddEmploymentAsync(PersonEmployment stack)
        {
            await _repository.AddEmploymentAsync(stack);
        }

        public async Task UpdateEmploymentAsync(PersonEmployment stack)
        {
            await _repository.UpdateEmploymentAsync(stack);
        }

        public async Task DeleteEmploymentAsync(Guid projectId, Guid personId)
        {
            await _repository.DeleteEmploymentAsync(projectId, personId);
        }

        public async Task<UpdatePerson> UpdateAsync(UpdatePerson updatePerson)
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
            person.Avatar = updatePerson.Avatar;
            person.CoverPhoto = updatePerson.CoverPhoto;

            try
            {
                await _repository.UpdateAsync(person);
                return _mapper.Map<UpdatePerson>(person);
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
                var instituteId = award.Institute.Id;

                if (!instituteId.HasValue)
                {
                    instituteId = Guid.NewGuid();
                    await _schoolRepository.InsertAsync(new School
                    {
                        Id = instituteId.Value,
                        Name = award.Institute.Name
                    });
                }

                var model = new PersonAward
                {
                    InstituteId = instituteId.Value,
                    Activities = award.Activities,
                    Description = award.Description,
                    Grade = award.Grade,
                    FieldOfStudy = award.FieldOfStudy,
                    StartYear = award.StartYear,
                    Title = award.Title,
                    EndYear = award.EndYear,
                    PersonId = award.PersonId
                };

                await _repository.AddAwardAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task UpdateAwardAsync(UpdatePersonAward award)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(award, Formatting.Indented));

            try
            {
                var instituteId = award.Institute.Id;

                if (!instituteId.HasValue)
                {
                    instituteId = Guid.NewGuid();
                    await _schoolRepository.InsertAsync(new School
                    {
                        Id = instituteId.Value,
                        Name = award.Institute.Name
                    });
                }

                var model = _mapper.Map<PersonAward>(award);
                model.InstituteId = instituteId;
                await _repository.UpdateAwardAsync(model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public async Task DeleteAwardAsync(Guid awardId, Guid personId)
        {
            try
            {
                await _repository.DeleteAwardAsync(awardId, personId);
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

        public async Task<PersonInterest> AddInterestAsync(InterestViewModel interest, Guid personId)
        {
            try
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

                if (!interestId.HasValue) throw new ArgumentNullException(nameof(interestId));

                var personInterest = new PersonInterest
                {
                    PersonId = personId,
                    InterestId = interestId.Value
                };

                return await _repository.AddInterestAsync(personInterest);
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
    
        #region Person Terms

        public async Task<FreelanceTerms> GetFreelanceTermsAsync(Guid personId)
        {
            // 1065694950
            return await _repository.GetFreelanceTermsAsync(personId);
        }

        public async Task AddFreelanceTermsAsync(FreelanceTerms terms)
        {
            await _repository.AddFreelanceTermsAsync(terms);
        }

        public async Task UpdateFreelanceTermsAsync(FreelanceTerms terms)
        {
            await _repository.UpdateFreelanceTermsAsync(terms);
        }

        public async Task DeleteFreelanceTermsAsync(Guid personId)
        {
            await _repository.DeleteFreelanceTermsAsync(personId);
        }

        #endregion

        public async Task<IEnumerable<GetPersonSkill>> GetSkillsAsync(Guid personId)
        {
            var skills = await _repository.GetSkillsAsync(personId);
            return _mapper.Map<IEnumerable<GetPersonSkill>>(skills);
        }

        public async Task<PersonSkill> AddSkillAsync(SkillViewModel skill, Guid personId)
        {
            try
            {
                // var interestId = interest.Id;
                // if (!interest.Id.HasValue)
                // {
                //     interestId = Guid.NewGuid();
                //
                //     await _interestRepository.InsertAsync(new Interest
                //     {
                //         Category = interest.Name,
                //         Id = interestId.Value
                //     });
                // }
                //
                // if (!interestId.HasValue) throw new ArgumentNullException(nameof(interestId));
                //
                // var personInterest = new PersonInterest
                // {
                //     PersonId = personId,
                //     InterestId = interestId.Value
                // };
                //
                // return await _repository.AddInterestAsync(personInterest);
                
                var skillId = skill.Id;
                if (!skill.Id.HasValue)
                {
                    skillId = Guid.NewGuid();
                    
                    await _skillRepository.InsertAsync(new Skill
                    {
                        Id = skillId.Value,
                        Name = skill.Name
                    });
                }
                
                if (!skillId.HasValue) throw new ArgumentNullException(nameof(skillId));
                
                return await _repository.AddSkillAsync(new PersonSkill
                {
                    PersonId = personId,
                    SkillId = skillId
                });
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

        public async Task<GetLookupCategory> AddCategoryAsync(CategoryViewModel category, Guid personId)
        {
            try
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
                        PersonId = personId,
                        CategoryId = categoryId.Value
                    });
                }

                return new GetLookupCategory
                {
                    Id = Guid.Parse(categoryId.ToString()),
                    Name = category.Name
                };
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