
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Commande ()
    /// </summary>
    public class Commande
    {
        public string Id { get; }
        public DateTime DateCommande { get; }
        public int Montant { get; }

        public Commande(string Id, DateTime DateCommande, int Montant)
        {
            this.Id = Id;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
        }
    }
}
