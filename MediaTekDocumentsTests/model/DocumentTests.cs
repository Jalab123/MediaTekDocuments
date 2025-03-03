using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class DocumentTests
    {

        private static readonly string Id = "00027";
        private static readonly string Titre = "Titre Test";
        private static readonly string Image = "imagetest.png";
        private static readonly string IdGenre = "10020";
        private static readonly string LeGenre = "Genre Test";
        private static readonly string IdPublic = "00005";
        private static readonly string LePublic = "Public Test";
        private static readonly string IdRayon = "DV006";
        private static readonly string LeRayon = "Rayon Test";

        private static readonly Document document = new Document(Id, Titre, Image, IdGenre, LeGenre, IdPublic, LePublic, IdRayon, LeRayon);

        [TestMethod]
        public void DocumentTest()
        {
            Assert.AreEqual(document.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(document.Titre, Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(document.Image, Image, "devrait réussir : image valorisée");
            Assert.AreEqual(document.IdGenre, IdGenre, "devrait réussir : idgenre valorisé");
            Assert.AreEqual(document.Genre, LeGenre, "devrait réussir : genre valorisé");
            Assert.AreEqual(document.IdPublic, IdPublic, "devrait réussir : idpublic valorisé");
            Assert.AreEqual(document.Public, LePublic, "devrait réussir : public valorisé");
            Assert.AreEqual(document.IdRayon, IdRayon, "devrait réussir : idrayon valorisé");
            Assert.AreEqual(document.Rayon, LeRayon, "devrait réussir : rayon valorisé");
        }
    }
}
