-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Oct 30, 2015 at 10:00 AM
-- Server version: 5.5.27
-- PHP Version: 5.4.7

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `shedulling`
--

-- --------------------------------------------------------

--
-- Table structure for table `tablelayout1`
--

CREATE TABLE IF NOT EXISTS `tablelayout1` (
  `date` date NOT NULL,
  `line` varchar(11) NOT NULL,
  `col1` varchar(100) NOT NULL,
  `col2` varchar(100) NOT NULL,
  `col3` varchar(100) NOT NULL,
  `col4` varchar(100) NOT NULL,
  `col5` varchar(100) NOT NULL,
  `col6` varchar(100) NOT NULL,
  `col7` varchar(100) NOT NULL,
  `col8` varchar(100) NOT NULL,
  `col9` varchar(100) NOT NULL,
  `col10` varchar(100) NOT NULL,
  `col11` varchar(100) NOT NULL,
  `col12` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE IF NOT EXISTS `users` (
  `Username` varchar(15) NOT NULL,
  `password` varchar(15) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`Username`, `password`) VALUES
('admin', '12345');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
