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

        private static readonly string FORMAT_DATE = "dd/MM/yyyy";

        [TestMethod()]
        public void ParutionDansAbonnementCorrectTest()
        {
            DateTime dateCom = DateTime.ParseExact("01/01/2020", FORMAT_DATE, CultureInfo.InvariantCulture);
            DateTime dateExp = DateTime.ParseExact("01/01/2024", FORMAT_DATE, CultureInfo.InvariantCulture);
            DateTime datePar = DateTime.ParseExact("01/01/2022", FORMAT_DATE, CultureInfo.InvariantCulture);
            // fonctionne car 01/01/2020 < 01/01/2022 < 01/01/2024 est correct
            Assert.IsTrue(FrmMediatek.ParutionDansAbonnement(dateCom, dateExp, datePar));
        }
        [TestMethod()]
        public void ParutionDansAbonnementIncorrectTest()
        {
            DateTime dateCom = DateTime.ParseExact("01/01/2020", FORMAT_DATE, CultureInfo.InvariantCulture);
            DateTime dateExp = DateTime.ParseExact("01/01/2022", FORMAT_DATE, CultureInfo.InvariantCulture);
            DateTime datePar = DateTime.ParseExact("01/01/2024", FORMAT_DATE, CultureInfo.InvariantCulture);
            // ne fonctionne pas car 01/01/2020 < 01/01/2024 < 01/01/2022 est incorrect
            Assert.IsFalse(FrmMediatek.ParutionDansAbonnement(dateCom, dateExp, datePar));
        }

    }
}