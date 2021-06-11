using Workshop.Service.API.Resources;

namespace Workshop.Service.API.Domain.Services.Communication
{
        public class WorkshopResponse : BaseResponse
        {
                public WorkshopResource Workshop { get; private set; }

                private WorkshopResponse(bool success, string message, WorkshopResource workshop) : base(success, message)
                {
                        Workshop = workshop;
                }

                /// <summary>
                /// Creates a success response.
                /// </summary>
                /// <param name="workshop">Saved workshop.</param>
                /// <returns>Response.</returns>
                public WorkshopResponse(WorkshopResource workshop) : this(true, string.Empty, workshop)
                { }

                /// <summary>
                /// Creates am error response.
                /// </summary>
                /// <param name="message">Error message.</param>
                /// <returns>Response.</returns>
                public WorkshopResponse(string message) : this(false, message, null)
                { }
        }
}