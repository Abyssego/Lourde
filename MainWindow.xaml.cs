using Org.BouncyCastle.Asn1.X500;
using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OxyPlot.Axes;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using WpfTextAlignment = System.Windows.TextAlignment;
using ITextTextAlignment = iText.Layout.Properties.TextAlignment;
using ITextVerticalAlignment = iText.Layout.Properties.VerticalAlignment;
using System.Collections;
using System.IO;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace Lourde
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Créé plusieurs listes, qui correspondent aux tables de la BDD, et que l'on utilisera dans l'application, 
        /// et qui seront par la suite remplis grâce à certaines méthode dans la classe Bdd
        /// </summary>
        List<RendezVous> lesRendezVous = new List<RendezVous>();
        List<Commerciaux> lesCommerciaux = new List<Commerciaux>();
        List<Client> lesClient = new List<Client>();
        List<Prospect> lesProspect = new List<Prospect>();
        List<Entreprise> lesEntreprise = new List<Entreprise>();
        List<Facture> lesFacture = new List<Facture>();
        List<Produit> lesProduit = new List<Produit>();
        List<ProduitType> lesProduitType = new List<ProduitType>();
        List<Stock> lesStock = new List<Stock>();
        List<Manager> lesManager = new List<Manager>();

        //Méthode pour convertir un String en Int
        public static int renvoieNum(String textWithNum)
        {
            int numColonne = 0;
            if (textWithNum != "")
            {
                int i = 0;
                string numC = "";


                while (i == 0)
                {
                    foreach (char lettre in textWithNum)
                    {
                        if (i == 0)
                        {
                            if (lettre != Convert.ToChar(" "))
                            {
                                numC += lettre;
                            }
                            else
                            {
                                i++;
                            }
                        }
                    }
                }
                numColonne = Convert.ToInt32(numC);
            }
            return numColonne;
        }

        //Méthode qui initialise les éléments de l'application, comme les DataGrids où sont stocker les données de la BDD
        public MainWindow()
        {
            InitializeComponent();

            Bdd.Initialize();


            /// <summary>
            /// Remplissage des listes, avec les données présentes dans la BDD, à l'aide des méthodes de la classe Bdd
            /// </summary>
            lesRendezVous = Bdd.SelectRendezVous();
            lesRendezVous.Sort((x, y) => -1 * x.NumRendezVous.CompareTo(y.NumRendezVous));

            lesCommerciaux = Bdd.SelectCommerciaux();
            lesCommerciaux.Sort((x, y) => -1 * x.NumCommerciaux.CompareTo(y.NumCommerciaux));

            lesClient = Bdd.SelectClient();
            lesClient.Sort((x, y) => -1 * x.NumClient.CompareTo(y.NumClient));

            lesProspect = Bdd.SelectProspect();
            lesProspect.Sort((x, y) => -1 * x.NumProspect.CompareTo(y.NumProspect));

            lesEntreprise = Bdd.SelectEntreprise();

            lesFacture = Bdd.SelectFacture();
            lesFacture.Sort((x, y) => -1 * x.NumFacture.CompareTo(y.NumFacture));

            lesProduit = Bdd.SelectProduit();
            lesProduit.Sort((x, y) => -1 * x.NumProduit.CompareTo(y.NumProduit));

            lesProduitType = Bdd.SelectProduitType();

            lesStock = Bdd.SelectStock();
            lesStock.Sort((x, y) => -1 * x.NumStock.CompareTo(y.NumStock));

            lesManager = Bdd.SelectManager();



            /// <summary>
            /// Les listes sont remplie avec les données de la BDD, il faut donc les afficher dans les DataGrids,
            /// et pour se faire on utilise une méthode intégré à C#, pour relier les DataGrids, aux listes remplies
            /// </summary>
            dtgRendezVous.ItemsSource = lesRendezVous;
            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgClient.ItemsSource = lesClient;
            dtgProspect.ItemsSource = lesProspect;
            dtgEntreprise.ItemsSource = lesEntreprise;
            dtgFacture.ItemsSource = lesFacture;
            dtgProduit.ItemsSource = lesProduit;
            dtgProduitType.ItemsSource = lesProduitType;
            dtgStock.ItemsSource = lesStock;


            /// <summary>
            /// On remplie les ComboBox, selon la colonne à laquelle elle corresponde, avec les méthode pour obtenir les données,
            /// de la classe Bdd
            /// </summary>
            cboCommerciauxRendezVous.ItemsSource = Bdd.SelectNomCommerciaux();
            cboClientFacture.ItemsSource = Bdd.SelectNomClient();
            cboProduitFacture.ItemsSource = Bdd.SelectNomProduit();
            cboProduitStock.ItemsSource = Bdd.SelectNomProduit();
            cboSirenClient.ItemsSource = Bdd.SelectNomEntreprise();
            cboSirenProspect.ItemsSource = Bdd.SelectNomEntreprise();
            cboProduitTypeProduit.ItemsSource = Bdd.SelectProduitType();



            /// <summary>
            /// Initilialiser tous les SelectItem à 0, car sinon, cela peux causer des bugs
            /// </summary>
            dtgRendezVous.SelectedItem = 0;
            dtgCommerciaux.SelectedItem = 0;
            dtgClient.SelectedItem = 0;
            dtgProspect.SelectedItem = 0;
            dtgEntreprise.SelectedItem = 0;
            dtgFacture.SelectedItem = 0;
            dtgProduitType.SelectedItem = 0;
            dtgProduit.SelectedItem = 0;
            dtgStock.SelectedItem = 0;


            /// <summary>
            /// Faire un Refresh sur tous les DataGrids pour qu'ils affichent les données avec lesquelles ils ont été relié,
            /// si, il n'y a pas de Refresh, il se peut que les données ne s'affichent pas
            /// </summary>
            dtgRendezVous.Items.Refresh();
            dtgCommerciaux.Items.Refresh();
            dtgProduit.Items.Refresh();
            dtgProspect.Items.Refresh();
            dtgClient.Items.Refresh();
            dtgStock.Items.Refresh();
            dtgProduitType.Items.Refresh();
            dtgEntreprise.Items.Refresh();
            dtgFacture.Items.Refresh();



            /// <summary>
            /// Faire un Refresh sur tous les ComboBox pour qu'elles affichent les données avec lesquelles elles ont été relié,
            /// si, il n'y a pas de Refresh, il se peut que les données ne s'affichent pas
            /// </summary>
            cboCommerciauxRendezVous.Items.Refresh();
            cboClientFacture.Items.Refresh();
            cboProduitFacture.Items.Refresh();
            cboProduitStock.Items.Refresh();
            cboSirenClient.Items.Refresh();
            cboSirenProspect.Items.Refresh();
            cboProduitTypeProduit.Items.Refresh();


            //On désactive les élément que l'on ne veut pas que l'utilisateur puisse intéragir sans être connecté
            tabProduit.IsEnabled = false;
            tabRendezVous.IsEnabled = false;
            tabEntreprise.IsEnabled = false;
            tabClient.IsEnabled = false;
            tabProspect.IsEnabled = false;
            tabCommerciaux.IsEnabled = false;
            tabStatistique.IsEnabled = false;
            tabFacture.IsEnabled = false;


            //On veut cacher les élements pour qu'il ne puisse pas les utiliser
            labelNomManager.IsEnabled = false;
            labelPrenomManager.IsEnabled = false;
            labelAdresseManager.IsEnabled = false;
            labelVilleManager.IsEnabled = false;
            labelCpManager.IsEnabled = false;
            labelMailManager.IsEnabled = false;
            labelTelManager.IsEnabled = false;
            labelPasswordManager.IsEnabled = false;
            txtNomManager.IsEnabled = false;
            txtPrenomManager.IsEnabled = false;
            txtAdresseManager.IsEnabled = false;
            txtVilleManager.IsEnabled = false;
            txtCpManager.IsEnabled = false;
            txtMailManager.IsEnabled = false;
            txtTelManager.IsEnabled = false;
            txtPasswordManager.IsEnabled = false;
            btnInscrir.IsEnabled = false;
            labelInscrirNewManager.IsEnabled = false;

            //Désactiver le changement de mot de passe
            txtAncienPasswordChangerPassword.IsEnabled = false;
            labelAncienPasswordChanger.IsEnabled = false;
            labelNouveauPasswordChanger.IsEnabled = false;
            labelMailChanger.IsEnabled = false;
            txtNouveauPasswordChangerPassword.IsEnabled = false;
            txtMailChangerPassword.IsEnabled = false;
            labelChangerPassword.IsEnabled = false;



            // Associez le gestionnaire d'événements Closing à votre méthode de confirmation
            Closing += MainWindow_Closing;

            //Afficher le graphique à l'ouverture de l'application
            afficheGraphiqueFacture();

        }

        //Méthode pour hasher les mots de passes
        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir le mot de passe en tableau de bytes
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Calculer le hachage
                byte[] hash = sha256.ComputeHash(bytes);

                // Convertir le hachage en chaîne hexadécimale
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        //Méthode pour demander la confirmation pour fermer l'application
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Affichez une boîte de dialogue de confirmation
            MessageBoxResult result = MessageBox.Show("Voulez-vous vraiment quitter l'application?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Si l'utilisateur clique sur "Non", annulez la fermeture de l'application
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgRendezVous
        private void dtgRendezVous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            RendezVous selectedRendezVous = dtgRendezVous.SelectedItem as RendezVous;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                txtNumRendezVous.Text = Convert.ToString(selectedRendezVous.NumRendezVous);
                txtNomRendezVous.Text = Convert.ToString(selectedRendezVous.NomRendezVous);
                txtPrenomRendezVous.Text = selectedRendezVous.PrenomRendezVous;
                txtMailRendezVous.Text = Convert.ToString(selectedRendezVous.MailRendezVous);
                txtTelRendezVous.Text = Convert.ToString(selectedRendezVous.TelRendezVous);
                txtNumSirenRendezVous.Text = Convert.ToString(selectedRendezVous.NumSirenRendezVous);
                txtNomSocieteRendezVous.Text = Convert.ToString(selectedRendezVous.NomSocieteRendezVous);
                txtVilleRendezVous.Text = Convert.ToString(selectedRendezVous.VilleRendezVous);
                txtCpRendezVous.Text = Convert.ToString(selectedRendezVous.CpRendezVous);
                txtAdresseRendezVous.Text = Convert.ToString(selectedRendezVous.AdresseRendezVous);
                txtDateRendezVous.Text = Convert.ToString(selectedRendezVous.DateRendezVous.Substring(0, 9));
                txtHeureDebutRendezVous.Text = selectedRendezVous.HeureDebutRendezVous;
                txtHeureFinRendezVous.Text = Convert.ToString(selectedRendezVous.HeureFinRendezVous);
                txtButRendezVous.Text = Convert.ToString(selectedRendezVous.ButRendezVous);
                txtDescriptionRendezVous.Text = Convert.ToString(selectedRendezVous.DescriptionRendezVous);


                // Coche et décoche des 2 cases à cocher chkFacture et chkAgessa
                if (selectedRendezVous.ConfirmationRendezVous == 1)
                { checkConfirmation.IsChecked = true; }
                else
                { checkConfirmation.IsChecked = false; }


                //cboCommerciauxRendezVous.SelectedItem = selectedRendezVous.LeCommerciaux;
                //cboCommerciauxRendezVous.SelectedIndex = dtgRendezVous.SelectedIndex;
                txtCommerciauxRendezVous.Text = Convert.ToString(selectedRendezVous.LeCommerciaux.NumCommerciaux) + " " + Convert.ToString(selectedRendezVous.LeCommerciaux.NomCommerciaux) + " " + Convert.ToString(selectedRendezVous.LeCommerciaux.PrenomCommerciaux);
            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un RendezVous
        private void btnRendezVousAjouter_Click(object sender, RoutedEventArgs e)
        {
            int confirmationRendezVous = 0;

            string nomRendezVous = Convert.ToString(txtNomRendezVous.Text);
            string prenomRendezVous = Convert.ToString(txtPrenomRendezVous.Text);
            string mailRendezVous = Convert.ToString(txtMailRendezVous.Text);
            string telRendezVous = Convert.ToString(txtTelRendezVous.Text);
            int numSirenEntrepriseRendezVous = Convert.ToInt32(txtNumSirenRendezVous.Text);
            string nomSocieteRendezVous = Convert.ToString(txtNomSocieteRendezVous.Text);
            string villeRendezVous = Convert.ToString(txtVilleRendezVous.Text);
            string cpRendezVous = Convert.ToString(txtCpRendezVous.Text);
            string adresseRendezVous = Convert.ToString(txtAdresseRendezVous.Text);
            string dateRendezVous = Convert.ToString(txtDateRendezVous.Text);
            dateRendezVous = dateRendezVous.Substring(0, 9);
            string heureDebutRendezVous = Convert.ToString(txtHeureDebutRendezVous.Text);
            string heureFinRendezVous = Convert.ToString(txtHeureFinRendezVous.Text);
            string butRendezVous = Convert.ToString(txtButRendezVous.Text);
            string descriptionRendezVous = Convert.ToString(txtDescriptionRendezVous.Text);

            if (checkConfirmation.IsChecked == true)
            {
                confirmationRendezVous = 1;
            }
            else
            {
                confirmationRendezVous = 0;
            }

            //RendezVous selectedRendezVousCbo = cboCommerciauxRendezVous.SelectedItem as RendezVous;     //Bien prendre depuis la cbo !!!!!

            String commerciauxSelect = txtCommerciauxRendezVous.Text;

            int numCommerciaux = MainWindow.renvoieNum(commerciauxSelect);


            int verifierDuplicate = 0;
            foreach(RendezVous leRendezVous in lesRendezVous)
            {
                if(mailRendezVous == leRendezVous.MailRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate ++;
                }
                if (telRendezVous == leRendezVous.TelRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (numSirenEntrepriseRendezVous == leRendezVous.NumSirenRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de siren, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if( verifierDuplicate == 0 )
            {
                Bdd.InsertRendezVous(nomRendezVous, prenomRendezVous, mailRendezVous, telRendezVous, numSirenEntrepriseRendezVous, nomSocieteRendezVous, villeRendezVous, cpRendezVous, adresseRendezVous, dateRendezVous, heureDebutRendezVous, heureFinRendezVous, butRendezVous, descriptionRendezVous, confirmationRendezVous, numCommerciaux);
                lesRendezVous.Clear();
                lesRendezVous = Bdd.SelectRendezVous();
                lesRendezVous.Sort((x, y) => -1 * x.NumRendezVous.CompareTo(y.NumRendezVous));
                dtgRendezVous.ItemsSource = lesRendezVous;
                dtgRendezVous.SelectedItem = 0;
                dtgRendezVous.Items.Refresh();
            }

        }

        //Méthode pour modifier un Rendez-Vous
        private void btnRendezVousModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            RendezVous selectedRendezVous = dtgRendezVous.SelectedItem as RendezVous;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedRendezVous.NumRendezVous = Convert.ToInt32(txtNumRendezVous.Text);
            selectedRendezVous.NomRendezVous = txtNomRendezVous.Text;
            selectedRendezVous.PrenomRendezVous = txtPrenomRendezVous.Text;
            selectedRendezVous.MailRendezVous = txtMailRendezVous.Text;
            selectedRendezVous.TelRendezVous = txtTelRendezVous.Text;
            selectedRendezVous.NumSirenRendezVous = Convert.ToInt32(txtNumSirenRendezVous.Text);
            selectedRendezVous.NomSocieteRendezVous = txtNomSocieteRendezVous.Text;
            selectedRendezVous.VilleRendezVous = txtVilleRendezVous.Text;
            selectedRendezVous.CpRendezVous = txtCpRendezVous.Text;
            selectedRendezVous.AdresseRendezVous = txtAdresseRendezVous.Text;
            selectedRendezVous.DateRendezVous = Convert.ToString(txtDateRendezVous.Text.Substring(0, 9));
            selectedRendezVous.HeureDebutRendezVous = txtHeureDebutRendezVous.Text;
            selectedRendezVous.HeureFinRendezVous = txtHeureFinRendezVous.Text;
            selectedRendezVous.ButRendezVous = txtButRendezVous.Text;
            selectedRendezVous.DescriptionRendezVous = txtDescriptionRendezVous.Text;

            if (checkConfirmation.IsChecked == true)
            {
                selectedRendezVous.ConfirmationRendezVous = 1;
            }
            else
            {
                selectedRendezVous.ConfirmationRendezVous = 0;
            }



            //String selectedRendezVousCbo = Convert.ToString(cboCommerciauxRendezVous.SelectedItem);

            String commerciauxSelect = txtCommerciauxRendezVous.Text;

            int numCommerciaux = MainWindow.renvoieNum(commerciauxSelect);


            int verifierDuplicate = 0;
            foreach (RendezVous leRendezVous in lesRendezVous)
            {
                if (selectedRendezVous.NumRendezVous == leRendezVous.NumRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedRendezVous.MailRendezVous == leRendezVous.MailRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedRendezVous.TelRendezVous == leRendezVous.TelRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedRendezVous.NumSirenRendezVous == leRendezVous.NumSirenRendezVous)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de siren, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if (verifierDuplicate == 0)
            { 
                Bdd.UpdateRendezVous(selectedRendezVous.NumRendezVous, selectedRendezVous.NomRendezVous, selectedRendezVous.PrenomRendezVous, selectedRendezVous.MailRendezVous, selectedRendezVous.TelRendezVous, selectedRendezVous.NumSirenRendezVous, selectedRendezVous.NomSocieteRendezVous, selectedRendezVous.VilleRendezVous, selectedRendezVous.CpRendezVous, selectedRendezVous.AdresseRendezVous, selectedRendezVous.DateRendezVous, selectedRendezVous.HeureDebutRendezVous, selectedRendezVous.HeureFinRendezVous, selectedRendezVous.ButRendezVous, selectedRendezVous.DescriptionRendezVous, selectedRendezVous.ConfirmationRendezVous, numCommerciaux);
                cboCommerciauxRendezVous.Items.Refresh();
                dtgRendezVous.Items.Refresh();
            }
        }

        //Méthode pour supprimer un Rendez-Vous
        private void btnRendezVousSupprimer_Click(object sender, RoutedEventArgs e)
        {
            RendezVous selectedRendezVous = dtgRendezVous.SelectedItem as RendezVous;
            Bdd.DeleteRendezVous(selectedRendezVous.NumRendezVous);

            lesRendezVous.Clear();
            lesRendezVous = Bdd.SelectRendezVous();
            lesRendezVous.Sort((x, y) => -1 * x.NumRendezVous.CompareTo(y.NumRendezVous));
            dtgRendezVous.ItemsSource = lesRendezVous;
            dtgRendezVous.SelectedItem = 0;
            dtgRendezVous.Items.Refresh();
        }















        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgCommerciaux
        private void dtgCommerciaux_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Commerciaux selectedCommerciaux = dtgCommerciaux.SelectedItem as Commerciaux;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat

                txtNumCommerciaux.Text = Convert.ToString(selectedCommerciaux.NumCommerciaux);
                txtNomCommerciaux.Text = Convert.ToString(selectedCommerciaux.NomCommerciaux);
                txtPrenomCommerciaux.Text = Convert.ToString(selectedCommerciaux.PrenomCommerciaux);
                txtAdresseCommerciaux.Text = Convert.ToString(selectedCommerciaux.AdresseCommerciaux);
                txtVilleCommerciaux.Text = Convert.ToString(selectedCommerciaux.VilleCommerciaux);
                txtCpCommerciaux.Text = Convert.ToString(selectedCommerciaux.CpCommerciaux);
                txtMailCommerciaux.Text = Convert.ToString(selectedCommerciaux.MailCommerciaux);
                txtTelCommerciaux.Text = Convert.ToString(selectedCommerciaux.TelCommerciaux);


            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un commerciaux
        private void btnCommerciauxAjouter_Click(object sender, RoutedEventArgs e)
        {


            string nomCommerciaux = Convert.ToString(txtNomCommerciaux.Text);
            string prenomCommerciaux = Convert.ToString(txtPrenomCommerciaux.Text);

            string adresseCommerciaux = Convert.ToString(txtAdresseCommerciaux.Text);
            string villeCommerciaux = Convert.ToString(txtVilleCommerciaux.Text);
            string cpCommerciaux = Convert.ToString(txtCpCommerciaux.Text);


            string mailCommerciaux = Convert.ToString(txtMailCommerciaux.Text);
            string telCommerciaux = Convert.ToString(txtTelCommerciaux.Text);

            int verifierDuplicate = 0;
            foreach (Commerciaux leCommerciaux in lesCommerciaux)
            {
                if (mailCommerciaux == leCommerciaux.MailCommerciaux)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (telCommerciaux == leCommerciaux.TelCommerciaux)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if (verifierDuplicate == 0)
            {
                Bdd.InsertCommerciaux(nomCommerciaux, prenomCommerciaux, adresseCommerciaux, villeCommerciaux, cpCommerciaux, mailCommerciaux, telCommerciaux); //Définie le mdp dans Bdd
                lesCommerciaux.Clear();
                lesCommerciaux = Bdd.SelectCommerciaux();
                lesCommerciaux.Sort((x, y) => -1 * x.NumCommerciaux.CompareTo(y.NumCommerciaux));
                dtgCommerciaux.ItemsSource = lesCommerciaux;
                dtgCommerciaux.SelectedItem = 0;
            }
        }
        
        //Méthode pour modifier un commerciaux
        private void btnCommerciauxModifier_Click(object sender, RoutedEventArgs e)
        {
            Commerciaux selectedCommerciaux = dtgCommerciaux.SelectedItem as Commerciaux;

            //Fait la modif dans l'affichage pour ne pas restart l'application
            selectedCommerciaux.NumCommerciaux = Convert.ToInt32(txtNumCommerciaux.Text);
            selectedCommerciaux.NomCommerciaux = txtNomCommerciaux.Text;
            selectedCommerciaux.PrenomCommerciaux = txtPrenomCommerciaux.Text;
            selectedCommerciaux.AdresseCommerciaux = txtAdresseCommerciaux.Text;
            selectedCommerciaux.VilleCommerciaux = txtVilleCommerciaux.Text;
            selectedCommerciaux.CpCommerciaux = txtCpCommerciaux.Text;
            selectedCommerciaux.MailCommerciaux = txtMailCommerciaux.Text;
            selectedCommerciaux.TelCommerciaux = txtTelCommerciaux.Text;


            int verifierDuplicate = 0;
            foreach (Commerciaux leCommerciaux in lesCommerciaux)
            {
                if (selectedCommerciaux.NumCommerciaux == leCommerciaux.NumCommerciaux)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if (verifierDuplicate == 0)
            {
                //Enregistre les changement dans la BDD
                Bdd.UpdateCommerciaux(Convert.ToInt32(txtNumCommerciaux.Text), txtNomCommerciaux.Text, txtPrenomCommerciaux.Text, txtAdresseCommerciaux.Text, txtVilleCommerciaux.Text, txtCpCommerciaux.Text, txtMailCommerciaux.Text, txtTelCommerciaux.Text);
                lesCommerciaux = Bdd.SelectCommerciaux();
                dtgCommerciaux.Items.Refresh();
            }
        }

        //Méthode pour supprimer un commerciaux
        private void btnCommerciauxSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Commerciaux selectedCommerciaux = dtgCommerciaux.SelectedItem as Commerciaux;
            Bdd.DeleteCommerciaux(selectedCommerciaux.NumCommerciaux);

            lesCommerciaux.Clear();
            lesCommerciaux = Bdd.SelectCommerciaux();
            lesCommerciaux.Sort((x, y) => -1 * x.NumCommerciaux.CompareTo(y.NumCommerciaux));
            dtgCommerciaux.ItemsSource = lesCommerciaux;
            dtgCommerciaux.SelectedItem = 0;
        }











        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgClient
        private void dtgClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Client selectedClient = dtgClient.SelectedItem as Client;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat

                txtNumClient.Text = Convert.ToString(selectedClient.NumClient);
                txtNomClient.Text = Convert.ToString(selectedClient.NomClient);
                txtPrenomClient.Text = Convert.ToString(selectedClient.PrenomClient);
                txtAdresseClient.Text = Convert.ToString(selectedClient.AdresseClient);
                txtVilleClient.Text = Convert.ToString(selectedClient.VilleClient);
                txtCpClient.Text = Convert.ToString(selectedClient.CpClient);
                txtMailClient.Text = Convert.ToString(selectedClient.MailClient);
                txtTelClient.Text = Convert.ToString(selectedClient.TelClient);

                //Bien laisser les deux !!! Celui de RendezVous Marche !!!!!!
                //cboSirenClient.SelectedItem = selectedClient.LeEntreprise;
                //cboSirenClien.SelectedIndex = dtgClient.SelectedIndex;

                txtSirenClient.Text = selectedClient.LeEntreprise.NumSirenEntreprise + " " + Convert.ToString(selectedClient.LeEntreprise.NomEntreprise);


            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un client
        private void btnClientAjouter_Click(object sender, RoutedEventArgs e)
        {


            string nomClient = Convert.ToString(txtNomClient.Text);
            string prenomClient = Convert.ToString(txtPrenomClient.Text);

            string adresseClient = Convert.ToString(txtAdresseClient.Text);
            string villeClient = Convert.ToString(txtVilleClient.Text);
            string cpClient = Convert.ToString(txtCpClient.Text);


            string mailClient = Convert.ToString(txtMailClient.Text);
            string telClient = Convert.ToString(txtTelClient.Text);



            //Client selectedClientCbo = cboSirenClient.SelectedItem as Client;
            String entrepriseSelect = txtSirenClient.Text;

            int numEntreprise = MainWindow.renvoieNum(entrepriseSelect);


            int verifierDuplicate = 0;
            foreach (Client leClient in lesClient)
            {
                if (mailClient == leClient.MailClient)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (telClient == leClient.TelClient)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                Bdd.InsertClient(nomClient, prenomClient, adresseClient, villeClient, cpClient, mailClient, telClient, numEntreprise);
                lesClient.Clear();
                lesClient = Bdd.SelectClient();
                lesClient.Sort((x, y) => -1 * x.NumClient.CompareTo(y.NumClient));
                dtgClient.ItemsSource = lesClient;
                dtgClient.SelectedItem = 0;
            }
        }

        //Méthode pour modifier un client
        private void btnClientModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            Client selectedClient = dtgClient.SelectedItem as Client;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedClient.NumClient = Convert.ToInt32(txtNumCommerciaux.Text);
            selectedClient.NomClient = txtNomClient.Text;
            selectedClient.PrenomClient = txtPrenomClient.Text;
            selectedClient.AdresseClient = txtAdresseClient.Text;
            selectedClient.VilleClient = txtVilleClient.Text;
            selectedClient.CpClient = txtCpClient.Text;
            selectedClient.MailClient = txtMailClient.Text;
            selectedClient.TelClient = txtTelClient.Text;



            //Client selectedClientCbo = cboSirenClient.SelectedItem as Client;
            String entrepriseSelect = txtSirenClient.Text;

            int numEntreprise = MainWindow.renvoieNum(entrepriseSelect);


            int verifierDuplicate = 0;
            foreach (Client leClient in lesClient)
            {
                if (selectedClient.NumClient == leClient.NumClient)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedClient.MailClient == leClient.MailClient)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedClient.TelClient == leClient.TelClient)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                //Enregistre les changement dans la BDD
                Bdd.UpdateClient(Convert.ToInt32(txtNumCommerciaux.Text), txtNomClient.Text, txtPrenomClient.Text, txtAdresseClient.Text, txtVilleClient.Text, txtCpClient.Text, txtMailClient.Text, txtTelClient.Text, numEntreprise);
                lesClient = Bdd.SelectClient();
                dtgCommerciaux.Items.Refresh();
            }
        }

        //Méthode pour supprimer un client
        private void btnClientSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Client selectedClient = dtgClient.SelectedItem as Client;
            Bdd.DeleteClient(selectedClient.NumClient);

            lesClient.Clear();
            lesClient = Bdd.SelectClient();
            lesClient.Sort((x, y) => -1 * x.NumClient.CompareTo(y.NumClient));
            dtgClient.ItemsSource = lesClient;
            dtgClient.SelectedItem = 0;
        }









        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgProspect
        private void dtgProspect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Prospect selectedProspect = dtgProspect.SelectedItem as Prospect;
            

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat

                txtNumProspect.Text = Convert.ToString(selectedProspect.NumProspect);
                txtNomProspect.Text = Convert.ToString(selectedProspect.NomProspect);
                txtPrenomProspect.Text = Convert.ToString(selectedProspect.PrenomProspect);
                txtAdresseProspect.Text = Convert.ToString(selectedProspect.AdresseProspect);
                txtVilleProspect.Text = Convert.ToString(selectedProspect.VilleProspect);
                txtCpProspect.Text = Convert.ToString(selectedProspect.CpProspect);
                txtMailProspect.Text = Convert.ToString(selectedProspect.MailProspect);
                txtTelProspect.Text = Convert.ToString(selectedProspect.TelProspect);

                //cboSirenProspect.SelectedItem = selectedProspect.LeEntreprise;
                //cboSirenProspect.SelectedIndex = dtgProspect.SelectedIndex;

                txtSirenProspect.Text = Convert.ToString(selectedProspect.LeEntreprise.NumSirenEntreprise) + " " + Convert.ToString(selectedProspect.LeEntreprise.NomEntreprise);

            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un prospect
        private void btnProspectAjouter_Click(object sender, RoutedEventArgs e)
        {


            string nomProspect = Convert.ToString(txtNomProspect.Text);
            string prenomProspect = Convert.ToString(txtPrenomProspect.Text);

            string adresseProspect = Convert.ToString(txtAdresseProspect.Text);
            string villeProspect = Convert.ToString(txtVilleProspect.Text);
            string cpProspect = Convert.ToString(txtCpProspect.Text);


            string mailProspect = Convert.ToString(txtMailProspect.Text);
            string telProspect = Convert.ToString(txtTelProspect.Text);



            //Prospect selectedProspectCbo = cboSirenProspect.SelectedItem as Prospect;
            String entrepriseSelect = txtSirenProspect.Text;

            int numEntreprise = MainWindow.renvoieNum(entrepriseSelect);


            int verifierDuplicate = 0;
            foreach (Prospect leProspect in lesProspect)
            {
                if (mailProspect == leProspect.MailProspect)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (telProspect == leProspect.TelProspect)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }


            if( verifierDuplicate == 0)
            {
                Bdd.InsertProspect(nomProspect, prenomProspect, adresseProspect, villeProspect, cpProspect, mailProspect, telProspect, numEntreprise);
                lesProspect.Clear();
                lesProspect = Bdd.SelectProspect();
                lesProspect.Sort((x, y) => -1 * x.NumProspect.CompareTo(y.NumProspect));
                dtgProspect.ItemsSource = lesProspect;
                dtgProspect.SelectedItem = 0;
            }
        }

        //Méthode pour modifier un prospect
        private void btnProspectModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            Prospect selectedProspect = dtgProspect.SelectedItem as Prospect;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedProspect.NumProspect = Convert.ToInt32(txtNumProspect.Text);
            selectedProspect.NomProspect = txtNomProspect.Text;
            selectedProspect.PrenomProspect = txtPrenomProspect.Text;
            selectedProspect.AdresseProspect = txtAdresseProspect.Text;
            selectedProspect.VilleProspect = txtVilleProspect.Text;
            selectedProspect.CpProspect = txtCpProspect.Text;
            selectedProspect.MailProspect = txtMailProspect.Text;
            selectedProspect.TelProspect = txtTelProspect.Text;


            //Prospect selectedProspectCbo = cboSirenProspect.SelectedItem as Prospect;
            String entrepriseSelect = txtSirenProspect.Text;

            int numEntreprise = MainWindow.renvoieNum(entrepriseSelect);


            int verifierDuplicate = 0;
            foreach (Prospect leProspect in lesProspect)
            {
                if (selectedProspect.NumProspect == leProspect.NumProspect)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedProspect.MailProspect == leProspect.MailProspect)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (selectedProspect.TelProspect == leProspect.TelProspect)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                //Enregistre les changement dans la BDD
                Bdd.UpdateProspect(Convert.ToInt32(txtNumProspect.Text), txtNomProspect.Text, txtPrenomProspect.Text, txtAdresseProspect.Text, txtVilleProspect.Text, txtCpProspect.Text, txtMailProspect.Text, txtTelProspect.Text, numEntreprise);
                lesProspect = Bdd.SelectProspect();

                dtgProspect.Items.Refresh();
            }

        }

        //Méthode pour supprimer un propsect
        private void btnProspectSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Prospect selectedProspect = dtgProspect.SelectedItem as Prospect;
            Bdd.DeleteProspect(selectedProspect.NumProspect);

            lesProspect.Clear();
            lesProspect = Bdd.SelectProspect();
            lesProspect.Sort((x, y) => -1 * x.NumProspect.CompareTo(y.NumProspect));
            dtgProspect.ItemsSource = lesProspect;
            dtgProspect.SelectedItem = 0;
        }














        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgProduitType
        private void dtgProduitType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            ProduitType selectedProduitType = dtgProduitType.SelectedItem as ProduitType;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                txtProduitType.Text = Convert.ToString(selectedProduitType.TypeProduit);

            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un type de produit
        private void btnProduitTypeAjouter_Click(object sender, RoutedEventArgs e)
        {

            string typeProduit = Convert.ToString(txtProduitType.Text);


            int verifierDuplicate = 0;
            foreach (ProduitType leProduitType in lesProduitType)
            {
                if (typeProduit == leProduitType.TypeProduit)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                Bdd.InsertProduitType(typeProduit);
                lesProduitType.Clear();
                lesProduitType = Bdd.SelectProduitType();
                dtgProduitType.ItemsSource = lesProduitType;
                dtgProduitType.SelectedItem = 0;
                dtgProduitType.Items.Refresh();
            }

        }

        //Méthode pour modifier un type de produit
        private void btnProduitTypeModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            ProduitType selectedProduitType = dtgProduitType.SelectedItem as ProduitType;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedProduitType.TypeProduit = Convert.ToString(txtProduitType.Text);


            int verifierDuplicate = 0;
            foreach (ProduitType leProduitType in lesProduitType)
            {
                if (selectedProduitType.TypeProduit == leProduitType.TypeProduit)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                Bdd.UpdateProduitType(selectedProduitType.TypeProduit);
                lesProduitType = Bdd.SelectProduitType();
                dtgProduitType.Items.Refresh();
            }
        }

        //Méthode pour supprimer un type de produit
        private void btnProduitTypeSupprimer_Click(object sender, RoutedEventArgs e)
        {
            ProduitType selectedProduitType = dtgProduitType.SelectedItem as ProduitType;
            Bdd.DeleteProduitType(selectedProduitType.TypeProduit);

            lesProduitType.Clear();
            lesProduitType = Bdd.SelectProduitType();
            dtgProduitType.ItemsSource = lesProduitType;
            dtgProduitType.SelectedItem = 0;
            dtgProduitType.Items.Refresh();
        }








        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgStock
        private void dtgStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Stock selectedStock = dtgStock.SelectedItem as Stock;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                txtNumStock.Text = Convert.ToString(selectedStock.NumStock);
                txtQteStock.Text = Convert.ToString(selectedStock.NombreStock);


                //cboProduitStock.SelectedItem = selectedStock.LeProduit;
                txtProduitStock.Text = selectedStock.LeProduit.NumProduit + " " + Convert.ToString(selectedStock.LeProduit.NomProduit);

            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un stock
        private void btnStockAjouter_Click(object sender, RoutedEventArgs e)
        {

            Int32 nombreStock = Convert.ToInt32(txtNumStock.Text);

            //Stock selectedStockCbo = cboProduitStock.SelectedItem as Stock;
            String produitSelect = txtProduitStock.Text;

            int numProduit = MainWindow.renvoieNum(produitSelect);


            Bdd.InsertStock(nombreStock, numProduit);
            lesStock.Clear();
            lesStock = Bdd.SelectStock();
            lesStock.Sort((x, y) => -1 * x.NumStock.CompareTo(y.NumStock));
            dtgStock.ItemsSource = lesStock;
            dtgStock.SelectedItem = 0;
            dtgStock.Items.Refresh();
        }

        //Méthode pour modifier un stock
        private void btnStockModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            Stock selectedStock = dtgStock.SelectedItem as Stock;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedStock.NumStock = Convert.ToInt32(txtNumStock.Text);
            selectedStock.NombreStock = Convert.ToInt32(txtQteStock.Text);

            //Stock selectedStockCbo = cboProduitStock.SelectedItem as Stock;
            String produitSelect = txtProduitStock.Text;

            int numProduit = MainWindow.renvoieNum(produitSelect);


            int verifierDuplicate = 0;
            foreach (Stock leStock in lesStock)
            {
                if (selectedStock.NumStock == leStock.NumStock)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if (verifierDuplicate == 0)
            {
                Bdd.UpdateStock(Convert.ToInt32(txtNumStock.Text), Convert.ToString(txtQteStock.Text), numProduit);
                lesStock = Bdd.SelectStock();
                dtgStock.Items.Refresh();
            }

        }

        //Méthode pour supprimer un stock
        private void btnStockSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Stock selectedStock = dtgStock.SelectedItem as Stock;
            Bdd.DeleteStock(selectedStock.NumStock);

            lesStock.Clear();
            lesStock = Bdd.SelectStock();
            lesStock.Sort((x, y) => -1 * x.NumStock.CompareTo(y.NumStock));
            dtgStock.ItemsSource = lesStock;
            dtgStock.SelectedItem = 0;
            dtgStock.Items.Refresh();
        }







        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgEntreprise
        private void dtgEntreprise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Entreprise selectedEntreprise = dtgEntreprise.SelectedItem as Entreprise;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                txtNumSirenEntreprise.Text = Convert.ToString(selectedEntreprise.NumSirenEntreprise);
                txtNomEntreprise.Text = Convert.ToString(selectedEntreprise.NomEntreprise);
                txtVilleSiegeEntreprise.Text = selectedEntreprise.VilleSiegeEntreprise;
                txtCpSiegeEntreprise.Text = Convert.ToString(selectedEntreprise.CpSiegeEntreprise);
                txtAdresseSiegeEntreprise.Text = Convert.ToString(selectedEntreprise.AdresseSiegeEntreprise);
                txtVilleLocauxEntreprise.Text = Convert.ToString(selectedEntreprise.VilleLocauxEntreprise);
                txtCpLocauxEntreprise.Text = Convert.ToString(selectedEntreprise.CpLocauxEntreprise);
                txtAdresseLocauxEntreprise.Text = Convert.ToString(selectedEntreprise.AdresseLocauxEntreprise);

            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter une entreprise
        private void btnEntrepriseAjouter_Click(object sender, RoutedEventArgs e)
        {

            int numSirenEntrepriseEntreprise = Convert.ToInt32(txtNumSirenEntreprise.Text);
            string nomEntreprise = Convert.ToString(txtNomEntreprise.Text);
            string villeSiegeEntreprise = Convert.ToString(txtVilleSiegeEntreprise.Text);
            string cpSiegeEntreprise = Convert.ToString(txtCpSiegeEntreprise.Text);
            string adresseSiegeEntreprise = Convert.ToString(txtAdresseSiegeEntreprise.Text);
            string villeLocauxEntreprise = Convert.ToString(txtVilleLocauxEntreprise.Text);
            string cpLocauxEntreprise = Convert.ToString(txtCpLocauxEntreprise.Text);
            string adresseLocauxEntreprise = Convert.ToString(txtAdresseLocauxEntreprise.Text);


            int verifierDuplicate = 0;
            foreach (Entreprise leEntreprise in lesEntreprise)
            {
                if (numSirenEntrepriseEntreprise == leEntreprise.NumSirenEntreprise)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de siren, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if (verifierDuplicate == 0)
            {
                Bdd.InsertEntreprise(numSirenEntrepriseEntreprise, nomEntreprise, villeSiegeEntreprise, cpSiegeEntreprise, adresseSiegeEntreprise, villeLocauxEntreprise, cpLocauxEntreprise, adresseLocauxEntreprise);
                lesEntreprise.Clear();
                lesEntreprise = Bdd.SelectEntreprise();
                dtgEntreprise.ItemsSource = lesEntreprise;
                dtgEntreprise.SelectedItem = 0;
                dtgEntreprise.Items.Refresh();
            }
        }

        //Méthode pour modifier une entreprise
        private void btnEntrepriseModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            Entreprise selectedEntreprise = dtgEntreprise.SelectedItem as Entreprise;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedEntreprise.NumSirenEntreprise = Convert.ToInt32(txtNumSirenEntreprise.Text);
            selectedEntreprise.NomEntreprise = txtNomEntreprise.Text;
            selectedEntreprise.VilleSiegeEntreprise = txtVilleSiegeEntreprise.Text;
            selectedEntreprise.CpSiegeEntreprise = txtCpSiegeEntreprise.Text;
            selectedEntreprise.AdresseSiegeEntreprise = txtAdresseSiegeEntreprise.Text;
            selectedEntreprise.VilleLocauxEntreprise = txtVilleLocauxEntreprise.Text;
            selectedEntreprise.CpLocauxEntreprise = txtCpLocauxEntreprise.Text;
            selectedEntreprise.AdresseLocauxEntreprise = txtAdresseLocauxEntreprise.Text;


            int verifierDuplicate = 0;
            foreach (Entreprise leEntreprise in lesEntreprise)
            {
                if (selectedEntreprise.NumSirenEntreprise == leEntreprise.NumSirenEntreprise)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de siren, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                Bdd.UpdateEntreprise(selectedEntreprise.NumSirenEntreprise, selectedEntreprise.NomEntreprise, selectedEntreprise.VilleSiegeEntreprise, selectedEntreprise.CpSiegeEntreprise, selectedEntreprise.AdresseSiegeEntreprise, selectedEntreprise.VilleLocauxEntreprise, selectedEntreprise.CpLocauxEntreprise, selectedEntreprise.CpLocauxEntreprise);
                lesEntreprise = Bdd.SelectEntreprise();
                dtgEntreprise.Items.Refresh();
            }
        }

        //Méthode pour supprimer une entreprise
        private void btnEntrepriseSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Entreprise selectedEntreprise = dtgEntreprise.SelectedItem as Entreprise;
            Bdd.DeleteEntreprise(selectedEntreprise.NumSirenEntreprise);

            lesEntreprise.Clear();
            lesEntreprise = Bdd.SelectEntreprise();
            dtgEntreprise.ItemsSource = lesEntreprise;
            dtgEntreprise.SelectedItem = 0;
            dtgEntreprise.Items.Refresh();
        }







        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgProduit
        private void dtgProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Produit selectedProduit = dtgProduit.SelectedItem as Produit;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                
                txtNumProduit.Text = Convert.ToString(selectedProduit.NumProduit);
                txtNomProduit.Text = Convert.ToString(selectedProduit.NomProduit);
                txtPetiteDescriptionProduit.Text = Convert.ToString(selectedProduit.PetiteDescriptionProduit);
                txtLongueDescriptionProduit.Text = Convert.ToString(selectedProduit.LongueDescriptionProduit);
                txtPrixProduit.Text = Convert.ToString(selectedProduit.PrixProduit);

                //cboProduitTypeProduit.SelectedItem = selectedProduit.LeProduitType;
                //cboProduitTypeProduit.SelectedIndex = dtgProduit.SelectedIndex;

                txtProduitTypeProduit.Text = Convert.ToString(selectedProduit.LeProduitType.TypeProduit);

            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter un produit
        private void btnProduitAjouter_Click(object sender, RoutedEventArgs e)
        {

            String nomProduit = Convert.ToString(txtNomProduit.Text);
            String prixProduit = Convert.ToString(txtPrixProduit.Text);
            String petiteProduit = Convert.ToString(txtPetiteDescriptionProduit.Text);
            String longueProduit = Convert.ToString(txtLongueDescriptionProduit.Text);

            //Produit selectedRendezVousCbo = cboProduitTypeProduit.SelectedItem as Produit;
            String nomTypeProduit = txtProduitTypeProduit.Text;


            Bdd.InsertProduit(nomProduit, prixProduit, petiteProduit, longueProduit, nomTypeProduit);
            lesProduit.Clear();
            lesProduit = Bdd.SelectProduit();
            lesProduit.Sort((x, y) => -1 * x.NumProduit.CompareTo(y.NumProduit));
            dtgProduit.ItemsSource = lesProduit;
            dtgProduit.SelectedItem = 0;
            dtgProduit.Items.Refresh();
        }

        //Méthode pour modifier un produit
        private void btnProduitModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            Produit selectedProduit = dtgProduit.SelectedItem as Produit;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedProduit.NumProduit = Convert.ToInt32(txtNumProduit.Text);
            selectedProduit.NomProduit = Convert.ToString(txtNomProduit.Text);
            selectedProduit.PetiteDescriptionProduit = Convert.ToString(txtPetiteDescriptionProduit.Text);
            selectedProduit.LongueDescriptionProduit = Convert.ToString(txtLongueDescriptionProduit.Text);
            selectedProduit.PrixProduit = Convert.ToString(txtPrixProduit.Text);

            //Produit selectedRendezVousCbo = cboProduitTypeProduit.SelectedItem as Produit;
            String nomTypeProduit = txtProduitTypeProduit.Text;


            int verifierDuplicate = 0;
            foreach (Produit leProduit in lesProduit)
            {
                if (selectedProduit.NumProduit == leProduit.NumProduit)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de siren, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                Bdd.UpdateProduit(selectedProduit.NumProduit, selectedProduit.NomProduit, selectedProduit.PetiteDescriptionProduit, selectedProduit.LongueDescriptionProduit, selectedProduit.PrixProduit, nomTypeProduit);
                lesProduit = Bdd.SelectProduit();
                dtgProduit.Items.Refresh();
            }
        }

        //Méthode pour supprimer un produit
        private void btnProduitSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Produit selectedProduit = dtgProduit.SelectedItem as Produit;
            Bdd.DeleteProduit(selectedProduit.NumProduit);

            lesProduit.Clear();
            lesProduit = Bdd.SelectProduit();
            lesProduit.Sort((x, y) => -1 * x.NumProduit.CompareTo(y.NumProduit));
            dtgProduit.ItemsSource = lesProduit;
            dtgProduit.SelectedItem = 0;
            dtgProduit.Items.Refresh();
        }







        //Méthode pour afficher dans les TextBoxs les éléments sélectionné dans le DataGrid dtgFacture
        private void dtgFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // On stock dans l'objet selectedContrat le Contrat selectionné dans le datagrid dtgContrat
            Facture selectedFacture = dtgFacture.SelectedItem as Facture;

            try
            {
                //Remplissage des Textboxs avec les données de l'objet Contrat selectedContrat récupéré dans le Datagrid dtgContrat
                txtNumFacture.Text = Convert.ToString(selectedFacture.NumFacture);
                txtDateFacture.Text = Convert.ToString(selectedFacture.DateFacture);
                txtQteFacture.Text = Convert.ToString(selectedFacture.QteFacture);


                //cboClientFacture.SelectedItem = selectedFacture.LeClient;
                //cboClientFacture.SelectedIndex = dtgFacture.SelectedIndex;

                txtClientFacture.Text = Convert.ToString(selectedFacture.LeClient.NumClient) + " " + Convert.ToString(selectedFacture.LeClient.NomClient) + " " + Convert.ToString(selectedFacture.LeClient.PrenomClient);


                //cboProduitFacture.SelectedItem = selectedFacture.LeProduit;
                //cboProduitFacture.SelectedIndex = dtgFacture.SelectedIndex;

                txtProduitFacture.Text = Convert.ToString(selectedFacture.LeProduit.NumProduit + " " + selectedFacture.LeProduit.NomProduit);

            }
            catch (Exception)
            {
                Console.WriteLine("Erreur sur la mise à jour du formulaire lors du changement dans le Datagrid dtgContrat");
            }
        }

        //Méthode pour ajouter une facture
        private void btnFactureAjouter_Click(object sender, RoutedEventArgs e)
        {

            String dateFacture = Convert.ToString(txtDateFacture.Text);
            Int32 qteFacture = Convert.ToInt32(txtQteFacture.Text);

            //Facture selectedFactureProduitCbo = cboProduitFacture.SelectedItem as Facture;
            String produitSelect = txtProduitFacture.Text;

            int numProduit = MainWindow.renvoieNum(produitSelect);

            //Facture selectedFactureClientCbo = cboClientFacture.SelectedItem as Facture;
            String clientSelect = txtClientFacture.Text;

            int numClient = MainWindow.renvoieNum(clientSelect);


            Bdd.InsertFacture(dateFacture, qteFacture, numProduit, numClient);
            lesFacture.Clear();
            lesFacture = Bdd.SelectFacture();
            lesFacture.Sort((x, y) => -1 * x.NumFacture.CompareTo(y.NumFacture));
            dtgFacture.ItemsSource = lesFacture;
            dtgFacture.SelectedItem = 0;
            dtgFacture.Items.Refresh();
        }

        //Méthode pour modifier une facture
        private void btnFactureModifier_Click(object sender, RoutedEventArgs e)
        {
            //On recherche à quel indice de la collection se trouve l'object selectionné dans le datagrid
            Facture selectedFacture = dtgFacture.SelectedItem as Facture;

            // On change les propritétés de l'objet à l'indice trouvé. On ne change pas le numéro de magazine via l'interface, trop de risques d'erreurs en base de données
            selectedFacture.NumFacture = Convert.ToInt32(txtNumProduit.Text);
            selectedFacture.DateFacture = Convert.ToString(txtNomProduit.Text);
            selectedFacture.QteFacture = Convert.ToInt32(txtPrixProduit.Text);



            Facture selectedFactureProduitCbo = cboProduitFacture.SelectedItem as Facture;
            String produitSelect = txtProduitFacture.Text;

            int numProduit = MainWindow.renvoieNum(produitSelect);


            Facture selectedFactureClientCbo = cboClientFacture.SelectedItem as Facture;
            String clientSelect = txtClientFacture.Text;

            int numClient = MainWindow.renvoieNum(produitSelect);


            int verifierDuplicate = 0;
            foreach (Facture leFacture in lesFacture)
            {
                if (selectedFacture.NumFacture == leFacture.NumFacture)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if(verifierDuplicate == 0)
            {
                Bdd.UpdateFacture(selectedFacture.NumFacture, selectedFacture.DateFacture, selectedFacture.QteFacture, numProduit, numClient);
                lesFacture = Bdd.SelectFacture();
                dtgFacture.Items.Refresh();
            }
        }

        //Méthode pour supprimer une facture
        private void btnFactureSupprimer_Click(object sender, RoutedEventArgs e)
        {
            Facture selectedFacture = dtgFacture.SelectedItem as Facture;
            Bdd.DeleteFacture(selectedFacture.NumFacture);

            lesFacture.Clear();
            lesFacture = Bdd.SelectFacture();
            lesFacture.Sort((x, y) => -1 * x.NumFacture.CompareTo(y.NumFacture));
            dtgFacture.ItemsSource = lesFacture;
            dtgFacture.SelectedItem = 0;
            dtgFacture.Items.Refresh();
        }



        //Méthode pour afficher le type de produit séléctionné dans la text box, en fonction du produit
        private void cboProduitTypeProduit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtProduitTypeProduit.Text = cboProduitTypeProduit.SelectedItem.ToString();
        }

        //Méthode pour afficher le produit séléctionné dans la text box, en fonction de la facture
        private void cboProduitFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtProduitFacture.Text = cboProduitFacture.SelectedItem.ToString();
        }

        //Méthode pour afficher le client séléctionné dans la text box, en fonction de la facture
        private void cboClientFacture_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtClientFacture.Text = cboClientFacture.SelectedItem.ToString();
        }

        //Méthode pour afficher le num siren séléctionné dans la text box, en fonction du client
        private void cboSirenClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSirenClient.Text = cboSirenClient.SelectedItem.ToString();
        }

        //Méthode pour afficher le num siren séléctionné dans la text box, en fonction du prospect
        private void cboSirenProspect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSirenProspect.Text = cboSirenProspect.SelectedItem.ToString();
        }

        //Méthode pour afficher le produit séléctionné dans la text box, en fonction du stock
        private void cboProduitStock_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtProduitStock.Text = cboProduitStock.SelectedItem.ToString();
        }

        //Méthode pour afficher le commerciaux séléctionné dans la text box, en fonction du rendez vous
        private void cboCommerciauxRendezVous_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtCommerciauxRendezVous.Text = cboCommerciauxRendezVous.SelectedItem.ToString();
        }




        //Méthode pour que dans la BDD, un prospect bascule dans la table client
        private void btnPasserProspectClient_Click(object sender, RoutedEventArgs e)
        {
            //Ajouté dans client le prospect
            string nomProspect = Convert.ToString(txtNomProspect.Text);
            string prenomProspect = Convert.ToString(txtPrenomProspect.Text);

            string adresseProspect = Convert.ToString(txtAdresseProspect.Text);
            string villeProspect = Convert.ToString(txtVilleProspect.Text);
            string cpProspect = Convert.ToString(txtCpProspect.Text);


            string mailProspect = Convert.ToString(txtMailProspect.Text);
            string telProspect = Convert.ToString(txtTelProspect.Text);



            //Prospect selectedProspectCbo = cboSirenProspect.SelectedItem as Prospect;
            String entrepriseSelect = txtSirenProspect.Text;

            int numEntreprise = MainWindow.renvoieNum(entrepriseSelect);



            Bdd.InsertClient(nomProspect, prenomProspect, adresseProspect, villeProspect, cpProspect, mailProspect, telProspect, numEntreprise);

            //Supprimé le prospect
            Prospect selectedProspect = dtgProspect.SelectedItem as Prospect;
            Bdd.DeleteProspect(selectedProspect.NumProspect);


            //Mettre à jour les DataGrid Prospect et Client
            lesProspect.Clear();
            lesProspect = Bdd.SelectProspect();
            lesProspect.Sort((x, y) => -1 * x.NumProspect.CompareTo(y.NumProspect));
            dtgProspect.ItemsSource = lesProspect;
            dtgProspect.SelectedItem = 0;

            lesClient.Clear();
            lesClient = Bdd.SelectClient();
            lesClient.Sort((x, y) => -1 * x.NumClient.CompareTo(y.NumClient));
            dtgClient.ItemsSource = lesClient;
            dtgClient.SelectedItem = 0;

        }










        //Méthode pour se connecter à l'application
        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            String mailToVerify = txtMail.Text;
            String passwordToVerify = txtPassword.Text;

            //Variable qui dira si oui ou non le mot de passe est correcte
            Boolean passwordIsTrue = false;

            foreach (Manager manager in lesManager)
            {
                if (manager.MailManager == mailToVerify)
                {
                    if(HashPassword(passwordToVerify) == manager.NumPasswordManager.Password)
                    {
                        passwordIsTrue = true;
                    }
                }
            }


            if(passwordIsTrue)
            {
                MessageBox.Show("Tu es connecté, bien joué");
                //Afficher les onglets
                tabProduit.IsEnabled = true;
                tabRendezVous.IsEnabled = true;
                tabEntreprise.IsEnabled = true;
                tabClient.IsEnabled = true;
                tabProspect.IsEnabled = true;
                tabCommerciaux.IsEnabled = true;
                tabStatistique.IsEnabled = true;
                tabFacture.IsEnabled = true;

                //Afficher la partie pour ajouter un nouveau manager
                labelNomManager.IsEnabled = true;
                labelPrenomManager.IsEnabled = true;
                labelAdresseManager.IsEnabled = true;
                labelVilleManager.IsEnabled = true;
                labelCpManager.IsEnabled = true;
                labelMailManager.IsEnabled = true;
                labelTelManager.IsEnabled = true;
                labelPasswordManager.IsEnabled = true;
                txtNomManager.IsEnabled = true;
                txtPrenomManager.IsEnabled = true;
                txtAdresseManager.IsEnabled = true;
                txtVilleManager.IsEnabled = true;
                txtCpManager.IsEnabled = true;
                txtMailManager.IsEnabled = true;
                txtTelManager.IsEnabled = true;
                txtPasswordManager.IsEnabled = true;
                btnInscrir.IsEnabled = true;
                labelInscrirNewManager.IsEnabled = true;

                //Afficher la partie pour changer de mot de passe
                txtAncienPasswordChangerPassword.IsEnabled = true;
                labelAncienPasswordChanger.IsEnabled = true;
                labelNouveauPasswordChanger.IsEnabled = true;
                labelMailChanger.IsEnabled = true;
                txtNouveauPasswordChangerPassword.IsEnabled = true;
                txtMailChangerPassword.IsEnabled = true;
                labelChangerPassword.IsEnabled = true;
            }
        }


        //Méthode pour inscrir un nouveau manager
        private void btnInscrir_Click(object sender, RoutedEventArgs e)
        {
            String nomManager = txtNomManager.Text;
            String prenomManager = txtPrenomManager.Text;
            String adresseManager = txtAdresseManager.Text;
            String villeManager = txtVilleManager.Text;
            String cpManager = txtCpManager.Text;
            String mailManager = txtMailManager.Text;
            String telManager = txtTelManager.Text;
            String passwordManager = txtPasswordManager.Text;



            string nomCommerciaux = Convert.ToString(txtNomCommerciaux.Text);
            string prenomCommerciaux = Convert.ToString(txtPrenomCommerciaux.Text);

            string adresseCommerciaux = Convert.ToString(txtAdresseCommerciaux.Text);
            string villeCommerciaux = Convert.ToString(txtVilleCommerciaux.Text);
            string cpCommerciaux = Convert.ToString(txtCpCommerciaux.Text);


            string mailCommerciaux = Convert.ToString(txtMailCommerciaux.Text);
            string telCommerciaux = Convert.ToString(txtTelCommerciaux.Text);

            int verifierDuplicate = 0;
            foreach (Manager leManager in lesManager)
            {
                if (mailCommerciaux == leManager.MailManager)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce mail, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
                if (telCommerciaux == leManager.TelManager)
                {
                    MessageBoxResult result = MessageBox.Show("Vous ne pouvez pas rentrer ce numéro de téléphone, il existe déjà", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                    verifierDuplicate++;
                }
            }

            if (verifierDuplicate == 0)
            {
                Bdd.InsertManager(nomManager, prenomManager, adresseManager, villeManager, cpManager, mailManager, telManager, 1); //Définie le mdp par 1 par défaut
                lesManager.Clear();
                lesManager = Bdd.SelectManager();
            }

        }

        //Méthode pour changer de mot de passe
        private void btnChangerPassword_Click(object sender, RoutedEventArgs e)
        {
            int numPassword = 0;   //Initialiser numPassword, pour pouvoir stocker le numéro du mot de passe

            String mailToModifier = txtMail.Text;
            String passwordToModifier = txtPassword.Text;

            //Variable qui dira si oui ou non le mot de passe est correcte
            Boolean passwordIsTrue = false;

            foreach (Manager manager in lesManager)
            {
                if (manager.MailManager == mailToModifier)
                {
                    if (HashPassword(passwordToModifier) == manager.NumPasswordManager.Password)
                    {
                        numPassword = manager.NumPasswordManager.NumPassword;
                        passwordIsTrue = true;
                    }
                }
            }


            if (passwordIsTrue)
            {
                Bdd.UpdateIdentification(numPassword, HashPassword(txtNouveauPasswordChangerPassword.Text)); //Changer le mot de passe en le hashant
                lesManager.Clear();
                lesManager = Bdd.SelectManager();
            }
        }









        //Méthode pour afficher les statistiques des factures
        public void afficheGraphiqueFacture()
        {
            List<String> listeFacture01 = new List<String>();
            List<String> listeFacture02 = new List<String>();
            List<String> listeFacture03 = new List<String>();
            List<String> listeFacture04 = new List<String>();
            List<String> listeFacture05 = new List<String>();
            List<String> listeFacture06 = new List<String>();
            List<String> listeFacture07 = new List<String>();
            List<String> listeFacture08 = new List<String>();
            List<String> listeFacture09 = new List<String>();
            List<String> listeFacture10 = new List<String>();
            List<String> listeFacture11 = new List<String>();
            List<String> listeFacture12 = new List<String>();

            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[3] == '0' && facture.DateFacture[4] == '1')
                {
                    listeFacture01.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[3] == '0' && facture.DateFacture[4] == '2')
                {
                    listeFacture02.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '3')
                {
                    listeFacture03.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '4')
                {
                    listeFacture04.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '5')
                {
                    listeFacture05.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '6')
                {
                    listeFacture06.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '7')
                {
                    listeFacture07.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '8')
                {
                    listeFacture08.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[4] == '9')
                {
                    listeFacture09.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[3] == '1' && facture.DateFacture[4] == '0')
                {
                    listeFacture10.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[3] == '1' && facture.DateFacture[4] == '1')
                {
                    listeFacture11.Add(facture.DateFacture);
                }
            }
            foreach (Facture facture in lesFacture)
            {
                if (facture.DateFacture[3] == '1' && facture.DateFacture[4] == '2')
                {
                    listeFacture12.Add(facture.DateFacture);
                }
            }


            var model = new PlotModel { Title = "Nombre de factures par mois" };

            List<List<string>> listesFactures = new List<List<string>>
               {
                listeFacture01, listeFacture02, listeFacture03, listeFacture04,
                listeFacture05, listeFacture06, listeFacture07, listeFacture08,
                listeFacture09, listeFacture10, listeFacture11, listeFacture12
            };

            // Configurer l'axe des X
            var categoryAxis = new CategoryAxis
            {
                Position = AxisPosition.Left,
                Title = "Nombre de factures",
                IsPanEnabled = false,
                IsZoomEnabled = false
            };

            model.Axes.Add(categoryAxis);

            int index = 1;

            foreach (var listeFacture in listesFactures)
            {
                int nombreDeFactures = listeFacture.Count;

                var barSeries = new BarSeries { Title = $"Mois {index}" };

                // Ajouter la barre à la position correspondante sur l'axe des X
                barSeries.Items.Add(new BarItem { Value = nombreDeFactures, CategoryIndex = index - 1 });

                // Ajouter la série au modèle
                model.Series.Add(barSeries);

                index++;
            }

            // Ajouter les mois à l'axe des X une seule fois après la boucle
            var monthNames = new[]
            {
                "Janvier", "Février", "Mars", "Avril", "Mai", "Juin",
                "Juillet", "Août", "Septembre", "Octobre", "Novembre", "Décembre"
            };

            (model.Axes[0] as CategoryAxis).Labels.AddRange(monthNames);

            // Configurer l'axe des X pour afficher uniquement des nombres entiers
            (model.Axes[0] as CategoryAxis).MajorStep = 1;

            plotView.Model = model;
        }

        //Méthode pour appeler la méthode qui affiche le graphique pourle mettre à jour
        private void btnActualiseGraphique_Click(object sender, RoutedEventArgs e)
        {
            afficheGraphiqueFacture();
        }

        //Méthode pour exporter la facture au format pdf
        private void btnFactureExporter_Click(object sender, RoutedEventArgs e)
        {
            // Récupération des données du formulaire (à adapter selon votre UI)
            Int32 numFacture = Convert.ToInt32(txtNumFacture.Text);
            String dateFacture = Convert.ToString(txtDateFacture.Text);
            Int32 qteFacture = Convert.ToInt32(txtQteFacture.Text);

            String produitSelect = txtProduitFacture.Text;
            int numProduit = renvoieNum(produitSelect);

            String clientSelect = txtClientFacture.Text;
            int numClient = renvoieNum(clientSelect);


            Client leClient = Bdd.SearchClient(numClient);
            Produit leProduit = Bdd.SearchProduit(numProduit);

            Double prix = Convert.ToDouble(leProduit.PrixProduit);
            Double prixTotal = Convert.ToDouble(qteFacture * prix);

            // Affichage de la boîte de dialogue de sauvegarde de fichier
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers PDF (*.pdf)|*.pdf";
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.Title = "Enregistrer le devis PDF";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Création du fichier PDF
                string fileName = saveFileDialog.FileName;
                PdfWriter writer = new PdfWriter(fileName);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Header
                Paragraph header = new Paragraph("DEVIS" + "Numéro : " + numFacture)
                    .SetTextAlignment(ITextTextAlignment.CENTER)
                    .SetFontSize(20);

                // New line
                Paragraph newline = new Paragraph(new Text("\n"));

                document.Add(newline);
                document.Add(header);

                // Informations du client
                Paragraph clientInfo = new Paragraph("Client: " + leClient.NomClient + " " + leClient.PrenomClient + "\nAdresse: " + leClient.AdresseClient + "\nVille: " + leClient.VilleClient + "\nCode Postal: " + leClient.CpClient
                                                + "\nNum Siren Entreprise: " + leClient.LeEntreprise.NumSirenEntreprise + "\nNom Entreprise: " + leClient.LeEntreprise.NomEntreprise)
                .SetTextAlignment(ITextTextAlignment.LEFT)
                .SetFontSize(12);

                document.Add(newline);
                document.Add(clientInfo);

                // Ligne de séparation
                LineSeparator ls = new LineSeparator(new SolidLine());
                document.Add(ls);

                // Produits et coûts
                Paragraph products = new Paragraph("Produits: ")
                    .SetTextAlignment(ITextTextAlignment.LEFT)
                    .SetFontSize(14);

                Table table = new Table(4, false);
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.CENTER)
                    .Add(new Paragraph("Nom")));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.LEFT)
                    .Add(new Paragraph("Description")));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.CENTER)
                    .Add(new Paragraph("Quantite")));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.CENTER)
                    .Add(new Paragraph("Prix Unitaire")));

                table.AddCell(new Cell(1, 1)
                    .Add(new Paragraph(leProduit.NomProduit)));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.LEFT)
                    .Add(new Paragraph(leProduit.PetiteDescriptionProduit)));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.RIGHT)
                    .Add(new Paragraph(Convert.ToString(qteFacture))));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.RIGHT)
                    .Add(new Paragraph(Convert.ToString(leProduit.PrixProduit))));



                table.AddCell(new Cell(1, 1)
                    .Add(new Paragraph("Total")));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.RIGHT)
                    .Add(new Paragraph(" ")));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.RIGHT)
                    .Add(new Paragraph(" ")));
                table.AddCell(new Cell(1, 1)
                    .SetTextAlignment(ITextTextAlignment.RIGHT)
                    .Add(new Paragraph(Convert.ToString(prixTotal))));

                document.Add(newline);
                document.Add(products);
                document.Add(table);

                // Notes
                Paragraph notes = new Paragraph("Remarques:\n- Les frais de livraison ne sont pas inclus.")
                    .SetTextAlignment(ITextTextAlignment.LEFT)
                    .SetFontSize(12);

                document.Add(newline);
                document.Add(notes);

                // Page numbers
                int n = pdf.GetNumberOfPages();
                for (int i = 1; i <= n; i++)
                {
                    document.ShowTextAligned(new Paragraph(String
                        .Format("Page " + i + " sur " + n)),
                        559, 806, i, ITextTextAlignment.RIGHT,
                        ITextVerticalAlignment.TOP, 0);
                }

                // Close document
                document.Close();
                MessageBox.Show($"Devis PDF sauvegardé sous {fileName}", "Devis PDF", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Méthode pour exporter le graphique
        private void btnCaptureGraphique_Click(object sender, RoutedEventArgs e)
        {

            // Capture d'écran du plotView
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)plotView.ActualWidth, (int)plotView.ActualHeight, 96, 96, PixelFormats.Pbgra32);

            renderTargetBitmap.Render(plotView);

            // Affichage de la boîte de dialogue de sauvegarde de fichier
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Fichiers PNG (*.png)|*.png";
            saveFileDialog.DefaultExt = "png";
            saveFileDialog.Title = "Enregistrer la capture d'écran";

            if (saveFileDialog.ShowDialog() == true)
            {
                // Conversion en image
                PngBitmapEncoder pngImage = new PngBitmapEncoder();
                pngImage.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

                // Sauvegarde de l'image au chemin sélectionné
                using (FileStream stream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                {
                    pngImage.Save(stream);
                }

                MessageBox.Show($"Capture d'écran du PlotView sauvegardée sous {saveFileDialog.FileName}", "Capture d'écran", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}
