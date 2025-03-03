
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Document (réunit les infomations communes à tous les documents : Livre, Revue, Dvd)
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Id du Document
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Titre du Document
        /// </summary>
        public string Titre { get; }
        /// <summary>
        /// Image du Document
        /// </summary>
        public string Image { get; }
        /// <summary>
        /// IdGenre du Document
        /// </summary>
        public string IdGenre { get; }
        /// <summary>
        /// Genre du Document
        /// </summary>
        public string Genre { get; }
        /// <summary>
        /// IdPublic du Document
        /// </summary>
        public string IdPublic { get; }
        /// <summary>
        /// Public du Document
        /// </summary>
        public string Public { get; }
        /// <summary>
        /// IdRayon du Document
        /// </summary>
        public string IdRayon { get; }
        /// <summary>
        /// Rayon du Document
        /// </summary>
        public string Rayon { get; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du Document</param>
        /// <param name="titre">Titre du Document</param>
        /// <param name="image">Image du Document</param>
        /// <param name="idGenre">IdGenre du Document</param>
        /// <param name="genre">Genre du Document</param>
        /// <param name="idPublic">IdPublic du Document</param>
        /// <param name="lePublic">LePublic du Document</param>
        /// <param name="idRayon">IdRayon du Document</param>
        /// <param name="rayon">Rayon du Document</param>
        public Document(string id, string titre, string image, string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
        {
            Id = id;
            Titre = titre;
            Image = image;
            IdGenre = idGenre;
            Genre = genre;
            IdPublic = idPublic;
            Public = lePublic;
            IdRayon = idRayon;
            Rayon = rayon;
        }
    }
}
