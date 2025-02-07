
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Commande ()
    /// </summary>
    public class Suivi
    {
        public string Id { get; set; }
        public string Statut { get; set; }

        public Suivi(string Id, string Statut)
        {
            this.Id = Id;
            this.Statut = Statut;
        }
    }
}
