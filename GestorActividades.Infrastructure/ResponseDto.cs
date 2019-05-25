namespace GestorActividades.Infrastructure
{
    public class ResponseDto<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseDto{T}"/> class.
        /// </summary>
        public ResponseDto()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseDto{T}"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        public ResponseDto(StatusCode statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the status message.
        /// </summary>
        /// <value>
        /// The status message.
        /// </value>
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public StatusCode StatusCode { get; set; }
    }
}
