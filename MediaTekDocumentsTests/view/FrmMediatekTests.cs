using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediaTekDocuments.view;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaTekDocuments.model;
using System.Globalization;

namespace MediaTekDocuments.view.Tests
{
    [TestClass()]
    public class FrmMediatekTests
    {

        public bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateExpiration, DateTime dateParution)
        {
            if (dateParution > dateCommande && dateParution < dateExpiration)
            {
                return true;
            }
            return false;
        }

        [TestMethod()]
        public void ParutionDansAbonnementCorrectTest()
        {
            DateTime dateCom = DateTime.ParseExact("2020-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dateExp = DateTime.ParseExact("2024-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime datePar = DateTime.ParseExact("2022-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            // fonctionne car 2020-01-01 < 2022-01-01 < 2024-01-01 est correct
            Assert.IsTrue(ParutionDansAbonnement(dateCom, dateExp, datePar));
        }
        [TestMethod()]
        public void ParutionDansAbonnementIncorrectTest()
        {
            DateTime dateCom = DateTime.ParseExact("2020-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime dateExp = DateTime.ParseExact("2022-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime datePar = DateTime.ParseExact("2024-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            // ne fonctionne pas car 2020-01-01 < 2024-01-01 < 2022-01-01 est incorrect
            Assert.IsFalse(ParutionDansAbonnement(dateCom, dateExp, datePar));
        }
    }
}