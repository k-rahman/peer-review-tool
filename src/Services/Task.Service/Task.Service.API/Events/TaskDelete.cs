using System;

namespace Task.Service.API.Events
{
        // when Task is deleted before it is published, then it will be removed from
        // Task service only but not from work service, that might change later depending
        // on the business rules.
        public record TaskDeleted(int Id);
}