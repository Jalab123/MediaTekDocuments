using MediaTekDocuments.model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MediaTekDocumentsTests.model
{
    [TestClass]
    public class UtilisateurTests
    {
        private static readonly string Id = "1";
        private static readonly string Login = "UserTest";
        private static readonly string Pwd = "PasswordTest";
        private static readonly string IdService = "1";

        private static readonly Utilisateur utilisateur = new Utilisateur(Id, Login, Pwd, IdService);

        [TestMethod]
        public void UtilisateurTest()
        {
            Assert.AreEqual(utilisateur.Id, Id, "devrait réussir : id valorisé");
            Assert.AreEqual(utilisateur.Login, Login, "devrait réussir : login valorisé");
            Assert.AreEqual(utilisateur.Pwd, Pwd, "devrait réussir : pwd valorisé");
            Assert.AreEqual(utilisateur.IdService, IdService, "devrait réussir : idservice valorisé");
        }
    }
}
