using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class RevueTests
    {

        private static readonly string Id = "10012";
        private static readonly string Titre = "Titre Test";
        private static readonly string Image = "imagetest.png";
        private static readonly string IdGenre = "10020";
        private static readonly string LeGenre = "Genre Test";
        private static readonly string IdPublic = "00005";
        private static readonly string LePublic = "Public Test";
        private static readonly string IdRayon = "DV006";
        private static readonly string LeRayon = "Rayon Test";
        private static readonly string Periodicite = "MS";
        private static readonly int DelaiMiseADispo = 10;

        private static readonly Revue revue = new Revue(Id, Titre, Image, IdGenre, LeGenre, IdPublic, LePublic, IdRayon, LeRayon, Periodicite, DelaiMiseADispo);

        [TestMethod]
        public void RevueTest()
        {
            Assert.AreEqual(revue.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(revue.Titre, Titre, "devrait réussir : titre valorisé");
            Assert.AreEqual(revue.Image, Image, "devrait réussir : image valorisée");
            Assert.AreEqual(revue.IdGenre, IdGenre, "devrait réussir : idgenre valorisé");
            Assert.AreEqual(revue.Genre, LeGenre, "devrait réussir : genre valorisé");
            Assert.AreEqual(revue.IdPublic, IdPublic, "devrait réussir : idpublic valorisé");
            Assert.AreEqual(revue.Public, LePublic, "devrait réussir : public valorisé");
            Assert.AreEqual(revue.IdRayon, IdRayon, "devrait réussir : idrayon valorisé");
            Assert.AreEqual(revue.Rayon, LeRayon, "devrait réussir : rayon valorisé");
            Assert.AreEqual(revue.Periodicite, Periodicite, "devrait réussir : periodicite valorisé");
            Assert.AreEqual(revue.DelaiMiseADispo, DelaiMiseADispo, "devrait réussir : delaimiseadispo valorisé");
        }
    }
}
