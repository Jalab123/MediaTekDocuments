
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Public (public concerné par le document) hérite de Categorie
    /// </summary>
    public class Public : Categorie
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du Public</param>
        /// <param name="libelle">Libelle du Public</param>
        public Public(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
