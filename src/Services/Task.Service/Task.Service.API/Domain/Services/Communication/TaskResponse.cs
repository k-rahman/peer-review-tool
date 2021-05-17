using Task.Service.API.Resources;

namespace Task.Service.API.Domain.Services.Communication
{
        public class TaskResponse : BaseResponse
        {
                public TaskResource Task { get; private set; }

                private TaskResponse(bool success, string message, TaskResource task) : base(success, message)
                {
                        Task = task;
                }

                /// <summary>
                /// Creates a success response.
                /// </summary>
                /// <param name="task">Saved task.</param>
                /// <returns>Response.</returns>
                public TaskResponse(TaskResource task) : this(true, string.Empty, task)
                { }

                /// <summary>
                /// Creates am error response.
                /// </summary>
                /// <param name="message">Error message.</param>
                /// <returns>Response.</returns>
                public TaskResponse(string message) : this(false, message, null)
                { }
        }
}