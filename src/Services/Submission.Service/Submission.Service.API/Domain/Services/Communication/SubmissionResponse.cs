using Submission.Service.API.Resources;

namespace Submission.Service.API.Domain.Services.Communication
{
        public class SubmissionResponse : BaseResponse
        {
                public SubmissionResource Submission { get; private set; }

                private SubmissionResponse(bool success, string message, SubmissionResource submission) : base(success, message)
                {
                        Submission = submission;
                }

                /// <summary>
                /// Creates a success response.
                /// </summary>
                /// <param name="submission">Saved submission.</param>
                /// <returns>Response.</returns>
                public SubmissionResponse(SubmissionResource submission) : this(true, string.Empty, submission) { }

                /// <summary>
                /// Creates am error response.
                /// </summary>
                /// <param name="message">Error message.</param>
                /// <returns>Response.</returns>
                public SubmissionResponse(string message) : this(false, message, null) { }
        }
}