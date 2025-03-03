using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class CategorieTests
    {
        private static readonly String Id = "C00001";
        private static readonly String Libelle = "Catégorie Test";

        private static readonly Categorie categorie = new Categorie(Id, Libelle);

        [TestMethod]
        public void CategorieTest()
        {
            Assert.AreEqual(categorie.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(categorie.Libelle, Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual(categorie.ToString(), Libelle, "devrait réussir : libelle retourné en format string");
        }
    }
}
