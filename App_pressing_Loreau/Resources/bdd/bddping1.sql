-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Client: localhost
-- Généré le: Mer 12 Novembre 2014 à 19:26
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
  `art_photo` varchar(150),
  `art_commentaire` text,
  `art_rendu` tinyint(1) NOT NULL,
  `art_TVA` float NOT NULL,
  `art_HT` float NOT NULL,
  `art_conv_id` int(11) DEFAULT NULL,
  `art_cmd_id` int(11) NOT NULL,
  `art_typ_id` int(11) NOT NULL,
  PRIMARY KEY (`art_id`),
  KEY `fk_article_convoyeur1_idx` (`art_conv_id`),
  KEY `fk_article_commande1_idx` (`art_cmd_id`),
  KEY `fk_article_type1_idx` (`art_typ_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `client`
-- clt_type : 0=client normal  et  1=client pro
-- clt_contactmail :  0=non   et   1=oui on les contacte par mail
-- clt_contactsms : 0=non   et   1=oui
--

CREATE TABLE IF NOT EXISTS `client` (
  `clt_id` int(11) NOT NULL AUTO_INCREMENT,
  `clt_type` tinyint(1) NOT NULL,
  `clt_nom` varchar(45) NOT NULL,
  `clt_prenom` varchar(45) NOT NULL,
  `clt_dateInscription` varchar(45) NOT NULL,
  `clt_contactmail` tinyint(1) NOT NULL,
  `clt_contactsms` tinyint(1) NOT NULL,
  `clt_fix` varchar(45),
  `clt_mob` varchar(45),
  `clt_adresse` text,
  `clt_dateNaissance` date,
  `clt_email` varchar(50),
  `clt_idCleanway` int(11),
  PRIMARY KEY (`clt_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `cmd_id` int(11) NOT NULL AUTO_INCREMENT,
  `cmd_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `cmd_payee` tinyint(1) NOT NULL,
  `cmd_clt_id` int(11) NOT NULL,
  `cmd_remise` float,
  PRIMARY KEY (`cmd_id`),
  KEY `fk_commande_client1_idx` (`cmd_clt_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `convoyeur`
--

CREATE TABLE IF NOT EXISTS `convoyeur` (
  `conv_id` int(11) NOT NULL AUTO_INCREMENT,
  `conv_emplacement` int(11) NOT NULL,
  PRIMARY KEY (`conv_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Contenu de la table `convoyeur`
--

INSERT INTO `convoyeur` (`conv_emplacement`) VALUES
(1),
(2),
(3),
(4),
(5),
(6),
(7),
(8),
(9),
(10),
(11),
(12),
(13),
(14),
(15),
(16),
(17),
(18),
(19),
(20),
(21),
(22),
(23),
(24),
(25),
(26),
(27),
(28),
(29),
(30),
(31),
(32),
(33),
(34),
(35),
(36),
(37),
(38),
(39),
(40),
(41),
(42),
(43),
(44),
(45),
(46),
(47),
(48),
(49),
(50);

-- --------------------------------------------------------

--
-- Structure de la table `departement`
--

CREATE TABLE IF NOT EXISTS `departement` (
  `dep_id` int(11) NOT NULL AUTO_INCREMENT,
  `dep_nom` varchar(45) NOT NULL,
  PRIMARY KEY (`dep_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Contenu de la table `departement`
--

INSERT INTO `departement` (`dep_nom`) VALUES
('Accesoire'),
('Ameublement'),
('Blanchisserie'),
('Classique'),
('Literie'),
('Manteaux'),
('Reppassage'),
('sous traitance'),
('Vente additionnelle');

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
  `log_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `log_message` varchar(45) NOT NULL,
  `log_emp_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`log_id`),
  KEY `fk_log_employe1_idx` (`log_emp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Structure de la table `paiement`
--

CREATE TABLE IF NOT EXISTS `paiement` (
  `pai_id` int(11) NOT NULL AUTO_INCREMENT,
  `pai_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `pai_montant` float NOT NULL,
  `pai_cmd_id` int(11) NOT NULL,
  `pai_type` varchar(45) NOT NULL,
  PRIMARY KEY (`pai_id`),
  KEY `fk_paiement_commande1_idx` (`pai_cmd_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;


-- --------------------------------------------------------

--
-- Structure de la table `type`
--

CREATE TABLE IF NOT EXISTS `type` (
  `typ_id` int(11) NOT NULL AUTO_INCREMENT,
  `typ_nom` varchar(45) NOT NULL,
  `typ_encombrement` float NOT NULL,
  `typ_TVA` float NOT NULL,
  `typ_HT` float NOT NULL,
  `typ_dep_id` int(11) NOT NULL,
  PRIMARY KEY (`typ_id`),
  UNIQUE KEY `typ_nom` (`typ_nom`),
  KEY `fk_typ_departement_idx` (`typ_dep_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

--
-- Contenu de la table `type`
--

INSERT INTO `type` (`typ_nom`, `typ_encombrement`, `typ_TVA`, `typ_HT`,`typ_dep_id`) VALUES

('Pantalon',1,20,4.48,1),
('Pantalon clair', 1, 20, 5.2, 1),
('Veste', 1,5, 20,4.48,1),
('Veste clair', 1,5, 20, 6, 1),
('Costume 2 pièces', 1,5, 20,8.96, 1),
('Gilet de costume', 1, 20,3.2,1),
('Polo', 1, 20, 3.68, 1),
('Tee shirt', 1, 20, 3.68,1),
('Pull', 3, 20, 4.4, 1),
('Gilet',3,20,4.4,1),
('Pantalon',1,20,4.48,1),
('Pantalon',1,20,4.48,1),
('Pantalon',1,20,4.48,1),
('Pantalon',1,20,4.48,1),
('Pantalon',1,20,4.48,1),
('Pantalon',1,20,4.48,1),

Gilet
Jupe
Cravate
Chemise
Chemise pliée
Chemisier
Robe
Robe de soirée
Blouson
Manteau
Manteau long
Doudoine
Doudoune longue
Imperméable
couette synthétique
Couette plume
Dessus de lit
Dessus de lit epais
couverture
Couverture epaisse
traversin
traversin plume
Oreiller
oreiller plulme
housse de canapé
Rideau simple
Double Rideauw
Drap plat
Drap amidonné
Drap housse
Housse de couette
Housse de couette a la main
Taie
Torchon
serviette de table
Nappe
Serviette de toilette
Peignoir
Chemise
Pantalon
Robe
jupe
Casquette
Chapeau
foulard
Echarpe
Tapis
manteau peau
manteau daim
jupe cuir
Location machine moquette
produit moquette
Brosse deboulochage
anti mite
coffret cravate
sachet couette
Bombe impéermabilisant

-- --------------------------------------------------------

--
-- Structure de la table `typepaiement`
--

CREATE TABLE IF NOT EXISTS `typepaiement` (
  `tpp_id` int(11) NOT NULL AUTO_INCREMENT,
  `tpp_nom` varchar(45) NOT NULL,
  PRIMARY KEY (`tpp_id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;




--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `article`
--
ALTER TABLE `article`
  ADD CONSTRAINT `fk_article_convoyeur1` FOREIGN KEY (`art_conv_id`) REFERENCES `convoyeur` (`conv_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_article_commande1` FOREIGN KEY (`art_cmd_id`) REFERENCES `commande` (`cmd_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_article_type1` FOREIGN KEY (`art_typ_id`) REFERENCES `type` (`typ_id`) ON DELETE CASCADE ON UPDATE CASCADE;

  ALTER TABLE `type`
  ADD CONSTRAINT `fk_typ_departement_idx` FOREIGN KEY (`typ_dep_id`) REFERENCES `departement` (`dep_id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Contraintes pour la table `commande`
--
ALTER TABLE `commande`
  ADD CONSTRAINT `fk_commande_client1` FOREIGN KEY (`cmd_clt_id`) REFERENCES `client` (`clt_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `log`
--
ALTER TABLE `log`
  ADD CONSTRAINT `fk_log_employe1` FOREIGN KEY (`log_emp_id`) REFERENCES `employe` (`emp_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;

--
-- Contraintes pour la table `paiement`
--
ALTER TABLE `paiement`
  ADD CONSTRAINT `fk_paiement_commande1` FOREIGN KEY (`pai_cmd_id`) REFERENCES `commande` (`cmd_id`) ON DELETE NO ACTION ON UPDATE NO ACTION;


/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
