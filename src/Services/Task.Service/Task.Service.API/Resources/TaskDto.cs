using System;

namespace Task.Service.API.Resources
{
  public record TaskDto(
  Guid Id,
  string Name,
  string Description,
  string link,
  DateTimeOffset publish,
  DateTimeOffset submissionStart,
  DateTimeOffset submissionEnd,
  DateTimeOffset reviewStart,
  DateTimeOffset reviewEnd,
  DateTimeOffset createdAt,
  int instructor_id
  );
}