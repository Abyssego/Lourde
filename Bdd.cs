using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lourde;
using MySql.Data.MySqlClient;


namespace Lourde

{
    public class Bdd
    {
        private static MySqlConnection connection;
        private static string server;
        private static string database;
        private static string uid;
        private static string password;



        //Initialisation des valeurs
        public static void Initialize()
        {
            //Information de connection pour la BDD
            server = "localhost";      //Adresse du serveur 
            database = "bdd_infotools";    //Nom de la BDD
            uid = "root";              //Identifiant d'un utilisateur
            password = "chacal";         //Mot de passe du même utilisateur
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private static bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                Console.WriteLine("Erreur connexion BDD");
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private static bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }



        //Méthode pour insérer un nouvel enregistrement à la table RendezVous
        public static void InsertRendezVous(string nomRendezVous, string prenomRendezVous, string mailRendezVous, string telRendezVous, int numSirenRendezVous, string nomSocieteRendezVous, string villeRendezVous, string cpRendezVous, string adresseRendezVous, string dateRendezVous, string heureDebutRendezVous, string heureFinRendezVous, string butRendezVous, string descriptionRendezVous, int confirmationRendezVous, int numCommerciaux)
        {
            //Requête Insertion RendezVous
            string query = "INSERT INTO rendezvous (nomRendezVous, prenomRendezVous, mailRendezVous, telRendezVous, numSirenRendezVous, nomSocieteRendezVous, villeRendezVous, cpRendezVous, adresseRendezVous, dateRendezVous, heureDebutRendezVous, heureFinRendezVous, butRendezVous, descriptionRendezVous, confirmationRendezVous, numCommerciaux) VALUES('" + nomRendezVous + "','" + prenomRendezVous + "','" + mailRendezVous + "','" + telRendezVous + "','" + numSirenRendezVous + "','" + nomSocieteRendezVous + "','" + villeRendezVous + "','" + cpRendezVous + "','" + adresseRendezVous + "','" + dateRendezVous + "','" + heureDebutRendezVous + "','" + heureFinRendezVous + "','" + butRendezVous + "','" + descriptionRendezVous + "','" + confirmationRendezVous + "','" + numCommerciaux + "')";
            MessageBox.Show(Convert.ToString(query));
            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateRendezVous(int numRendezVous, string nomRendezVous, string prenomRendezVous, string mailRendezVous, string telRendezVous, int numSirenRendezVous, string nomSocieteRendezVous, string villeRendezVous, string cpRendezVous, string adresseRendezVous, string dateRendezVous, string heureDebutRendezVous, string heureFinRendezVous, string butRendezVous, string descriptionRendezVous, int confirmationRendezVous, int NumCommerciaux)
        {
            //Update RendezVous
            string query = "UPDATE rendezvous SET numRendezVous='" + numRendezVous + "', nomRendezVous='" + nomRendezVous + "', prenomRendezVous='" + prenomRendezVous + "', mailRendezVous ='" + mailRendezVous + "', telRendezVous ='" + telRendezVous + "', numSirenRendezVous ='" + numSirenRendezVous + "', nomSocieteRendezVous ='" + nomSocieteRendezVous + "', villeRendezVous ='" + villeRendezVous + "', cpRendezVous = '" + cpRendezVous + "', adresseRendezVous ='" + adresseRendezVous + "', dateRendezVous = '" + dateRendezVous + "', heureDebutRendezVous ='" + heureDebutRendezVous + "', heureFinRendezVous = '" + heureFinRendezVous + "', butRendezVous ='" + butRendezVous + "', descriptionRendezVous ='" + descriptionRendezVous + "', confirmationRendezVous='" + confirmationRendezVous + "', numCommerciaux='" + NumCommerciaux + "' WHERE numRendezVous=" + numRendezVous;
            Console.WriteLine(query);
            MessageBox.Show(Convert.ToString(query));
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;
                
                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table RendezVous
        public static void DeleteRendezVous(int numRendezVous)
        {
            //Delete Contrat
            string query = "DELETE FROM RendezVous WHERE numRendezVous=" + numRendezVous;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table RendezVous
        public static List<RendezVous> SelectRendezVous()
        {
            //Select statement
            string query = "SELECT * FROM rendezvous INNER JOIN commerciaux ON rendezvous.numCommerciaux = commerciaux.numCommerciaux INNER JOIN identification ON commerciaux.numPassword = identification.numPassword";

            //Create a list to store the result
            List<RendezVous> dbRendezVous = new List<RendezVous>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReader["numPassword"]), Convert.ToString(dataReader["Password"]));
                    Commerciaux leCommerciaux = new Commerciaux(Convert.ToInt16(dataReader["numCommerciaux"]), Convert.ToString(dataReader["nomCommerciaux"]), Convert.ToString(dataReader["prenomCommerciaux"]), Convert.ToString(dataReader["adresseCommerciaux"]), Convert.ToString(dataReader["villeCommerciaux"]), Convert.ToString(dataReader["cpCommerciaux"]), Convert.ToString(dataReader["mailCommerciaux"]), Convert.ToString(dataReader["telCommerciaux"]), leIdentification);
                    RendezVous leRendezVous = new RendezVous(Convert.ToInt16(dataReader["numRendezVous"]), Convert.ToString(dataReader["nomRendezVous"]), Convert.ToString(dataReader["prenomRendezVous"]), Convert.ToString(dataReader["mailRendezVous"]), Convert.ToString(dataReader["telRendezVous"]), Convert.ToInt32(dataReader["numSirenRendezVous"]), Convert.ToString(dataReader["nomSocieteRendezVous"]), Convert.ToString(dataReader["villeRendezVous"]), Convert.ToString(dataReader["cpRendezVous"]), Convert.ToString(dataReader["adresseRendezVous"]), Convert.ToString(dataReader["dateRendezVous"]), Convert.ToString(dataReader["heureDebutRendezVous"]), Convert.ToString(dataReader["heureFinRendezVous"]), Convert.ToString(dataReader["butRendezVous"]), Convert.ToString(dataReader["descriptionRendezVous"]), Convert.ToInt32(dataReader["confirmationRendezVous"]), leCommerciaux);
                    dbRendezVous.Add(leRendezVous);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbRendezVous;
            }
            else
            {
                return dbRendezVous;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table RendezVous
        public static RendezVous SearchRendezVous(int numRendezVous)
        {
            //Select statement
            string query = "SELECT * FROM rendezvous INNER JOIN commerciaux ON rendezvous.numCommerciaux = commerciaux.numCommerciaux INNER JOIN identification ON commerciaux.numPassword = identification.numPassword WHERE numRendezVous = " + numRendezVous;

            //Create a list to store the result
            List<RendezVous> dbRendezVous = new List<RendezVous>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReaderS["numPassword"]), Convert.ToString(dataReaderS["Password"]));
                    Commerciaux leCommerciaux = new Commerciaux(Convert.ToInt16(dataReaderS["numcommerciaux"]), Convert.ToString(dataReaderS["nomcommerciaux"]), Convert.ToString(dataReaderS["prenomcommerciaux"]), Convert.ToString(dataReaderS["adressecommerciaux"]), Convert.ToString(dataReaderS["villecommerciaux"]), Convert.ToString(dataReaderS["cpcommerciaux"]), Convert.ToString(dataReaderS["mailcommerciaux"]), Convert.ToString(dataReaderS["telcommerciaux"]), leIdentification);
                    RendezVous leRendezVous = new RendezVous(Convert.ToInt32(dataReaderS["numRendezVous"]), Convert.ToString(dataReaderS["nomRendezVous"]), Convert.ToString(dataReaderS["prenomRendezVous"]), Convert.ToString(dataReaderS["mailRendezVous"]), Convert.ToString(dataReaderS["telRendezVous"]), Convert.ToInt32(dataReaderS["numSirenRendezVous"]), Convert.ToString(dataReaderS["nomSocieteRendezVous"]), Convert.ToString(dataReaderS["villeRendezVous"]), Convert.ToString(dataReaderS["cpRendezVous"]), Convert.ToString(dataReaderS["adresseRendezVous"]), Convert.ToString(dataReaderS["dateSirenRendezVous"]), Convert.ToString(dataReaderS["heureDebutRendezVous"]), Convert.ToString(dataReaderS["heureFinRendezVous"]), Convert.ToString(dataReaderS["butRendezVous"]), Convert.ToString(dataReaderS["descriptionRendezVous"]), Convert.ToInt32(dataReaderS["confirmationRendezVous"]), leCommerciaux);
                    dbRendezVous.Add(leRendezVous);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbRendezVous[0];

            }
            else
            {
                //retour de la collection pour être affichée
                return dbRendezVous[0];
            }

        }






