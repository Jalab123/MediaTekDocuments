using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class PublicTests
    {
        private static readonly string Id = "00005";
        private static readonly string Libelle = "Public Test";

        private static readonly Public unpublic = new Public(Id, Libelle);

        [TestMethod]
        public void PublicTest()
        {
            Assert.AreEqual(unpublic.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(unpublic.Libelle, Libelle, "devrait réussir : libelle valorisé");
        }
    }
}
