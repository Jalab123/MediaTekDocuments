using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static MediaTekDocumentsTests.model.LivreDvdTests;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class LivreTests
    {

        private static readonly string Id = "00027";
        private static readonly string Titre = "Titre Test";
        private static readonly string Image = "imagetest.png";
        private static readonly string Isbn = "1234567891234";
        private static readonly string Auteur = "Auteur Test";
        private static readonly string Collection = "Collection Test";
        private static readonly string IdGenre = "10020";
        private static readonly string LeGenre = "Genre Test";
        private static readonly string IdPublic = "00006";
        private static readonly string LePublic = "Public Test";
        private static readonly string IdRayon = "DV0006";
        private static readonly string LeRayon = "Rayon Test";

        private static readonly Livre livre = new Livre(Id, Titre, Image, Isbn, Auteur, Collection, IdGenre, LeGenre, IdPublic, LePublic, IdRayon, LeRayon);

        [TestMethod]
        public void LivreTest()
        {
            Assert.AreEqual(livre.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(livre.Titre, Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(livre.Image, Image, "devrait réussir : image valorisée");
            Assert.AreEqual(livre.Isbn, Isbn, "devrait réussir : isbn valorisé");
            Assert.AreEqual(livre.Auteur, Auteur, "devrait réussir : auteur valorisé");
            Assert.AreEqual(livre.Collection, Collection, "devrait réussir : collection valorisée");
            Assert.AreEqual(livre.IdGenre, IdGenre, "devrait réussir : idgenre valorisé");
            Assert.AreEqual(livre.Genre, LeGenre, "devrait réussir : genre valorisé");
            Assert.AreEqual(livre.IdPublic, IdPublic, "devrait réussir : idpublic valorisé");
            Assert.AreEqual(livre.Public, LePublic, "devrait réussir : public valorisé");
            Assert.AreEqual(livre.IdRayon, IdRayon, "devrait réussir : idrayon valorisé");
            Assert.AreEqual(livre.Rayon, LeRayon, "devrait réussir : rayon valorisé");
        }
    }
}
