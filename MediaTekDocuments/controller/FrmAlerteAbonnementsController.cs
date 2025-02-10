using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmAlertAbonnementsController
    /// </summary>
    class FrmAlertAbonnementsController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAlertAbonnementsController()
        {
            access = Access.GetInstance();
        }

        public List<Abonnement> GetDerniersAbonnements()
        {
            return access.GetDerniersAbonnements();
        }

        public List<Document> GetDocument(string iddocument)
        {
            return access.GetDocument(iddocument);
        }
    }
}
