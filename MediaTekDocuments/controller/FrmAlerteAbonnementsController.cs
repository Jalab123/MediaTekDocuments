﻿using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmAlertAbonnementsController
    /// </summary>
    public class FrmAlerteAbonnementsController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAlerteAbonnementsController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Récupère les Abonnements qui se terminent dans 30 jours
        /// </summary>
        /// <returns>Liste d'objets Abonnement</returns>
        public List<Abonnement> GetDerniersAbonnements()
        {
            return access.GetDerniersAbonnements();
        }

        /// <summary>
        /// Recupère un Document grâce à un id spécifique
        /// </summary>
        /// <param name="iddocument">L'id du Document</param>
        /// <returns>Liste d'objets Document</returns>
        public List<Document> GetDocument(string iddocument)
        {
            return access.GetDocument(iddocument);
        }
    }
}
