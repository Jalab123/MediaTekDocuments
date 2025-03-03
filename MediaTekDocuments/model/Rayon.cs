
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Rayon (rayon de classement du document) hérite de Categorie
    /// </summary>
    public class Rayon : Categorie
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du Rayon</param>
        /// <param name="libelle">Libelle du Rayon</param>
        public Rayon(string id, string libelle) : base(id, libelle)
        {
        }

    }
}
