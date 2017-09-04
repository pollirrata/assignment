namespace Gaona.Assigment.Web.Model
{
    /// <summary>
    /// Model of the diff JSON payload sent to the endpoints.
    /// </summary>
    public class DiffRequest
    {
        /// <summary>
        /// The base64 string value for the encoded binary data
        /// </summary>
        public string Data { get; set; }
    }
}
