using Work.Service.API.Resources;

namespace Work.Service.API.Domain.Services.Communication
{
        public class WorkResponse : BaseResponse
        {
                public WorkResource Work { get; private set; }

                private WorkResponse(bool success, string message, WorkResource work) : base(success, message)
                {
                        Work = work;
                }

                /// <summary>
                /// Creates a success response.
                /// </summary>
                /// <param name="work">Saved work.</param>
                /// <returns>Response.</returns>
                public WorkResponse(WorkResource work) : this(true, string.Empty, work) { }

                /// <summary>
                /// Creates am error response.
                /// </summary>
                /// <param name="message">Error message.</param>
                /// <returns>Response.</returns>
                public WorkResponse(string message) : this(false, message, null) { }
        }
}