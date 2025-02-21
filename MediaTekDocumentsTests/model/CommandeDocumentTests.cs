using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class CommandeDocumentTests
    {

        private static readonly string Id = "1";
        private static readonly string IdLivreDVD = "00026";
        private static readonly string IdSuivi = "1";
        private static readonly DateTime DateCommande = DateTime.ParseExact("2025-03-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private static readonly int Montant = 10;
        private static readonly int NbExemplaire = 1;
        private static readonly string Statut = "En cours";

        private static readonly CommandeDocument commandedocument = new CommandeDocument(Id, IdLivreDVD, IdSuivi, DateCommande, Montant, NbExemplaire, Statut);

        [TestMethod]
        public void CommandeDocumentTest()
        {
            Assert.AreEqual(commandedocument.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(commandedocument.IdLivreDVD, IdLivreDVD, "devrait réussir : idlivredvd valorisé");
            Assert.AreEqual(commandedocument.IdSuivi, IdSuivi, "devrait réussir : idsuivi valorisé");
            Assert.AreEqual(commandedocument.DateCommande, DateCommande, "devrait réussir : datecommande valorisée");
            Assert.AreEqual(commandedocument.Montant, Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(commandedocument.Statut, Statut, "devrait réussir : statut valorisé");
        }
    }
}
