using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System.Text;
using System;
using System.Globalization;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    class FrmMediatekController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmMediatekController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// getter sur la liste des genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// getter sur la liste des livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// getter sur la liste des revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// getter sur les rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// getter sur les publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        /// <summary>
        /// getter sur les commandes
        /// </summary>
        /// <returns>Liste d'objets Commande</returns>
        public List<Commande> GetAllCommandes()
        {
            return access.GetAllCommandes();
        }

        /// <summary>
        /// getter sur les commandes
        /// </summary>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetAllCommandeDocuments()
        {
            return access.GetAllCommandeDocuments();
        }

        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        public List<CommandeDocument> GetCommandeDocumentsLivreDvd(string idlivredvd)
        {
            return access.GetCommandeDocumentsLivreDvd(idlivredvd);
        }

        public List<Abonnement> GetAbonnementsRevue(string idrevue)
        {
            return access.GetAbonnementsRevue(idrevue);
        }

        /// <summary>
        /// récupère les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocuement">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
        }

        public List<Abonnement> GetDerniersAbonnements()
        {
            return access.GetDerniersAbonnements();
        }

        public bool CreerCommande(Commande commande)
        {
            return access.CreerCommande(commande);
        }

        public bool CreerCommandeDocumentsLivreDvd(CommandeDocument commandeDocument)
        {
            return access.CreerCommandeDocumentsLivreDvd(commandeDocument);
        }

        public bool CreerAbonnementRevue(Abonnement abonnement)
        {
            return access.CreerAbonnementRevue(abonnement);
        }

        public bool CreerSuivi(Suivi suivi)
        {
            return access.CreerSuivi(suivi);
        }

        public bool SupprimerSuivi(Suivi suivi)
        {
            return access.SupprimerSuivi(suivi);
        }

        public bool ModifierSuivi(Suivi suivi)
        {
            return access.ModifierSuivi(suivi);
        }

        public bool SupprimerCommande(Commande commande)
        {
            return access.SupprimerCommande(commande);
        }

        public bool SupprimerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.SupprimerCommandeDocument(commandeDocument);
        }


        /// <summary>
        /// Crée un exemplaire d'une revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }
    }
}
