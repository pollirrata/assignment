using System;
using System.Web.Http;
using Gaona.Assigment.Web.Model;
using Gaona.Assignment.Business;
using Gaona.Assignment.Data;

namespace Gaona.Assigment.Web.Controllers
{
    /// <summary>
    /// Controller with the endpoint methods to determine the diff
    /// </summary>
    public class DiffController : ApiController
    {
        #region ctor
        private IDataManager _dataManager;
        private IDiffer _differ;
        /// <summary>
        /// Default constructor required for the API
        /// </summary>
        public DiffController()
        {

        }


        /// <summary>
        /// Constructor intended for unit test
        /// </summary>
        /// <param name="dataManager">Data manager to be injected</param>
        /// <param name="differ">Class used to perform the actual diffs</param>
        public DiffController(IDataManager dataManager, IDiffer differ)
        {
            _dataManager = dataManager;
            _differ = differ;
        }
        #endregion

        /// <summary>
        /// Initialize the data manager and the differ instances. This is called on each
        /// action to ensure that if any error happens it gets caught by the global exception filter
        /// </summary>
        private void Initialize()
        {
            if (_dataManager == null)
            {
                _dataManager = new DataManager();
            }
            if (_differ == null)
            {
                _differ = new Differ();
            }
        }

        /// <summary>
        /// Set the left side for the diff process
        /// </summary>
        /// <param name="id">The id of the comparision</param>
        /// <param name="request">JSON base64 encoded binary data</param>
        /// <returns>HTTP Response</returns>
        [Route("v1/diff/{id}/left")]
        public IHttpActionResult PostLeft(string id, [FromBody] DiffRequest request)
        {
            return ProcessInput(id, "left", request);
        }

        /// <summary>
        /// Set the right side for the diff process
        /// </summary>
        /// <param name="id">The id of the comparision</param>
        /// <param name="request">JSON base64 encoded binary data</param>
        /// <returns>HTTP Response</returns>
        [Route("v1/diff/{id}/right")]
        public IHttpActionResult PostRight(string id, [FromBody] DiffRequest request)
        {
            return ProcessInput(id, "right", request);
        }

        /// <summary>
        /// Process the input for the diff
        /// </summary>
        /// <param name="id">The id of the comparision</param>
        /// <param name="side">left or right</param>
        /// <param name="request">JSON base64 encoded binary data</param>
        /// <returns>HTTP Response</returns>
        private IHttpActionResult ProcessInput(string id, string side, DiffRequest request)
        {
            if (string.IsNullOrEmpty(request?.Data)) return BadRequest();

            Initialize();

            _dataManager.Add($"{id}-{side}", request.Data);
            return Created($"v1/diff/{id}/{side}", request.Data);
        }


        /// <summary>
        /// Process the diff of the inputs matching the ID
        /// </summary>
        /// <param name="id">The id of the diff comparision</param>
        /// <returns>HTTP Response</returns>
        [Route("v1/diff/{id}")]
        public IHttpActionResult GetDiff(string id)
        {
            Initialize();
            var left = _dataManager.TryRetrieve($"{id}-left", string.Empty);
            var right = _dataManager.TryRetrieve($"{id}-right", string.Empty);

            if (string.IsNullOrEmpty(left) || string.IsNullOrEmpty(right))
            {
                return BadRequest("Left or right data is missing. Please validate");
            }

            DiffResult result = _differ.Diff(left, right);


            return Ok(result);
        }


        /// <summary>
        /// Method for testing the exception handling process
        /// </summary>
        /// <returns>Prettyfied HTTP Response</returns>
        [Route("v1/error")]
        public IHttpActionResult GetError()
        {
            throw new ApplicationException();
        }
    }
}
