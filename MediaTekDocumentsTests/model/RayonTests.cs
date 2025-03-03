using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class RayonTests
    {

        private static readonly string Id = "DV006";
        private static readonly string Libelle = "Rayon Test";

        private static readonly Rayon rayon = new Rayon(Id, Libelle);

        [TestMethod]
        public void RayonTest()
        {
            Assert.AreEqual(rayon.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(rayon.Libelle, Libelle, "devrait réussir : libelle valorisé");
        }
    }
}
