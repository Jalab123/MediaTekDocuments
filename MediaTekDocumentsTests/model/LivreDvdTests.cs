using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class LivreDvdTests
    {
        private static readonly string Id = "00027";
        private static readonly string Titre = "Titre Test";
        private static readonly string Image = "imagetest.png";
        private static readonly string IdGenre = "10020";
        private static readonly string LeGenre = "Genre Test";
        private static readonly string IdPublic = "00006";
        private static readonly string LePublic = "Public Test";
        private static readonly string IdRayon = "DV0006";
        private static readonly string LeRayon = "Rayon Test";

        private static readonly LivreDvd livredvd = new LivreDvd(Id, Titre, Image, IdGenre, LeGenre, IdPublic, LePublic, IdRayon, LeRayon);

        [TestMethod]
        public void LivreDvdTest()
        {
            Assert.AreEqual(livredvd.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(livredvd.Titre, Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(livredvd.Image, Image, "devrait réussir : image valorisée");
            Assert.AreEqual(livredvd.IdGenre, IdGenre, "devrait réussir : idgenre valorisé");
            Assert.AreEqual(livredvd.Genre, LeGenre, "devrait réussir : genre valorisé");
            Assert.AreEqual(livredvd.IdPublic, IdPublic, "devrait réussir : idpublic valorisé");
            Assert.AreEqual(livredvd.Public, LePublic, "devrait réussir : public valorisé");
            Assert.AreEqual(livredvd.IdRayon, IdRayon, "devrait réussir : idrayon valorisé");
            Assert.AreEqual(livredvd.Rayon, LeRayon, "devrait réussir : rayon valorisé");
        }

        public class LivreDvd : Document
        {
            public LivreDvd(string id, string titre, string image, string idGenre, string genre,
                string idPublic, string lePublic, string idRayon, string rayon)
                : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
            {
            }

        }
    }
}
