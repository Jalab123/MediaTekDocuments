using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class DvdTests
    {

        private static readonly string Id = "20005";
        private static readonly string Titre = "Titre Test";
        private static readonly string Image = "imagetest.png";
        private static readonly int Duree = 30;
        private static readonly string Realisateur = "Réalisateur Test";
        private static readonly string Synopsis = "Synopsis Test";
        private static readonly string IdGenre = "10020";
        private static readonly string LeGenre = "Genre Test";
        private static readonly string IdPublic = "00005";
        private static readonly string LePublic = "Public Test";
        private static readonly string IdRayon = "DV006";
        private static readonly string LeRayon = "Rayon Test";

        private readonly Dvd dvd = new Dvd(Id, Titre, Image, Duree, Realisateur, Synopsis, IdGenre, LeGenre, IdPublic, LePublic, IdRayon, LeRayon);


        [TestMethod]
        public void DvdTest()
        {
            Assert.AreEqual(dvd.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(dvd.Titre, Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(dvd.Image, Image, "devrait réussir : image valorisée");
            Assert.AreEqual(dvd.Duree, Duree, "devrait réussir : duree valorisée");
            Assert.AreEqual(dvd.Realisateur, Realisateur, "devrait réussir : image valorisée");
            Assert.AreEqual(dvd.Synopsis, Synopsis, "devrait réussir : synopsis valorisé");
            Assert.AreEqual(dvd.IdGenre, IdGenre, "devrait réussir : idgenre valorisé");
            Assert.AreEqual(dvd.Genre, LeGenre, "devrait réussir : genre valorisé");
            Assert.AreEqual(dvd.IdPublic, IdPublic, "devrait réussir : idpublic valorisé");
            Assert.AreEqual(dvd.Public, LePublic, "devrait réussir : public valorisé");
            Assert.AreEqual(dvd.IdRayon, IdRayon, "devrait réussir : idrayon valorisé");
            Assert.AreEqual(dvd.Rayon, LeRayon, "devrait réussir : rayon valorisé");
        }
    }
}
