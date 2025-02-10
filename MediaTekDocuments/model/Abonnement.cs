
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Abonnement ()
    /// </summary>
    public class Abonnement
    {
        public string Id { get; set; }
        public string DateCommande { get; set; }
        public string Montant { get; set; }
        public string DateFinAbonnement { get; set; }
        public string IdRevue { get; set; }

        public Abonnement(string Id, string DateCommande, string Montant, string DateFinAbonnement, string IdRevue)
        {
            this.Id = Id;
            this.DateCommande = DateCommande;
            this.Montant = Montant;
            this.DateFinAbonnement = DateFinAbonnement;
            this.IdRevue = IdRevue;
        }
    }
}
