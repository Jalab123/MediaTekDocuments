
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Genre : hérite de Categorie
    /// </summary>
    public class Genre : Categorie
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du Genre</param>
        /// <param name="libelle">Libelle du Genre</param>
        public Genre(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
