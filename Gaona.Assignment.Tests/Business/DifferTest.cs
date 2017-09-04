using Gaona.Assignment.Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gaona.Assignment.Tests.Business
{
    [TestClass]
    public class DifferTest
    {
        private Differ _sut;
        //base-64 string values, big are image files, small are intended to easily determine the diffs
        private string _big1, _big2;
        private string _small1, _small2;

        [TestInitialize]
        public void Init()
        {
            _sut = new Differ();


            _big1 =
               "iVBORw0KGgoAAAANSUhEUgAAAB4AAAAeCAMAAAAM7l6QAAAAMFBMVEX" +
               "///+NmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmqaNmq" +
               "ZMjrU7AAAAD3RSTlMAoEAQ8LAw0ODAYCBQkHCRjMcEAAAApElEQVR42r3S2w6DIBAE0O4uKDed" +
               "///bZk1aGDC+NOm8GDjZDKivv2STHUASvUOL+CTbOhrQE7Z59lIp2iS4V+YIYK+9JZOq67dRAOjIv" +
               "lGpKY6cABmWBxBGBlCmLmIu23zN3B6mA3efEwsQjE4a+d7jhtBJPdndukKI6/WmD1U9E0C+" +
               "fLFbz6tTWoQnFunOMTV/sHNG10dnZqc/ZHXSNaquP+YNmD0L7UqbR+YAAAAASUVORK5CYII=";

            _big2 = "iVBORw0KGgoAAAANSUhEUgAAABYAAAAWCAYAAADE" +
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


            _small1 = "VGhlIGJyb3duIGZ4IGp1bXBzIG92ZXIgdGhlIGJpZyBncmF5IGZlbmNl";
            _small2 = "VGhlIGdyZWVuIGZ4IGp1bXBzIG92ZXIgdGhlIGJpZyBibHVlIGZlbmNl";

        }

        [TestMethod]
        public void EqualBigDataTest()
        {
            //Arrange
            string left = _big1;
            string right = _big1;

            //Act
            DiffResult result = _sut.Diff(left, right);


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("equal", result.Description);
            Assert.IsNull(result.Locations);

        }

        [TestMethod]
        public void EqualSmallDataTest()
        {
            //Arrange
            string left = _small2;
            string right = _small2;

            //Act
            DiffResult result = _sut.Diff(left, right);


            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("equal", result.Description);
            Assert.IsNull(result.Locations);

        }

        [TestMethod]
        public void NotEqualSizeTest()
        {
            //Arrange
            string left = _big1;
            string right = _big2;

            //Act
            DiffResult result = _sut.Diff(left, right);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("different size", result.Description);
            Assert.IsNull(result.Locations);
        }

        [TestMethod]
        public void EqualSizeDifferentContentTest()
        {
            //Arrange
            string left = _small1; //The brown fóx jumps over the big gray fence
            string right = _small2; //The green fóx jumps over the big blue fence

            //Act
            DiffResult result = _sut.Diff(left, right);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("diffs", result.Description);
            Assert.IsNotNull(result.Locations);
            Assert.AreEqual(3, result.Locations.Count);

            //g vs b
            Assert.AreEqual(4, result.Locations[0].Offset);
            Assert.AreEqual(1, result.Locations[0].Size);

            //ee vs ow
            Assert.AreEqual(6, result.Locations[1].Offset);
            Assert.AreEqual(2, result.Locations[1].Size);

            //gray vs blue
            Assert.AreEqual(32, result.Locations[2].Offset);
            Assert.AreEqual(4, result.Locations[2].Size);
        }
    }
}
