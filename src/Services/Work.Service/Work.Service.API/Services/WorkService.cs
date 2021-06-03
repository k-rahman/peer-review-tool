using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Work.Service.API.Domain.Repositories;
using Work.Service.API.Domain.Services;
using Work.Service.API.Domain.Services.Communication;
using Work.Service.API.Resources;

namespace Work.Service.API.Services
{
        public class WorkService : IWorkService
        {
                private readonly IMapper _mapper;
                private readonly IWorkRepository _workRepository;
                private readonly IUnitOfWork _unitOfWork;

                public WorkService(IMapper mapper, IWorkRepository workRepository, IUnitOfWork unitOfWork)
                {
                        _mapper = mapper;
                        _workRepository = workRepository;
                        _unitOfWork = unitOfWork;
                }
                public async Task<WorkResource> GetAuthorWorkByTaskAsync(Guid taskUid, int authorId)
                {
                        var result = await _workRepository.GetAuthorWorkByTaskAsync(taskUid, authorId);
                        var works = _mapper.Map<Domain.Models.Work, WorkResource>(result);
                        return works;
                }

                public async Task<WorkResource> GetByIdAsync(int id)
                {
                        var result = await _workRepository.GetByIdAsync(id);
                        return _mapper.Map<Domain.Models.Work, WorkResource>(result);
                }

                public async Task<WorkResponse> UpdateAsync(int id, SaveWorkResource work)
                {
                        var existingWork = await _workRepository.GetByIdAsync(id);

                        if (existingWork == null)
                                return new WorkResponse($"Work with Id {id} was not found.");

                        _mapper.Map<SaveWorkResource, Domain.Models.Work>(work, existingWork);

                        if (existingWork.Submitted == null) // first time to upload content
                                existingWork.Submitted = DateTimeOffset.Now;
                        else
                                existingWork.Modified = DateTimeOffset.Now;

                        try
                        {
                                await _unitOfWork.CompleteAsync();

                                var updatedWork = _mapper.Map<Domain.Models.Work, WorkResource>(existingWork);

                                return new WorkResponse(updatedWork);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new WorkResponse($"An error occurred when updating the work: {ex.Message}");
                        }
                }
        }
}