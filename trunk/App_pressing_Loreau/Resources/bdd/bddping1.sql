-- phpMyAdmin SQL Dump
-- version 4.0.4
-- http://www.phpmyadmin.net
--
-- Client: localhost
-- Généré le: Mer 14 Janvier 2015 à 19:55
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
  `art_photo` varchar(150) DEFAULT NULL,
  `art_commentaire` text,
  `art_rendu` tinyint(1) NOT NULL,
  `art_TVA` float NOT NULL,
  `art_TTC` float NOT NULL,
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
--

CREATE TABLE IF NOT EXISTS `client` (
  `clt_id` int(11) NOT NULL AUTO_INCREMENT,
  `clt_type` tinyint(1) NOT NULL,
  `clt_nom` varchar(45) NOT NULL,
  `clt_prenom` varchar(45) NOT NULL,
  `clt_dateInscription` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `clt_contactmail` tinyint(1) NOT NULL,
  `clt_contactsms` tinyint(1) NOT NULL,
  `clt_fix` varchar(45) DEFAULT NULL,
  `clt_mob` varchar(45) DEFAULT NULL,
  `clt_adresse` text,
  `clt_dateNaissance` date DEFAULT NULL,
  `clt_email` varchar(50) DEFAULT NULL,
  `clt_idCleanway` int(11) DEFAULT NULL,
  PRIMARY KEY (`clt_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=44 ;

--
-- Contenu de la table `client`
--

INSERT INTO `client` (`clt_type`, `clt_nom`, `clt_prenom`, `clt_dateInscription`, `clt_contactmail`, `clt_contactsms`, `clt_fix`, `clt_mob`, `clt_adresse`, `clt_dateNaissance`, `clt_email`, `clt_idCleanway`) VALUES

( 0, 'Faye', 'Pauline', '2015-01-14 16:58:41', 0, 0, NULL, NULL, "12\\Place des faienciers\\76100\\Rouen\\", '1992-10-27', NULL, 12);

-- --------------------------------------------------------

--
-- Structure de la table `commande`
--

CREATE TABLE IF NOT EXISTS `commande` (
  `cmd_id` int(11) NOT NULL AUTO_INCREMENT,
  `cmd_date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `cmd_payee` tinyint(1) NOT NULL,
  `cmd_clt_id` int(11) NOT NULL,
  `cmd_remise` float DEFAULT NULL,
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
  `conv_encombrement` float NOT NULL,
  PRIMARY KEY (`conv_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=51 ;

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Contenu de la table `departement`
--

INSERT INTO `departement` (`dep_nom`) VALUES
('Classique'),
('Manteaux'),
('Literie'),
('Ameublement'),
('Blanchisserie'),
('Reppassage'),
('Accesoire'),
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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Contenu de la table `employe`
--

INSERT INTO `employe` (`emp_nom`, `emp_prenom`) VALUES
('Taquet', 'Pierre'),
('Lefevre', 'David');

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
  `typ_nom` varchar(45) CHARACTER SET utf8 NOT NULL,
  `typ_encombrement` float NOT NULL,
  `typ_TVA` float NOT NULL,
  `typ_TTC` float NOT NULL,
  `typ_dep_id` int(11) NOT NULL,
  PRIMARY KEY (`typ_id`),
  KEY `fk_typ_departement_idx` (`typ_dep_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=153 ;

--
-- Contenu de la table `type`
--

INSERT INTO `type` (`typ_nom`, `typ_encombrement`, `typ_TVA`, `typ_TTC`, `typ_dep_id`) VALUES
('Pantalon clair', 1, 20, 5.2, 10),
('Pantalon', 1, 20, 4.48, 10),
('Veste', 1.5, 20, 4.48, 10),
('Veste clair', 1, 20, 6, 10),
('Costume 2 pièces', 1, 20, 8.96, 10),
('Gilet de costume', 1, 20, 3.2, 10),
('Polo', 1, 20, 3.68, 10),
('Tee shirt', 1, 20, 3.68,10),
('Pull', 3, 20, 4.4, 10),
('Gilet', 3, 20, 4.4, 10),
('Jupe', 1, 20, 4.8, 10),
('Cravate', 0, 20, 4.96,10),
('Chemise', 1, 20, 3.76,10),
('Chemise pliée', 0, 20, 4.16, 10),
('Chemisier', 1, 20, 4.16, 10),
('Robe', 3, 20, 6.96, 10),
('Robe de soirée', 3, 20, 12, 10),
('Blouson', 1, 20, 6.96, 11),
('Manteau', 1, 20, 8.8, 11),
('Manteau long ', 1, 20, 10.4,11),
('Doudoune ', 1, 20, 12.4,11),
('Doudoune longue ', 1, 20, 14.8,11),
('Imprerméable ', 1.5, 20, 12,11),
('Couette synthétique ', 0, 20, 14,12),
('Couette plume', 0, 20, 18.8, 12),
('Dessus de lit', 0, 20, 12.8, 12),
('Dessus de lit epais', 0, 20, 14,12),
('Couverture', 0, 20, 12,12),
('Couverture epaisse', 0, 20, 13.6,12),
('Traversin', 0, 20, 9.6, 12),
('Traversin plume', 0, 20, 15.2,12),
('Oreiller', 0, 20, 6,12),
('Oreiller plume ', 0, 20, 10,12),
('Housse de canapé', 0, 20, 24,12),
('Rideau simple', 0, 20, 5.6,13),
('Double rideau', 0, 20, 9.6,13),
('Drap plat', 10, 20, 2.32, 14),
('Drap amidonné', 10, 20, 3.6, 14),
('Drap house', 10, 20, 2.8, 14),
('Housse de couette ', 10, 20, 5.04, 14),
('Housse de couette à la main', 10, 20, 6.16, 14),
('Taie', 10, 20, 1.6, 14),
('Torchon', 10, 20, 0.96, 14),
('Serviette de table ', 10, 20, 0.88, 14),
('Nappe', 10, 20, 6.4, 14),
('Serviette de toilette', 10, 20, 4, 14),
('Peignoir', 10, 20, 6, 14),
('Chemises', 1, 20, 2.4, 15),
('Pantalons', 1, 20, 2.4, 15),
('Robes', 3, 20, 4.8, 15),
('Jupes', 1, 20, 3.2, 15),
('Casquette', 0, 20, 4, 16),
('Chapeau', 0, 20, 4, 16),
('Foulard', 1, 20, 4.8, 16),
('Echarpe', 1, 20, 4, 16),
('Tapis', 0, 20, 5.6, 17),
('Manteau peau', 0, 20, 52, 17),
('Manteau daim ', 0, 20, 60, 17),
('Jupe cuir ', 0, 20, 60, 17),
('Location machine moquette', 0, 20, 28, 17),
('Produit moquette', 0, 20, 8, 17),
('Brosse deboulochage', 0, 20, 4, 18),
('Anti mite', 0, 20, 3.6, 18),
('Coffret cravate ', 0, 20, 11.92, 18),
('Sachet couette', 0, 20, 7.92, 18),
('Bombe impéermabilisant', 0, 20, 6, 18);

-- --------------------------------------------------------

--
-- Structure de la table `typepaiement`
--

CREATE TABLE IF NOT EXISTS `typepaiement` (
  `tpp_id` int(11) NOT NULL AUTO_INCREMENT,
  `tpp_nom` varchar(45) NOT NULL,
  PRIMARY KEY (`tpp_id`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=7 ;

--
-- Contenu de la table `typepaiement`
--

INSERT INTO `typepaiement` (`tpp_nom`) VALUES
('CleanWay'),
('CarteBancaire'),
('Espece '),
('AmericanExpress'),
('Cheque'),
('Nature');

--
-- Contraintes pour les tables exportées
--

--
-- Contraintes pour la table `article`
--
ALTER TABLE `article`
  ADD CONSTRAINT `fk_article_commande1` FOREIGN KEY (`art_cmd_id`) REFERENCES `commande` (`cmd_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_article_convoyeur1` FOREIGN KEY (`art_conv_id`) REFERENCES `convoyeur` (`conv_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk_article_type1` FOREIGN KEY (`art_typ_id`) REFERENCES `type` (`typ_id`) ON DELETE CASCADE ON UPDATE CASCADE;

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

--
-- Contraintes pour la table `type`
--
ALTER TABLE `type`
  ADD CONSTRAINT `fk_typ_departement_idx` FOREIGN KEY (`typ_dep_id`) REFERENCES `departement` (`dep_id`) ON DELETE CASCADE ON UPDATE CASCADE;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
