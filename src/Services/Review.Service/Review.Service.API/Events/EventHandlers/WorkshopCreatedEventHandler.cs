using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using Review.Service.API.Domain.Models;
using Review.Service.API.Domain.Repositories;
using Workshop.Service.Contracts;

namespace Review.Service.API.Events.EventHandlers
{
        public class WorkshopCreatedEventHandler : IConsumer<WorkshopCreated>
        {
                private readonly ICriterionRepository _criterionRepository;
                private readonly IMapper _mapper;
                private readonly IUnitOfWork _unitOfWork;

                public WorkshopCreatedEventHandler(ICriterionRepository criterionRepository, IMapper mapper, IUnitOfWork unitOfWork)
                {
                        _criterionRepository = criterionRepository;
                        _mapper = mapper;
                        _unitOfWork = unitOfWork;
                }

                public async Task Consume(ConsumeContext<WorkshopCreated> context)
                {
                        var message = context.Message;
                        var criteria = _mapper.Map<IList<Criterion>>(message.Criteria);

                        var existingCriteria = new List<Criterion>();

                        foreach (var criterion in criteria)
                        {
                                criterion.WorkshopUid = message.Uid;

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