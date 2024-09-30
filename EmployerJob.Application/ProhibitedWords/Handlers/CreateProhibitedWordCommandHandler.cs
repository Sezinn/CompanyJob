using EmployerJob.Application.Common.Models.BaseModels;
using EmployerJob.Application.ProhibitedWords.Commands;
using EmployerJob.Application.Redis;
using EmployerJob.Domain.Entities;
using EmployerJob.Infrastructure.Persistence.Repositories;
using MediatR;

namespace EmployerJob.Application.ProhibitedWords.Handlers
{
    public class CreateProhibitedWordCommandHandler : IRequestHandler<CreateProhibitedWordCommand, BoolRef>
    {
        private readonly IProhibitedWordRepository _prohibitedWordRepository;
        private readonly IRedisContext redisContext;
        public CreateProhibitedWordCommandHandler(IProhibitedWordRepository prohibitedWordRepository, IRedisContext redisContext)
        {
            _prohibitedWordRepository = prohibitedWordRepository;
            this.redisContext = redisContext;
        }

        public async Task<BoolRef> Handle(CreateProhibitedWordCommand request, CancellationToken cancellationToken)
        {
            var existingProhibitedWord = _prohibitedWordRepository.FindAsync(x => x.Word == request.Word).Result.FirstOrDefault();
            if (existingProhibitedWord != null)
            {
                throw new ArgumentException("Bu kelime zaten var.");
            }

            var company = new ProhibitedWord
            {
                Word = request.Word
            };

            await _prohibitedWordRepository.AddAsync(company);
            var result = await _prohibitedWordRepository.SaveChangesAsync();

            await redisContext.Remove(1, "ProhibitionWords");

            var prohibitedWords = _prohibitedWordRepository.GetAllAsync().Result.Select(pw => pw.Word).ToList();

            if (prohibitedWords.Any())
            {
                await redisContext.SaveAsync(1, "ProhibitionWords", prohibitedWords, TimeSpan.FromSeconds(2592000));
            }

            return result ? new BoolRef(true) : throw new Exception("İşveren kaydedilemedi."); ;
        }
    }
}
