using Gaona.Assignment.Data;
using Gaona.Assignment.Data.Stores;
using Gaona.Assignment.Data.Stores.InProc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gaona.Assignment.Tests.Data
{
    [TestClass]
    public class DataManagerTest
    {
        private DataManager _sut;
        private IDataStore _dataStore;

        [TestInitialize]
        public void Init()
        {
            _dataStore = new SimpleInProcDataStore();
            _sut = new DataManager(_dataStore);

        }


        [TestMethod]
        public void AddItemToStoreNoExistingKey()
        {
            //Arrange
            string value = "5";
            string key = "key";

            //Act
            _sut.Add(key, value);

            //Assert
            Assert.AreEqual(_dataStore.Get(key), value);

        }

        [TestMethod]
        public void AddItemToStoreExistingKey()
        {
            //Arrange
            string key = "key2";
            _dataStore.Add(key, "5");


            //Act
            _sut.Add(key, "6"); //try adding when the key already exists

            //Assert
            Assert.AreEqual(_dataStore.Get(key), "6");
        }

        [TestMethod]
        public void TryGettingExistingItem()
        {
            //Arrange
            string key = "key3";
            _dataStore.Add(key, "5");

            //Act
            string item = _sut.TryRetrieve(key);

            //Assert
            Assert.AreEqual(item, "5");
        }

        [TestMethod]
        public void TryGetttingNonExistingItemNoDefaultValue()
        {
            //Arrange
            _dataStore.Add("key4", "5");

            //Act
            string item = _sut.TryRetrieve("key5");

            //Assert
            Assert.IsNull(item);
        }

        [TestMethod]
        public void TryGettingNonExistingItemDefaultValue()
        {
            //Arrange
            _dataStore.Add("key6", "10");

            //Act
            string item = _sut.TryRetrieve("key7", string.Empty);


            //Assert
            Assert.AreEqual(item, string.Empty);
        }

        [TestMethod]
        public void TryGettingItemFromEmptyDataStore()
        {

            //Act
            string item = _sut.TryRetrieve("key");

            //Assert
            Assert.IsNull(item);
        }
    }
}
