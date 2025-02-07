
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Commande ()
    /// </summary>
    public class Commande
    {
        public string Id { get; }
        public string DateCommande { get; }
        public string Montant { get; }

        public Commande(string Id, string DateCommande, string Montant)
        {
            this.Id = Id;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
        }
    }
}
