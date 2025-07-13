using Strongly;

namespace StudentProgressTracker.Shared.Ids;

[Strongly(
    backingType: StronglyType.Guid,
    converters: StronglyConverter.EfValueConverter
        | StronglyConverter.SystemTextJson
        | StronglyConverter.SwaggerSchemaFilter
        | StronglyConverter.TypeConverter
)]
public partial struct CourseId;
