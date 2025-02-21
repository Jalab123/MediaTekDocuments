using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class ServiceTests
    {

        private static readonly string Id = "1";
        private static readonly string Nom = "Service Test";

        private static readonly Service service = new Service(Id, Nom);

        [TestMethod]
        public void ServiceTest()
        {
            Assert.AreEqual(service.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(service.Nom, Nom, "devrait réussir : nom valorisé");
        }
    }
}
