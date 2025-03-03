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
    /// <summary>
    /// Classe FrmAlerteAbonnements
    /// </summary>
    public partial class FrmAlerteAbonnements : Form
    {
        /// <summary>
        /// Contrôleur
        /// </summary>
        private readonly FrmAlerteAbonnementsController controller;
        
        /// <summary>
        /// Constructeur
        /// </summary>
        public FrmAlerteAbonnements()
        {
            InitializeComponent();
            controller = new FrmAlerteAbonnementsController();
        }

        /// <summary>
        /// Remplit le DataGridView lors du chargement
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void FrmAlerteAbonnements_Load(object sender, EventArgs e)
        {
            RemplirDGVAA();
        }

        /// <summary>
        /// Ferme la fenêtre lors du clic sur le bouton OK
        /// </summary>
        /// <param name="sender">Emetteur</param>
        /// <param name="e">Evenement</param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Remplit le DataGridView avec les abonnements se terminant dans les 30 jours
        /// </summary>
        private void RemplirDGVAA()
        {
            // récupération des abonnements
            List<Abonnement> lab = controller.GetDerniersAbonnements();
            foreach (Abonnement a in lab)
            {
                List<Document>  doc = controller.GetDocument(a.IdRevue);
                foreach (Document d in doc)
                {
                    dgvAA.Rows.Add(d.Titre.ToString(), a.DateFinAbonnement.ToString());
                }
            }
            // remplissage du DataGridView
            foreach (DataGridViewRow row in dgvAA.Rows)
            {
                if (row.Cells["DateFinAbonnement"].Value != null)
                {
                    DateTime date = Convert.ToDateTime(row.Cells["DateFinAbonnement"].Value);
                    row.Cells["DateFinAbonnement"].Value = date.ToString("dd/MM/yyyy");
                }
            }

        }
    }
}
