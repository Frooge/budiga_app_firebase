-- phpMyAdmin SQL Dump
-- version 5.1.0
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 27, 2022 at 01:58 PM
-- Server version: 10.4.18-MariaDB
-- PHP Version: 7.4.18

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `budiga_app`
--

-- --------------------------------------------------------

--
-- Table structure for table `attendance`
--

CREATE TABLE `attendance` (
  `id` bigint(10) NOT NULL,
  `user_id` bigint(11) NOT NULL,
  `time_in` datetime NOT NULL,
  `time_out` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `attendance`
--

INSERT INTO `attendance` (`id`, `user_id`, `time_in`, `time_out`) VALUES
(1, 1, '2022-05-14 00:50:44', '2022-05-14 00:50:52'),
(2, 3, '2022-05-14 00:51:00', '2022-05-14 00:51:04'),
(3, 1, '2022-05-14 00:53:49', '2022-05-14 00:54:11'),
(4, 2, '2022-05-14 16:46:06', '2022-05-14 16:46:26'),
(5, 1, '2022-05-14 16:46:30', '2022-05-14 16:47:22'),
(6, 2, '2022-05-14 16:47:27', '2022-05-14 16:47:30'),
(7, 1, '2022-05-14 17:44:12', '2022-05-14 17:44:29'),
(8, 1, '2022-05-14 17:45:07', '2022-05-14 17:45:45'),
(9, 1, '2022-05-22 21:56:20', '2022-05-22 21:57:04'),
(10, 1, '2022-05-22 21:58:56', '2022-05-22 21:59:03'),
(11, 1, '2022-05-22 21:59:20', '2022-05-22 21:59:36'),
(12, 1, '2022-05-22 22:05:24', '2022-05-22 22:05:41'),
(13, 1, '2022-05-23 00:05:58', '2022-05-23 00:06:28'),
(14, 1, '2022-05-25 22:34:37', '2022-05-25 22:34:48'),
(15, 1, '2022-05-27 15:06:27', '2022-05-27 15:11:28'),
(16, 2, '2022-05-27 15:23:25', '2022-05-27 15:23:42'),
(17, 1, '2022-05-27 15:26:02', '2022-05-27 15:27:17'),
(18, 1, '2022-05-27 15:51:33', '2022-05-27 15:54:33'),
(19, 4, '2022-05-27 17:04:19', '2022-05-27 17:04:43'),
(20, 4, '2022-05-27 17:07:31', '2022-05-27 17:08:53'),
(21, 4, '2022-05-27 17:10:28', '2022-05-27 17:12:26'),
(22, 4, '2022-05-27 17:14:48', '2022-05-27 17:14:55'),
(23, 1, '2022-05-27 17:14:58', '2022-05-27 17:15:33'),
(24, 1, '2022-05-27 17:38:19', '2022-05-27 17:40:13'),
(25, 1, '2022-05-27 17:49:40', '2022-05-27 17:50:31'),
(26, 1, '2022-05-27 18:06:31', '2022-05-27 18:07:01'),
(27, 1, '2022-05-27 18:19:51', '2022-05-27 18:20:24');

-- --------------------------------------------------------

--
-- Table structure for table `invoice`
--

CREATE TABLE `invoice` (
  `id` bigint(10) NOT NULL,
  `user_id` bigint(10) NOT NULL,
  `total_price` float NOT NULL,
  `customer_pay` float NOT NULL,
  `customer_change` float NOT NULL,
  `created_date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `invoice`
--

INSERT INTO `invoice` (`id`, `user_id`, `total_price`, `customer_pay`, `customer_change`, `created_date`) VALUES
(7, 1, 49, 50, 1, '2022-05-12 02:55:39'),
(8, 1, 460, 500, 40, '2022-05-12 02:58:06'),
(9, 1, 398, 500, 102, '2022-05-12 03:05:06'),
(10, 1, 38, 50, 12, '2022-05-12 03:08:02'),
(11, 1, 404, 500, 96, '2021-05-14 16:20:13'),
(12, 1, 104, 150, 46, '2022-05-14 16:15:50'),
(13, 1, 60, 100, 40, '2022-05-25 23:12:27'),
(14, 1, 40, 50, 10, '2022-05-25 23:20:45'),
(15, 1, 396, 400, 4, '2022-05-25 23:30:06'),
(16, 1, 912, 1000, 88, '2022-04-01 16:34:46');

-- --------------------------------------------------------

--
-- Table structure for table `item`
--

CREATE TABLE `item` (
  `id` bigint(20) NOT NULL,
  `barcode` varchar(13) NOT NULL DEFAULT 'N/A',
  `name` varchar(64) NOT NULL,
  `brand` varchar(64) NOT NULL,
  `price` float NOT NULL,
  `quantity` int(11) NOT NULL,
  `is_deleted` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `item`
--

INSERT INTO `item` (`id`, `barcode`, `name`, `brand`, `price`, `quantity`, `is_deleted`) VALUES
(0, '9780206369293', 'Skyflake', 'Biskwet', 6, 6, 0),
(2, '9780206369286', 'Shampoo', 'Sunsilk', 19, 2, 0),
(3, '9780216369214', 'Yakult', 'Nestle', 99, 977, 0),
(4, '9780246369253', 'Magnolia', 'Nestle', 200, 40, 0),
(5, '9780246359254', 'Iced Tea', 'Nestle', 8, 75, 0),
(6, '9780246359216', 'Orange Juice', 'XXX', 10, 45, 0),
(7, '11111111111', 'C2 335ml', 'URC', 20, 4, 1),
(8, '9780246359285', 'Soap', 'Safeguard', 20, 31, 0),
(10, '9780246359223', 'Tide Ultra OXI', 'Tide', 600, 100, 0),
(11, '9780246353238', 'Beng-Beng', 'Mayora', 7, 250, 0),
(12, '9785246353264', 'C2 500ml', 'URC', 10, 200, 0);

-- --------------------------------------------------------

--
-- Table structure for table `item_history`
--

CREATE TABLE `item_history` (
  `id` bigint(20) NOT NULL,
  `item_id` int(11) NOT NULL,
  `barcode` varchar(13) NOT NULL DEFAULT 'N/A',
  `name` varchar(64) NOT NULL,
  `brand` varchar(64) NOT NULL,
  `price` float NOT NULL,
  `quantity` int(11) NOT NULL,
  `action` varchar(10) NOT NULL,
  `comitted_date` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `item_history`
--

INSERT INTO `item_history` (`id`, `item_id`, `barcode`, `name`, `brand`, `price`, `quantity`, `action`, `comitted_date`) VALUES
(1, 0, 'N/A', 'Skyflake', 'Biskwet', 11, 6, 'UPDATED', '2022-05-24 21:45:46'),
(2, 0, 'N/A', 'Skyflake', 'Biskwet', 11, 6, 'UPDATED', '2022-05-24 21:50:25'),
(3, 2, 'N/A', 'Shampoo', 'Sunsilk', 19, 2, 'UPDATED', '2022-05-24 21:51:53'),
(4, 6, 'N/A', 'Orange Juice', 'XXX', 24, 1, 'UPDATED', '2022-05-24 22:01:50'),
(5, 6, 'N/A', 'Orange Juice', 'XXX', 24, 1, 'UPDATED', '2022-05-24 22:02:18'),
(6, 7, '11111111111', 'C2', 'idk', 20, 4, 'DELETED', '2022-05-24 22:08:39'),
(7, 6, 'N/A', 'Orange Juice', 'XXX', 24, 1, 'UPDATED', '2022-05-24 22:09:00'),
(8, 6, 'N/A', 'Orange Juice', 'XXX', 24, 1, 'UPDATED', '2022-05-24 23:12:40'),
(9, 8, '12349081231', 'Safeguard', 'idk', 20, 31, 'UPDATED', '2022-05-25 23:12:27'),
(10, 5, 'N/A', 'Iced Tea', 'Nestle', 20, 77, 'UPDATED', '2022-05-25 23:20:46'),
(11, 3, 'N/A', 'Yakult', 'Nestle', 99, 989, 'UPDATED', '2022-05-25 23:30:06'),
(12, 3, 'N/A', 'Yakult', 'Nestle', 99, 985, 'UPDATED', '2022-05-27 15:58:35'),
(13, 3, '012345678901', 'Yakult', 'Nestle', 99, 985, 'UPDATED', '2022-05-27 15:59:07'),
(14, 3, '012354678901', 'Yakult', 'Nestle', 99, 985, 'UPDATED', '2022-05-27 16:00:33'),
(15, 2, 'N/A', 'Shampoo', 'Sunsilk', 19, 2, 'UPDATED', '2022-05-27 16:27:04'),
(16, 3, '0123546789014', 'Yakult', 'Nestle', 99, 985, 'UPDATED', '2022-05-27 16:27:55'),
(17, 4, 'N/A', 'Magnolia', 'Nestle', 200, 2, 'UPDATED', '2022-05-27 16:28:03'),
(18, 5, 'N/A', 'Iced Tea', 'Nestle', 20, 75, 'UPDATED', '2022-05-27 16:28:09'),
(19, 6, 'N/A', 'Orange Juice', 'XXX', 24, 1, 'UPDATED', '2022-05-27 16:28:15'),
(20, 8, '12349081231', 'Safeguard', 'idk', 20, 31, 'UPDATED', '2022-05-27 16:28:37'),
(21, 0, '9780246359223', 'Tide Ultra OXI', 'Tide', 600, 100, 'ADDED', '2022-05-27 16:29:56'),
(22, 0, '9780246353238', 'Beng-Beng', 'Mayora', 7, 250, 'ADDED', '2022-05-27 16:31:27'),
(23, 0, '9785246353264', 'C2', 'URC', 10, 200, 'ADDED', '2022-05-27 16:33:06'),
(24, 6, '9780246359216', 'Orange Juice', 'XXX', 24, 1, 'UPDATED', '2022-05-27 16:33:42'),
(25, 4, '9780246369253', 'Magnolia', 'Nestle', 200, 2, 'UPDATED', '2022-05-27 16:34:00'),
(26, 6, '9780246359216', 'Orange Juice', 'XXX', 24, 50, 'UPDATED', '2022-05-27 16:34:45'),
(27, 3, '9780216369214', 'Yakult', 'Nestle', 99, 985, 'UPDATED', '2022-05-27 16:34:46'),
(28, 7, '11111111111', 'C2 335ml', 'URC', 20, 4, 'DELETED', '2022-05-27 17:00:33'),
(29, 7, '11111111111', 'C2 335ml', 'URC', 20, 4, 'UPDATED', '2022-05-27 17:01:40'),
(30, 7, '11111111111', 'C2 335ml', 'URC', 20, 4, 'DELETED', '2022-05-27 17:01:59');

-- --------------------------------------------------------

--
-- Table structure for table `order`
--

CREATE TABLE `order` (
  `id` bigint(20) NOT NULL,
  `item_id` bigint(20) NOT NULL,
  `invoice_id` bigint(20) NOT NULL,
  `quantity` int(11) NOT NULL,
  `subtotal_price` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `order`
--

INSERT INTO `order` (`id`, `item_id`, `invoice_id`, `quantity`, `subtotal_price`) VALUES
(5, 0, 7, 1, 11),
(6, 2, 7, 2, 38),
(7, 4, 8, 2, 400),
(8, 5, 8, 3, 60),
(9, 4, 9, 1, 200),
(10, 3, 9, 2, 198),
(11, 2, 10, 2, 38),
(12, 5, 11, 6, 120),
(13, 6, 11, 1, 24),
(14, 7, 11, 1, 20),
(15, 8, 11, 2, 40),
(16, 4, 11, 1, 200),
(17, 0, 12, 6, 66),
(18, 2, 12, 2, 38),
(19, 8, 13, 3, 60),
(20, 5, 14, 2, 40),
(21, 3, 15, 4, 396),
(22, 6, 16, 5, 120),
(23, 3, 16, 8, 792);

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `id` bigint(10) NOT NULL,
  `fname` varchar(32) NOT NULL,
  `lname` varchar(32) NOT NULL,
  `username` varchar(32) NOT NULL,
  `password` varchar(32) NOT NULL,
  `contact` varchar(32) NOT NULL,
  `user_role` enum('Admin','Employee') NOT NULL,
  `created_date` datetime DEFAULT current_timestamp(),
  `updated_date` datetime DEFAULT current_timestamp(),
  `is_deleted` int(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`id`, `fname`, `lname`, `username`, `password`, `contact`, `user_role`, `created_date`, `updated_date`, `is_deleted`) VALUES
(1, 'John', 'Doe', 'admin', '1234', '12345678901', 'Admin', '2022-04-19 20:46:15', '2022-05-13 23:54:21', 0),
(2, 'Mary', 'Sue', 'employee', '1234', '09876543210', 'Employee', '2022-04-19 20:46:15', '2022-04-19 20:46:15', 0),
(3, 'Jade', 'Rosales', 'JRosales', '1234', '12345678901', 'Employee', '2022-05-13 23:50:33', '2022-05-14 00:02:06', 0),
(4, 'Jose', 'Rizal', 'jriz', '1234', '091238929', 'Employee', '2022-05-27 16:40:52', '2022-05-27 16:40:52', 0);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `attendance`
--
ALTER TABLE `attendance`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `invoice`
--
ALTER TABLE `invoice`
  ADD PRIMARY KEY (`id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `item`
--
ALTER TABLE `item`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `item_history`
--
ALTER TABLE `item_history`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `order`
--
ALTER TABLE `order`
  ADD PRIMARY KEY (`id`),
  ADD KEY `barcode_id` (`item_id`),
  ADD KEY `purchase_id` (`invoice_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `attendance`
--
ALTER TABLE `attendance`
  MODIFY `id` bigint(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=28;

--
-- AUTO_INCREMENT for table `invoice`
--
ALTER TABLE `invoice`
  MODIFY `id` bigint(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT for table `item`
--
ALTER TABLE `item`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `item_history`
--
ALTER TABLE `item_history`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT for table `order`
--
ALTER TABLE `order`
  MODIFY `id` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=24;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `id` bigint(10) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `attendance`
--
ALTER TABLE `attendance`
  ADD CONSTRAINT `attendance_userID` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Constraints for table `invoice`
--
ALTER TABLE `invoice`
  ADD CONSTRAINT `userID_purchase` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);

--
-- Constraints for table `order`
--
ALTER TABLE `order`
  ADD CONSTRAINT `invoiceID_order` FOREIGN KEY (`invoice_id`) REFERENCES `invoice` (`id`),
  ADD CONSTRAINT `itemID_order` FOREIGN KEY (`item_id`) REFERENCES `item` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
