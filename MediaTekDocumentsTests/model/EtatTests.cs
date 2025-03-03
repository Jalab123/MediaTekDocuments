using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class EtatTests
    {
        private static readonly String Id = "00005";
        private static readonly String Libelle = "Etat Test";

        private static readonly Etat etat = new Etat(Id, Libelle);

        [TestMethod]
        public void EtatTest()
        {
            Assert.AreEqual(etat.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(etat.Libelle, Libelle, "devrait réussir : libelle valorisé");
        }
    }
}
