Feature: MediaTekDocuments

Scenario: Recherche titre document
	Given je saisis le titre de document Catastrophes au Brésil
	Then le resultat trouvé a pour numéro de document 00017

Scenario: Recherche numéro document
	Given je saisis le numéro de document 00017
	When je clic sur le bouton rechercher
	Then le resultat trouvé a pour titre Catastrophes au Brésil

Scenario: Recherche genre
	When je sélectionne le genre Bande dessinée
	Then le nombre de livres obtenu est de 5

Scenario: Recherche public
	When je sélectionne le public Ados
	Then le nombre de livres obtenu est de 3

Scenario: Recherche rayon
	When je sélectionne le rayon BD Adultes
	Then le nombre de livres obtenu est de 4