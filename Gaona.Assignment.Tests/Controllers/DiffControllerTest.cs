using System;
using System.Web.Http.Results;
using Gaona.Assigment.Web.Controllers;
using Gaona.Assigment.Web.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Gaona.Assignment.Business;
using Gaona.Assignment.Data;


namespace Gaona.Assignment.Tests.Controllers
{
    [TestClass]
    public class DiffControllerTest
    {
        private DiffController _sut;
        private Mock<IDataManager> _dataManagerMock;
        private Mock<IDiffer> _differMock;
        private string _img1, _img2;


        [TestInitialize]
        public void Init()
        {
            _dataManagerMock = new Mock<IDataManager>(MockBehavior.Strict);
            _differMock = new Mock<IDiffer>(MockBehavior.Strict);
            _sut = new DiffController(_dataManagerMock.Object, _differMock.Object);


            //base64 for image files
            _img2 = "iVBORw0KGgoAAAANSUhEUgAAABYAAAAWCAYAAADE" +
                         "tGw7AAAABGdBTUEAALGPC/xhBQAAAkFJREFUOBGllT1oFEEUx38zuxf1MGghQiqFCMFCK7" +
                         "WwEez8OBQLQRQLLSxEcpUE/Eij2EqIBIuQws4qIaK9pEgheB6ijVbiaWEhivFj1x3fG5zc7" +
                         "p17l4sDy8x77//+zLw3819DyXAzbE9+ccpAzcGYzCMCTZyjheG5hYVoI4/MJVb+RSH44nCz" +
                         "DKffmMBRF5JqMVq0JPmDsUxGV5g1hiwfLRC7KUaTjEUB7M6D+q2FdDHezFlzka8Bu0qspGnG" +
                         "shx7WwgOMgvRs3gTh0JppFSgx9edrpfUc8C+5AdzYTOe2Nd0wOMHgsLsOJ1OcUx9VruvjSoA8o" +
                         "Ydylt915njjoKsXqle3Y8Oz2D3X4NoQ19SD3DscXfZa6XotZ4ZcZXo4C3icy8xO470hIZgaqhZ" +
                         "vfzB0Ws2W3cRn3xMdHwehnf2gkplGdMd64ta87CjJ4jPv8IeuCE5vvdducqpkaQr0s/x+S3u/" +
                         "VNBFR5bO8uRxP7tw5a2t3zlfn4hW54kezEtlz8tBTpDK5ZoQ76+Tzh7/YDfS1dh5WMp4WrA0Yily" +
                         "PNy986sOjsW7lOTtHkP11rqiJSblQoLxt2nmn7njXRyoCaW0UrjnlTqHLUqGip9ZcBB/EKaSW0n" +
                         "NMffl796qnL5f8Nw09RpKoknVpH2eirSt25mw1xlnNsh3xOroSKteiq/nYchuJZZjy8buz40zo" +
                         "U8XvzdQ6XPq5QISne07dFGaU3D8dsR2Wje6FyrSqmg6NsX4IiIQKKXX+aGv1KXedeZE+w/" +
                         "BTKl4mGKsZwAAAAASUVORK5CYII=";
            _img1 =
               "iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAMAAAAM7l6QAAAAMFBMVEX" +
               "///+NmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmq" +
               "ZMjrU7AAAAD3RSTlMAoEAQ8LAw0ODAYCBQkHCRjMcEAAAApElEQVR42r3S2w6DIBAE0O4uKDed" +
               "///bZk1aGDC+NOm8GDjZDKivv2STHUASvUOL+CTbOhrQE7Z59lIp2iS4V+YIYK+9JZOq67dRAOjIv" +
               "lGpKY6cABmWBxBGBlCmLmIu23zN3B6mA3efEwsQjE4a+d7jhtBJPdndukKI6/WmD1U9E0C+" +
               "fLFbz6tTWoQnFunOMTV/sHNG10dnZqc/ZHXSNaquP+YNmD0L7UqbR+YAAAAASUVORK5CYII=";
        }

        [TestMethod]
        public void PostCorrectDataTest()
        {
            //Arrange
            string id = "1";
            _dataManagerMock.Setup(mock => mock.Add(id + "-left", _img1));

            //Act
            var response = _sut.PostLeft(id, new DiffRequest() { Data = _img1 });

            //Assert
            Assert.IsInstanceOfType(response, typeof(CreatedNegotiatedContentResult<string>));
        }

        [TestMethod]
        public void PostMissingDataTest()
        {
            //Arrange
            string id = "2";

            //Act
            var response = _sut.PostRight(id, null);

            //Assert
            Assert.IsInstanceOfType(response, typeof(BadRequestResult));
        }

        [TestMethod]
        public void GetDiffExistingDataTest()
        {
            //Arrange
            string id = "id";
            string data = "VGhlIGdyZWVuIGZ4IGZsaWVzIG92ZXIgdGhlIGJpZyBibHVlIGZhbmN5";
            _dataManagerMock.Setup(mock => mock.TryRetrieve($"{id}-left", string.Empty)).Returns(data);
            _dataManagerMock.Setup(mock => mock.TryRetrieve($"{id}-right", string.Empty)).Returns(data);
            _differMock.Setup(mock => mock.Diff(data, data)).Returns(new DiffResult("equal"));


            //Act
            var result = _sut.GetDiff(id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(OkNegotiatedContentResult<DiffResult>));
        }

        [TestMethod]
        public void GetDiffMissingDataTest()
        {
            //Arrange
            string id = "id2";
            string data = _img2;
            _dataManagerMock.Setup(mock => mock.TryRetrieve($"{id}-left", String.Empty)).Returns(data);
            _dataManagerMock.Setup(mock => mock.TryRetrieve($"{id}-right", string.Empty)).Returns(string.Empty);

            //Act
            var result = _sut.GetDiff(id);

            //Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));

        }


        [TestMethod]
        [ExpectedException(typeof(ApplicationException))] //Assert
        public void GetErrorTest()
        {
            //Arrange

            //Act
            _sut.GetError();
        }
    }
}