        //Méthode pour insérer un nouvel enregistrement à la table Commerciaux
        public static void InsertCommerciaux(string nomCommerciaux, string prenomCommerciaux, string adresseCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO commerciaux (nomCommerciaux, prenomCommerciaux, adresseCommerciaux, villeCommerciaux, cpCommerciaux, mailCommerciaux, telCommerciaux, numPassword) VALUES('" + nomCommerciaux + "','" + prenomCommerciaux + "','" + adresseCommerciaux + "','" + villeCommerciaux + "','" + cpCommerciaux + "','" + mailCommerciaux + "','" + telCommerciaux + "','" + 1 + "')"; //Password 1 password par défaut

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateCommerciaux(int numCommerciaux, string nomCommerciaux, string prenomCommerciaux, string adresseCommerciaux, string villeCommerciaux, string cpCommerciaux, string mailCommerciaux, string telCommerciaux)
        {
            //Update Contrat
            string query = "UPDATE commerciaux SET numCommerciaux='" + numCommerciaux + "', nomCommerciaux='" + nomCommerciaux + "', prenomCommerciaux='" + prenomCommerciaux + "', adresseCommerciaux ='" + adresseCommerciaux + "', villeCommerciaux ='" + villeCommerciaux + "', cpCommerciaux ='" + cpCommerciaux + "', mailCommerciaux ='" + mailCommerciaux + "', telCommerciaux ='" + telCommerciaux + "', numPassword = '" + 1 +"' WHERE numCommerciaux = '" + numCommerciaux + "'";  //Password par défaut
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Commerciaux 
        public static void DeleteCommerciaux(int numCommerciaux)
        {
            //Delete Contrat
            string query = "DELETE FROM Commerciaux WHERE NumCommerciaux=" + numCommerciaux;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Commerciaux
        public static List<Commerciaux> SelectCommerciaux()
        {
            //Select statement
            string query = "SELECT * FROM commerciaux INNER JOIN identification ON commerciaux.numPassword = identification.numPassword";

            //Create a list to store the result
            List<Commerciaux> dbCommerciaux = new List<Commerciaux>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReader["numpassword"]), Convert.ToString(dataReader["password"]));
                    Commerciaux leCommerciaux = new Commerciaux(Convert.ToInt16(dataReader["numcommerciaux"]), Convert.ToString(dataReader["nomcommerciaux"]), Convert.ToString(dataReader["prenomcommerciaux"]), Convert.ToString(dataReader["adressecommerciaux"]), Convert.ToString(dataReader["villecommerciaux"]), Convert.ToString(dataReader["cpcommerciaux"]), Convert.ToString(dataReader["mailcommerciaux"]), Convert.ToString(dataReader["telcommerciaux"]), leIdentification);
                    dbCommerciaux.Add(leCommerciaux);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbCommerciaux;
            }
            else
            {
                return dbCommerciaux;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Commerciaux
        public static Commerciaux SearchCommerciaux(int numCommerciaux)
        {
            //Select statement
            string query = "SELECT * FROM commerciaux WHERE INNER JOIN identification ON commerciaux.numPassword = identification.numPassword numCommerciaux = " + numCommerciaux;

            //Create a list to store the result
            List<Commerciaux> dbCommerciaux = new List<Commerciaux>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReaderS["numPassword"]), Convert.ToString(dataReaderS["Password"]));
                    Commerciaux leCommerciaux = new Commerciaux(Convert.ToInt16(dataReaderS["numcommerciaux"]), Convert.ToString(dataReaderS["nomcommerciaux"]), Convert.ToString(dataReaderS["prenomcommerciaux"]), Convert.ToString(dataReaderS["adressecommerciaux"]), Convert.ToString(dataReaderS["villecommerciaux"]), Convert.ToString(dataReaderS["cpcommerciaux"]), Convert.ToString(dataReaderS["mailcommerciaux"]), Convert.ToString(dataReaderS["telcommerciaux"]), leIdentification);
                    dbCommerciaux.Add(leCommerciaux);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbCommerciaux[0];
            }
            else
            {
                //retour de la collection pour être affichée
                return dbCommerciaux[0];
            }

        }







        //Méthode pour insérer un nouvel enregistrement à la table Manager
        public static void InsertManager(string nomManager, string prenomManager, string adresseManager, string villeManager, string cpManager, string mailManager, string telManager, int lePassword)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO manager (nomManager, prenomManager, adresseManager, villeManager, cpManager, mailManager, telManager, numPassword) VALUES('" + nomManager + "','" + prenomManager + "','" + adresseManager + "','" + villeManager + "','" + cpManager + "','" + mailManager + "','" + telManager + "','" + lePassword + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateManager(int numManager, string nomManager, string prenomManager, string adresseManager, string villeManager, string cpManager, string mailManager, string telManager, Identification lePassword)
        {
            //Update Contrat
            string query = "UPDATE manager SET numManager='" + numManager + "', nomManager='" + nomManager + "', prenomManager='" + prenomManager + "', adresseManager ='" + adresseManager + "', villeManager ='" + villeManager + "', cpManager ='" + cpManager + "', mailManager ='" + mailManager + "', telManager ='" + telManager + "', numPassword = " + lePassword;
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Manager
        public static void DeleteManager(int numManager)
        {
            //Delete Contrat
            string query = "DELETE FROM manager WHERE NumCommerciaux=" + numManager;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Manager
        public static List<Manager> SelectManager()
        {
            //Select statement
            string query = "SELECT * FROM manager INNER JOIN identification ON manager.numPassword = identification.numPassword";

            //Create a list to store the result
            List<Manager> dbManager = new List<Manager>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReader["numpassword"]), Convert.ToString(dataReader["password"]));
                    Manager leManager = new Manager(Convert.ToInt16(dataReader["numManager"]), Convert.ToString(dataReader["nomManager"]), Convert.ToString(dataReader["prenomManager"]), Convert.ToString(dataReader["adresseManager"]), Convert.ToString(dataReader["villeManager"]), Convert.ToString(dataReader["cpManager"]), Convert.ToString(dataReader["mailManager"]), Convert.ToString(dataReader["telManager"]), leIdentification);
                    dbManager.Add(leManager);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbManager;
            }
            else
            {
                return dbManager;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Manager
        public static Manager SearchManager(int numManager)
        {
            //Select statement
            string query = "SELECT * FROM manager INNER JOIN identification ON manager.numPassword = identification.numPassword WHERE numManager = " + numManager;

            //Create a list to store the result
            List<Manager> dbManager = new List<Manager>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReaderS["numPassword"]), Convert.ToString(dataReaderS["Password"]));
                    Manager leManager = new Manager(Convert.ToInt32(dataReaderS["numManager"]), Convert.ToString(dataReaderS["nomManager"]), Convert.ToString(dataReaderS["prenomManager"]), Convert.ToString(dataReaderS["adresseManager"]), Convert.ToString(dataReaderS["villeManager"]), Convert.ToString(dataReaderS["cpManager"]), Convert.ToString(dataReaderS["mailManager"]), Convert.ToString(dataReaderS["telManager"]), leIdentification);
                    dbManager.Add(leManager);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbManager[0];
            }
            else
            {
                //retour de la collection pour être affichée
                return dbManager[0];
            }

        }








        //Méthode pour insérer un nouvel enregistrement à la table Client
        public static void InsertClient(string nomClient, string prenomClient, string adresseClient, string villeClient, string cpClient, string mailClient, string telClient, int numEntreprise)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO client (nomClient, prenomClient, adresseClient, villeClient, cpClient, mailClient, telClient, numSirenEntreprise) VALUES('" + nomClient  + "','" + prenomClient + "','" + adresseClient + "','" + villeClient + "','" + cpClient + "','" + mailClient + "','" + telClient + "','" + numEntreprise + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateClient(int numClient, string nomClient, string prenomClient, string adresseClient, string villeClient, string cpClient, string mailClient, string telClient, int numEntreprise)
        {
            //Update Contrat
            string query = "UPDATE client SET numClient='" + numClient + "', nomClient='" + nomClient + "', prenomClient='" + prenomClient + "', adresseMClient ='" + adresseClient + "', villeClient ='" + villeClient + "', cpClient ='" + cpClient + "', mailClient ='" + mailClient + "', telClient ='" + telClient + "', numSirenEntreprise = " + numEntreprise;
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Client
        public static void DeleteClient(int numClient)
        {
            //Delete Contrat
            string query = "DELETE FROM client WHERE numClient=" + numClient;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Client
        public static List<Client> SelectClient()
        {
            //Select statement
            string query = "SELECT * FROM client INNER JOIN entreprise ON client.numSirenEntreprise = entreprise.numSirenEntreprise";

            //Create a list to store the result
            List<Client> dbClient = new List<Client>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReader["numSirenEntreprise"]), Convert.ToString(dataReader["nomEntreprise"]), Convert.ToString(dataReader["villeSiegeEntreprise"]), Convert.ToString(dataReader["cpSiegeEntreprise"]), Convert.ToString(dataReader["adresseSiegeEntreprise"]), Convert.ToString(dataReader["villeLocauxEntreprise"]), Convert.ToString(dataReader["cpLocauxEntreprise"]), Convert.ToString(dataReader["adresseLocauxEntreprise"]));
                    Client leClient = new Client(Convert.ToInt32(dataReader["numClient"]), Convert.ToString(dataReader["nomClient"]), Convert.ToString(dataReader["prenomClient"]), Convert.ToString(dataReader["adresseClient"]), Convert.ToString(dataReader["villeClient"]), Convert.ToString(dataReader["cpClient"]), Convert.ToString(dataReader["mailClient"]), Convert.ToString(dataReader["telClient"]), leEntreprise);
                    dbClient.Add(leClient);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbClient;
            }
            else
            {
                return dbClient;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Client
        public static Client SearchClient(int numClient)
        {
            //Select statement
            string query = "SELECT * FROM client INNER JOIN entreprise ON client.numSirenEntreprise = entreprise.numSirenEntreprise WHERE numClient = '" + numClient + "'";

            //Create a list to store the result
            List<Client> dbClient = new List<Client>();

            if(Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReaderS["numSirenEntreprise"]), Convert.ToString(dataReaderS["nomEntreprise"]), Convert.ToString(dataReaderS["villeSiegeEntreprise"]), Convert.ToString(dataReaderS["cpSiegeEntreprise"]), Convert.ToString(dataReaderS["adresseSiegeEntreprise"]), Convert.ToString(dataReaderS["villeLocauxEntreprise"]), Convert.ToString(dataReaderS["cpLocauxEntreprise"]), Convert.ToString(dataReaderS["adresseLocauxEntreprise"]));
                    Client leClient = new Client(Convert.ToInt32(dataReaderS["numClient"]), Convert.ToString(dataReaderS["nomClient"]), Convert.ToString(dataReaderS["prenomClient"]), Convert.ToString(dataReaderS["adresseClient"]), Convert.ToString(dataReaderS["villeClient"]), Convert.ToString(dataReaderS["cpClient"]), Convert.ToString(dataReaderS["mailClient"]), Convert.ToString(dataReaderS["telClient"]), leEntreprise);
                    dbClient.Add(leClient);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbClient[0];

            }
            else
            {
                return dbClient[0];
            }
        }








        //Méthode pour insérer un nouvel enregistrement à la table Prospect
        public static void InsertProspect(string nomProspect, string prenomProspect, string adresseProspect, string villeProspect, string cpProspect, string mailProspect, string telProspect, int numEntreprise)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO prospect (nomProspect, prenomProspect, adresseProspect, villeProspect, cpProspect, mailProspect, telProspect, numSirenEntreprise) VALUES('" + nomProspect + "','" + prenomProspect + "','" + adresseProspect + "','" + villeProspect + "','" + cpProspect + "','" + mailProspect + "','" + telProspect + "','" + numEntreprise + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateProspect(int numProspect, string nomProspect, string prenomProspect, string adresseProspect, string villeProspect, string cpProspect, string mailProspect, string telProspect, int numEntreprise)
        {
            //Update Contrat
            string query = "UPDATE prospect SET numProspect='" + numProspect + "', nomProspect='" + nomProspect + "', prenomProspect='" + prenomProspect + "', adresseProspect ='" + adresseProspect + "', villeProspect ='" + villeProspect + "', cpProspect ='" + cpProspect + "', mailProspect ='" + mailProspect + "', telProspect ='" + telProspect + "', numSirenEntreprise = '" + numEntreprise +"'";
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Prospect
        public static void DeleteProspect(int numProspect)
        {
            //Delete Contrat
            string query = "DELETE FROM prospect WHERE numProspect=" + numProspect;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Prospect
        public static List<Prospect> SelectProspect()
        {
            //Select statement
            string query = "SELECT * FROM prospect INNER JOIN entreprise ON prospect.numSirenEntreprise = entreprise.numSirenEntreprise";

            //Create a list to store the result
            List<Prospect> dbProspect = new List<Prospect>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReader["numSirenEntreprise"]), Convert.ToString(dataReader["nomEntreprise"]), Convert.ToString(dataReader["villeSiegeEntreprise"]), Convert.ToString(dataReader["cpSiegeEntreprise"]), Convert.ToString(dataReader["adresseSiegeEntreprise"]), Convert.ToString(dataReader["villeLocauxEntreprise"]), Convert.ToString(dataReader["cpLocauxEntreprise"]), Convert.ToString(dataReader["adresseLocauxEntreprise"]));
                    Prospect leProspect = new Prospect(Convert.ToInt32(dataReader["numProspect"]), Convert.ToString(dataReader["nomProspect"]), Convert.ToString(dataReader["prenomProspect"]), Convert.ToString(dataReader["adresseProspect"]), Convert.ToString(dataReader["villeProspect"]), Convert.ToString(dataReader["cpProspect"]), Convert.ToString(dataReader["mailProspect"]), Convert.ToString(dataReader["telProspect"]), leEntreprise);
                    dbProspect.Add(leProspect);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProspect;
            }
            else
            {
                return dbProspect;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Prospect
        public static Prospect SearchProspect(int numProspect)
        {
            //Select statement
            string query = "SELECT * FROM prospect INNER JOIN entreprise ON prospect.numSirenEntreprise = entreprise.numSirenEntreprise WHERE numProspect = '" + numProspect + "'";

            //Create a list to store the result
            List<Prospect> dbProspect = new List<Prospect>();
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReaderS["numSirenEntreprise"]), Convert.ToString(dataReaderS["nomEntreprise"]), Convert.ToString(dataReaderS["villeSiegeEntreprise"]), Convert.ToString(dataReaderS["cpSiegeEntreprise"]), Convert.ToString(dataReaderS["adresseSiegeEntreprise"]), Convert.ToString(dataReaderS["villeLocauxEntreprise"]), Convert.ToString(dataReaderS["cpLocauxEntreprise"]), Convert.ToString(dataReaderS["adresseLocauxEntreprise"]));
                    Prospect leProspect = new Prospect(Convert.ToInt32(dataReaderS["numProspect"]), Convert.ToString(dataReaderS["nomProspect"]), Convert.ToString(dataReaderS["prenomProspect"]), Convert.ToString(dataReaderS["adresseProspect"]), Convert.ToString(dataReaderS["villeProspect"]), Convert.ToString(dataReaderS["cpProspect"]), Convert.ToString(dataReaderS["mailProspect"]), Convert.ToString(dataReaderS["telProspect"]), leEntreprise);
                    dbProspect.Add(leProspect);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProspect[0];

            }
            else
            {
                //retour de la collection pour être affichée
                return dbProspect[0];
            }

        }








        //Méthode pour insérer un nouvel enregistrement à la table Stock
        public static void InsertStock(int nombreStock, int numProduit)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO stock (nombreStock, numProduit) VALUES('" + nombreStock + "','" + numProduit + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateStock(int numStock, string nombreStock, int numProduit)
        {
            //Update Contrat
            string query = "UPDATE stock SET numStock='" + numStock + "', nombreStock='" + nombreStock + "', numProduit = '" + numProduit + "'";
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Stock
        public static void DeleteStock(int numStock)
        {
            //Delete Contrat
            string query = "DELETE FROM stock WHERE numStock=" + numStock;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Stock
        public static List<Stock> SelectStock()
        {
            //Select statement
            string query = "SELECT * FROM stock INNER JOIN produit ON stock.numProduit = produit.numProduit INNER JOIN produittype ON produit.typeProduit = produittype.typeProduit";

            //Create a list to store the result
            List<Stock> dbStock = new List<Stock>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReader["typeProduit"]));
                    Produit leProduit = new Produit(Convert.ToInt32(dataReader["numProduit"]), Convert.ToString(dataReader["nomProduit"]), Convert.ToString(dataReader["petiteDescriptionProduit"]), Convert.ToString(dataReader["longueDescriptionProduit"]), Convert.ToString(dataReader["prixProduit"]), leProduitType);
                    Stock leStock = new Stock(Convert.ToInt32(dataReader["numStock"]), Convert.ToInt32(dataReader["nombreStock"]), leProduit);
                    
                    dbStock.Add(leStock);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbStock;
            }
            else
            {
                return dbStock;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Stock
        public static Stock SearchStock(int numStock)
        {
            //Select statement
            string query = "SELECT * FROM stock INNER JOIN produit ON stock.numProduit = produit.numProduit INNER JOIN produittype ON produit.typeProduit = produittype.typeProduit WHERE numStock = '" + numStock + "'";

            //Create a list to store the result
            List<Stock> dbStock = new List<Stock>();

            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReaderS["typeProduit"]));
                    Produit leProduit = new Produit(Convert.ToInt32(dataReaderS["numProduit"]), Convert.ToString(dataReaderS["nomProduit"]), Convert.ToString(dataReaderS["petiteDescriptionProduit"]), Convert.ToString(dataReaderS["longueDescriptionProduit"]), Convert.ToString(dataReaderS["prixProduit"]), leProduitType);
                    Stock leStock = new Stock(Convert.ToInt32(dataReaderS["numStock"]), Convert.ToInt32(dataReaderS["nombreStock"]), leProduit);
                    dbStock.Add(leStock);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbStock[0];
            }
            else
            {
                //retour de la collection pour être affichée
                return dbStock[0];
            }

        }





        //Méthode pour insérer un nouvel enregistrement à la table typeProduit
        public static void InsertProduitType(string typeProduit)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO produittype (typeProduit) VALUES('" + typeProduit + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateProduitType(string typeProduit)
        {
            //Update Contrat
            string query = "UPDATE produittype SET typeProduit='" + typeProduit + "'";
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table typeProduit
        public static void DeleteProduitType(string typeProduit)
        {
            //Delete Contrat
            string query = "DELETE FROM produittype WHERE typeProduit='" + typeProduit +"'";   //Pas oublié les ' '

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table ProduitType
        public static List<ProduitType> SelectProduitType()
        {
            //Select statement
            string query = "SELECT * FROM produittype";

            //Create a list to store the result
            List<ProduitType> dbProduitType = new List<ProduitType>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReader["typeProduit"]));

                    dbProduitType.Add(leProduitType);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProduitType;
            }
            else
            {
                return dbProduitType;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table typeProduit
        public static ProduitType SearchProduitType(int typeProduit)
        {
            //Select statement
            string query = "SELECT * FROM produittype WHERE typeProduit = " + typeProduit;

            //Create a list to store the result
            List<ProduitType> dbProduitType = new List<ProduitType>();


            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReaderS["typeProduit"]));
                    dbProduitType.Add(leProduitType);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProduitType[0];


            }
            else
            {
                //retour de la collection pour être affichée
                return dbProduitType[0];
            }
        }









        //Méthode pour insérer un nouvel enregistrement à la table Produit
        public static void InsertProduit(string nomProduit, string petiteDescriptionProduit, string longueDescriptionProduit, string prixProduit, String leProduitType)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO produit (nomProduit, petiteDescriptionProduit, longueDescriptionProduit, prixProduit, typeProduit) VALUES('" + nomProduit + "','" + petiteDescriptionProduit + "','" + longueDescriptionProduit + "','" + prixProduit + "','" + leProduitType +"')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateProduit(int numProduit, string nomProduit, string petiteDescriptionProduit, string longueDescriptionProduit, string prixProduit, String leProduitType)
        {
            //Update Contrat
            string query = "UPDATE produit SET numProduit='" + numProduit + "', nomProduit='" + nomProduit + "', petiteDescriptionProduit='" + petiteDescriptionProduit + "', longueDescriptionProduit ='" + longueDescriptionProduit + "', prixProduit ='" + prixProduit + "', typeProduit ='" + leProduitType + "'";
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Produit
        public static void DeleteProduit(int numProduit)
        {
            //Delete Contrat
            string query = "DELETE FROM produit WHERE numProduit=" + numProduit;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Produit
        public static List<Produit> SelectProduit()
        {
            //Select statement
            string query = "SELECT * FROM produit INNER JOIN produittype ON produit.typeProduit = produittype.typeProduit";

            //Create a list to store the result
            List<Produit> dbProduit = new List<Produit>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReader["typeProduit"]));
                    Produit leProduit = new Produit(Convert.ToInt32(dataReader["numProduit"]), Convert.ToString(dataReader["nomProduit"]), Convert.ToString(dataReader["petiteDescriptionProduit"]), Convert.ToString(dataReader["longueDescriptionProduit"]), Convert.ToString(dataReader["prixProduit"]), leProduitType);
                    dbProduit.Add(leProduit);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProduit;
            }
            else
            {
                return dbProduit;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Produit
        public static Produit SearchProduit(int numProduit)
        {
            //Select statement
            string query = "SELECT * FROM produit WHERE numProduit = " + numProduit;

            //Create a list to store the result
            List<Produit> dbProduit = new List<Produit>();

            if (Bdd.OpenConnection() == true)
            {

                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReaderS["typeProduit"]));
                    Produit leProduit = new Produit(Convert.ToInt32(dataReaderS["numProduit"]), Convert.ToString(dataReaderS["nomProduit"]), Convert.ToString(dataReaderS["petiteDescriptionProduit"]), Convert.ToString(dataReaderS["longueDescriptionProduit"]), Convert.ToString(dataReaderS["prixProduit"]), leProduitType);
                    dbProduit.Add(leProduit);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbProduit[0];
            }
            else
            {
                //retour de la collection pour être affichée
                return dbProduit[0];
            }


        }










        //Méthode pour insérer un nouvel enregistrement à la table Entreprise
        public static void InsertEntreprise(int numSirenEntreprise, string nomEntreprise, string villeSiegeEntreprise, string cpSiegeEntreprise, string adresseSiegeEntreprise, string villeLocauxEntreprise, string cpLocauxEntreprise, string adresseLocauxEntreprise)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO entreprise (numSirenEntreprise, nomEntreprise, villeSiegeEntreprise, cpSiegeEntreprise, adresseSiegeEntreprise, villeLocauxEntreprise,cpLocauxEntreprise, adresseLocauxEntreprise) VALUES('" + numSirenEntreprise + "','" + nomEntreprise + "','" + villeSiegeEntreprise + "','" + cpSiegeEntreprise + "','" + adresseSiegeEntreprise + "','" + villeLocauxEntreprise + "','" + cpLocauxEntreprise + "','" + adresseLocauxEntreprise + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateEntreprise(int numSirenEntreprise, string nomEntreprise, string villeSiegeEntreprise, string cpSiegeEntreprise, string adresseSiegeEntreprise, string villeLocauxEntreprise, string cpLocauxEntreprise, string adresseLoacauxEntreprise)
        {
            //Update Contrat
            string query = "UPDATE entreprise SET numSirenEntreprise='" + numSirenEntreprise + "', nomEntreprise='" + nomEntreprise + "', villeSiegeEntreprise='" + villeSiegeEntreprise + "', cpSiegeEntreprise ='" + cpSiegeEntreprise + "', adresseSiegeEntreprise ='" + adresseSiegeEntreprise + "', villeLocauxEntreprise ='" + villeLocauxEntreprise + "', cpLocauxEntreprise ='" + cpLocauxEntreprise + "', adresseLocauxEntreprise ='" + adresseLoacauxEntreprise;
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Entreprise
        public static void DeleteEntreprise(int numEntreprise)
        {
            //Delete Contrat
            string query = "DELETE FROM entreprise WHERE numSirenEntreprise=" + numEntreprise;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Entreprise
        public static List<Entreprise> SelectEntreprise()
        {
            //Select statement
            string query = "SELECT * FROM entreprise";

            //Create a list to store the result
            List<Entreprise> dbEntreprise = new List<Entreprise>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReader["numSirenEntreprise"]), Convert.ToString(dataReader["nomEntreprise"]), Convert.ToString(dataReader["villeSiegeEntreprise"]), Convert.ToString(dataReader["cpSiegeEntreprise"]), Convert.ToString(dataReader["adresseSiegeEntreprise"]), Convert.ToString(dataReader["villeLocauxEntreprise"]), Convert.ToString(dataReader["cpLocauxEntreprise"]), Convert.ToString(dataReader["adresseLocauxEntreprise"]));
                    dbEntreprise.Add(leEntreprise);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbEntreprise;
            }
            else
            {
                return dbEntreprise;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Entreprise
        public static Entreprise SearchEntreprise(int numEntreprise)
        {
            //Select statement
            string query = "SELECT * FROM entreprise WHERE numEntreprise = " + numEntreprise;

            //Create a list to store the result
            List<Entreprise> dbEntreprise = new List<Entreprise>();

            if (Bdd.OpenConnection() == true)
            {

                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReaderS["numSirenEntreprise"]), Convert.ToString(dataReaderS["nomEntreprise"]), Convert.ToString(dataReaderS["villeSiegeEntreprise"]), Convert.ToString(dataReaderS["cpSiegeEntreprise"]), Convert.ToString(dataReaderS["adresseSiegeEntreprise"]), Convert.ToString(dataReaderS["villeLocauxEntreprise"]), Convert.ToString(dataReaderS["cpLocauxEntreprise"]), Convert.ToString(dataReaderS["adresseLocauxEntreprise"]));
                    dbEntreprise.Add(leEntreprise);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbEntreprise[0];


            }
            else
            {
                //retour de la collection pour être affichée
                return dbEntreprise[0];
            }


        }













        //Méthode pour insérer un nouvel enregistrement à la table Facture
        public static void InsertFacture(string dateFacture, int qteFacture, int numProduit, int numClient)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO facture (dateFacture, qteFacture, numProduit, numClient) VALUES('" + dateFacture + "','" + qteFacture + "','" + numProduit + "','" + numClient + "')";

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        public static void UpdateFacture(int numFacture, string dateFacture, int qteFacture, int numProduit, int numClient)
        {
            //Update Contrat
            string query = "UPDATE facture SET numFacturek='" + numFacture + "', dateFacture='" + dateFacture + "', qteFacture = " + qteFacture + "', numProduit = " + numProduit + "', numClient = " + numClient;
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }


        //Méthode pour supprimer un élément au numéro donnée de la table Facture
        public static void DeleteFacture(int numFacture)
        {
            //Delete Contrat
            string query = "DELETE FROM facture WHERE numFacture=" + numFacture;

            if (Bdd.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                Bdd.CloseConnection();
            }
        }


        //Méthode pour afficher tous les enregistrements de la table Facture
        public static List<Facture> SelectFacture()
        {
            //Select statement
            string query = "SELECT * FROM facture INNER JOIN client ON facture.numClient = client.numClient INNER JOIN entreprise ON client.numSirenEntreprise = entreprise.numSirenEntreprise INNER JOIN produit ON facture.numProduit = produit.numProduit INNER JOIN produittype ON produit.typeProduit = produittype.typeProduit";

            //Create a list to store the result
            List<Facture> dbFacture = new List<Facture>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReader["numSirenEntreprise"]), Convert.ToString(dataReader["nomEntreprise"]), Convert.ToString(dataReader["villeSiegeEntreprise"]), Convert.ToString(dataReader["cpSiegeEntreprise"]), Convert.ToString(dataReader["adresseSiegeEntreprise"]), Convert.ToString(dataReader["villeLocauxEntreprise"]), Convert.ToString(dataReader["cpLocauxEntreprise"]), Convert.ToString(dataReader["adresseLocauxEntreprise"]));
                    Client leClient = new Client(Convert.ToInt32(dataReader["numClient"]), Convert.ToString(dataReader["nomClient"]), Convert.ToString(dataReader["prenomClient"]), Convert.ToString(dataReader["adresseClient"]), Convert.ToString(dataReader["villeClient"]), Convert.ToString(dataReader["cpClient"]), Convert.ToString(dataReader["mailClient"]), Convert.ToString(dataReader["telClient"]), leEntreprise);
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReader["typeProduit"]));
                    Produit leProduit = new Produit(Convert.ToInt32(dataReader["numProduit"]), Convert.ToString(dataReader["nomProduit"]), Convert.ToString(dataReader["petiteDescriptionProduit"]), Convert.ToString(dataReader["longueDescriptionProduit"]), Convert.ToString(dataReader["prixProduit"]), leProduitType);
                    Facture leFacture = new Facture(Convert.ToInt32(dataReader["numFacture"]), Convert.ToString(dataReader["dateFacture"]), Convert.ToInt32(dataReader["qteFacture"]), leProduit, leClient);

                    dbFacture.Add(leFacture);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbFacture;
            }
            else
            {
                return dbFacture;
            }
        }

        //Méthode qui renvoie l'enregistrement concerné par rapport au numéro pour la table Facture
        public static Facture SearchFacture(int numFacture)
        {
            //Select statement
            string query = "SELECT * FROM facture WHERE numFacture = " + numFacture;

            //Create a list to store the result
            List<Facture> dbFacture = new List<Facture>();

            if (Bdd.OpenConnection() == true)
            {

                //Creation Command MySQL
                MySqlCommand cmdS = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReaderS = cmdS.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReaderS.Read())
                {
                    Entreprise leEntreprise = new Entreprise(Convert.ToInt32(dataReaderS["numSirenEntreprise"]), Convert.ToString(dataReaderS["nomEntreprise"]), Convert.ToString(dataReaderS["villeSiegeEntreprise"]), Convert.ToString(dataReaderS["cpSiegeEntreprise"]), Convert.ToString(dataReaderS["adresseSiegeEntreprise"]), Convert.ToString(dataReaderS["villeLocauxEntreprise"]), Convert.ToString(dataReaderS["cpLocauxEntreprise"]), Convert.ToString(dataReaderS["adresseLocauxEntreprise"]));
                    Client leClient = new Client(Convert.ToInt32(dataReaderS["numClient"]), Convert.ToString(dataReaderS["nomClient"]), Convert.ToString(dataReaderS["prenomClient"]), Convert.ToString(dataReaderS["adresseClient"]), Convert.ToString(dataReaderS["villeClient"]), Convert.ToString(dataReaderS["cpClient"]), Convert.ToString(dataReaderS["mailClient"]), Convert.ToString(dataReaderS["telClient"]), leEntreprise);
                    ProduitType leProduitType = new ProduitType(Convert.ToString(dataReaderS["typeProduit"]));
                    Produit leProduit = new Produit(Convert.ToInt32(dataReaderS["numProduit"]), Convert.ToString(dataReaderS["nomProduit"]), Convert.ToString(dataReaderS["petiteDescriptionProduit"]), Convert.ToString(dataReaderS["longueDescriptionProduit"]), Convert.ToString(dataReaderS["prixProduit"]), leProduitType);
                    Facture leFacture = new Facture(Convert.ToInt32(dataReaderS["numFacture"]), Convert.ToString(dataReaderS["dateFacture"]), Convert.ToInt32(dataReaderS["qteFacture"]), leProduit, leClient);
                    dbFacture.Add(leFacture);
                }

                //fermeture du Data Reader
                dataReaderS.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbFacture[0];


            }
            else
            {
                //retour de la collection pour être affichée
                return dbFacture[0];
            }


        }





        //Méthode pour obtenir tous les enregistrements de la table Identification
        public static List<Identification> SelectIdentification()
        {
            //Select statement
            string query = "SELECT * FROM identification";

            //Create a list to store the result
            List<Identification> dbIdentification = new List<Identification>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    Identification leIdentification = new Identification(Convert.ToInt32(dataReader["numpassword"]), Convert.ToString(dataReader["password"]));
                    dbIdentification.Add(leIdentification);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return dbIdentification;
            }
            else
            {
                return dbIdentification;
            }
        }








        //Méthode pour obtenir le num, le nom, et le prenom tous les enregistrements de la table Commerciaux
        public static List<String> SelectNomCommerciaux()
        {
            //Select statement
            string query = "SELECT numCommerciaux, nomCommerciaux, prenomCommerciaux FROM commerciaux";

            //Create a list to store the result
            List<String> listeNomCommerciaux = new List<String>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    String leCommerciaux = Convert.ToString(dataReader["numCommerciaux"]) + " " + Convert.ToString(dataReader["nomCommerciaux"]) + " " + Convert.ToString(dataReader["prenomCommerciaux"]);
           
                    listeNomCommerciaux.Add(leCommerciaux);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return listeNomCommerciaux;
            }
            else
            {
                return listeNomCommerciaux;
            }
        }

        //Méthode pour obtenir le num, le nom, et le prenom tous les enregistrements de la table Client
        public static List<String> SelectNomClient()
        {
            //Select statement
            string query = "SELECT numClient, nomClient, prenomClient FROM client";

            //Create a list to store the result
            List<String> listeNomClient = new List<String>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    String leClient = Convert.ToString(dataReader["numClient"]) + " " + Convert.ToString(dataReader["nomClient"]) + " " + Convert.ToString(dataReader["prenomClient"]);

                    listeNomClient.Add(leClient);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return listeNomClient;
            }
            else
            {
                return listeNomClient;
            }
        }

        //Méthode pour obtenir le numéro, et nom de tous les enregistrements de la table produit
        public static List<String> SelectNomProduit()
        {
            //Select statement
            string query = "SELECT numProduit, nomProduit FROM produit";

            //Create a list to store the result
            List<String> listeNomProduit = new List<String>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    String leProduit = Convert.ToString(dataReader["numProduit"]) + " " + Convert.ToString(dataReader["nomProduit"]);

                    listeNomProduit.Add(leProduit);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return listeNomProduit;
            }
            else
            {
                return listeNomProduit;
            }
        }


        //Méthode pour obtenir le numSiren, et nom d'entreprise de tous les enregistrements de la table Entreprise
        public static List<String> SelectNomEntreprise()
        {
            //Select statement
            string query = "SELECT numSirenEntreprise, nomEntreprise FROM entreprise";

            //Create a list to store the result
            List<String> listeNomEntreprise = new List<String>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    String leEntreprise = Convert.ToString(dataReader["numSirenEntreprise"]) + " " + Convert.ToString(dataReader["nomEntreprise"]);

                    listeNomEntreprise.Add(leEntreprise);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return listeNomEntreprise;
            }
            else
            {
                return listeNomEntreprise;
            }
        }


        //Méthode pour obtenir tous les typePorduit des enregistrements de la table produitType
        public static List<String> SelectNomProduitType()
        {
            //Select statement
            string query = "SELECT typeProduit FROM produittype";

            //Create a list to store the result
            List<String> listeNomProduitType = new List<String>();

            //Ouverture connection
            if (Bdd.OpenConnection() == true)
            {
                //Creation Command MySQL
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Création d'un DataReader et execution de la commande
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Lecture des données et stockage dans la collection
                while (dataReader.Read())
                {
                    String leProduitType = Convert.ToString(dataReader["typeProduit"]);

                    listeNomProduitType.Add(leProduitType);
                }

                //fermeture du Data Reader
                dataReader.Close();

                //fermeture Connection
                Bdd.CloseConnection();

                //retour de la collection pour être affichée
                return listeNomProduitType;
            }
            else
            {
                return listeNomProduitType;
            }
        }






        //Méthode pour insérer un nouveau mot de passe, dans la table Identification
        public static void InsertIdentification(string password)
        {
            //Requête Insertion Contrat
            string query = "INSERT INTO identification (password) VALUES('" + password + "')"; 

            //open connection
            if (Bdd.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }

        //Méthode pour changer le mot de passe dans la table Identification
        public static void UpdateIdentification(int numPassword, string password)
        {
            //Update Contrat
            string query = "UPDATE identification SET numPassword='" + numPassword + "',  password = " + password;
            Console.WriteLine(query);
            //Open connection
            if (Bdd.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                Bdd.CloseConnection();
            }
        }

    }
}


