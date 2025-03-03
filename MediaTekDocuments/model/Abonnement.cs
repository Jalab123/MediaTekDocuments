
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Modèles
    /// </summary>
    internal class NamespaceDoc
    {

    }

    /// <summary>
    /// Classe métier Abonnement ()
    /// </summary>
    public class Abonnement
    {
        /// <summary>
        /// Id de l'Abonnement
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// DateCommande de l'Abonnement
        /// </summary>
        public DateTime DateCommande { get; set; }
        /// <summary>
        /// Montant de l'Abonnement
        /// </summary>
        public int Montant { get; set; }
        /// <summary>
        /// DateFinAbonnement de l'Abonnement
        /// </summary>
        public DateTime DateFinAbonnement { get; set; }
        /// <summary>
        /// IdRevue de l'Abonnement
        /// </summary>
        public string IdRevue { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Id">Id de l'Abonnement</param>
        /// <param name="DateCommande">DateCommande de l'Abonnement</param>
        /// <param name="Montant">Montant de l'Abonnement</param>
        /// <param name="DateFinAbonnement">DateFinAbonnement de l'Abonnement</param>
        /// <param name="IdRevue">IdRevue de l'Abonnement</param>
        public Abonnement(string Id, DateTime DateCommande, int Montant, DateTime DateFinAbonnement, string IdRevue)
        { 
            this.Id = Id;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
            this.DateFinAbonnement = DateFinAbonnement;
            this.IdRevue = IdRevue;
        }
    }
}
