using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class SuiviTests
    {

        private static readonly String Id = "1";
        private static readonly String Statut = "En cours";

        private static readonly Suivi suivi = new Suivi(Id, Statut);

        [TestMethod]
        public void SuiviTest()
        {
            Assert.AreEqual(suivi.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(suivi.Statut, Statut, "devrait réussir : statut valorisé");
        }
    }
}
