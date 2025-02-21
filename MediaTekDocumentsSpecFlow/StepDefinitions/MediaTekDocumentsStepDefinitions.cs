using MediaTekDocuments.view;
using NUnit.Framework;
using System;
using System.Windows.Forms;
using TechTalk.SpecFlow;
using static System.Net.Mime.MediaTypeNames;

namespace SpecFlowMediaTekDocuments.StepDefinitions
{
    [Binding]
    public class MediaTekDocumentsStepDefinitions
    {
        private readonly FrmMediatek frmMediatek = new FrmMediatek(1);

        [Given(@"je saisis le numéro de document (.*)")]
        public void GivenJeSaisisLeNumeroDeDocument(string valeur)
        {
            SelectTab();
            TextBox txbLivresNumRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresNumRecherche"];
            txbLivresNumRecherche.Text = valeur.ToString();
        }

        [When(@"je clic sur le bouton rechercher")]
        public void WhenJeClicSurLeBoutonRechercher()
        {
            SelectTab();
            Button btn = (Button)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["btnLivresNumRecherche"];
            btn.PerformClick();
        }
        

        [Then(@"le resultat trouvé a pour titre (.*)")]
        public void ThenLeResultatTrouveAPourTitre(string valeur)
        {
            SelectTab();
            TextBox txt = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresInfos"].Controls["txbLivresTitre"];
            Assert.AreEqual(valeur, txt.Text);

        }

        [Given(@"je saisis le titre de document (.*)")]
        public void GivenJeSaisisLeTitreDeDocument(string valeur)
        {
            SelectTab();
            TextBox txbLivresNumRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresTitreRecherche"];
            txbLivresNumRecherche.Text = valeur.ToString();
        }

        [Then(@"le resultat trouvé a pour numéro de document (.*)")]
        public void ThenLeResultatTrouveAPourNumeroDeDocument(string valeur)
        {
            SelectTab();
            TextBox txt = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresInfos"].Controls["txbLivresNumero"];
            Assert.AreEqual(valeur, txt.Text);
        }

        [When(@"je sélectionne le genre (.*)")]
        public void WhenJeSelectionneLeGenre(string valeur)
        {
            SelectTab();
            ComboBox cbxLivresGenres = (ComboBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["cbxLivresGenres"];
            int indiceLigne = cbxLivresGenres.FindStringExact(valeur);
            cbxLivresGenres.SelectedIndex = indiceLigne;
        }

        [When(@"je sélectionne le public (.*)")]
        public void WhenJeSelectionneLePublic(string valeur)
        {
            SelectTab();
            ComboBox cbxLivresPublics = (ComboBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["cbxLivresPublics"];
            int indiceLigne = cbxLivresPublics.FindStringExact(valeur);
            cbxLivresPublics.SelectedIndex = indiceLigne;
        }

        [When(@"je sélectionne le rayon (.*)")]
        public void WhenJeSelectionneLeRayon(string valeur)
        {
            SelectTab();
            ComboBox cbxLivresRayons = (ComboBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["cbxLivresRayons"];
            int indiceLigne = cbxLivresRayons.FindStringExact(valeur);
            cbxLivresRayons.SelectedIndex = indiceLigne;
        }

        [Then(@"le nombre de livres obtenu est de (.*)")]
        public void ThenLeNombreDeLivresObtenuEstDe(int valeur)
        {
            SelectTab();
            DataGridView dgv = (DataGridView)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["dgvLivresListe"];
            Assert.AreEqual(valeur, dgv.RowCount);
        }


        public void SelectTab()
        {
            TabControl tabOngletsApplication = (TabControl)frmMediatek.Controls["tabOngletsApplication"];
            frmMediatek.Visible = true;
            tabOngletsApplication.SelectedTab = (TabPage)tabOngletsApplication.Controls["tabLivres"];
        }
    }
}
