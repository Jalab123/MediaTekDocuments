﻿using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using Serilog;
using System.Windows.Forms;



namespace MediaTekDocuments.dal
{

    /// <summary>
    /// Accès aux données
    /// </summary>
    internal class NamespaceDoc
    {

    }

    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// Chaîne de connexion
        /// </summary>
        private static readonly string connectionName = "MediaTekDocuments.Properties.Settings.mediaTekDocumentsConnectionString";
        /// <summary>
        /// Chaîne de l'URI
        /// </summary>
        private static readonly string connectionUriApi = "MediaTekDocuments.Properties.Settings.mediaTekDocumentsConnectionUriApi";
        /// <summary>
        /// Adresse de l'API
        /// </summary>
        private static readonly string uriApi = GetConnectionStringByName(connectionUriApi);
        /// <summary>
        /// Instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// Instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// Méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// Méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";
        /// <summary>
        /// Méthode HTTP pour update
        /// </summary>
        private const string PUT = "PUT";
        /// <summary>
        /// Méthode HTTP pour delete
        /// </summary>
        private const string DELETE = "DELETE";
        /// <summary>
        /// Commandedocument
        /// </summary>
        private const string COMMANDE_DOCUMENT = "commandedocument";
        /// <summary>
        /// Champs
        /// </summary>
        private const string CHAMPS = "champs=";


        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            try
            {

                String authenticationString = GetConnectionStringByName(connectionName);
                api = ApiRest.GetInstance(uriApi, authenticationString);
                Log.Information("Connexion à l'API réussie (Constructeur).");
                Console.WriteLine("Connexion à l'API réussie.");
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Erreur fatale: impossible d'accéder à l'API (Constructeur).");
                Console.WriteLine(e.Message);
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Récupération de la chaîne de connexion
        /// </summary>
        /// <param name="name">Chaîne au format string à récupérer</param>
        /// <returns></returns>
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            Log.Information("Chaîne de connection récupérée avec succès (GetConnectionStringByName).");
            Console.WriteLine("Chaîne de connection récupérée avec succès.");
            return returnValue;
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            Log.Information("Instance récupérée avec succès (GetInstance).");
            Console.WriteLine("Instance récupérée avec succès.");
            return instance;
        }

