
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Categorie (réunit les informations des classes Public, Genre et Rayon)
    /// </summary>
    public class Categorie
    {
        /// <summary>
        /// Id de la Categorie
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Libelle de la Categorie
        /// </summary>
        public string Libelle { get; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id de la Categorie</param>
        /// <param name="libelle">Libelle de la Categorie</param>
        public Categorie(string id, string libelle)
        {
            this.Id = id;
            this.Libelle = libelle;
        }

        /// <summary>
        /// Récupération du libellé pour l'affichage dans les combos
        /// </summary>
        /// <returns>Libelle</returns>
        public override string ToString()
        {
            return this.Libelle;
        }

    }
}
