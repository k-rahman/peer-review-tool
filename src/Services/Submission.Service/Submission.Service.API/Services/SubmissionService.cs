using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Submission.Service.API.Domain.Repositories;
using Submission.Service.API.Domain.Services;
using Submission.Service.API.Domain.Services.Communication;
using Submission.Service.API.Resources;

namespace Submission.Service.API.Services
{
        public class SubmissionService : ISubmissionService
        {
                private readonly IMapper _mapper;
                private readonly ISubmissionRepository _submissionRepository;
                private readonly IUnitOfWork _unitOfWork;

                public SubmissionService(IMapper mapper, ISubmissionRepository submissionRepository, IUnitOfWork unitOfWork)
                {
                        _mapper = mapper;
                        _submissionRepository = submissionRepository;
                        _unitOfWork = unitOfWork;
                }

                public async Task<SubmissionResource> GetAuthorSubmissionByWorkshopUidAsync(Guid taskUid, string authorId)
                {
                        var result = await _submissionRepository.GetAuthorSubmissionByWorkshopUidAsync(taskUid, authorId);
                        var submissions = _mapper.Map<Domain.Models.Submission, SubmissionResource>(result);
                        return submissions;
                }

                public async Task<SubmissionResource> GetByIdAsync(int id)
                {
                        var result = await _submissionRepository.GetByIdAsync(id);
                        return _mapper.Map<Domain.Models.Submission, SubmissionResource>(result);
                }

                public async Task<SubmissionResponse> UpdateAsync(int id, SaveSubmissionResource submission)
                {
                        var existingSubmission = await _submissionRepository.GetByIdAsync(id);

                        if (existingSubmission == null)
                                return new SubmissionResponse($"Submission with Id {id} was not found.");

                        _mapper.Map<SaveSubmissionResource, Domain.Models.Submission>(submission, existingSubmission);

                        if (existingSubmission.Submitted == null) // first time to upload content
                                existingSubmission.Submitted = DateTimeOffset.Now;
                        else
                                existingSubmission.Modified = DateTimeOffset.Now;

                        try
                        {
                                await _unitOfWork.CompleteAsync();

                                var updatedSubmission = _mapper.Map<Domain.Models.Submission, SubmissionResource>(existingSubmission);

                                return new SubmissionResponse(updatedSubmission);
                        }
                        catch (Exception ex)
                        {
                                // Do some logging stuff
                                return new SubmissionResponse($"An error occurred when updating the submission: {ex.Message}");
                        }
                }
        }
}