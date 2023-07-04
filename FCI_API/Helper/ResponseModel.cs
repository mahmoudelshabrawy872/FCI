using System.Net;

namespace FCI_API.Helper
{
    /// <summary>
    /// Represents a response model that encapsulates the response data and status.
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseModel"/> class.
        /// </summary>
        public ResponseModel()
        {
            ErrorMessages = new List<string>();
        }

        /// <summary>
        /// Gets or sets the HTTP status code of the response.
        /// </summary>
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// Gets or sets a value indicating whether the response is successful.
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// Gets or sets the result data of the response.
        /// </summary>
        public object? Result { get; set; }

        /// <summary>
        /// Gets or sets the error messages associated with the response.
        /// </summary>
        public List<string>? ErrorMessages { get; set; }
    }
}