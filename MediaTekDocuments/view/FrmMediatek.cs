﻿using System;
using System.Windows.Forms;
using MediaTekDocuments.model;
using MediaTekDocuments.controller;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Text;

namespace MediaTekDocuments.view

{
    /// <summary>
    /// Classe d'affichage
    /// </summary>
    public partial class FrmMediatek : Form
    {
        #region Commun

        /// <summary>
        /// Contrôleur
        /// </summary>
        private readonly FrmMediatekController controller;

        /// <summary>
        /// BindingSource Genres
        /// </summary>
        private readonly BindingSource bdgGenres = new BindingSource();
        /// <summary>
        /// BindingSource Publics
        /// </summary>
        private readonly BindingSource bdgPublics = new BindingSource();
        /// <summary>
        /// BindingSource Rayons
        /// </summary>
        private readonly BindingSource bdgRayons = new BindingSource();

        /// <summary>
        /// Liste lesCommandes
        /// </summary>
        private List<Commande> lesCommandes = new List<Commande>();
        /// <summary>
        /// Liste lesCommandeDocuments
        /// </summary>
        private List<CommandeDocument> lesCommandeDocuments = new List<CommandeDocument>();
        /// <summary>
        /// Liste lesSuivis
        /// </summary>
        private List<Suivi> lesSuivis = new List<Suivi>();

        /// <summary>
        /// Niveau de droits:
        /// 0 -> pas d'accès
        /// 1 -> accès à la recherche
        /// 2 -> accès à la gestion
        /// </summary>
        private readonly int niveauDroits;

        /// <summary>
        /// Numéro introuvable
        /// </summary>
        private const string NUMERO_INTROUVABLE = "numéro introuvable";
        /// <summary>
        /// Erreur
        /// </summary>
        private const string ERREUR = "Erreur";
        /// <summary>
        /// Erreur permissions
        /// </summary>
        private const string ERREUR_PERMISSIONS = "Erreur : permissions insuffisantes.";
        /// <summary>
        /// En cours
        /// </summary>
        private const string EN_COURS = "En cours";
        /// <summary>
        /// Succès
        /// </summary>
        private const string SUCCES = "Succès";
        /// <summary>
        /// Livrée
        /// </summary>
        private const string LIVREE = "Livrée";
        /// <summary>
        /// Relancée
        /// </summary>
        private const string RELANCEE = "Relancée";
        /// <summary>
        /// Réglée
        /// </summary>
        private const string REGLEE = "Réglée";
        /// <summary>
        /// Confirmation
        /// </summary>
        private const string CONFIRMATION = "Confirmation";
        /// <summary>
        /// Format date
        /// </summary>
        private const string FORMAT_DATE = "dd/MM/yyyy";

        /// <summary>
        /// Constructeur : création du contrôleur lié à ce formulaire
        /// </summary>
        /// <param name="niveauDroits">Niveau de droits</param>
        public FrmMediatek(int niveauDroits)
        {
            InitializeComponent();
            this.controller = new FrmMediatekController();
            this.niveauDroits = niveauDroits;
            // si l'utilisateur a le droit de gestion, les derniers abonnements sont affichés
            if (niveauDroits == 2 && controller.GetDerniersAbonnements().Any())
            {
                FrmAlerteAbonnements fenetre = new FrmAlerteAbonnements();
                fenetre.ShowDialog();
            }
            // si l'utilisateur n'a pas le droit de gestion, désactive les onglets de gestion
            if (niveauDroits == 1)
            {
                tabGestionCommandesLivres.Enabled = false;
                tabGestionCommandesDvd.Enabled = false;
                tabGestionCommandesRevues.Enabled = false;
            }
        }

