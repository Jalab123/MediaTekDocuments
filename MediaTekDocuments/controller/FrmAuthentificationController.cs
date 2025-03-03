using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.dal;

namespace MediaTekDocuments.controller
{
    /// <summary>
    /// Contrôleur lié à FrmAuthentificationController
    /// </summary>
    public class FrmAuthentificationController
    {
        /// <summary>
        /// Objet d'accès aux données
        /// </summary>
        private readonly Access access;

        /// <summary>
        /// Récupération de l'instance unique d'accès aux données
        /// </summary>
        public FrmAuthentificationController()
        {
            access = Access.GetInstance();
        }

        /// <summary>
        /// Recupère les utilisateurs correspondant à une combinaison login et pwd (tentative d'authentification)
        /// </summary>
        /// <param name="login">Nom de l'Utilisateur concerné</param>
        /// <param name="pwd">Mot de passe de l'Utilisateur concerné</param>
        /// <returns>Liste d'objets Utilisateur</returns>
        public List<Utilisateur> GetUtilisateur(string login, string pwd)
        {
            return access.GetUtilisateur(login, pwd);
        }

        /// <summary>
        /// Recupère un Service grâce à un id spécifique
        /// </summary>
        /// <param name="id">Id du Service concerné</param>
        /// <returns>Liste d'objets Service</returns>
        public List<Service> GetService(string id)
        {
            return access.GetService(id);
        }
    }
}
