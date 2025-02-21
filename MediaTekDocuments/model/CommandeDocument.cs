
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeDocument ()
    /// </summary>
    public class CommandeDocument
    {
        public string Id { get; set; }
        public string IdLivreDVD { get; set; }
        public string IdSuivi { get; set; }
        public DateTime DateCommande { get; set; }
        public int Montant { get; set; }
        public int NbExemplaire { get; set; }
        public string Statut { get; set; }

        public CommandeDocument(string Id, string IdLivreDVD, string IdSuivi, DateTime DateCommande, int Montant, int NbExemplaire, string Statut)
        {
            this.Id = Id;
            this.IdLivreDVD = IdLivreDVD;
            this.IdSuivi = IdSuivi;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
            this.NbExemplaire = NbExemplaire;
            this.Statut = Statut;
        }

    }
}
