
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Abonnement ()
    /// </summary>
    public class Abonnement
    {
        public string Id { get; set; }
        public DateTime DateCommande { get; set; }
        public int Montant { get; set; }
        public DateTime DateFinAbonnement { get; set; }
        public string IdRevue { get; set; }

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
