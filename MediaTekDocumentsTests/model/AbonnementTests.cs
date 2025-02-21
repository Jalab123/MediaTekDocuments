﻿using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class AbonnementTests
    {
        private static readonly string Id = "1";
        private static readonly DateTime DateCommande = DateTime.ParseExact("2025-03-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private static readonly int Montant = 10;
        private static readonly DateTime DateFinAbonnement = DateTime.ParseExact("2025-09-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
        private static readonly string IdRevue = "10012";

        private static readonly Abonnement abonnement = new Abonnement(Id, DateCommande, Montant, DateFinAbonnement, IdRevue);

        [TestMethod]
        public void AbonnementTest()
        {
            Assert.AreEqual(abonnement.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(abonnement.DateCommande, DateCommande, "devrait réussir : datecommande valorisée");
            Assert.AreEqual(abonnement.Montant, Montant, "devrait réussir : montant valorisé");
            Assert.AreEqual(abonnement.DateFinAbonnement, DateFinAbonnement, "devrait réussir : datefinabonnement valorisée");
            Assert.AreEqual(abonnement.IdRevue, IdRevue, "devrait réussir : idrevue valorisé");
        }
    }
}
