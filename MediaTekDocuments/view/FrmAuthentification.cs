using MediaTekDocuments.controller;
using MediaTekDocuments.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaTekDocuments.view
{
    /// <summary>
    /// Classe FrmAuthentification
    /// </summary>
    public partial class FrmAuthentification : Form
    {
        /// <summary>
        /// Contrôleur
        /// </summary>
        private readonly FrmAuthentificationController controller;
        /// <summary>
        /// Niveau de droits:
        /// 0 -> pas d'accès
        /// 1 -> accès à la recherche
        /// 2 -> accès à la gestion
        /// </summary>
        private int niveauDroits = 0;

        /// <summary>
        /// Erreur
        /// </summary>
        private const string ERREUR = "Erreur";

        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmAuthentification()
        {
            InitializeComponent();
            controller = new FrmAuthentificationController();
        }

        /// <summary>
        /// Tente de connecter l'utilisateur lors du clic sur le bouton Se connecter
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnConnexion_Click(object sender, EventArgs e)
        {
            if (txbLogin.Text == "" || txbPwd.Text == "")
            {
                MessageBox.Show("Erreur : veuillez remplir tous les champs.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string hash = HacherSHA1(txbPwd.Text);
            List<Utilisateur> utilisateur = controller.GetUtilisateur(txbLogin.Text, hash);
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
                niveauDroits = CalculNiveauDroits(utilisateur[0]);
                List<Service> service = controller.GetService(utilisateur[0].IdService);
                if (service.Count > 0)
                {
                    MessageBox.Show("Connecté en tant que : " + txbLogin.Text + " (" + service[0].Nom + ").", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                } else {
                    MessageBox.Show("Erreur : veuillez contacter un administrateur.", ERREUR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
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

        /// <summary>
        /// Calcule le niveau de droits en fonction du service auquel l'utilisateur est rattaché
        /// </summary>
        /// <param name="utilisateur">L'utilisateur voulant se connecter</param>
        /// <returns>0 -> Pas de service / Culture, 1 -> Prêts, 2 -> Administratif / Administrateur</returns>
        private static int CalculNiveauDroits(Utilisateur utilisateur)
        {
            // Administrateur / Administratif -> toutes les permissions
            if (utilisateur.IdService == "1" || utilisateur.IdService == "2")
            {
                return 2;
            }
            // Prêts -> permissions partielles
            else if (utilisateur.IdService == "3")
            {
                return 1;
            }
            // Culture -> aucune permission
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Hacher un mot de passe en SHA1
        /// </summary>
        /// <param name="input">Entrée</param>
        /// <returns>Le mot de passe haché</returns>
        static string HacherSHA1(string input)
        {
            using (SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha1.ComputeHash(bytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
