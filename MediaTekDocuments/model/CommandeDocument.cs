﻿
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
        public string DateCommande { get; set; }
        public string Montant { get; set; }
        public string NbExemplaire { get; set; }
        public string Statut { get; set; }

        public CommandeDocument(string Id, string IdLivreDVD, string IdSuivi, string DateCommande, string Montant, string NbExemplaire, string Statut)
        {
            this.Id = Id;
            this.IdLivreDVD = IdLivreDVD;
            this.IdSuivi = IdSuivi;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
            this.NbExemplaire = NbExemplaire;
            this.Statut = Statut;
        }



        public override string ToString()
        {
            return $"Id: {Id}";
        }
    }
}
