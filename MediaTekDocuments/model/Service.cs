
using System;

namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Service ()
    /// </summary>
    public class Service
    {
        /// <summary>
        /// Id du Service
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Nom du Service
        /// </summary>
        public string Nom { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Id">Id du Service</param>
        /// <param name="Nom">Nom du Service</param>
        public Service(string Id, string Nom)
        {
            this.Id = Id;
            this.Nom = Nom;
        }
    }
}
