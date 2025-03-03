
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Dvd hérite de LivreDvd : contient des propriétés spécifiques aux dvd
    /// </summary>
    public class Dvd : LivreDvd
    {
        /// <summary>
        /// Duree du Dvd
        /// </summary>
        public int Duree { get; }
        /// <summary>
        /// Realisateur du Dvd
        /// </summary>
        public string Realisateur { get; }
        /// <summary>
        /// Synopsis du Dvd
        /// </summary>
        public string Synopsis { get; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id du Dvd</param>
        /// <param name="titre">Titre du Dvd</param>
        /// <param name="image">Image du Dvd</param>
        /// <param name="duree">Duree du Dvd</param>
        /// <param name="realisateur">Realisateur du Dvd</param>
        /// <param name="synopsis">Synopsis du Dvd</param>
        /// <param name="idGenre">IdGenre du Dvd</param>
        /// <param name="genre">Genre du Dvd</param>
        /// <param name="idPublic">IdPublic du Dvd</param>
        /// <param name="lePublic">LePublic du Dvd</param>
        /// <param name="idRayon">IdRayon du Dvd</param>
        /// <param name="rayon">Rayon du Dvd</param>
        public Dvd(string id, string titre, string image, int duree, string realisateur, string synopsis,
            string idGenre, string genre, string idPublic, string lePublic, string idRayon, string rayon)
            : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            this.Duree = duree;
            this.Realisateur = realisateur;
            this.Synopsis = synopsis;
        }

    }
}
