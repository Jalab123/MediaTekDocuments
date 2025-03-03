
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier CommandeDocument ()
    /// </summary>
    public class CommandeDocument
    {
        /// <summary>
        /// Id du CommandeDocument
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// IdLivreDVD du CommandeDocument
        /// </summary>
        public string IdLivreDVD { get; set; }
        /// <summary>
        /// IdSuivi du CommandeDocument
        /// </summary>
        public string IdSuivi { get; set; }
        /// <summary>
        /// DateCommande du CommandeDocument
        /// </summary>
        public DateTime DateCommande { get; set; }
        /// <summary>
        /// Montant du CommandeDocument
        /// </summary>
        public int Montant { get; set; }
        /// <summary>
        /// NbExemplaire du CommandeDocument
        /// </summary>
        public int NbExemplaire { get; set; }
        /// <summary>
        /// Statut du CommandeDocument
        /// </summary>
        public string Statut { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Id">Id du CommandeDocument</param>
        /// <param name="IdLivreDVD">IdLivreDVD</param>
        /// <param name="IdSuivi">IdSuivi du CommandeDocument</param>
        /// <param name="DateCommande">DateCommande du CommandeDocument</param>
        /// <param name="Montant">Montant du CommandeDocument</param>
        /// <param name="NbExemplaire">NbExemplaire du CommandeDocument</param>
        /// <param name="Statut">Statut du CommandeDocument</param>
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
