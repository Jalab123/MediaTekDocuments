# MediatekDocuments
Cette application permet de gérer les documents (livres, DVD, revues) d'une médiathèque. Elle a été codée en C# sous Visual Studio 2022. C'est une application de bureau, prévue d'être installée sur plusieurs postes accédant à la même base de données.<br>
L'application exploite une API REST pour accéder à la BDD MySQL.

## Lien vers les dépôts d'origine
<br>Vous pouvez retrouver le dépôt d'origine de l'application C# en cliquant sur ce lien: https://github.com/CNED-SLAM/MediaTekDocuments .
<br>Vous pouvez retrouver le dépôt d'origine de l'API rest en cliquant sur ce lien: https://github.com/CNED-SLAM/rest_mediatekdocuments .
<br>Dans le readme de ces dépôts se trouvent l'application C# et l'API rest d'origine, ainsi que la présentation d'origine.

## Présentation des fonctionnalités ajoutées
<br>Dans cette partie, vous pourrez retrouver les fonctionnalités ajoutées à l'application.

### Fenêtre d'authentification
<br>Lorsque l'application est lancée, une fenêtre demande à l'utilisateur de s'authentifier.
<br>Son niveau de droit variera en fonction de son service: accès complet, partiel ou interdit à l'application.

![img](images/1.png)
![img](images/2.png)
![img](images/3.png)
![img](images/4.png)

### Fenêtre des abonnements se terminant dans les 30 prochains jours

<br>Si l'utilisateur a les permissions suffisantes (gestion), une fenêtre s'affiche au démarrage lui montrant la liste des abonnements se terminant dans les 30 prochains jours.
<br>Cette fenêtre ne s'affichera pas si aucun abonnement n'est dans cette situation.

![img](images/5.png)

### Fenêtre de gestion des livres

<br> Il est possible d'ajouter une nouvelle commande de livre une fois son numéro sélectionné, en précisant sa date de commande, son montant et son nombre d'exemplaires.

![img](images/6.png)

<br> Il est possible de modifier le statut d'une commande une fois son numéro sélectionné, en respectant l'ordre des étapes.
<br> En cours -> Livrée / Relancée
<br> Relancée -> Livrée / En cours
<br> Livrée -> Réglée
<br> Réglée: Etape finale
<br> Lorsqu'une commande passe à "Livrée", les exemplaires sont automatiquement ajoutés dans la base de données.

![img](images/7.png)

<br> Il est possible de supprimer une commande de livre une fois son numéro sélectionné, à condition que le statut de la commande ne soit pas à "Livrée".

![img](images/8.png)
![img](images/9.png)

### Fenêtre de gestion des dvds

<br> Il est possible d'ajouter une nouvelle commande de dvd une fois son numéro sélectionné, en précisant sa date de commande, son montant et son nombre d'exemplaires.

![img](images/10.png)

<br> Il est possible de modifier le statut d'une commande une fois son numéro sélectionné, en respectant l'ordre des étapes.
<br> En cours -> Livrée / Relancée
<br> Relancée -> Livrée / En cours
<br> Livrée -> Réglée
<br> Réglée: Etape finale
<br> Lorsqu'une commande passe à "Livrée", les exemplaires sont automatiquement ajoutés dans la base de données.

![img](images/11.png)

<br> Il est possible de supprimer une commande de dvd une fois son numéro sélectionné, à condition que le statut de la commande ne soit pas à "Livrée".

![img](images/12.png)
![img](images/13.png)

### Fenêtre de gestion des revues

<br> Il est possible d'ajouter une nouvelle commande de revue une fois son numéro sélectionné, en précisant sa date de commande, son montant et sa date d'expiration.

![img](images/14.png)

<br> Il est possible d'afficher la fenêtre des abonnements se terminant dans les 30 prochains jours.

![img](images/15.png)

<br> Il est possible de supprimer une commande de revue une fois son numéro sélectionné, à condition qu'aucun exemplaire n'est rattaché.

![img](images/16.png)
![img](images/17.png)
![img](images/18.png)

### Vidéo de présentation des fonctionnalités
<br>Pour plus d'informations, il est possible de consulter cette vidéo, montrant toutes les
fonctionnalités de la nouvelle application: https://youtu.be/d0FO9RWz3g0 .

### Documentations techniques
<br>Vous pourrez retrouver les documentations techniques de l'application C# et de l'API rest dans le dossier "documentations".
<br>Pour les consulter en local, lancez Wamp64 et placez les deux dossiers contenus dans le dossier "documentations" du dépôt dans votre dossier "C:/wamp64/www".
<br>Vous pourrez ainsi les consulter via les liens suivants: http://localhost/mediatekdocuments_doc (application C#) et http://localhost/rest_mediatekdocuments_doc (API rest).
<br>Vous pouvez aussi accéder aux documentations en ligne via les liens suivants: http://mediatekdocumentsugo.francecentral.cloudapp.azure.com/rest_mediatekdocuments_doc/ (application C#) et http://mediatekdocumentsugo.francecentral.cloudapp.azure.com/mediatekdocuments_doc/html/85d46cf5-bf55-169b-05a9-fbd4084b4f5d.htm (API rest).

### Installer l'application
<br>Pour installer l'application, rendez-vous dans le dossier "installateurs".
<br>Si vous voulez tester l'application en local, ouvrez le setup "MediaTekDocumentsInstalleurLocal", et suivez les étapes pour procéder à l'installation.
<br>Si vous voulez tester l'application en ligne, ouvrez le setup "MediaTekDocumentsInstalleurOnline", et suivez les étapes pour procéder à l'installation.
<br>Dans le cas où vous avez choisi l'application en ligne, l'application sera directement prête à être utilisée: un raccourci sur le bureau a normalement été créé, et l'application doit normalement apparaître dans la liste des programmes. Vous n'avez plus qu'à exécuter l'application, et à vous authentifier.
<br>Dans le cas où vous avez choisi l'application en local, l'application sera installée: un raccourci sur le bureau a normalement été créé, et l'application doit normalement apparaître dans la liste des programmes. Cependant, l'API rest et la base de données n'ont pas encore été installés. Suivez donc les étapes suivants pour pouvoir installer l'API rest et la base de données en local.
<br>1) Téléchargez l'API rest via ce lien: https://github.com/Jalab123/rest_mediatekdocuments.
<br>2) Renommer le dossier "rest_mediatekdocuments", placez-le dans votre dossier "C:/wamp64/www" et lancez Wamp64.
<br>3) Ouvrez une console en mode administrateur, dirigez vous dans le dossier de l'API rest avec la commande "C:/wamp64/www", et exécutez la commande "composer install".
<br>4) Connectez-vous à PhpMyAdmin via ce lien: http://localhost/phpmyadmin .
<br>5) Créez une nouvelle base de données "mediatek86".
<br>6) Allez dans "Importer" et sélectionnez le fichier "mediatek86.sql", puis exécutez.
<br>7) L'API rest et la base de données ont été correctement mis en place. Vous n'avez plus qu'à lancer l'application, et à vous authentifier. Adaptez les informations contenues dans le ".env" de l'API rest si besoin (notamment si votre utilisateur de base de données est différent).