        /// <summary>
        /// Rempli un des 3 combo (genre, public, rayon)
        /// </summary>
        /// <param name="lesCategories">liste des objets de type Genre ou Public ou Rayon</param>
        /// <param name="bdg">bindingsource contenant les informations</param>
        /// <param name="cbx">combobox à remplir</param>
        public static void RemplirComboCategorie(List<Categorie> lesCategories, BindingSource bdg, ComboBox cbx)
        {
            bdg.DataSource = lesCategories;
            cbx.DataSource = bdg;
            if (cbx.Items.Count > 0)
            {
                cbx.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Convertit un id dans un format à 5 caractères
        /// Exemple: 1 -> 00001, 22 -> 00022
        /// </summary>
        /// <param name="id">L'id à convertir</param>
        /// <returns>L'id converti en format 5 caractères (string)</returns>
        public static string ConvertId(string id)
        {
            int c = 5 - id.Length;
            if (c > 0)
            {
                StringBuilder bld = new StringBuilder();
                for (int i = 0; i < c; i++)
                {
                    bld.Append("0");
                }
                bld.Append(id);
                return bld.ToString();
            }
            return id;
        }

        /// <summary>
        /// Vérifie si une date de parution est comprise entre sa date de commande et sa date de fin
        /// </summary>
        /// <param name="dateCommande">Date de la commande</param>
        /// <param name="dateExpiration">Date de fin de la commande</param>
        /// <param name="dateParution">Date de parution de l'exemplaire</param>
        /// <returns>Vrai si la date de parution est comprise entre sa date de commande et sa date de fin, Faux sinon</returns>
        public static bool ParutionDansAbonnement(DateTime dateCommande, DateTime dateExpiration, DateTime dateParution)
        {
            if (dateParution > dateCommande && dateParution < dateExpiration)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Vérifie si une date passée en format string peut-être convertie dans un certain format
        /// </summary>
        /// <param name="dateString">Date en format string</param>
        /// <param name="format">Format de la date</param>
        /// <returns>Vrai si la date peut être convertie, Faux sinon</returns>
        public static bool DateConversion(string dateString, string format)
        {
            try
            {
                DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Vérifie si un string peut être converti en int
        /// </summary>
        /// <param name="intString">String à convertir en int</param>
        /// <returns>Vrai si le string peut être converti, Faux sinon</returns>
        public static bool IntConversion(string intString)
        {
            return int.TryParse(intString, out _);
        }

        /// <summary>
        /// Vérifie si un string peut être converti en float
        /// </summary>
        /// <param name="floatString">String à convertir en float</param>
        /// <returns>Vrai si le string peut être converti, Faux sinon</returns>
        public static bool FloatConversion(string floatString)
        {
            return float.TryParse(floatString, out _);
        }
        #endregion

        #region Onglet Livres

        /// <summary>
        /// BindingSource Livres
        /// </summary>
        private readonly BindingSource bdgLivresListe = new BindingSource();
        /// <summary>
        /// Liste lesLivres
        /// </summary>
        private List<Livre> lesLivres = new List<Livre>();

        /// <summary>
        /// Ouverture de l'onglet Livres : 
        /// appel des méthodes pour remplir le datagrid des livres et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void TabLivres_Enter(object sender, EventArgs e)
        {
            lesLivres = controller.GetAllLivres();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxLivresGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxLivresPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxLivresRayons);
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">Liste de Livres</param>
        private void RemplirLivresListe(List<Livre> livres)
        {
            bdgLivresListe.DataSource = livres;
            dgvLivresListe.DataSource = bdgLivresListe;
            dgvLivresListe.Columns["isbn"].Visible = false;
            dgvLivresListe.Columns["idRayon"].Visible = false;
            dgvLivresListe.Columns["idGenre"].Visible = false;
            dgvLivresListe.Columns["idPublic"].Visible = false;
            dgvLivresListe.Columns["image"].Visible = false;
            dgvLivresListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvLivresListe.Columns["id"].DisplayIndex = 0;
            dgvLivresListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du livre dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void BtnLivresNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbLivresNumRecherche.Text.Equals(""))
            {
                txbLivresTitreRecherche.Text = "";
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                Livre livre = lesLivres.Find(x => x.Id.Equals(txbLivresNumRecherche.Text));
                if (livre != null)
                {
                    List<Livre> livres = new List<Livre>() { livre };
                    RemplirLivresListe(livres);
                }
                else
                {
                    MessageBox.Show(NUMERO_INTROUVABLE);
                    RemplirLivresListeComplete();
                }
            }
            else
            {
                RemplirLivresListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des livres dont le titre matche acec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void TxbLivresTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbLivresTitreRecherche.Text.Equals(""))
            {
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
                txbLivresNumRecherche.Text = "";
                List<Livre> lesLivresParTitre;
                lesLivresParTitre = lesLivres.FindAll(x => x.Titre.ToLower().Contains(txbLivresTitreRecherche.Text.ToLower()));
                RemplirLivresListe(lesLivresParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxLivresGenres.SelectedIndex < 0 && cbxLivresPublics.SelectedIndex < 0 && cbxLivresRayons.SelectedIndex < 0
                    && txbLivresNumRecherche.Text.Equals(""))
                {
                    RemplirLivresListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du Livre sélectionné
        /// </summary>
        /// <param name="livre">Le Livre</param>
        private void AfficheLivresInfos(Livre livre)
        {
            txbLivresAuteur.Text = livre.Auteur;
            txbLivresCollection.Text = livre.Collection;
            txbLivresImage.Text = livre.Image;
            txbLivresIsbn.Text = livre.Isbn;
            txbLivresNumero.Text = livre.Id;
            txbLivresGenre.Text = livre.Genre;
            txbLivresPublic.Text = livre.Public;
            txbLivresRayon.Text = livre.Rayon;
            txbLivresTitre.Text = livre.Titre;
            string image = livre.Image;
            try
            {
                pcbLivresImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbLivresImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du Livre
        /// </summary>
        private void VideLivresInfos()
        {
            txbLivresAuteur.Text = "";
            txbLivresCollection.Text = "";
            txbLivresImage.Text = "";
            txbLivresIsbn.Text = "";
            txbLivresNumero.Text = "";
            txbLivresGenre.Text = "";
            txbLivresPublic.Text = "";
            txbLivresRayon.Text = "";
            txbLivresTitre.Text = "";
            pcbLivresImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le Genre
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void CbxLivresGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresGenres.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Genre genre = (Genre)cbxLivresGenres.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la Catégorie de Public
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void CbxLivresPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresPublics.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Public lePublic = (Public)cbxLivresPublics.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirLivresListe(livres);
                cbxLivresRayons.SelectedIndex = -1;
                cbxLivresGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le Rayon
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void CbxLivresRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxLivresRayons.SelectedIndex >= 0)
            {
                txbLivresTitreRecherche.Text = "";
                txbLivresNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxLivresRayons.SelectedItem;
                List<Livre> livres = lesLivres.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirLivresListe(livres);
                cbxLivresGenres.SelectedIndex = -1;
                cbxLivresPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du Livre
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void DgvLivresListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvLivresListe.CurrentCell != null)
            {
                try
                {
                    Livre livre = (Livre)bdgLivresListe.List[bdgLivresListe.Position];
                    AfficheLivresInfos(livre);
                }
                catch
                {
                    VideLivresZones();
                }
            }
            else
            {
                VideLivresInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Livres
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void BtnLivresAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Livres
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void BtnLivresAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Livres
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void BtnLivresAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirLivresListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Livres
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirLivresListeComplete()
        {
            RemplirLivresListe(lesLivres);
            VideLivresZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideLivresZones()
        {
            cbxLivresGenres.SelectedIndex = -1;
            cbxLivresRayons.SelectedIndex = -1;
            cbxLivresPublics.SelectedIndex = -1;
            txbLivresNumRecherche.Text = "";
            txbLivresTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void DgvLivresListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideLivresZones();
            string titreColonne = dgvLivresListe.Columns[e.ColumnIndex].HeaderText;
            List<Livre> sortedList = new List<Livre>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesLivres.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesLivres.OrderBy(o => o.Titre).ToList();
                    break;
                case "Collection":
                    sortedList = lesLivres.OrderBy(o => o.Collection).ToList();
                    break;
                case "Auteur":
                    sortedList = lesLivres.OrderBy(o => o.Auteur).ToList();
                    break;
                case "Genre":
                    sortedList = lesLivres.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesLivres.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesLivres.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirLivresListe(sortedList);
        }

        #endregion

        #region Onglet Dvd

        /// <summary>
        /// BindingSource Dvds
        /// </summary>
        private readonly BindingSource bdgDvdListe = new BindingSource();
        /// <summary>
        /// Liste lesDvd
        /// </summary>
        private List<Dvd> lesDvd = new List<Dvd>();

        /// <summary>
        /// Ouverture de l'onglet Dvds : 
        /// appel des méthodes pour remplir le datagrid des dvd et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void tabDvd_Enter(object sender, EventArgs e)
        {
            lesDvd = controller.GetAllDvd();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxDvdGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxDvdPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxDvdRayons);
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Remplit le datagrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="Dvds">Liste de Dvds</param>
        private void RemplirDvdListe(List<Dvd> Dvds)
        {
            bdgDvdListe.DataSource = Dvds;
            dgvDvdListe.DataSource = bdgDvdListe;
            dgvDvdListe.Columns["idRayon"].Visible = false;
            dgvDvdListe.Columns["idGenre"].Visible = false;
            dgvDvdListe.Columns["idPublic"].Visible = false;
            dgvDvdListe.Columns["image"].Visible = false;
            dgvDvdListe.Columns["synopsis"].Visible = false;
            dgvDvdListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDvdListe.Columns["id"].DisplayIndex = 0;
            dgvDvdListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage du Dvd dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnDvdNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbDvdNumRecherche.Text.Equals(""))
            {
                txbDvdTitreRecherche.Text = "";
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(txbDvdNumRecherche.Text));
                if (dvd != null)
                {
                    List<Dvd> Dvd = new List<Dvd>() { dvd };
                    RemplirDvdListe(Dvd);
                }
                else
                {
                    MessageBox.Show(NUMERO_INTROUVABLE);
                    RemplirDvdListeComplete();
                }
            }
            else
            {
                RemplirDvdListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des Dvd dont le titre matche avec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void txbDvdTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbDvdTitreRecherche.Text.Equals(""))
            {
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
                txbDvdNumRecherche.Text = "";
                List<Dvd> lesDvdParTitre;
                lesDvdParTitre = lesDvd.FindAll(x => x.Titre.ToLower().Contains(txbDvdTitreRecherche.Text.ToLower()));
                RemplirDvdListe(lesDvdParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxDvdGenres.SelectedIndex < 0 && cbxDvdPublics.SelectedIndex < 0 && cbxDvdRayons.SelectedIndex < 0
                    && txbDvdNumRecherche.Text.Equals(""))
                {
                    RemplirDvdListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations du Dvd sélectionné
        /// </summary>
        /// <param name="dvd">Le Dvd</param>
        private void AfficheDvdInfos(Dvd dvd)
        {
            txbDvdRealisateur.Text = dvd.Realisateur;
            txbDvdSynopsis.Text = dvd.Synopsis;
            txbDvdImage.Text = dvd.Image;
            txbDvdDuree.Text = dvd.Duree.ToString();
            txbDvdNumero.Text = dvd.Id;
            txbDvdGenre.Text = dvd.Genre;
            txbDvdPublic.Text = dvd.Public;
            txbDvdRayon.Text = dvd.Rayon;
            txbDvdTitre.Text = dvd.Titre;
            string image = dvd.Image;
            try
            {
                pcbDvdImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbDvdImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations du Dvd
        /// </summary>
        private void VideDvdInfos()
        {
            txbDvdRealisateur.Text = "";
            txbDvdSynopsis.Text = "";
            txbDvdImage.Text = "";
            txbDvdDuree.Text = "";
            txbDvdNumero.Text = "";
            txbDvdGenre.Text = "";
            txbDvdPublic.Text = "";
            txbDvdRayon.Text = "";
            txbDvdTitre.Text = "";
            pcbDvdImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le Genre
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxDvdGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdGenres.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Genre genre = (Genre)cbxDvdGenres.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la Catégorie de Public
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxDvdPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdPublics.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Public lePublic = (Public)cbxDvdPublics.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdRayons.SelectedIndex = -1;
                cbxDvdGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le Rayon
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxDvdRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxDvdRayons.SelectedIndex >= 0)
            {
                txbDvdTitreRecherche.Text = "";
                txbDvdNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxDvdRayons.SelectedItem;
                List<Dvd> Dvd = lesDvd.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirDvdListe(Dvd);
                cbxDvdGenres.SelectedIndex = -1;
                cbxDvdPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations du Dvd
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvDvdListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDvdListe.CurrentCell != null)
            {
                try
                {
                    Dvd dvd = (Dvd)bdgDvdListe.List[bdgDvdListe.Position];
                    AfficheDvdInfos(dvd);
                }
                catch
                {
                    VideDvdZones();
                }
            }
            else
            {
                VideDvdInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvds
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnDvdAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvds
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnDvdAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Dvds
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnDvdAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirDvdListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Dvds
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirDvdListeComplete()
        {
            RemplirDvdListe(lesDvd);
            VideDvdZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideDvdZones()
        {
            cbxDvdGenres.SelectedIndex = -1;
            cbxDvdRayons.SelectedIndex = -1;
            cbxDvdPublics.SelectedIndex = -1;
            txbDvdNumRecherche.Text = "";
            txbDvdTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvDvdListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideDvdZones();
            string titreColonne = dgvDvdListe.Columns[e.ColumnIndex].HeaderText;
            List<Dvd> sortedList = new List<Dvd>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesDvd.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesDvd.OrderBy(o => o.Titre).ToList();
                    break;
                case "Duree":
                    sortedList = lesDvd.OrderBy(o => o.Duree).ToList();
                    break;
                case "Realisateur":
                    sortedList = lesDvd.OrderBy(o => o.Realisateur).ToList();
                    break;
                case "Genre":
                    sortedList = lesDvd.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesDvd.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesDvd.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirDvdListe(sortedList);
        }
        #endregion

        #region Onglet Revues

        /// <summary>
        /// BindingSource Revue
        /// </summary>
        private readonly BindingSource bdgRevuesListe = new BindingSource();
        /// <summary>
        /// Liste lesRevues
        /// </summary>
        private List<Revue> lesRevues = new List<Revue>();

        /// <summary>
        /// Ouverture de l'onglet Revues : 
        /// appel des méthodes pour remplir le datagrid des revues et des combos (genre, rayon, public)
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void tabRevues_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            RemplirComboCategorie(controller.GetAllGenres(), bdgGenres, cbxRevuesGenres);
            RemplirComboCategorie(controller.GetAllPublics(), bdgPublics, cbxRevuesPublics);
            RemplirComboCategorie(controller.GetAllRayons(), bdgRayons, cbxRevuesRayons);
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Remplit le dategrid avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues">Liste de Revues</param>
        private void RemplirRevuesListe(List<Revue> revues)
        {
            bdgRevuesListe.DataSource = revues;
            dgvRevuesListe.DataSource = bdgRevuesListe;
            dgvRevuesListe.Columns["idRayon"].Visible = false;
            dgvRevuesListe.Columns["idGenre"].Visible = false;
            dgvRevuesListe.Columns["idPublic"].Visible = false;
            dgvRevuesListe.Columns["image"].Visible = false;
            dgvRevuesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvRevuesListe.Columns["id"].DisplayIndex = 0;
            dgvRevuesListe.Columns["titre"].DisplayIndex = 1;
        }

        /// <summary>
        /// Recherche et affichage de la Revue dont on a saisi le numéro.
        /// Si non trouvé, affichage d'un MessageBox.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnRevuesNumRecherche_Click(object sender, EventArgs e)
        {
            if (!txbRevuesNumRecherche.Text.Equals(""))
            {
                txbRevuesTitreRecherche.Text = "";
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbRevuesNumRecherche.Text));
                if (revue != null)
                {
                    List<Revue> revues = new List<Revue>() { revue };
                    RemplirRevuesListe(revues);
                }
                else
                {
                    MessageBox.Show(NUMERO_INTROUVABLE);
                    RemplirRevuesListeComplete();
                }
            }
            else
            {
                RemplirRevuesListeComplete();
            }
        }

        /// <summary>
        /// Recherche et affichage des revues dont le titre matche avec la saisie.
        /// Cette procédure est exécutée à chaque ajout ou suppression de caractère
        /// dans le textBox de saisie.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void txbRevuesTitreRecherche_TextChanged(object sender, EventArgs e)
        {
            if (!txbRevuesTitreRecherche.Text.Equals(""))
            {
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
                txbRevuesNumRecherche.Text = "";
                List<Revue> lesRevuesParTitre;
                lesRevuesParTitre = lesRevues.FindAll(x => x.Titre.ToLower().Contains(txbRevuesTitreRecherche.Text.ToLower()));
                RemplirRevuesListe(lesRevuesParTitre);
            }
            else
            {
                // si la zone de saisie est vide et aucun élément combo sélectionné, réaffichage de la liste complète
                if (cbxRevuesGenres.SelectedIndex < 0 && cbxRevuesPublics.SelectedIndex < 0 && cbxRevuesRayons.SelectedIndex < 0
                    && txbRevuesNumRecherche.Text.Equals(""))
                {
                    RemplirRevuesListeComplete();
                }
            }
        }

        /// <summary>
        /// Affichage des informations de la Revue sélectionnée
        /// </summary>
        /// <param name="revue">La Revue</param>
        private void AfficheRevuesInfos(Revue revue)
        {
            txbRevuesPeriodicite.Text = revue.Periodicite;
            txbRevuesImage.Text = revue.Image;
            txbRevuesDateMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbRevuesNumero.Text = revue.Id;
            txbRevuesGenre.Text = revue.Genre;
            txbRevuesPublic.Text = revue.Public;
            txbRevuesRayon.Text = revue.Rayon;
            txbRevuesTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbRevuesImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbRevuesImage.Image = null;
            }
        }

        /// <summary>
        /// Vide les zones d'affichage des informations de la Revue
        /// </summary>
        private void VideRevuesInfos()
        {
            txbRevuesPeriodicite.Text = "";
            txbRevuesImage.Text = "";
            txbRevuesDateMiseADispo.Text = "";
            txbRevuesNumero.Text = "";
            txbRevuesGenre.Text = "";
            txbRevuesPublic.Text = "";
            txbRevuesRayon.Text = "";
            txbRevuesTitre.Text = "";
            pcbRevuesImage.Image = null;
        }

        /// <summary>
        /// Filtre sur le Genre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxRevuesGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesGenres.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Genre genre = (Genre)cbxRevuesGenres.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Genre.Equals(genre.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur la catégorie de Public
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxRevuesPublics_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesPublics.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Public lePublic = (Public)cbxRevuesPublics.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Public.Equals(lePublic.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesRayons.SelectedIndex = -1;
                cbxRevuesGenres.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Filtre sur le Rayon
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxRevuesRayons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRevuesRayons.SelectedIndex >= 0)
            {
                txbRevuesTitreRecherche.Text = "";
                txbRevuesNumRecherche.Text = "";
                Rayon rayon = (Rayon)cbxRevuesRayons.SelectedItem;
                List<Revue> revues = lesRevues.FindAll(x => x.Rayon.Equals(rayon.Libelle));
                RemplirRevuesListe(revues);
                cbxRevuesGenres.SelectedIndex = -1;
                cbxRevuesPublics.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Sur la sélection d'une ligne ou cellule dans le grid
        /// affichage des informations de la Revue
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvRevuesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRevuesListe.CurrentCell != null)
            {
                try
                {
                    Revue revue = (Revue)bdgRevuesListe.List[bdgRevuesListe.Position];
                    AfficheRevuesInfos(revue);
                }
                catch
                {
                    VideRevuesZones();
                }
            }
            else
            {
                VideRevuesInfos();
            }
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Revues
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnRevuesAnnulPublics_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Revues
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnRevuesAnnulRayons_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Sur le clic du bouton d'annulation, affichage de la liste complète des Revues
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnRevuesAnnulGenres_Click(object sender, EventArgs e)
        {
            RemplirRevuesListeComplete();
        }

        /// <summary>
        /// Affichage de la liste complète des Revues
        /// et annulation de toutes les recherches et filtres
        /// </summary>
        private void RemplirRevuesListeComplete()
        {
            RemplirRevuesListe(lesRevues);
            VideRevuesZones();
        }

        /// <summary>
        /// vide les zones de recherche et de filtre
        /// </summary>
        private void VideRevuesZones()
        {
            cbxRevuesGenres.SelectedIndex = -1;
            cbxRevuesRayons.SelectedIndex = -1;
            cbxRevuesPublics.SelectedIndex = -1;
            txbRevuesNumRecherche.Text = "";
            txbRevuesTitreRecherche.Text = "";
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvRevuesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            VideRevuesZones();
            string titreColonne = dgvRevuesListe.Columns[e.ColumnIndex].HeaderText;
            List<Revue> sortedList = new List<Revue>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesRevues.OrderBy(o => o.Id).ToList();
                    break;
                case "Titre":
                    sortedList = lesRevues.OrderBy(o => o.Titre).ToList();
                    break;
                case "Periodicite":
                    sortedList = lesRevues.OrderBy(o => o.Periodicite).ToList();
                    break;
                case "DelaiMiseADispo":
                    sortedList = lesRevues.OrderBy(o => o.DelaiMiseADispo).ToList();
                    break;
                case "Genre":
                    sortedList = lesRevues.OrderBy(o => o.Genre).ToList();
                    break;
                case "Public":
                    sortedList = lesRevues.OrderBy(o => o.Public).ToList();
                    break;
                case "Rayon":
                    sortedList = lesRevues.OrderBy(o => o.Rayon).ToList();
                    break;
            }
            RemplirRevuesListe(sortedList);
        }
        #endregion

        #region Onglet Paarutions

        /// <summary>
        /// BindingSource Exemplaire
        /// </summary>
        private readonly BindingSource bdgExemplairesListe = new BindingSource();
        /// <summary>
        /// Liste lesExemplaires
        /// </summary>
        private List<Exemplaire> lesExemplaires = new List<Exemplaire>();
        /// <summary>
        /// Etat neuf
        /// </summary>
        const string ETATNEUF = "00001";

        /// <summary>
        /// Ouverture de l'onglet : récupère le revues et vide tous les champs.
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void tabReceptionRevue_Enter(object sender, EventArgs e)
        {
            lesRevues = controller.GetAllRevues();
            txbReceptionRevueNumero.Text = "";
        }

        /// <summary>
        /// Remplit le dategrid des Rxemplaires avec la liste reçue en paramètre
        /// </summary>
        /// <param name="exemplaires">Liste d'Exemplaires</param>
        private void RemplirReceptionExemplairesListe(List<Exemplaire> exemplaires)
        {
            if (exemplaires != null)
            {
                bdgExemplairesListe.DataSource = exemplaires;
                dgvReceptionExemplairesListe.DataSource = bdgExemplairesListe;
                dgvReceptionExemplairesListe.Columns["idEtat"].Visible = false;
                dgvReceptionExemplairesListe.Columns["id"].Visible = false;
                dgvReceptionExemplairesListe.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvReceptionExemplairesListe.Columns["numero"].DisplayIndex = 0;
                dgvReceptionExemplairesListe.Columns["dateAchat"].DisplayIndex = 1;
            }
            else
            {
                bdgExemplairesListe.DataSource = null;
            }
        }

        /// <summary>
        /// Recherche d'un numéro de Revue et affiche ses informations
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnReceptionRechercher_Click(object sender, EventArgs e)
        {
            if (!txbReceptionRevueNumero.Text.Equals(""))
            {
                Revue revue = lesRevues.Find(x => x.Id.Equals(txbReceptionRevueNumero.Text));
                if (revue != null)
                {
                    AfficheReceptionRevueInfos(revue);
                }
                else
                {
                    MessageBox.Show(NUMERO_INTROUVABLE);
                }
            }
        }

        /// <summary>
        /// Si le numéro de Revue est modifié, la zone de l'Exemplaire est vidée et inactive
        /// les informations de la Revue sont aussi effacées
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void txbReceptionRevueNumero_TextChanged(object sender, EventArgs e)
        {
            txbReceptionRevuePeriodicite.Text = "";
            txbReceptionRevueImage.Text = "";
            txbReceptionRevueDelaiMiseADispo.Text = "";
            txbReceptionRevueGenre.Text = "";
            txbReceptionRevuePublic.Text = "";
            txbReceptionRevueRayon.Text = "";
            txbReceptionRevueTitre.Text = "";
            pcbReceptionRevueImage.Image = null;
            RemplirReceptionExemplairesListe(null);
            AccesReceptionExemplaireGroupBox(false);
        }

        /// <summary>
        /// Affichage des informations de la Revue sélectionnée et les Exemplaires
        /// </summary>
        /// <param name="revue">La Revue</param>
        private void AfficheReceptionRevueInfos(Revue revue)
        {
            // informations sur la revue
            txbReceptionRevuePeriodicite.Text = revue.Periodicite;
            txbReceptionRevueImage.Text = revue.Image;
            txbReceptionRevueDelaiMiseADispo.Text = revue.DelaiMiseADispo.ToString();
            txbReceptionRevueNumero.Text = revue.Id;
            txbReceptionRevueGenre.Text = revue.Genre;
            txbReceptionRevuePublic.Text = revue.Public;
            txbReceptionRevueRayon.Text = revue.Rayon;
            txbReceptionRevueTitre.Text = revue.Titre;
            string image = revue.Image;
            try
            {
                pcbReceptionRevueImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbReceptionRevueImage.Image = null;
            }
            // affiche la liste des Exemplaires de la Revue
            AfficheReceptionExemplairesRevue();
        }

        /// <summary>
        /// Récupère et affiche les Exemplaires d'une Revue
        /// </summary>
        private void AfficheReceptionExemplairesRevue()
        {
            string idDocuement = txbReceptionRevueNumero.Text;
            lesExemplaires = controller.GetExemplairesRevue(idDocuement);
            RemplirReceptionExemplairesListe(lesExemplaires);
            AccesReceptionExemplaireGroupBox(true);
        }

        /// <summary>
        /// Permet ou interdit l'accès à la gestion de la réception d'un Exemplaire
        /// et vide les objets graphiques
        /// </summary>
        /// <param name="acces">True ou False</param>
        private void AccesReceptionExemplaireGroupBox(bool acces)
        {
            grpReceptionExemplaire.Enabled = acces;
            txbReceptionExemplaireImage.Text = "";
            txbReceptionExemplaireNumero.Text = "";
            pcbReceptionExemplaireImage.Image = null;
            dtpReceptionExemplaireDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Recherche image sur disque (pour l'exemplaire à insérer)
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnReceptionExemplaireImage_Click(object sender, EventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                // positionnement à la racine du disque où se trouve le dossier actuel
                InitialDirectory = Path.GetPathRoot(Environment.CurrentDirectory),
                Filter = "Files|*.jpg;*.bmp;*.jpeg;*.png;*.gif"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
            }
            txbReceptionExemplaireImage.Text = filePath;
            try
            {
                pcbReceptionExemplaireImage.Image = Image.FromFile(filePath);
            }
            catch
            {
                pcbReceptionExemplaireImage.Image = null;
            }
        }

        /// <summary>
        /// Enregistrement du nouvel Exemplaire
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnReceptionExemplaireValider_Click(object sender, EventArgs e)
        {
            if (!txbReceptionExemplaireNumero.Text.Equals(""))
            {
                try
                {
                    int numero = int.Parse(txbReceptionExemplaireNumero.Text);
                    DateTime dateAchat = dtpReceptionExemplaireDate.Value;
                    string photo = txbReceptionExemplaireImage.Text;
                    string idEtat = ETATNEUF;
                    string idDocument = txbReceptionRevueNumero.Text;
                    Exemplaire exemplaire = new Exemplaire(numero, dateAchat, photo, idEtat, idDocument);
                    if (controller.CreerExemplaire(exemplaire))
                    {
                        AfficheReceptionExemplairesRevue();
                    }
                    else
                    {
                        MessageBox.Show("numéro de publication déjà existant", ERREUR);
                    }
                }
                catch
                {
                    MessageBox.Show("le numéro de parution doit être numérique", "Information");
                    txbReceptionExemplaireNumero.Text = "";
                    txbReceptionExemplaireNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("numéro de parution obligatoire", "Information");
            }
        }

        /// <summary>
        /// Tri sur une colonne
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvExemplairesListe_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string titreColonne = dgvReceptionExemplairesListe.Columns[e.ColumnIndex].HeaderText;
            List<Exemplaire> sortedList = new List<Exemplaire>();
            switch (titreColonne)
            {
                case "Numero":
                    sortedList = lesExemplaires.OrderBy(o => o.Numero).Reverse().ToList();
                    break;
                case "DateAchat":
                    sortedList = lesExemplaires.OrderBy(o => o.DateAchat).Reverse().ToList();
                    break;
                case "Photo":
                    sortedList = lesExemplaires.OrderBy(o => o.Photo).ToList();
                    break;
            }
            RemplirReceptionExemplairesListe(sortedList);
        }

        /// <summary>
        /// Affichage de l'image de l'exemplaire suite à la sélection d'un exemplaire dans la liste
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvReceptionExemplairesListe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvReceptionExemplairesListe.CurrentCell != null)
            {
                Exemplaire exemplaire = (Exemplaire)bdgExemplairesListe.List[bdgExemplairesListe.Position];
                string image = exemplaire.Photo;
                try
                {
                    pcbReceptionExemplaireRevueImage.Image = Image.FromFile(image);
                }
                catch
                {
                    pcbReceptionExemplaireRevueImage.Image = null;
                }
            }
            else
            {
                pcbReceptionExemplaireRevueImage.Image = null;
            }
        }
        #endregion

        #region Onglet Gestion Commandes Livres

        /// <summary>
        /// BindingSource Commandes Livres
        /// </summary>
        private readonly BindingSource bdgLivresListeCommandes = new BindingSource();

        /// <summary>
        /// Récupère les listes nécessaires et remplit le ComboBox lors de l'affichage de l'onglet
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void tabGestionCommandesLivres_Enter(object sender, EventArgs e)
        {
            cbxGCLNumeroDocument.Text = "";
            cbxGCLNumeroDocument.Items.Clear();
            lesLivres = controller.GetAllLivres();
            lesCommandes = controller.GetAllCommandes();
            lesSuivis = controller.GetAllSuivis();
            RemplirCbxNumeroDocument(lesLivres);
        }

        /// <summary>
        /// Remplit le ComboBox avec la liste reçue en paramètre
        /// </summary>
        /// <param name="livres">Liste de Livres</param>
        private void RemplirCbxNumeroDocument(List<Livre> livres)
        {
            List<Livre> sortedList = livres.OrderBy(o => o.Id).ToList();
            foreach (Livre livre in sortedList)
            {
                cbxGCLNumeroDocument.Items.Add(livre.Id);
            }
        }

        /// <summary>
        /// Lors de la sélection d'un index dans le ComboBox, remplit avec les infos du Livre, désactive par défaut les éléments liés à la modification / suppression 
        /// et tente de charger l'image
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxNumeroDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Livre livre = lesLivres.Find(x => x.Id.Equals(cbxGCLNumeroDocument.SelectedItem));
            txbGCLISBN.Text = livre.Isbn.ToString();
            txbGCLTitre.Text = livre.Titre.ToString();
            txbGCLAuteur.Text = livre.Auteur.ToString();
            txbGCLCollection.Text = livre.Collection.ToString();
            txbGCLGenre.Text = livre.Genre.ToString();
            txbGCLPublic.Text = livre.Public.ToString();
            txbGCLRayon.Text = livre.Rayon.ToString();
            txbGCLCheminImg.Text = livre.Image.ToString();
            lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(livre.Id);
            RemplirLivresListeCommandes(lesCommandeDocuments);
            cbxGCLStatut.SelectedIndex = -1;
            cbxGCLStatut.Items.Clear();
            lblGCLStatut.Enabled = false;
            cbxGCLStatut.Enabled = false;
            btnGCLModifierStatut.Enabled = false;
            btnCGLSupprimerCommande.Enabled = false;
            string image = livre.Image;
            try
            {
                pcbGCLImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbGCLImage.Image = null;
            }
        }

        /// <summary>
        /// Remplit le DataGridView avec la liste reçue en paramètre
        /// </summary>
        /// <param name="commandeDocuments">Liste de CommandeDocuments</param>
        private void RemplirLivresListeCommandes(List<CommandeDocument> commandeDocuments)
        {
            bdgLivresListeCommandes.DataSource = commandeDocuments;
            dgvCGLCommandesLivre.DataSource = bdgLivresListeCommandes;
            dgvCGLCommandesLivre.Columns["IdLivreDVD"].Visible = false;
            dgvCGLCommandesLivre.Columns["IdSuivi"].Visible = false;
            dgvCGLCommandesLivre.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            cbxGCLStatut.SelectedIndex = -1;
            cbxGCLStatut.Items.Clear();
            dgvCGLCommandesLivre.ClearSelection();
            lblGCLStatut.Enabled = false;
            cbxGCLStatut.Enabled = false;
            btnGCLModifierStatut.Enabled = false;
            btnCGLSupprimerCommande.Enabled = false;
        }

        /// <summary>
        /// Lors du clic sur le bouton Nouvelle commande, vérifie les informations, puis crée une Commande, le Commandedocument et le Suivi liés
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnGCLNouvelleCommande_Click(object sender, EventArgs e)
        {
            Livre livre = lesLivres.Find(x => x.Id.Equals(cbxGCLNumeroDocument.SelectedItem));
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (livre == null)
            {
                MessageBox.Show("Erreur : veuillez saisir un Numéro de document valide.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txbGCLDateCommande.Text == "" || txbGCLMontant.Text == "" || txbGCLNbExemplaire.Text == "")
            {
                MessageBox.Show("Erreur : veuillez remplir tous les champs.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!DateConversion(txbGCLDateCommande.Text, FORMAT_DATE))
            {
                MessageBox.Show("Erreur : le format de la date est incorrect (attendu: JJ/MM/AAAA).", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!FloatConversion(txbGCLMontant.Text))
            {
                MessageBox.Show("Erreur : le montant entré est incorrect.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IntConversion(txbGCLNbExemplaire.Text))
            {
                MessageBox.Show("Erreur : le nombre d'exemplaires entré est incorrect.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int valeur = 0;
            if (lesCommandes.Count != 0)
            {
                valeur = int.Parse(lesCommandes[lesCommandes.Count - 1].Id) + 1;
            }
            Commande commande = new Commande(ConvertId(valeur.ToString()), DateTime.ParseExact(txbGCLDateCommande.Text, FORMAT_DATE, CultureInfo.InvariantCulture), int.Parse(txbGCLMontant.Text));
            controller.CreerCommande(commande);
            int valeur2 = 0;
            if (lesSuivis.Count != 0)
            {
                valeur2 = int.Parse(lesSuivis[lesSuivis.Count - 1].Id) + 1;
            }
            Suivi suivi = new Suivi(ConvertId(valeur2.ToString()), EN_COURS);
            controller.CreerSuivi(suivi);
            CommandeDocument commandeDocument = new CommandeDocument(ConvertId(valeur.ToString()), livre.Id, ConvertId(valeur2.ToString()), DateTime.ParseExact(txbGCLDateCommande.Text, FORMAT_DATE, CultureInfo.InvariantCulture), int.Parse(txbGCLMontant.Text), int.Parse(txbGCLNbExemplaire.Text), null);
            controller.CreerCommandeDocumentsLivreDvd(commandeDocument);
            MessageBox.Show("La commande a été ajoutée avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
            lesCommandes = controller.GetAllCommandes();
            lesSuivis = controller.GetAllSuivis();
            lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(livre.Id);
            RemplirLivresListeCommandes(lesCommandeDocuments);
        }

        /// <summary>
        /// Lors de la sélection d'une ligne, modifie les options possibles pour la modification et suppression en fonction du statut
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvCGLCommandesLivre_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cbxGCLStatut.SelectedIndex = -1;
            cbxGCLStatut.Items.Clear();
            DataGridViewRow selectedRow = dgvCGLCommandesLivre.Rows[e.RowIndex];
            if (selectedRow.Cells[0].Value != null)
            {
                string contenu = selectedRow.Cells[6].Value.ToString();
                switch (contenu)
                {
                    case EN_COURS:
                        lblGCLStatut.Enabled = true;
                        cbxGCLStatut.Enabled = true;
                        btnGCLModifierStatut.Enabled = true;
                        cbxGCLStatut.Items.Add(EN_COURS);
                        cbxGCLStatut.Items.Add(LIVREE);
                        cbxGCLStatut.Items.Add(RELANCEE);
                        cbxGCLStatut.SelectedIndex = 0;
                        btnCGLSupprimerCommande.Enabled = true;
                        break;
                    case LIVREE:
                        lblGCLStatut.Enabled = true;
                        cbxGCLStatut.Enabled = true;
                        btnGCLModifierStatut.Enabled = true;
                        cbxGCLStatut.Items.Add(LIVREE);
                        cbxGCLStatut.Items.Add(REGLEE);
                        cbxGCLStatut.SelectedIndex = 0;
                        btnCGLSupprimerCommande.Enabled = false;
                        break;
                    case RELANCEE:
                        lblGCLStatut.Enabled = true;
                        cbxGCLStatut.Enabled = true;
                        btnGCLModifierStatut.Enabled = true;
                        cbxGCLStatut.Items.Add(RELANCEE);
                        cbxGCLStatut.Items.Add(EN_COURS);
                        cbxGCLStatut.Items.Add(LIVREE);
                        cbxGCLStatut.SelectedIndex = 0;
                        btnCGLSupprimerCommande.Enabled = true;
                        break;
                    case REGLEE:
                        lblGCLStatut.Enabled = false;
                        cbxGCLStatut.Enabled = false;
                        btnGCLModifierStatut.Enabled = false;
                        btnCGLSupprimerCommande.Enabled = true;
                        break;
                    default:
                        lblGCLStatut.Enabled = false;
                        cbxGCLStatut.Enabled = false;
                        btnGCLModifierStatut.Enabled = false;
                        btnCGLSupprimerCommande.Enabled = false;
                        break;
                }
            }

        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvCGLCommandesLivre_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Livre livre = lesLivres.Find(x => x.Id.Equals(cbxGCLNumeroDocument.SelectedItem));
            lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(livre.Id);
            string titreColonne = dgvCGLCommandesLivre.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.Id).ToList();
                    break;
                case "DateCommande":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.Montant).ToList();
                    break;
                case "NbExemplaire":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Statut":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.Statut).ToList();
                    break;
            }
            RemplirLivresListeCommandes(sortedList);
        }

        /// <summary>
        /// Lors du clic sur le bouton Modifier le statut, modifie le statut du Suivi selon la sélection dans le ComboBox
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnGCLModifierStatut_Click(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult confirmation = MessageBox.Show("Etes-vous sûr de vouloir modifier le statut de cette commande? (" + dgvCGLCommandesLivre.CurrentRow.Cells[6].Value.ToString() + " -> " + cbxGCLStatut.SelectedItem.ToString() + ")", CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                Suivi leSuivi = lesSuivis.Find(x => x.Id.Equals(dgvCGLCommandesLivre.CurrentRow.Cells[2].Value));
                leSuivi.Statut = cbxGCLStatut.SelectedItem.ToString();
                controller.ModifierSuivi(leSuivi);
                MessageBox.Show("Le statut de la commande a été modifié avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Livre livre = lesLivres.Find(x => x.Id.Equals(cbxGCLNumeroDocument.SelectedItem));
                lesCommandes = controller.GetAllCommandes();
                lesSuivis = controller.GetAllSuivis();
                lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(livre.Id);
                RemplirLivresListeCommandes(lesCommandeDocuments);
            }
        }

        /// <summary>
        /// Lors du clic sur le bouton Supprimer la commande, supprime la Commande, le CommandeDocument et le Suivi lié
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnCGLSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxGCLNumeroDocument.SelectedItem is null){
                MessageBox.Show("Erreur, veuillez sélectionner un numéro de document.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvCGLCommandesLivre.CurrentRow.Cells[6].Value.ToString() != LIVREE)
            {
                DialogResult confirmation = MessageBox.Show("Etes-vous sûr de vouloir supprimer cette commande?", CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmation == DialogResult.Yes)
                {
                    Commande laCommande = lesCommandes.Find(x => x.Id.Equals(dgvCGLCommandesLivre.CurrentRow.Cells[0].Value));
                    CommandeDocument leCommandeDocument = controller.GetCommandeDocumentsLivreDvd(cbxGCLNumeroDocument.SelectedItem.ToString())[0];
                    controller.SupprimerCommande(laCommande);
                    controller.SupprimerSuivi(new Suivi(leCommandeDocument.IdSuivi, null));
                    MessageBox.Show("La commande a été supprimée avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Livre livre = lesLivres.Find(x => x.Id.Equals(cbxGCLNumeroDocument.SelectedItem));
                    lesCommandes = controller.GetAllCommandes();
                    lesSuivis = controller.GetAllSuivis();
                    lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(livre.Id);
                    RemplirLivresListeCommandes(lesCommandeDocuments);
                }
            }
        }

        #endregion

        #region Onglet Gestion Commandes Dvd

        /// <summary>
        /// BindingSource Commandes
        /// </summary>
        private readonly BindingSource bdgDvdsListeCommandes = new BindingSource();

        /// <summary>
        /// Récupère les listes nécessaires et remplit le ComboBox lors de l'affichage de l'onglet
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void tabGestionCommandesDvd_Enter(object sender, EventArgs e)
        {
            cbxGCDNumeroDocument.Text = "";
            cbxGCDNumeroDocument.Items.Clear();
            lesDvd = controller.GetAllDvd();
            lesCommandes = controller.GetAllCommandes();
            lesSuivis = controller.GetAllSuivis();
            RemplirCbxDvdNumeroDocument(lesDvd);
        }

        /// <summary>
        /// Remplit le ComboBox avec la liste reçue en paramètre
        /// </summary>
        /// <param name="dvds">Liste de Dvds</param>
        private void RemplirCbxDvdNumeroDocument(List<Dvd> dvds)
        {
            List<Dvd> sortedList = dvds.OrderBy(o => o.Id).ToList();
            foreach (Dvd dvd in sortedList)
            {
                cbxGCDNumeroDocument.Items.Add(dvd.Id);
            }
        }

        /// <summary>
        /// Lors de la sélection d'un index dans le ComboBox, remplit avec les infos du Dvd, désactive par défaut les éléments liés à la modification / suppression 
        /// et tente de charger l'image
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxGCDNumeroDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Dvd dvd = lesDvd.Find(x => x.Id.Equals(cbxGCDNumeroDocument.SelectedItem));
            txbGCDDuree.Text = dvd.Duree.ToString();
            txbGCDTitre.Text = dvd.Titre.ToString();
            txbGCDRealisateur.Text = dvd.Realisateur.ToString();
            txbGCDSynopsis.Text = dvd.Synopsis.ToString();
            txbGCDGenre.Text = dvd.Genre.ToString();
            txbGCDPublic.Text = dvd.Public.ToString();
            txbGCDRayon.Text = dvd.Rayon.ToString();
            txbGCDCheminImg.Text = dvd.Image.ToString();
            lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(dvd.Id);
            RemplirDvdsListeCommandes(lesCommandeDocuments);
            cbxGCDStatut.SelectedIndex = -1;
            cbxGCDStatut.Items.Clear();
            lblGCDStatut.Enabled = false;
            cbxGCDStatut.Enabled = false;
            btnGCDModifierStatut.Enabled = false;
            btnGCDSupprimerCommande.Enabled = false;
            string image = dvd.Image;
            try
            {
                pcbGCDImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbGCDImage.Image = null;
            }
        }

        /// <summary>
        /// Remplit le DataGridView avec la liste reçue en paramètre avec les CommandeDocument
        /// </summary>
        /// <param name="commandeDocuments">Liste de CommandeDocuments</param>
        private void RemplirDvdsListeCommandes(List<CommandeDocument> commandeDocuments)
        {
            bdgDvdsListeCommandes.DataSource = commandeDocuments;
            dgvCGDCommandesDvd.DataSource = bdgDvdsListeCommandes;
            dgvCGDCommandesDvd.Columns["IdLivreDVD"].Visible = false;
            dgvCGDCommandesDvd.Columns["IdSuivi"].Visible = false;
            dgvCGDCommandesDvd.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            cbxGCDStatut.SelectedIndex = -1;
            cbxGCDStatut.Items.Clear();
            dgvCGDCommandesDvd.ClearSelection();
            lblGCDStatut.Enabled = false;
            cbxGCDStatut.Enabled = false;
            btnGCDModifierStatut.Enabled = false;
            btnGCDSupprimerCommande.Enabled = false;
        }

        /// <summary>
        /// Lors du clic sur le bouton Nouvelle commande, vérifie les informations, puis crée une Commande, le Commandedocument et le Suivi liés
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnGCDNouvelleCommande_Click(object sender, EventArgs e)
        {
            Dvd dvd = lesDvd.Find(x => x.Id.Equals(cbxGCDNumeroDocument.SelectedItem));
            if (dvd == null)
            {
                MessageBox.Show("Erreur : veuillez saisir un Numéro de document valide.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txbGCDDateCommande.Text == "" || txbGCDMontant.Text == "" || txbGCDNbExemplaire.Text == "")
            {
                MessageBox.Show("Erreur : veuillez remplir tous les champs.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!DateConversion(txbGCDDateCommande.Text, FORMAT_DATE))
            {
                MessageBox.Show("Erreur : le format de la date est incorrect (attendu: JJ/MM/AAAA).", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!FloatConversion(txbGCDMontant.Text))
            {
                MessageBox.Show("Erreur : le montant entré est incorrect.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!IntConversion(txbGCDNbExemplaire.Text))
            {
                MessageBox.Show("Erreur : le nombre d'exemplaires entré est incorrect.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int valeur = 0;
            if (lesCommandes.Count != 0)
            {
                valeur = int.Parse(lesCommandes[lesCommandes.Count - 1].Id) + 1;
            }
            Commande commande = new Commande(ConvertId(valeur.ToString()), DateTime.ParseExact(txbGCDDateCommande.Text, FORMAT_DATE, CultureInfo.InvariantCulture), int.Parse(txbGCDMontant.Text));
            controller.CreerCommande(commande);
            int valeur2 = 0;
            if (lesSuivis.Count != 0)
            {
                valeur2 = int.Parse(lesSuivis[lesSuivis.Count - 1].Id) + 1;
            }
            Suivi suivi = new Suivi(ConvertId(valeur2.ToString()), EN_COURS);
            controller.CreerSuivi(suivi);
            CommandeDocument commandeDocument = new CommandeDocument(ConvertId(valeur.ToString()), dvd.Id, ConvertId(valeur2.ToString()), DateTime.ParseExact(txbGCDDateCommande.Text, FORMAT_DATE, CultureInfo.InvariantCulture), int.Parse(txbGCDMontant.Text), int.Parse(txbGCDNbExemplaire.Text), null);
            controller.CreerCommandeDocumentsLivreDvd(commandeDocument);
            MessageBox.Show("La commande a été ajoutée avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
            lesCommandes = controller.GetAllCommandes();
            lesSuivis = controller.GetAllSuivis();
            lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(dvd.Id);
            RemplirDvdsListeCommandes(lesCommandeDocuments);
        }

        /// <summary>
        /// Lors de la sélection d'une ligne, modifie les options possibles pour la modification et suppression en fonction du statut
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvCGDCommandesDvd_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            cbxGCDStatut.SelectedIndex = -1;
            cbxGCDStatut.Items.Clear();
            DataGridViewRow selectedRow = dgvCGDCommandesDvd.Rows[e.RowIndex];
            if (selectedRow.Cells[0].Value != null)
            {
                string contenu = selectedRow.Cells[6].Value.ToString();
                switch (contenu)
                {
                    case EN_COURS:
                        lblGCDStatut.Enabled = true;
                        cbxGCDStatut.Enabled = true;
                        btnGCDModifierStatut.Enabled = true;
                        cbxGCDStatut.Items.Add(EN_COURS);
                        cbxGCDStatut.Items.Add(LIVREE);
                        cbxGCDStatut.Items.Add(RELANCEE);
                        cbxGCDStatut.SelectedIndex = 0;
                        btnGCDSupprimerCommande.Enabled = true;
                        break;
                    case LIVREE:
                        lblGCDStatut.Enabled = true;
                        cbxGCDStatut.Enabled = true;
                        btnGCDModifierStatut.Enabled = true;
                        cbxGCDStatut.Items.Add(LIVREE);
                        cbxGCDStatut.Items.Add(REGLEE);
                        cbxGCDStatut.SelectedIndex = 0;
                        btnGCDSupprimerCommande.Enabled = false;
                        break;
                    case RELANCEE:
                        lblGCDStatut.Enabled = true;
                        cbxGCDStatut.Enabled = true;
                        btnGCDModifierStatut.Enabled = true;
                        cbxGCDStatut.Items.Add(RELANCEE);
                        cbxGCDStatut.Items.Add(EN_COURS);
                        cbxGCDStatut.Items.Add(LIVREE);
                        cbxGCDStatut.SelectedIndex = 0;
                        btnGCDSupprimerCommande.Enabled = true;
                        break;
                    case REGLEE:
                        lblGCDStatut.Enabled = false;
                        cbxGCDStatut.Enabled = false;
                        btnGCDModifierStatut.Enabled = false;
                        btnGCDSupprimerCommande.Enabled = true;
                        break;
                    default:
                        lblGCDStatut.Enabled = false;
                        cbxGCDStatut.Enabled = false;
                        btnGCDModifierStatut.Enabled = false;
                        btnGCDSupprimerCommande.Enabled = false;
                        break;
                }
            }

        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvCGDCommandesDvd_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Dvd dvd = lesDvd.Find(x => x.Id.Equals(cbxGCDNumeroDocument.SelectedItem));
            lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(dvd.Id);
            string titreColonne = dgvCGDCommandesDvd.Columns[e.ColumnIndex].HeaderText;
            List<CommandeDocument> sortedList = new List<CommandeDocument>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.Id).ToList();
                    break;
                case "DateCommande":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.Montant).ToList();
                    break;
                case "NbExemplaire":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.NbExemplaire).ToList();
                    break;
                case "Statut":
                    sortedList = lesCommandeDocuments.OrderBy(o => o.Statut).ToList();
                    break;
            }
            RemplirDvdsListeCommandes(sortedList);
        }

        /// <summary>
        /// Lors du clic sur le bouton Modifier le statut, modifie le statut du Suivi selon la sélection dans le ComboBox
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnGCDModifierStatut_Click(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult confirmation = MessageBox.Show("Etes-vous sûr de vouloir modifier le statut de cette commande? (" + dgvCGDCommandesDvd.CurrentRow.Cells[6].Value.ToString() + " -> " + cbxGCDStatut.SelectedItem.ToString() + ")", CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                Suivi leSuivi = lesSuivis.Find(x => x.Id.Equals(dgvCGDCommandesDvd.CurrentRow.Cells[2].Value));
                leSuivi.Statut = cbxGCDStatut.SelectedItem.ToString();
                controller.ModifierSuivi(leSuivi);
                MessageBox.Show("Le statut de la commande a été modifié avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Dvd dvd = lesDvd.Find(x => x.Id.Equals(cbxGCDNumeroDocument.SelectedItem));
                lesCommandes = controller.GetAllCommandes();
                lesSuivis = controller.GetAllSuivis();
                lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(dvd.Id);
                RemplirDvdsListeCommandes(lesCommandeDocuments);
            }
        }

        /// <summary>
        /// Lors du clic sur le bouton Supprimer la commande, supprime la Commande, le CommandeDocument et le Suivi lié
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnCGDSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxGCDNumeroDocument.SelectedItem is null)
            {
                MessageBox.Show("Erreur, veuillez sélectionner un numéro de document.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dgvCGDCommandesDvd.CurrentRow.Cells[6].Value.ToString() != LIVREE)
            {
                DialogResult confirmation = MessageBox.Show("Etes-vous sûr de vouloir supprimer cette commande?", CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirmation == DialogResult.Yes)
                {
                    Commande laCommande = lesCommandes.Find(x => x.Id.Equals(dgvCGDCommandesDvd.CurrentRow.Cells[0].Value));
                    CommandeDocument leCommandeDocument = controller.GetCommandeDocumentsLivreDvd(cbxGCDNumeroDocument.SelectedItem.ToString())[0];
                    controller.SupprimerCommande(laCommande);
                    controller.SupprimerSuivi(new Suivi(leCommandeDocument.IdSuivi, null));
                    MessageBox.Show("La commande a été supprimée avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Dvd dvd = lesDvd.Find(x => x.Id.Equals(cbxGCDNumeroDocument.SelectedItem));
                    lesCommandes = controller.GetAllCommandes();
                    lesSuivis = controller.GetAllSuivis();
                    lesCommandeDocuments = controller.GetCommandeDocumentsLivreDvd(dvd.Id);
                    RemplirDvdsListeCommandes(lesCommandeDocuments);
                }
            }
        }


        #endregion

        #region Onglet Gestion Commandes Revue

        /// <summary>
        /// Liste lesAbonnements
        /// </summary>
        private List<Abonnement> lesAbonnements = new List<Abonnement>();
        /// <summary>
        /// BindingSource Abonnements Revues
        /// </summary>
        private readonly BindingSource bdgRevuesListeAbonnements = new BindingSource();

        /// <summary>
        /// Récupère les listes nécessaires et remplit le ComboBox lors de l'affichage de l'onglet
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void tabGestionCommandesRevues_Enter(object sender, EventArgs e)
        {
            cbxGCRNumeroDocument.Text = "";
            cbxGCRNumeroDocument.Items.Clear();
            lesRevues = controller.GetAllRevues();
            lesCommandes = controller.GetAllCommandes();
            lesSuivis = controller.GetAllSuivis();
            RemplirCbxRevuesNumeroDocument(lesRevues);
        }

        /// <summary>
        /// Remplit le ComboBox avec la liste reçue en paramètre
        /// </summary>
        /// <param name="revues">Liste de Revues</param>
        private void RemplirCbxRevuesNumeroDocument(List<Revue> revues)
        {
            List<Revue> sortedList = revues.OrderBy(o => o.Id).ToList();
            foreach (Revue revue in sortedList)
            {
                cbxGCRNumeroDocument.Items.Add(revue.Id);
            }
        }

        /// <summary>
        /// Lors de la sélection d'un index dans le ComboBox, remplit avec les infos de la Revue et tente de charger l'image
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void cbxGCRNumeroDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Revue revue = lesRevues.Find(x => x.Id.Equals(cbxGCRNumeroDocument.SelectedItem));
            txbGCRTitre.Text = revue.Titre.ToString();
            txbGCRPeriodicite.Text = revue.Periodicite.ToString();
            txbGCRDelaiMAD.Text = revue.DelaiMiseADispo.ToString();
            txbGCRGenre.Text = revue.Genre.ToString();
            txbGCRPublic.Text = revue.Public.ToString();
            txbGCRRayon.Text = revue.Rayon.ToString();
            txbGCRCheminImg.Text = revue.Image.ToString();
            lesAbonnements = controller.GetAbonnementsRevue(revue.Id);
            RemplirRevuesListeAbonnements(lesAbonnements);
            string image = revue.Image;
            try
            {
                pcbGCRImage.Image = Image.FromFile(image);
            }
            catch
            {
                pcbGCRImage.Image = null;
            }
        }

        /// <summary>
        /// Remplit le DataGridView avec la liste reçue en paramètre avec les Abonnements
        /// </summary>
        /// <param name="abonnements">Liste d'Abonnements</param>
        private void RemplirRevuesListeAbonnements(List<Abonnement> abonnements)
        {
            bdgRevuesListeAbonnements.DataSource = abonnements;
            dgvCGRCommandesRevues.DataSource = bdgRevuesListeAbonnements;
            dgvCGRCommandesRevues.Columns["IdRevue"].Visible = false;
            dgvCGRCommandesRevues.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvCGRCommandesRevues.ClearSelection();
            btnGCRSupprimerCommande.Enabled = false;
        }

        /// <summary>
        /// Lors du clic sur le bouton Nouvelle commande, vérifie les informations, puis crée une Commande et l'Abonnement lié
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnGCRNouvelleCommande_Click(object sender, EventArgs e)
        {
            Revue revue = lesRevues.Find(x => x.Id.Equals(cbxGCRNumeroDocument.SelectedItem));
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (revue == null)
            {
                MessageBox.Show("Erreur : veuillez saisir un Numéro de document valide.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txbGCRDateCommande.Text == "" || txbGCRMontant.Text == "" || txbGCRDateExpiration.Text == "")
            {
                MessageBox.Show("Erreur : veuillez remplir tous les champs.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!DateConversion(txbGCRDateCommande.Text, FORMAT_DATE) || !DateConversion(txbGCRDateExpiration.Text, FORMAT_DATE))
            {
                MessageBox.Show("Erreur : le format de la date est incorrect (attendu: JJ/MM/AAAA).", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!FloatConversion(txbGCRMontant.Text))
            {
                MessageBox.Show("Erreur : le montant entré est incorrect.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int valeur = 0;
            if (lesCommandes.Count != 0)
            {
                valeur = int.Parse(lesCommandes[lesCommandes.Count - 1].Id) + 1;
            }
            Commande commande = new Commande(ConvertId(valeur.ToString()), DateTime.ParseExact(txbGCRDateCommande.Text, FORMAT_DATE, CultureInfo.InvariantCulture), int.Parse(txbGCRMontant.Text));
            controller.CreerCommande(commande);
            Abonnement abonnement = new Abonnement(ConvertId(valeur.ToString()), DateTime.ParseExact(txbGCRDateCommande.Text, FORMAT_DATE, CultureInfo.InvariantCulture), int.Parse(txbGCRMontant.Text), DateTime.ParseExact(txbGCRDateExpiration.Text, FORMAT_DATE, CultureInfo.InvariantCulture), revue.Id);
            controller.CreerAbonnementRevue(abonnement);
            MessageBox.Show("La commande a été ajoutée avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
            lesCommandes = controller.GetAllCommandes();
            lesAbonnements = controller.GetAbonnementsRevue(revue.Id);
            RemplirRevuesListeAbonnements(lesAbonnements);
        }

        /// <summary>
        /// Lors de la sélection d'une ligne, active la suppression si une ligne est sélectionnée et valide
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvCGRCommandesRevues_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow selectedRow = dgvCGRCommandesRevues.Rows[e.RowIndex];
            if (selectedRow.Cells[0].Value != null)
            {
                btnGCRSupprimerCommande.Enabled = true;
            }
        }

        /// <summary>
        /// Tri sur les colonnes
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void dgvCGRCommandesRevues_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Revue revue = lesRevues.Find(x => x.Id.Equals(cbxGCRNumeroDocument.SelectedItem));
            lesAbonnements = controller.GetAbonnementsRevue(revue.Id);
            string titreColonne = dgvCGRCommandesRevues.Columns[e.ColumnIndex].HeaderText;
            List<Abonnement> sortedList = new List<Abonnement>();
            switch (titreColonne)
            {
                case "Id":
                    sortedList = lesAbonnements.OrderBy(o => o.Id).ToList();
                    break;
                case "DateCommande":
                    sortedList = lesAbonnements.OrderBy(o => o.DateCommande).ToList();
                    break;
                case "Montant":
                    sortedList = lesAbonnements.OrderBy(o => o.Montant).ToList();
                    break;
                case "DateFinAbonnement":
                    sortedList = lesAbonnements.OrderBy(o => o.DateFinAbonnement).ToList();
                    break;
            }
            RemplirRevuesListeAbonnements(sortedList);
        }

        /// <summary>
        /// Lors du clic sur le bouton Supprimer la commande, supprime la Commande et l'Abonnement lié (si aucun exemplaire rattaché)
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnGCRSupprimerCommande_Click(object sender, EventArgs e)
        {
            if (niveauDroits < 2)
            {
                MessageBox.Show(ERREUR_PERMISSIONS, ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (cbxGCRNumeroDocument.SelectedItem is null)
            {
                MessageBox.Show("Erreur, veuillez sélectionner un numéro de document.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Exemplaire> lesExemplairesRevue = controller.GetExemplairesRevue(cbxGCRNumeroDocument.SelectedItem.ToString());
            DateTime date1 = DateTime.ParseExact(dgvCGRCommandesRevues.CurrentRow.Cells[1].Value.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime date2 = DateTime.ParseExact(dgvCGRCommandesRevues.CurrentRow.Cells[3].Value.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            if (!DateConversion(date1.ToString(FORMAT_DATE), FORMAT_DATE) || !DateConversion(date2.ToString(FORMAT_DATE), FORMAT_DATE))
            {
                return;
            }
            DateTime dateCom = DateTime.ParseExact(date1.ToString(FORMAT_DATE), FORMAT_DATE, CultureInfo.InvariantCulture);
            DateTime dateExp = DateTime.ParseExact(date2.ToString(FORMAT_DATE), FORMAT_DATE, CultureInfo.InvariantCulture);
            foreach (Exemplaire exemplaire in lesExemplairesRevue)
            {
                DateTime dateExemplaire = DateTime.ParseExact(exemplaire.DateAchat.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                if (DateConversion(dateExemplaire.ToString(FORMAT_DATE), FORMAT_DATE) && ParutionDansAbonnement(dateCom, dateExp, DateTime.ParseExact(dateExemplaire.ToString(FORMAT_DATE), FORMAT_DATE, CultureInfo.InvariantCulture))){
                    MessageBox.Show("Erreur : un exemplaire est rattaché.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            DialogResult confirmation = MessageBox.Show("Etes-vous sûr de vouloir supprimer cette commande?", CONFIRMATION, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmation == DialogResult.Yes)
            {
                Commande laCommande = lesCommandes.Find(x => x.Id.Equals(dgvCGRCommandesRevues.CurrentRow.Cells[0].Value));
                controller.SupprimerCommande(laCommande);
                MessageBox.Show("La commande a été supprimée avec succès.", SUCCES, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Revue revue = lesRevues.Find(x => x.Id.Equals(cbxGCRNumeroDocument.SelectedItem));
                lesCommandes = controller.GetAllCommandes();
                lesAbonnements = controller.GetAbonnementsRevue(revue.Id);
                RemplirRevuesListeAbonnements(lesAbonnements);
            }
        }

        /// <summary>
        /// Bouton permettant de réafficher les abonnements se terminant dans les 30 jours sans avoir à relancer l'application 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGCRAbonnements_Click(object sender, EventArgs e)
        {
            FrmAlerteAbonnements fenetre = new FrmAlerteAbonnements();
            fenetre.Show();
        }

        #endregion

    }
}