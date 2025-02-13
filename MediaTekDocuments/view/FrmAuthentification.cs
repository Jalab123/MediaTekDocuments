using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    public partial class FrmAuthentification : Form
    {
        private readonly FrmAuthentificationController controller;
        private int niveauDroits = 0;

        private const string ERREUR = "Erreur";

        public FrmAuthentification()
        {
            InitializeComponent();
            controller = new FrmAuthentificationController();
        }

        private void btnConnexion_Click(object sender, EventArgs e)
        {
            if (txbLogin.Text == "" || txbPwd.Text == "")
            {
                MessageBox.Show("Erreur : veuillez remplir tous les champs.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Utilisateur> utilisateur = controller.GetUtilisateur(txbLogin.Text, txbPwd.Text);
            // Login / Pwd incorrect
            if (utilisateur.Count == 0)
            {
                Console.WriteLine("Utilisateur pas reconnu.");
                MessageBox.Show("Erreur : nom d'utilisateur ou mot de passe incorrect.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // Succès
            else if (utilisateur.Count == 1)
            {
                Console.WriteLine("Utilisateur connecté.");
                niveauDroits = calculNiveauDroits(utilisateur[0]);
            }
            // Compte relié à plusieurs services
            else
            {
                Console.WriteLine("Problème technique.");
                MessageBox.Show("Erreur : veuillez contacter un administrateur.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (niveauDroits == 0)
            {
                MessageBox.Show("Erreur : vous n'avez pas la permission d'accéder à l'application.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            } else {
                this.Visible = false;
                FrmMediatek fenetre = new FrmMediatek(niveauDroits);
                fenetre.ShowDialog();
                this.Close();
            }
        }

        private int calculNiveauDroits(Utilisateur utilisateur)
        {
            // Administrateur / Administratif -> toutes les permissions
            if (utilisateur.IdService == "1" || utilisateur.IdService == "2")
            {
                return 2;
            // Prêts -> permissions partielles
            }
            else if (utilisateur.IdService == "3")
            {
                return 1;
            // Culture -> aucune permission
            }
            else
            {
                return 0;
            }
        }
    }
}
