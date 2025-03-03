
namespace MediaTekDocuments.model
{
    /// <summary>
    /// Classe métier Utilisateur ()
    /// </summary>
    public class Utilisateur
    {
        /// <summary>
        /// Id de l'Utilisateur
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Login de l'Utilisateur
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Pwd de l'Utilisateur
        /// </summary>
        public string Pwd { get; set; }
        /// <summary>
        /// IdService de l'Utilisateur
        /// </summary>
        public string IdService { get; set; }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="Id">Id de l'Utilisateur</param>
        /// <param name="Login">Login de l'Utilisateur</param>
        /// <param name="Pwd">Pwd de l'Utilisateur</param>
        /// <param name="IdService">IdService de l'Utilisateur</param>
        public Utilisateur(string Id, string Login, string Pwd, string IdService)
        {
            this.Id = Id;
            this.Login = Login;
            this.Pwd = Pwd;
            this.IdService = IdService;
        }
    }
}
