using System;
using System.Threading.Tasks;
using AutoMapper;
using Workshop.Service.API.Domain.Models;
using Workshop.Service.API.Domain.Repositories;
using Workshop.Service.API.Domain.Services;
using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Services
{
	public class ParticipantService : IParticipantService
	{

		private readonly IMapper _mapper;
		private readonly IParticipantRepository _participantRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ParticipantService(IMapper mapper, IParticipantRepository participantRepository, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_participantRepository = participantRepository;
			_unitOfWork = unitOfWork;
		}

		public Participant GetByEmail(string email)
		{
			return _participantRepository.GetByEmail(email);
		}

		public async Task UpdateParticipant(SaveParticipantResource participant, string participantId)
		{
			var existingParticipant = _participantRepository.GetById(participantId);

			if (existingParticipant != null)
			{
				_mapper.Map<SaveParticipantResource, Participant>(participant, existingParticipant);

				try
				{
					await _unitOfWork.CompleteAsync();
				}
				catch (Exception e)
				{
					var exception = $"{e}";
				}
			}
		}
	}
}