        /// <summary>
        /// Retourne tous les Genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre", null);
            Log.Information("Genres récupérés avec succès (GetAllGenres).");
            Console.WriteLine("Genres récupérés avec succès.");
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les Rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon", null);
            Log.Information("Rayons récupérés avec succès (GetAllRayons).");
            Console.WriteLine("Rayons récupérés avec succès.");
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les Catégories de Public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public", null);
            Log.Information("Publics récupérés avec succès (GetAllPublics).");
            Console.WriteLine("Publics récupérés avec succès.");
            return new List<Categorie>(lesPublics);
        }

        /// <summary>
        /// Retourne tous les Livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre", null);
            Log.Information("Livres récupérés avec succès (GetAllLivres).");
            Console.WriteLine("Livres récupérés avec succès.");
            return lesLivres;
        }

        /// <summary>
        /// Retourne tous les Dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd", null);
            Log.Information("Dvd récupérés avec succès (GetAllDvd).");
            Console.WriteLine("Dvd récupérés avec succès.");
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les Revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue", null);
            Log.Information("Revues récupérés avec succès (GetAllRevues).");
            Console.WriteLine("Revues récupérés avec succès.");
            return lesRevues;
        }

        /// <summary>
        /// Retourne toutes les Commandes à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Commande</returns>
        public List<Commande> GetAllCommandes()
        {
            List<Commande> lesCommandes = TraitementRecup<Commande>(GET, "commande", null);
            Log.Information("Commandes récupérées avec succès (GetAllCommandes).");
            Console.WriteLine("Commandes récupérées avec succès.");
            return lesCommandes;
        }

        /// <summary>
        /// Retourne tous les Commandedocuments à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetAllCommandeDocuments()
        {
            List<CommandeDocument> lesCommandeDocuments = TraitementRecup<CommandeDocument>(GET, COMMANDE_DOCUMENT, null);
            Log.Information("CommandeDocuments récupérés avec succès (GetAllCommandeDocuments).");
            Console.WriteLine("CommandeDocuments récupérés avec succès.");
            return lesCommandeDocuments;
        }

        /// <summary>
        /// Retourne les Abonnements se terminant dans moins de 30 jours à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Abonnement</returns>
        public List<Abonnement> GetDerniersAbonnements()
        {
            List<Abonnement> lesAbonnements = TraitementRecup<Abonnement>(GET, "abonnement", null);
            Log.Information("Derniers abonnements récupérés avec succès (GetDerniersAbonnements).");
            Console.WriteLine("Derniers abonnements récupérés avec succès.");
            return lesAbonnements;
        }

        /// <summary>
        /// Retourne les Suivis à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Suivi</returns>
        public List<Suivi> GetAllSuivis()
        {
            List<Suivi> lesSuivis = TraitementRecup<Suivi>(GET, "suivi", null);
            Log.Information("Suivis récupérés avec succès (GetAllSuivis).");
            Console.WriteLine("Suivis récupérés avec succès.");
            return lesSuivis;
        }

        /// <summary>
        /// Retourne les commandedocument grâce à un id de livre ou dvd spécifique
        /// </summary>
        /// <param name="idlivredvd">L'id du Livre ou Dvd concerné</param>
        /// <returns>Liste d'objets CommandeDocument</returns>
        public List<CommandeDocument> GetCommandeDocumentsLivreDvd(string idlivredvd)
        {
            String jsonIdCommandeDocument = ConvertToJson("idlivredvd", idlivredvd);
            List<CommandeDocument> lesCommandeDocuments = TraitementRecup<CommandeDocument>(GET, COMMANDE_DOCUMENT + "/" + jsonIdCommandeDocument, null);
            Log.Information("CommandeDocuments du LivreDvd récupérés avec succès (GetCommandeDocumentsLivreDvd).");
            Console.WriteLine("CommandeDocuments du LivreDvd récupérés avec succès.");
            return lesCommandeDocuments;
        }

        /// <summary>
        /// Retourne les Documents grâce à un id spécifique
        /// </summary>
        /// <param name="iddocument">L'id du Document concerné</param>
        /// <returns>Liste d'objets Document</returns>
        public List<Document> GetDocument(string iddocument)
        {
            String jsonIdCommandeDocument = ConvertToJson("id", iddocument);
            List<Document> lesCommandeDocuments = TraitementRecup<Document>(GET, "document/" + jsonIdCommandeDocument, null);
            Log.Information("Document récupéré avec succès (GetDocument).");
            Console.WriteLine("Document récupéré avec succès.");
            return lesCommandeDocuments;
        }

        /// <summary>
        /// Retourne les Services grâce à un id spécifique
        /// </summary>
        /// <param name="id">L'id du Service concerné</param>
        /// <returns>Liste d'objets Service</returns>
        public List<Service> GetService(string id)
        {
            String jsonIdCommandeDocument = ConvertToJson("id", id);
            List<Service> lesCommandeDocuments = TraitementRecup<Service>(GET, "service/" + jsonIdCommandeDocument, null);
            Log.Information("Service récupéré avec succès (GetService).");
            Console.WriteLine("Service récupéré avec succès.");
            return lesCommandeDocuments;
        }

        /// <summary>
        /// Retourne les Utilisateurs grâce à un login et pwd spécifique (tentative d'authentification)
        /// </summary>
        /// <param name="login">Nom de l'Utilisateur concerné</param>
        /// <param name="pwd">Mot de passe de l'Utilisateur concerné</param>
        /// <returns>Liste d'objets Utilisateur</returns>
        public List<Utilisateur> GetUtilisateur(string login, string pwd)
        {
            String jsonUtilisateur = "{" + "\"login\": \"" + login + "\"," + "\"pwd\": \"" + pwd + "\"" + "}";
            List<Utilisateur> lesUtilisateurs = TraitementRecup<Utilisateur>(GET, "utilisateur/" + jsonUtilisateur, null);
            Log.Information("Utilisateur récupéré avec succès (GetUtilisateur).");
            Console.WriteLine("Utilisateur récupéré avec succès.");
            return lesUtilisateurs;
        }

        /// <summary>
        /// Retourne les Abonnements grâce à un id de Revue spécifique
        /// </summary>
        /// <param name="idrevue">L'id de la Revue concernée</param>
        /// <returns>Liste d'objets Abonnement</returns>
        public List<Abonnement> GetAbonnementsRevue(string idrevue)
        {
            String jsonIdAbonnement = ConvertToJson("idrevue", idrevue);
            List<Abonnement> lesAbonnements = TraitementRecup<Abonnement>(GET, "abonnement/" + jsonIdAbonnement, null);
            Log.Information("Abonnements récupérés avec succès (GetAbonnementsRevue).");
            Console.WriteLine("Abonnements récupérés avec succès.");
            return lesAbonnements;
        }


        /// <summary>
        /// Retourne les Exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">L'id de la Revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            String jsonIdDocument = ConvertToJson("id", idDocument);
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + jsonIdDocument, null);
            Log.Information("Exemplaires récupérés avec succès (GetExemplairesRevue).");
            Console.WriteLine("Exemplaires récupérés avec succès.");
            return lesExemplaires;
        }

        /// <summary>
        /// Crée une Commande dans la BDD
        /// </summary>
        /// <param name="commande">L'objet Commande concerné</param>
        /// <returns>True si la création a pu se faire (retour != null)</returns>
        public bool CreerCommande(Commande commande)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(commande);
            try
            {
                List<Commande> liste = TraitementRecup<Commande>(POST, "commande", CHAMPS + jsonExemplaire);
                Log.Information("Commande créée avec succès (CreerCommande).");
                Console.WriteLine("Commande créée avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: la Commande n'a pas pu être créée.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Crée un Commandedocument dans la BDD
        /// </summary>
        /// <param name="commandeDocument">L'objet CommandeDocument concerné </param>
        /// <returns>True si la création a pu se faire (retour != null)</returns>
        public bool CreerCommandeDocumentsLivreDvd(CommandeDocument commandeDocument)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(commandeDocument);
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, COMMANDE_DOCUMENT, CHAMPS + jsonExemplaire);
                Log.Information("CommandeDocument crée avec succès (CreerCommandeDocumentsLivreDvd).");
                Console.WriteLine("CommandeDocument crée avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: le CommandeDocument n'a pas pu être créé.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Crée un Abonnement dans la BDD
        /// </summary>
        /// <param name="abonnement">L'objet Abonnement concerné</param>
        /// <returns>True si la création a pu se faire (retour != null)</returns>
        public bool CreerAbonnementRevue(Abonnement abonnement)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(abonnement);
            try
            {
                List<Abonnement> liste = TraitementRecup<Abonnement>(POST, "abonnement", CHAMPS + jsonExemplaire);
                Log.Information("Abonnement créée avec succès (CreerAbonnementRevue).");
                Console.WriteLine("Abonnement créée avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: l'Abonnement n'a pas pu être créé.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Crée un Suivi dans la BDD
        /// </summary>
        /// <param name="suivi">L'objet Suivi concerné</param>
        /// <returns>True si la création a pu se faire (retour != null)</returns>
        public bool CreerSuivi(Suivi suivi)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(suivi);
            try
            {
                List<Suivi> liste = TraitementRecup<Suivi>(POST, "suivi", CHAMPS + jsonExemplaire);
                Log.Information("Suivi crée avec succès (CreerSuivi).");
                Console.WriteLine("Suivi crée avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: le Suivi n'a pas pu être créé.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un Suivi dans la BDD
        /// </summary>
        /// <param name="suivi">L'objet Suivi concerné</param>
        /// <returns>True si la suppression a pu se faire (retour != null)</returns>
        public bool SupprimerSuivi(Suivi suivi)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(suivi);
            try
            {
                List<Suivi> liste = TraitementRecup<Suivi>(DELETE, "suivi/" + jsonExemplaire, null);
                Log.Information("Suivi supprimé avec succès (SupprimerSuivi).");
                Console.WriteLine("Suivi supprimé avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: le Suivi n'a pas pu être supprimé.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Modifie un Suivi dans la BDD
        /// </summary>
        /// <param name="suivi">L'objet Suivi concerné</param>
        /// <returns>True si la modification a pu se faire (retour != null)</returns>
        public bool ModifierSuivi(Suivi suivi)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(suivi);
            try
            {
                List<Suivi> liste = TraitementRecup<Suivi>(PUT, "suivi", CHAMPS + jsonExemplaire);
                Log.Information("Suivi modifié avec succès (ModifierSuivi).");
                Console.WriteLine("Suivi modifié avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: le Suivi n'a pas pu être modifié.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime une Commande dans la BDD
        /// </summary>
        /// <param name="commande">L'objet Commande concerné</param>
        /// <returns>True si la suppression a pu se faire (retour != null)</returns>
        public bool SupprimerCommande(Commande commande)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(commande);
            try
            {
                List<Commande> liste = TraitementRecup<Commande>(DELETE, "commande/" + jsonExemplaire, null);
                Log.Information("Commande supprimée avec succès (SupprimerCommande).");
                Console.WriteLine("Commande supprimée avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: la Commande n'a pas pu être supprimée.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Supprime un CommandeDocument dans la BDD
        /// </summary>
        /// <param name="commandeDocument">L'objet CommandeDocument concerné</param>
        /// <returns>True si la suppression a pu se faire (retour != null)</returns>
        public bool SupprimerCommandeDocument(CommandeDocument commandeDocument)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(commandeDocument);
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(DELETE, COMMANDE_DOCUMENT, CHAMPS + jsonExemplaire);
                Log.Information("CommandeDocument supprimé avec succès (SupprimerCommandeDocument).");
                Console.WriteLine("CommandeDocument supprimé avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: le CommandeDocument n'a pas pu être supprimé.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Crée un Exemplaire dans la BDD
        /// </summary>
        /// <param name="exemplaire">L'objet Exemplaire concerné</param>
        /// <returns>True si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire);
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire", CHAMPS + jsonExemplaire);
                Log.Information("Exemplaire créé avec succès (CreerExemplaire).");
                Console.WriteLine("Exemplaire créé avec succès.");
                return (liste != null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erreur: l'Exemplaire n'a pas pu être créé.");
                Console.WriteLine(ex.Message);
            }
            return false;
        }

        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée dans l'url</param>
        /// <param name="parametres">paramètres à envoyer dans le body, au format</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message, String parametres)
        {
            // trans
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message, parametres);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                    Console.WriteLine(methode + "-" + message + "-" + parametres);
                }
                else
                {
                    Log.Error("Erreur: l'API a retourné une erreur {Code} message = {Message} (TraitementRecup).", code, (string)retour["message"]);
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                }
            }catch(Exception e)
            {
                Log.Fatal(e, "Erreur fatale: impossible d'accéder à l'API lors du traitement (TraitementRecup).");
                Console.WriteLine("Erreur lors de l'accès à l'API : "+e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Convertit en json un couple nom/valeur
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="valeur"></param>
        /// <returns>couple au format json</returns>
        private static String ConvertToJson(Object nom, Object valeur)
        {
            var dictionary = new Dictionary<object, object> { { nom, valeur } };
            return JsonConvert.SerializeObject(dictionary);
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}
