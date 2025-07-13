using Strongly;

namespace StudentProgressTracker.Shared.Ids;

[Strongly(
    backingType: StronglyType.String,
    converters: StronglyConverter.EfValueConverter
        | StronglyConverter.SystemTextJson
        | StronglyConverter.SwaggerSchemaFilter
        | StronglyConverter.TypeConverter
)]
public partial struct UserId;
