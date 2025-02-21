using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class CommandeTests
    {
        private static readonly string Id = "1";
        private static readonly DateTime DateCommande = DateTime.ParseExact("2025-03-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private static readonly int Montant = 10;

        private static readonly Commande commande = new Commande(Id, DateCommande, Montant);

        [TestMethod]
        public void CommandeTest()
        {
            Assert.AreEqual(commande.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(commande.DateCommande, DateCommande, "devrait réussir : datecommande valorisée");
            Assert.AreEqual(commande.Montant, Montant, "devrait réussir : montant valorisé");
        }
    }
}
