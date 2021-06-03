using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MassTransit;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Task.Service.Contracts;

namespace Review.Service.API.Events.EventHandlers
{
        public class TaskCreatedEventHandler : IConsumer<TaskCreated>
        {
                private readonly ICriterionRepository _criterionRepository;
                private readonly IMapper _mapper;
                private readonly IUnitOfWork _unitOfWork;

                public TaskCreatedEventHandler(ICriterionRepository criterionRepository, IMapper mapper, IUnitOfWork unitOfWork)
                {
                        _criterionRepository = criterionRepository;
                        _mapper = mapper;
                        _unitOfWork = unitOfWork;
                }

                public async System.Threading.Tasks.Task Consume(ConsumeContext<TaskCreated> context)
                {
                        var message = context.Message;
                        var criteria = _mapper.Map<IList<Criterion>>(message.Criteria);

                        var existingCriteria = new List<Criterion>();

                        foreach (var criterion in criteria)
                        {
                                criterion.TaskUid = message.Uid;

                                // check if works deadline entry already created before
                                var existingCriterion = await _criterionRepository.GetByIdAsync(criterion.Id);

                                if (existingCriterion != null)
                                        existingCriteria.Add(existingCriterion);
                        }

                        // if all criteria in the message exists in the database, do nothing
                        if (existingCriteria.Count == criteria.Count)
                                return;

                        // if some of the criteria exist, get the ones the don't exist and insert into database
                        if (existingCriteria.Count < criteria.Count && existingCriteria.Count > 0)
                        {
                                var newCriteria = criteria.Where(c => !existingCriteria.Contains(c));

                                foreach (var criterion in newCriteria)
                                {
                                        await _criterionRepository.InsertAsync(criterion);
                                }

                                await _unitOfWork.CompleteAsync();

                                return;
                        }

                        foreach (var criterion in criteria)
                        {
                                await _criterionRepository.InsertAsync(criterion);
                        }

                        await _unitOfWork.CompleteAsync();
                }
        }
}