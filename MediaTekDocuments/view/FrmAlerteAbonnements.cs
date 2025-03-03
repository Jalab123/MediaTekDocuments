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
    public partial class FrmAlerteAbonnements : Form
    {
        private readonly FrmAlerteAbonnementsController controller;
        public FrmAlerteAbonnements()
        {
            InitializeComponent();
            controller = new FrmAlerteAbonnementsController();
        }

        private void FrmAlerteAbonnements_Load(object sender, EventArgs e)
        {
            RemplirDGVAA();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RemplirDGVAA()
        {
            List<Abonnement> lab = controller.GetDerniersAbonnements();
            foreach (Abonnement a in lab)
            {
                List<Document>  doc = controller.GetDocument(a.IdRevue);
                foreach (Document d in doc)
                {
                    dgvAA.Rows.Add(d.Titre.ToString(), a.DateFinAbonnement.ToString());
                }
            }
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
