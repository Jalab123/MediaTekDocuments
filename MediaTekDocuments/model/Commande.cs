
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Commande ()
    /// </summary>
    public class Commande
    {
        /// <summary>
        /// Id de la Commande
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// DateCommande de la Commande
        /// </summary>
        public DateTime DateCommande { get; }
        /// <summary>
        /// Montant de la Commande
        /// </summary>
        public int Montant { get; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Id">Id de la Commande</param>
        /// <param name="DateCommande">DateCommande de la Commande</param>
        /// <param name="Montant">Montant de la Commande</param>
        public Commande(string Id, DateTime DateCommande, int Montant)
        {
            this.Id = Id;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
        }
    }
}
