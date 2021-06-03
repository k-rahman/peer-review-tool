using AutoMapper;
using MassTransit;
using Work.Service.API.Domain.Models;
using Work.Service.API.Domain.Repositories;
using Task.Service.Contracts;
using System;

namespace Work.Service.API.Events.EventHandlers
{
        public class TaskCreatedEventHandler : IConsumer<TaskCreated>
        {
                private readonly IWorksDeadlineRepository _workRepository;
                private readonly IMapper _mapper;
                private readonly IUnitOfWork _unitOfWork;

                public TaskCreatedEventHandler(IWorksDeadlineRepository workRepository, IMapper mapper, IUnitOfWork unitOfWork)
                {
                        _workRepository = workRepository;
                        _mapper = mapper;
                        _unitOfWork = unitOfWork;
                }

                public async System.Threading.Tasks.Task Consume(ConsumeContext<TaskCreated> context)
                {
                        var message = context.Message;

                        // check if works deadline entry already created before
                        var existingDeadline = await _workRepository.GetByIdAsync(message.Id);

                        if (existingDeadline != null)
                                return;

                        var newDeadline = _mapper.Map<TaskCreated, WorksDeadline>(message);

                        // must be wrapped with try, catch block
                        await _workRepository.InsertAsync(newDeadline);
                        await _unitOfWork.CompleteAsync();
                }
        }
}