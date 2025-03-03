
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Suivi ()
    /// </summary>
    public class Suivi
    {
        /// <summary>
        /// Id du Suivi
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Statut du Suivi
        /// </summary>
        public string Statut { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Id">Id du Suivi</param>
        /// <param name="Statut">Statut du Suivi</param>
        public Suivi(string Id, string Statut)
        {
            this.Id = Id;
            this.Statut = Statut;
        }
    }
}
