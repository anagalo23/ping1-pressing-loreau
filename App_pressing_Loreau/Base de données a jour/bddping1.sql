-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Client: localhost
-- Généré le: Lun 10 Novembre 2014 à 18:14
-- Version du serveur: 5.6.12-log
-- Version de PHP: 5.4.16

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Base de données: `bddping1`
--
CREATE DATABASE IF NOT EXISTS `bddping1` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `bddping1`;

-- --------------------------------------------------------

--
-- Structure de la table `article`
--

CREATE TABLE IF NOT EXISTS `article` (
  `art_id` int(11) NOT NULL AUTO_INCREMENT,
  `art_photo` varchar(150) NOT NULL,
  `art_commentaire` text NOT NULL,
  `art_rendu` tinyint(1) NOT NULL,
  `convoyeur_conv_id` int(11) NOT NULL,
  `departement_dep_id` int(11) NOT NULL,
  `type_typ_id` int(11) NOT NULL,
  PRIMARY KEY (`art_id`),
  KEY `fk_article_convoyeur1_idx` (`convoyeur_conv_id`),
  KEY `fk_article_departement1_idx` (`departement_dep_id`),
  KEY `fk_article_type1_idx` (`type_typ_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `client`
--

CREATE TABLE IF NOT EXISTS `client` (
  `clt_id` int(11) NOT NULL AUTO_INCREMENT,
  `clt_nom` varchar(45) NOT NULL,
  `clt_prenom` varchar(45) NOT NULL,
  `clt_num_fix` varchar(45) NOT NULL,
  `clt_num_portable` varchar(45) NOT NULL,
  `clt_adresse` text NOT NULL,
  `clt_date_naissance` date NOT NULL,
  `clt_email` varchar(50) NOT NULL,
  `clt_date_inscription` varchar(45) NOT NULL,
  `clt_idCleanway` int(11) NOT NULL,
  `clt_contactmail` tinyint(1) NOT NULL,
  `clt_contactsms` tinyint(1) NOT NULL,
  PRIMARY KEY (`clt_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `clientpro`
--

CREATE TABLE IF NOT EXISTS `clientpro` (
  `cltp_id` int(11) NOT NULL AUTO_INCREMENT,
  `cltp_noment` varchar(45) NOT NULL,
  `cltp_num_fix` varchar(45) NOT NULL,
  `cltp_num_portable` varchar(45) NOT NULL,
  `cltp_adresse` text NOT NULL,
  `clt_email` varchar(50) NOT NULL,
  `clt_date_inscription` varchar(45) NOT NULL,
  PRIMARY KEY (`cltp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `cmd_id` int(11) NOT NULL AUTO_INCREMENT,
  `cmd_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `cmd_payee` tinyint(1) NOT NULL,
  `client_clt_id` int(11) NOT NULL,
  `clientpro_cltp_id` int(11) NOT NULL,
  PRIMARY KEY (`cmd_id`),
  KEY `fk_commande_client1_idx` (`client_clt_id`),
  KEY `fk_commande_clientpro1_idx` (`clientpro_cltp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `convoyeur`
--

CREATE TABLE IF NOT EXISTS `convoyeur` (
  `conv_id` int(11) NOT NULL AUTO_INCREMENT,
  `conv_emplacement` int(11) NOT NULL,
  PRIMARY KEY (`conv_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `convoyeur`
--

INSERT INTO `convoyeur` (`conv_id`, `conv_emplacement`) VALUES
(1, 12),
(2, 21);

-- --------------------------------------------------------

--
-- Structure de la table `departement`
--

CREATE TABLE IF NOT EXISTS `departement` (
  `dep_id` int(11) NOT NULL AUTO_INCREMENT,
  `dep_nom` varchar(45) NOT NULL,
  PRIMARY KEY (`dep_id`),
  UNIQUE KEY `dep_nom` (`dep_nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Contenu de la table `departement`
--

INSERT INTO `departement` (`dep_id`, `dep_nom`) VALUES
(2, 'Accesoire'),
(6, 'Ameublement'),
(7, 'Blanchisserie'),
(3, 'Classique'),
(5, 'Literie'),
(4, 'Manteaux'),
(1, 'Reppassage'),
(8, 'sous traitance'),
(9, 'Vente additionnelle');

-- --------------------------------------------------------

--
-- Structure de la table `employe`
--

CREATE TABLE IF NOT EXISTS `employe` (
  `emp_id` int(11) NOT NULL AUTO_INCREMENT,
  `emp_nom` varchar(45) NOT NULL,
  `emp_prenom` varchar(45) NOT NULL,
  PRIMARY KEY (`emp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `log`
--

CREATE TABLE IF NOT EXISTS `log` (
  `log_id` int(11) NOT NULL AUTO_INCREMENT,
  `log_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `log_message` varchar(45) NOT NULL,
  `employe_emp_id` int(11) NOT NULL,
  PRIMARY KEY (`log_id`),
  KEY `fk_log_employe1_idx` (`employe_emp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `paiement`
--

CREATE TABLE IF NOT EXISTS `paiement` (
  `pai_id` int(11) NOT NULL AUTO_INCREMENT,
  `pai_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `pai_montant` varchar(45) NOT NULL,
  `commande_cmd_id` int(11) NOT NULL,
  PRIMARY KEY (`pai_id`),
  KEY `fk_paiement_commande1_idx` (`commande_cmd_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `prix`
--

CREATE TABLE IF NOT EXISTS `prix` (
  `tva_tva` float NOT NULL,
  `prix_ht` float NOT NULL,
  `commande_cmd_id` int(11) NOT NULL,
  `article_art_id` int(11) NOT NULL,
  PRIMARY KEY (`commande_cmd_id`,`article_art_id`),
  KEY `fk_prix_article1_idx` (`article_art_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Structure de la table `type`
--

CREATE TABLE IF NOT EXISTS `type` (
  `typ_id` int(11) NOT NULL AUTO_INCREMENT,
  `type_nom` varchar(45) NOT NULL,
  PRIMARY KEY (`typ_id`),
  UNIQUE KEY `typ_id` (`typ_id`),
  UNIQUE KEY `type_nom` (`type_nom`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Contenu de la table `type`
--

INSERT INTO `type` (`typ_id`, `type_nom`) VALUES
(4, 'Pantalon clair'),
(3, 'Patalon'),
(1, 'Veste'),
(2, 'Veste clair');

-- --------------------------------------------------------

--
-- Structure de la table `typepaiement`
--

CREATE TABLE IF NOT EXISTS `typepaiement` (
  `tpp_id` int(11) NOT NULL AUTO_INCREMENT,
  `tpp_nom` varchar(45) NOT NULL,
  `paiement_pai_id` int(11) NOT NULL,
  `paiement_commande_cmd_id` int(11) NOT NULL,
  PRIMARY KEY (`tpp_id`),
  KEY `fk_typepaiement_paiement1_idx` (`paiement_pai_id`,`paiement_commande_cmd_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `article`
--
ALTER TABLE `article`
  ADD CONSTRAINT `fk_article_convoyeur1` FOREIGN KEY (`convoyeur_conv_id`) REFERENCES `convoyeur` (`conv_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_article_departement1` FOREIGN KEY (`departement_dep_id`) REFERENCES `departement` (`dep_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_article_type1` FOREIGN KEY (`type_typ_id`) REFERENCES `type` (`typ_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `fk_commande_client1` FOREIGN KEY (`client_clt_id`) REFERENCES `bdd_pressing`.`client` (`clt_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_commande_clientpro1` FOREIGN KEY (`clientpro_cltp_id`) REFERENCES `bdd_pressing`.`clientpro` (`cltp_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `log`
--
ALTER TABLE `log`
  ADD CONSTRAINT `fk_log_employe1` FOREIGN KEY (`employe_emp_id`) REFERENCES `bdd_pressing`.`employe` (`emp_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `paiement`
--
ALTER TABLE `paiement`
  ADD CONSTRAINT `fk_paiement_commande1` FOREIGN KEY (`commande_cmd_id`) REFERENCES `bdd_pressing`.`commande` (`cmd_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `prix`
--
ALTER TABLE `prix`
  ADD CONSTRAINT `fk_prix_article1` FOREIGN KEY (`article_art_id`) REFERENCES `bdd_pressing`.`article` (`art_id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  ADD CONSTRAINT `fk_prix_commande1` FOREIGN KEY (`commande_cmd_id`) REFERENCES `bdd_pressing`.`commande` (`cmd_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `typepaiement`
--
ALTER TABLE `typepaiement`
  ADD CONSTRAINT `fk_typepaiement_paiement1` FOREIGN KEY (`paiement_pai_id`) REFERENCES `bdd_pressing`.`paiement` (`pai_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
