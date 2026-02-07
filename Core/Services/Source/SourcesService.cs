using Db;
using Db.Models;

namespace Core;

public sealed class SourcesService : ISourcesService
{
    private readonly IEnumerable<ISourceDataValidator> _validators;
    private readonly ISourcesRepository _repository;
    
    public SourcesService(
        ISourcesRepository repo,
        IEnumerable<ISourceDataValidator> validators
    )
    {
        _repository = repo;
        _validators = validators;
    }
    
    public async Task AddSourceAsync(ISourceData sourceData, CancellationToken cancel = default)
    {
        await ValidateSourceDataAsync(sourceData, cancel).ConfigureAwait(false);
        
        var existing = await _repository.GetAsync(sourceData.Id, cancel).ConfigureAwait(false);

        if (existing is not null)
        {
            throw new SourceAlreadyExistException();
        }
            
        var source = new Source
        {
            Url = sourceData.Url,
            Type = sourceData.Type,
        };
            
        await _repository.CreateAsync(source, cancel).ConfigureAwait(false);
    }

    public async Task<IEnumerable<ISourceData>> GetAllSourcesAsync(CancellationToken cancel = default)
    {
        var data = await _repository.GetManyAsync(cancel: cancel).ConfigureAwait(false);
        
        return data.Select(s => new SourceData
        {
            Id = s.Id,
            Url = s.Url,
            Type = s.Type,
        });
    }

    public async Task RemoveSourceAsync(ISourceData sourceUri, CancellationToken cancel = default)
    {
        await _repository.DeleteAsync(sourceUri.Id, cancel).ConfigureAwait(false);
    }
    
    private async Task ValidateSourceDataAsync(ISourceData sourceData, CancellationToken cancel = default)
    {
        var validator = _validators.FirstOrDefault(v => v.SupportedType == sourceData.Type);

        if (validator is null)
        {
            throw new NoValidatorForSourceDataException(sourceData.Type);
        }
        
        var isValid = await validator.IsValidAsync(sourceData, cancel).ConfigureAwait(false);

        if (!isValid)
        {
            throw new SourceUrlIsNotValidException();
        }
    }
}