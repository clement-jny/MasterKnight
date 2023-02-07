-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Hôte : localhost
-- Généré le : dim. 18 déc. 2022 à 18:00
-- Version du serveur : 5.7.39
-- Version de PHP : 8.1.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de données : `MasterKnight`
--
CREATE DATABASE IF NOT EXISTS `MasterKnight` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `MasterKnight`;

-- --------------------------------------------------------

--
-- Structure de la table `armor`
--

CREATE TABLE `armor` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `life_point` double NOT NULL,
  `protection` double NOT NULL,
  `price` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `armor`
--

INSERT INTO `armor` (`id`, `name`, `life_point`, `protection`, `price`) VALUES
(1, 'Armure en maille', 10, 2, 5);

-- --------------------------------------------------------

--
-- Structure de la table `consumable`
--

CREATE TABLE `consumable` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `bonus` tinyint(4) NOT NULL,
  `effect` varchar(255) NOT NULL,
  `value` int(11) NOT NULL,
  `price` double NOT NULL,
  `fk_player_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `consumable`
--

INSERT INTO `consumable` (`id`, `name`, `bonus`, `effect`, `value`, `price`, `fk_player_id`) VALUES
(1, 'Bread', 1, 'LifePoint', 2, 5, 1),
(2, 'Soup', 1, 'LifePoint', 2, 2, 0);

-- --------------------------------------------------------

--
-- Structure de la table `inventory`
--

CREATE TABLE `inventory` (
  `fk_player_id` int(11) NOT NULL,
  `fk_consumable_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `inventory`
--

INSERT INTO `inventory` (`fk_player_id`, `fk_consumable_id`) VALUES
(1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `monster`
--

CREATE TABLE `monster` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `life_point` double NOT NULL,
  `strength` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `monster`
--

INSERT INTO `monster` (`id`, `name`, `life_point`, `strength`) VALUES
(1, 'Ogre', 10, 5);

-- --------------------------------------------------------

--
-- Structure de la table `player`
--

CREATE TABLE `player` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `base_life_point` double NOT NULL,
  `life_point` double NOT NULL,
  `base_strength` double NOT NULL,
  `strength` double NOT NULL,
  `money` double NOT NULL,
  `fk_armor_id` int(11) NOT NULL,
  `fk_weapon_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `player`
--

INSERT INTO `player` (`id`, `name`, `base_life_point`, `life_point`, `base_strength`, `strength`, `money`, `fk_armor_id`, `fk_weapon_id`) VALUES
(1, 'Clément', 10, 10, 5, 5, 1000, 1, 1);

-- --------------------------------------------------------

--
-- Structure de la table `weapon`
--

CREATE TABLE `weapon` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL,
  `damage` double NOT NULL,
  `price` double NOT NULL,
  `category` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Déchargement des données de la table `weapon`
--

INSERT INTO `weapon` (`id`, `name`, `damage`, `price`, `category`) VALUES
(1, 'Bout de bois', 1, 0, 1);

-- --------------------------------------------------------

--
-- Structure de la table `__EFMigrationsHistory`
--

CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Déchargement des données de la table `__EFMigrationsHistory`
--

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`) VALUES
('20221018215229_Initial', '6.0.10'),
('20221018223800_Adding relationships', '6.0.10'),
('20221021213841_Modify table', '6.0.10');

--
-- Index pour les tables déchargées
--

--
-- Index pour la table `armor`
--
ALTER TABLE `armor`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `consumable`
--
ALTER TABLE `consumable`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `inventory`
--
ALTER TABLE `inventory`
  ADD PRIMARY KEY (`fk_player_id`,`fk_consumable_id`),
  ADD KEY `fk_consumable_id` (`fk_consumable_id`);

--
-- Index pour la table `monster`
--
ALTER TABLE `monster`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `player`
--
ALTER TABLE `player`
  ADD PRIMARY KEY (`id`),
  ADD KEY `IX_player_fk_armor_id` (`fk_armor_id`),
  ADD KEY `IX_player_fk_weapon_id` (`fk_weapon_id`);

--
-- Index pour la table `weapon`
--
ALTER TABLE `weapon`
  ADD PRIMARY KEY (`id`);

--
-- Index pour la table `__EFMigrationsHistory`
--
ALTER TABLE `__EFMigrationsHistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT pour les tables déchargées
--

--
-- AUTO_INCREMENT pour la table `armor`
--
ALTER TABLE `armor`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `consumable`
--
ALTER TABLE `consumable`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT pour la table `monster`
--
ALTER TABLE `monster`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `player`
--
ALTER TABLE `player`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT pour la table `weapon`
--
ALTER TABLE `weapon`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Contraintes pour les tables déchargées
--

--
-- Contraintes pour la table `inventory`
--
ALTER TABLE `inventory`
  ADD CONSTRAINT `inventory_ibfk_1` FOREIGN KEY (`fk_player_id`) REFERENCES `player` (`id`),
  ADD CONSTRAINT `inventory_ibfk_2` FOREIGN KEY (`fk_consumable_id`) REFERENCES `consumable` (`id`);

--
-- Contraintes pour la table `player`
--
ALTER TABLE `player`
  ADD CONSTRAINT `FK_player_armor_fk_armor_id` FOREIGN KEY (`fk_armor_id`) REFERENCES `armor` (`id`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_player_weapon_fk_weapon_id` FOREIGN KEY (`fk_weapon_id`) REFERENCES `weapon` (`id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
