using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class GenreTests
    {

        private static readonly String Id = "10021";
        private static readonly String Libelle = "Horreur";

        private static readonly Genre genre = new Genre(Id, Libelle);

        [TestMethod]
        public void GenreTest()
        {
            Assert.AreEqual(genre.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(genre.Libelle, Libelle, "devrait réussir : libelle valorisé");
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual(genre.ToString(), Libelle, "devrait réussir : libelle retourné en format string");
        }
    }
}
