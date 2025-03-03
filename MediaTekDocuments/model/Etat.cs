
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Etat (état d'usure d'un document)
    /// </summary>
    public class Etat
    {
        /// <summary>
        /// Id de l'Etat
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Libelle de l'Etat
        /// </summary>
        public string Libelle { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id de l'Etat</param>
        /// <param name="libelle">Libelle de l'Etat</param>
        public Etat(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

    }
}
