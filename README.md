Documentation technique qui décrit l'architecture, les fonctionnalités et les composants de l'application "Lourde" de l’entreprise Infotools. L'application est conçue pour que les managers puissent gérer les informations sur plusieurs choses, comme les clines, facture, et les commerciaux.

Architecture Générale
L'application suit une architecture à trois couches, comprenant une interface utilisateur, une couche métier et une couche de données.

Interface Utilisateur (UI)
L'interface utilisateur est développée en utilisant WPF (Windows Presentation Foundation) et XAML (eXtensible Application Markup Language). Elle offre une expérience utilisateur conviviale et intuitive pour interagir avec l'application.

Couche de Données
La couche de données est responsable de l'accès aux données persistantes. Elle utilise une base de données SQL Server pour stocker et récupérer les données de l'application. La classe `BDD.cs` gère les opérations CRUD (Create, Read, Update, Delete) avec la base de données.

Fonctionnalités Principales
- **Gestion des Clients** : Permet d'ajouter, modifier, supprimer et afficher les informations des clients.
- **Gestion des Prospects** : Permet d'ajouter, modifier, supprimer et afficher les informations des prospects.
- **Gestion des Commerciaux** : Permet d'ajouter, modifier, supprimer et afficher les informations des commerciaux.
- **Gestion des Managers** : Permet d'ajouter, modifier, supprimer et afficher les informations des managers.
- **Gestion des Produits** : Permet d'ajouter, modifier, supprimer et afficher les informations des produits.
- **Gestion des Stocks** : Permet d'ajouter, modifier, supprimer et afficher les informations des stocks.
- **Gestion des Factures** : Permet de générer des factures pour les transactions effectuées par les clients.
- **Gestion des Rendez-vous** : Permet de planifier et de gérer les rendez-vous avec les clients.
Composants Principaux
1. **MainWindow.xaml.cs** : Contient la logique de l'interface utilisateur principale de l'application.
2. **Service.cs** : Implémente la logique métier de l'application, y compris la gestion des clients, des produits, des factures, etc.
3. **BDD.cs** : Gère l'accès aux données persistantes en interagissant avec la base de données SQL Server.
4. **Modèles (Client.cs, Produit.cs, Facture.cs, RendezVous.cs, etc.)** : Définit les structures de données pour les différents entités de l'application.
5. **MainWindow.xaml** : Fournit l'interface utilisateur principale de l'application, développée en utilisant XAML.

Technologies Utilisées
- **Langage de Programmation** : C# (.NET Framework)
- **Framework UI** : WPF (Windows Presentation Foundation)
- **Base de Données** : Microsoft SQL Server
- **Outils de Développement** : Visual Studio


Remarques
- Pour toute modification de la base de données, veuillez mettre à jour le modèle correspondant dans le code source de l'application.
- Les données sensibles telles que les informations de connexion à la base de données doivent être gérées avec soin pour des raisons de sécurité.

Ceci conclut la documentation technique de l'application "Lourde" d’Infotools. En cas de questions ou de problèmes, veuillez contacter Massié Loan développeur de l’application.
