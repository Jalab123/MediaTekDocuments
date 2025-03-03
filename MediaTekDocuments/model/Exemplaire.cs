using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Exemplaire (exemplaire d'une revue)
    /// </summary>
    public class Exemplaire
    {
        /// <summary>
        /// Numero de l'Exemplaire
        /// </summary>
        public int Numero { get; set; }
        /// <summary>
        /// Photo de l'Exemplaire
        /// </summary>
        public string Photo { get; set; }
        /// <summary>
        /// DateAchat de l'Exemplaire
        /// </summary>
        public DateTime DateAchat { get; set; }
        /// <summary>
        /// IdEtat de l'Exemplaire
        /// </summary>
        public string IdEtat { get; set; }
        /// <summary>
        /// Id de l'Exemplaire
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="numero">Numero de l'Exemplaire</param>
        /// <param name="dateAchat">DateAchat de l'Exemplaire</param>
        /// <param name="photo">Photo de l'Exemplaire</param>
        /// <param name="idEtat">IdEtat de l'Exemplaire</param>
        /// <param name="idDocument">IdDocument de l'Exemplaire</param>
        public Exemplaire(int numero, DateTime dateAchat, string photo, string idEtat, string idDocument)
        {
            this.Numero = numero;
            this.DateAchat = dateAchat;
            this.Photo = photo;
            this.IdEtat = idEtat;
            this.Id = idDocument;
        }

    }
}
