
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Livre hérite de LivreDvd : contient des propriétés spécifiques aux livres
    /// </summary>
    public class Livre : LivreDvd
    {
        /// <summary>
        /// Isbn du Livre
        /// </summary>
        public string Isbn { get; }
        /// <summary>
        /// Auteur du Livre
        /// </summary>
        public string Auteur { get; }
        /// <summary>
        /// Collection du Livre
        /// </summary>
        public string Collection { get; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du Livre</param>
        /// <param name="titre">Titre du Livre</param>
        /// <param name="image">Image du Livre</param>
        /// <param name="isbn">Isbn du Livre</param>
        /// <param name="auteur">Auteur du Livre</param>
        /// <param name="collection">Collection du Livre</param>
        /// <param name="idGenre">IdGenre du Livre</param>
        /// <param name="genre">Genre du Livre</param>
        /// <param name="idPublic">IdPublic du Livre</param>
        /// <param name="lePublic">LePublic du Livre</param>
        /// <param name="idRayon">IdRayon du Livre</param>
        /// <param name="rayon">Rayon du Livre</param>
        public Livre(string id, string titre, string image, string isbn, string auteur, string collection,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.Isbn = isbn;
            this.Auteur = auteur;
            this.Collection = collection;
        }



    }
}
