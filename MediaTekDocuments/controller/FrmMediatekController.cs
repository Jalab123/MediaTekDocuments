using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;
using System.Text;
using System;
using System.Globalization;


namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleurs
    /// </summary>
    internal class NamespaceDoc
    {

    }

    /// <summary>
    /// Contrôleur lié à FrmMediatek
    /// </summary>
    public class FrmMediatekController
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
        /// Getter sur la liste des Genres
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            return access.GetAllGenres();
        }

        /// <summary>
        /// Getter sur la liste des Livres
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            return access.GetAllLivres();
        }

        /// <summary>
        /// Getter sur la liste des Dvd
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            return access.GetAllDvd();
        }

        /// <summary>
        /// Getter sur la liste des Revues
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            return access.GetAllRevues();
        }

        /// <summary>
        /// Getter sur les Rayons
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            return access.GetAllRayons();
        }

        /// <summary>
        /// Getter sur les Publics
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            return access.GetAllPublics();
        }

        /// <summary>
        /// Getter sur les Commandes
        /// </summary>
        /// <returns>Liste d'objets Commande</returns>
        public List<Commande> GetAllCommandes()
        {
            return access.GetAllCommandes();
        }

        /// <summary>
        /// Getter sur les Commandedocuments
        /// </summary>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetAllCommandeDocuments()
        {
            return access.GetAllCommandeDocuments();
        }

        /// <summary>
        /// Getter sur les Suivis
        /// </summary>
        /// <returns>Liste d'objets Suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            return access.GetAllSuivis();
        }

        /// <summary>
        /// Récupère les commandedocument grâce à un id de livre ou dvd spécifique
        /// </summary>
        /// <param name="idlivredvd">L'id du Livre ou Dvd concerné</param>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetCommandeDocumentsLivreDvd(string idlivredvd)
        {
            return access.GetCommandeDocumentsLivreDvd(idlivredvd);
        }

        /// <summary>
        /// Récupère un Abonnement grâce à un id de Revue spécifique
        /// </summary>
        /// <param name="idrevue">L'id de la Revue concernée</param>
        /// <returns>Liste d'objets Abonnement</returns>
        public List<Abonnement> GetAbonnementsRevue(string idrevue)
        {
            return access.GetAbonnementsRevue(idrevue);
        }

        /// <summary>
        /// Récupère les Exemplaires d'une Revue
        /// </summary>
        /// <param name="idDocuement">L'id de la Revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocuement)
        {
            return access.GetExemplairesRevue(idDocuement);
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
        /// Crée une Commande dans la BDD
        /// </summary>
        /// <param name="commande">L'objet Commande concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerCommande(Commande commande)
        {
            return access.CreerCommande(commande);
        }

        /// <summary>
        /// Crée un Commandedocument dans la BDD
        /// </summary>
        /// <param name="commandeDocument">L'objet CommandeDocument concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerCommandeDocumentsLivreDvd(CommandeDocument commandeDocument)
        {
            return access.CreerCommandeDocumentsLivreDvd(commandeDocument);
        }

        /// <summary>
        /// Crée un Abonnement dans la BDD
        /// </summary>
        /// <param name="abonnement">L'objet Abonnement concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerAbonnementRevue(Abonnement abonnement)
        {
            return access.CreerAbonnementRevue(abonnement);
        }

        /// <summary>
        /// Crée un Suivi dans la BDD
        /// </summary>
        /// <param name="suivi">L'objet Suivi concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerSuivi(Suivi suivi)
        {
            return access.CreerSuivi(suivi);
        }

        /// <summary>
        /// Supprime un Suivi dans la BDD
        /// </summary>
        /// <param name="suivi">L'objet Suivi concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerSuivi(Suivi suivi)
        {
            return access.SupprimerSuivi(suivi);
        }

        /// <summary>
        /// Modifie un Suivi dans la BDD
        /// </summary>
        /// <param name="suivi">L'objet Suivi concerné</param>
        /// <returns>True si la modification a pu se faire</returns>
        public bool ModifierSuivi(Suivi suivi)
        {
            return access.ModifierSuivi(suivi);
        }

        /// <summary>
        /// Supprime une Commande dans la BDD
        /// </summary>
        /// <param name="commande">L'objet Commande concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerCommande(Commande commande)
        {
            return access.SupprimerCommande(commande);
        }

        /// <summary>
        /// Supprimer un Commandedocument dans la BDD
        /// </summary>
        /// <param name="commandeDocument">L'objet CommandeDocument concerné</param>
        /// <returns>True si la suppression a pu se faire</returns>
        public bool SupprimerCommandeDocument(CommandeDocument commandeDocument)
        {
            return access.SupprimerCommandeDocument(commandeDocument);
        }


        /// <summary>
        /// Crée un Exemplaire d'une Revue dans la bdd
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si la création a pu se faire</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            return access.CreerExemplaire(exemplaire);
        }
    }
}
