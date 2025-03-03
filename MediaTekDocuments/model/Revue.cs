
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Revue hérite de Document : contient des propriétés spécifiques aux revues
    /// </summary>
    public class Revue : Document
    {
        /// <summary>
        /// Periodicite de la Revue
        /// </summary>
        public string Periodicite { get; set; }
        /// <summary>
        /// DelaiMiseADispo de la Revue
        /// </summary>
        public int DelaiMiseADispo { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="id">Id de la Revue</param>
        /// <param name="titre">Titre de la Revue</param>
        /// <param name="image">Image de la Revue</param>
        /// <param name="idGenre">IdGenre de la Revue</param>
        /// <param name="genre">Genre de la Revue</param>
        /// <param name="idPublic">IdPublic de la Revue</param>
        /// <param name="lePublic">LePublic de la Revue</param>
        /// <param name="idRayon">IdRayon de la Revue</param>
        /// <param name="rayon">Rayon de la Revue</param>
        /// <param name="periodicite">Periodicite de la Revue</param>
        /// <param name="delaiMiseADispo">DelaiMiseADispo de la Revue</param>
        public Revue(string id, string titre, string image, string idGenre, string genre,
            string idPublic, string lePublic, string idRayon, string rayon,
            string periodicite, int delaiMiseADispo)
             : base(id, titre, image, idGenre, genre, idPublic, lePublic, idRayon, rayon)
        {
            Periodicite = periodicite;
            DelaiMiseADispo = delaiMiseADispo;
        }

    }
}
