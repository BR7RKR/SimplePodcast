using Db.Models;

namespace Core;

public class NoValidatorForSourceDataException(SourceType type) : Exception($"No validator for {type} source type");