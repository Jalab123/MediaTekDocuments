using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class ExemplaireTests
    {

        private static readonly int Numero = 10012;
        private static readonly DateTime DateAchat = DateTime.ParseExact("2025-03-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private static readonly string Photo = "phototest.png";
        private static readonly string IdEtat = "00005";
        private static readonly string Id = "00027";

        private static readonly Exemplaire exemplaire = new Exemplaire(Numero, DateAchat, Photo, IdEtat, Id);

        [TestMethod]
        public void ExemplaireTest()
        {
            Assert.AreEqual(exemplaire.Numero, Numero, "devrait réussir : numero valorisé");
            Assert.AreEqual(exemplaire.DateAchat, DateAchat, "devrait réussir : dateachat valorisé");
            Assert.AreEqual(exemplaire.Photo, Photo, "devrait réussir : photo valorisée");
            Assert.AreEqual(exemplaire.IdEtat, IdEtat, "devrait réussir : idetat valorisé");
            Assert.AreEqual(exemplaire.Id, Id, "devrait réussir : id valorisé");
        }
    }
}
