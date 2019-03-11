/*
SQLyog Ultimate v10.42 
MySQL - 5.5.5-10.1.36-MariaDB : Database - toko
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`toko` /*!40100 DEFAULT CHARACTER SET latin1 */;

USE `toko`;

/*Table structure for table `barang` */

DROP TABLE IF EXISTS `barang`;

CREATE TABLE `barang` (
  `KodeBarang` varchar(100) NOT NULL,
  `NamaBarang` varchar(250) DEFAULT NULL,
  `HargaBeli` int(11) DEFAULT NULL,
  `HargaJual` int(11) DEFAULT NULL,
  `Stock` int(11) DEFAULT NULL,
  PRIMARY KEY (`KodeBarang`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `barang` */

insert  into `barang`(`KodeBarang`,`NamaBarang`,`HargaBeli`,`HargaJual`,`Stock`) values ('BR-0001','Barang 01',1000,1500,100),('BR-0002','Barang 02',1250,1750,100),('BR-0003','Barang 03',1500,2001,100),('BR-0004','Barang 04',1750,2250,100),('BR-0005','Barang 05',2000,2500,100),('BR-0006','Barang 06',2250,2750,100),('BR-0007','Barang 07',2500,3000,100),('BR-0008','Barang 08',2750,3250,100),('BR-0009','Barang 09',3000,3500,100),('BR-0010','Barang 10',3250,3750,100),('BR-0011','Barang 11',3500,4000,100),('BR-0012','Barang 12',3750,4250,100),('BR-0013','Barang 13',4000,4500,100),('BR-0014','Barang 14',4250,4750,100),('BR-0015','Barang 15',4500,5000,100),('BR-0016','Barang 16',4750,5250,100),('BR-0017','Barang 17',5000,5500,100),('BR-0018','Barang 18',5250,5750,100),('BR-0019','Barang 19',5500,6000,100),('BR-0020','Barang 20',5750,6250,100),('BR-0021','Barang 21',6000,6500,100),('BR-0022','Barang 22',6250,6750,100),('BR-0023','Barang 23',6500,7000,100),('BR-0024','Barang 24',6750,7250,100),('BR-0025','Barang 25',7000,7500,100),('BR-0026','Barang 26',7250,7750,100),('BR-0027','Barang 27',7500,8000,100),('BR-0028','Barang 28',7750,8250,100),('BR-0029','Barang 29',8000,8500,100),('BR-0030','Barang 30',8250,8750,100),('BR-0031','Barang 31',8500,9000,100),('BR-0032','Barang 32',8750,9250,100),('BR-0033','Barang 33',9000,9500,100),('BR-0034','Barang 34',9250,9750,100),('BR-0035','Barang 35',9500,10000,100),('BR-0036','Barang 36',9750,10250,100),('BR-0037','Barang 37',10000,10500,100),('BR-0038','Barang 38',10250,10750,100),('BR-0039','Barang 39',10500,11000,100),('BR-0040','Barang 40',10750,11250,100),('BR-0041','Barang 41',11000,11500,100),('BR-0042','Barang 42',11250,11750,100),('BR-0043','Barang 43',11500,12000,100),('BR-0044','Barang 44',11750,12250,100),('BR-0045','Barang 45',12000,12500,100),('BR-0046','Barang 46',12250,12750,100),('BR-0047','Barang 47',12500,13000,100),('BR-0048','Barang 48',12750,13250,100),('BR-0049','Barang 49',13000,13500,100),('BR-0050','Barang 50',13250,13750,100);

/*Table structure for table `hutang_bayar` */

DROP TABLE IF EXISTS `hutang_bayar`;

CREATE TABLE `hutang_bayar` (
  `IdHutangBayar` int(11) NOT NULL AUTO_INCREMENT,
  `Nominal` float NOT NULL,
  `Kode` enum('HUTANG','BAYAR') NOT NULL,
  `NoKustomer` varchar(50) DEFAULT NULL,
  `Waktu` datetime DEFAULT NULL,
  `Keterangan` varchar(250) DEFAULT NULL,
  `Kasir` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`IdHutangBayar`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

/*Data for the table `hutang_bayar` */

insert  into `hutang_bayar`(`IdHutangBayar`,`Nominal`,`Kode`,`NoKustomer`,`Waktu`,`Keterangan`,`Kasir`) values (1,7500,'HUTANG','2','2019-03-10 16:22:00','Hutang belanja','Khoirul Mustofa'),(2,20000,'BAYAR','2','2019-03-10 16:22:57','Bayar hutang','Khoirul Mustofa'),(3,10000,'HUTANG','2','2019-03-10 16:43:20','55','Khoirul Mustofa'),(4,4000,'HUTANG','1','2019-03-10 16:46:18','','Khoirul Mustofa');

/*Table structure for table `jual` */

DROP TABLE IF EXISTS `jual`;

CREATE TABLE `jual` (
  `KodeJual` varchar(50) NOT NULL,
  `Waktu` datetime DEFAULT NULL,
  `NoKustomer` varchar(50) DEFAULT NULL,
  `Kasir` varchar(50) DEFAULT NULL,
  `Lokasi` varchar(50) DEFAULT NULL,
  `TotalBayar` int(11) DEFAULT NULL,
  `UangBayar` int(11) DEFAULT NULL,
  `Keterangan` text,
  PRIMARY KEY (`KodeJual`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

/*Data for the table `jual` */

insert  into `jual`(`KodeJual`,`Waktu`,`NoKustomer`,`Kasir`,`Lokasi`,`TotalBayar`,`UangBayar`,`Keterangan`) values ('PJL-100319065524','2019-03-10 18:55:24','','Khoirul Mustofa','Kasir 1',17500,70000,''),('PJL-100319065553','2019-03-10 18:55:53','','Khoirul Mustofa','Kasir 1',14502,99999,'');

/*Table structure for table `jual_detail` */

DROP TABLE IF EXISTS `jual_detail`;

CREATE TABLE `jual_detail` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `KodeJual` varchar(50) DEFAULT NULL,
  `KodeBarang` varchar(100) DEFAULT NULL,
  `Jumlah` int(11) DEFAULT NULL,
  `HargaBeli` int(11) DEFAULT NULL,
  `HargaJual` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=latin1;

/*Data for the table `jual_detail` */

insert  into `jual_detail`(`id`,`KodeJual`,`KodeBarang`,`Jumlah`,`HargaBeli`,`HargaJual`) values (1,'PJL-100319065524','BR-0001',2,1000,1500),(2,'PJL-100319065524','BR-0002',2,1250,1750),(3,'PJL-100319065524','BR-0004',1,1750,2250),(4,'PJL-100319065524','BR-0007',1,2500,3000),(5,'PJL-100319065524','BR-0008',1,2750,3250),(6,'PJL-100319065524','BR-0005',1,2000,2500),(7,'PJL-100319065553','BR-0003',2,1500,2001),(8,'PJL-100319065553','BR-0006',2,2250,2750),(9,'PJL-100319065553','BR-0008',1,2750,3250),(10,'PJL-100319065553','BR-0002',1,1250,1750);

/*Table structure for table `kustomer` */

DROP TABLE IF EXISTS `kustomer`;

CREATE TABLE `kustomer` (
  `NoKustomer` int(50) NOT NULL AUTO_INCREMENT,
  `Nama` varchar(200) DEFAULT NULL,
  `Alamat` varchar(250) DEFAULT NULL,
  `Email` varchar(200) DEFAULT NULL,
  `Hanphone` varchar(15) DEFAULT NULL,
  `Point` int(11) DEFAULT NULL,
  PRIMARY KEY (`NoKustomer`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=latin1;

/*Data for the table `kustomer` */

insert  into `kustomer`(`NoKustomer`,`Nama`,`Alamat`,`Email`,`Hanphone`,`Point`) values (1,'Customer 01','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail','081230400086',100),(2,'Customer 02','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com2','081230400086',100),(3,'Customer 03','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com3','081230400086',100),(4,'Customer 04','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com4','081230400086',100),(5,'Customer 05','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com5','081230400086',100),(6,'Customer 06','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com6','081230400086',100),(7,'Customer 07','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com7','081230400086',100),(8,'Customer 08','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com8','081230400086',100),(9,'Customer 09','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com9','081230400086',100),(10,'Customer 10','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com10','081230400086',100),(11,'Customer 11','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com11','081230400086',100),(12,'Customer 12','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com12','081230400086',100),(13,'Customer 13','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com13','081230400086',100),(14,'Customer 14','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com14','081230400086',100),(15,'Customer 15','Jl Gunem RT 05 RW 02 Kec Gunem Kab Rembang','customer@mail.com15','081230400086',100),(16,'Ttttttttttttttttt Ttttttttttttt','ttttttttt','ttttttt','33',33),(17,'Dg','dg','gd','131313',5);

/*Table structure for table `users` */

DROP TABLE IF EXISTS `users`;

CREATE TABLE `users` (
  `UserId` int(11) NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) NOT NULL,
  `Passwords` varchar(300) NOT NULL,
  `NamaLengkap` varchar(200) DEFAULT NULL,
  `Alamat` varchar(300) DEFAULT NULL,
  `NoKTP` varchar(20) DEFAULT NULL,
  `Level` enum('Kasir','Owner') DEFAULT 'Kasir',
  PRIMARY KEY (`UserId`),
  UNIQUE KEY `UniqUserName` (`UserName`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

/*Data for the table `users` */

insert  into `users`(`UserId`,`UserName`,`Passwords`,`NamaLengkap`,`Alamat`,`NoKTP`,`Level`) values (1,'admin','3d186804534370c3c817db0563f0e461','Khoirul Mustofa','Rembang','123456789','Owner'),(2,'kasir','c33367701511b4f6020ec61ded352059','Kasir 11','Rembang1','123456781','Kasir');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
