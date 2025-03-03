
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier LivreDvd hérite de Document
    /// </summary>
    public abstract class LivreDvd : Document
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du LivreDvd</param>
        /// <param name="titre">Titre du LivreDvd</param>
        /// <param name="image">Image du LivreDvd</param>
        /// <param name="idGenre">IdGenre du LivreDvd</param>
        /// <param name="genre">Genre du LivreDvd</param>
        /// <param name="idPublic">IdPublic du LivreDvd</param>
        /// <param name="lePublic">LePublic du LivreDvd</param>
        /// <param name="idRayon">IdRayon du LivreDvd</param>
        /// <param name="rayon">Rayon du LivreDvd</param>
        protected LivreDvd(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
        }

    }
}
