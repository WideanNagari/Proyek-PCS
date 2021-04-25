DROP TABLE ALAT_MUSIK cascade constraint purge;
DROP TABLE CUSTOMER cascade constraint purge;
DROP TABLE D_BELI cascade constraint purge;
DROP TABLE D_JUAL cascade constraint purge;
DROP TABLE H_BELI cascade constraint purge;
DROP TABLE H_JUAL cascade constraint purge;
DROP TABLE JENIS_ALAT_MUSIK cascade constraint purge;
DROP TABLE KARYAWAN cascade constraint purge;
DROP TABLE PRODUSEN cascade constraint purge;
DROP TABLE PROMO cascade constraint purge;
DROP TABLE SUPPLIER cascade constraint purge;

create table jenis_alat_musik
(
	id_jenis varchar2(5) primary key,
	nama_jenis varchar2(20)
);

create table Produsen
(
	id_produsen varchar2(4) primary key,
	nama_produsen varchar2(20)
);

create table alat_musik
(
	id_alat_musik varchar2(5) primary key,
	nama_alat_musik varchar2(40),
	id_jenis references jenis_alat_musik(id_jenis),
	id_produsen references produsen(id_produsen),
	stok number,
	harga number,
	nama_file varchar2(10)
);

create table karyawan
(
	id_karyawan varchar2(6) primary key,
	nama_karyawan varchar2(20),
	jk_karyawan varchar2(1),
	password varchar2(10),
	alamat_karyawan varchar2(20),
	noTelp_karyawan varchar2(12),
	dob_karyawan date,
	tgl_masuk date,
	status_karyawan varchar2(1)
);

create table customer
(
	id_customer varchar2(6) primary key,
	nama_customer varchar2(20),
	jk_customer varchar2(1),
	alamat_customer varchar2(50),
	noTelp_customer varchar2(12)
);

create table supplier
(
	id_supplier varchar2(5) primary key,
	nama_supplier varchar2(20),
	cp_supplier varchar2(20),
	pn_supplier varchar2(12),
	alamat_supplier varchar2(30)
);

create table promo
(
	kode_promo varchar2(15) primary key,
	nama_promo varchar2(20),
	besar_potongan number
);

create table h_beli
(
	nota_beli varchar2(10) primary key,
	tgl_beli date,
	id_karyawan references karyawan(id_karyawan),
	id_supplier references supplier(id_supplier),
	subtotal_beli number
);

create table d_beli
(
	nota_beli references h_beli(nota_beli),
	id_alat_musik references alat_musik(id_alat_musik),
	harga_beli number,
	quantity number,
	constraint pk_dbeli primary key(nota_beli,id_alat_musik)
);

create table h_jual
(
	nota_jual varchar2(10) primary key,
	tgl_jual date,
	id_customer references customer(id_customer),
	id_karyawan references karyawan(id_karyawan),
	harga_total number,
	kode_promo references promo(kode_promo),
	subtotal_jual number
);

create table d_jual
(
	nota_jual references h_jual(nota_jual),
	id_alat_musik references alat_musik(id_alat_musik),
	quantity number,
	constraint pk_djual primary key(nota_jual,id_alat_musik)
);

insert into jenis_alat_musik values('GTR01','Gitar Akustik');
insert into jenis_alat_musik values('GTR02','Gitar Klasik');
insert into jenis_alat_musik values('PNO01','Piano Akustik');
insert into jenis_alat_musik values('PNO02','Piano Digital');
insert into jenis_alat_musik values('KBD01','Keyboard');
insert into jenis_alat_musik values('BLA01','Biola');
insert into jenis_alat_musik values('DRM01','Drum Set');
insert into jenis_alat_musik values('UKL01','Ukulele');

insert into produsen values('PD01','Yamaha');
insert into produsen values('PD02','Cort');
insert into produsen values('PD03','Ibanez');
insert into produsen values('PD04','Kawai');
insert into produsen values('PD05','Ritmuller');
insert into produsen values('PD06','Roland');
insert into produsen values('PD07','Korg');
insert into produsen values('PD08','Casio');
insert into produsen values('PD09','Rolling');
insert into produsen values('PD10','Pearl');
insert into produsen values('PD11','Karl Hofner');
insert into produsen values('PD12','Scott Cao');
insert into produsen values('PD13','Giuseppi');
insert into produsen values('PD14','Cowboy');
insert into produsen values('PD15','Dreamwood');
insert into produsen values('PD16','Ludwig');
insert into produsen values('PD17','Tama');
insert into produsen values('PD18','Sonor');
insert into produsen values('PD19','Skylark');
insert into produsen values('PD20','Samuel Eastman');
insert into produsen values('PD21','Strauss');

insert into alat_musik values('RF001','Roland FP30','PNO02','PD06',3,8750000,'RF001.jpg');
insert into alat_musik values('YF001','Yamaha FX310','GTR01','PD01',10,1050000,'YF001.jpg');
insert into alat_musik values('GC001','Giuseppi Classic Violin GV-10','BLA01','PD13',9,1300000,'GC001.jpeg');
insert into alat_musik values('CA001','Cort AC120CE','GTR02','PD02',10,2090000,'CA001.jpg');
insert into alat_musik values('TI001','Tama Imperialstar Ip62h6nb-Bob 6pcs','DRM01','PD17',5,8700000,'TI001.jpg');
insert into alat_musik values('KU001','Kawai UM15 Upright','PNO01','PD04',0	,8200000,'KU001.jpg');
insert into alat_musik values('YA001','Yamaha APX600','GTR01','PD01',7,3140000,'YA001.jpg');
insert into alat_musik values('YP001','Yamaha PSS F30','KBD01','PD01',8,1150000,'YP001.jpg');
insert into alat_musik values('ES001','Eastman Series VL-80 3/4','BLA01','PD20',12,5288000,'ES001.png');
insert into alat_musik values('CC001','Casio CTX800','KBD01','PD08',9,3100000,'CC001.jpg');
insert into alat_musik values('YC001','Yamaha C315','GTR02','PD01',11,900000,'YC001.jpg');
insert into alat_musik values('RU001','Ritmuller UP 120 R1','PNO01','PD05',2,43000000,'RU001.jpg');
insert into alat_musik values('LV001','Ludwig Vistalite Zep Series 5pc Drum Set','DRM01','PD16',7,68150000,'LV001.jpg');
insert into alat_musik values('CS001','Cowboy Soprano 21','UKL01','PD14',11,300000,'CS001.jpg');
insert into alat_musik values('YP002','Yamaha P45','PNO02','PD01',5,5600000,'YP001.jpg');
insert into alat_musik values('CA002','Cort AD-810-OP','GTR01','PD02',6,1150000,'CA002.jpg');
insert into alat_musik values('CC002','Cowboy Concert 23','UKL01','PD14',6,280000,'CC002.jpg');
insert into alat_musik values('KB001','Korg B2sp','PNO02','PD07',5,7300000,'KB001.jpg');
insert into alat_musik values('KK001','Kawai K300 Upright','PNO01','PD04',1,66130000,'KK001.jpg');
insert into alat_musik values('SU001','Strauss Up 132','PNO01','PD21',1,95200000,'SU001.jpeg');
insert into alat_musik values('RJ001','Rolling JB 1026','DRM01','PD09',4,5755000,'RJ001.jpg');
insert into alat_musik values('YC002','Yamaha C80','GTR02','PD01',10,1800000,'YC002.jpg');
insert into alat_musik values('IA001','Ibanez AEG15II LG','GTR01','PD03',7,2790000,'IA001.jpg');
insert into alat_musik values('PE001','Pearl exx 725spc','DRM01','PD10',1,9775000,'PE001.jpg');
insert into alat_musik values('CT001','Cowboy Tenor 25','UKL01','PD14',5,338000,'CT001.jpg');
insert into alat_musik values('KK002','Korg Kross 2 88','PNO02','PD07',5,12500000,'KK002.jpg');
insert into alat_musik values('KH001','Karl Hofner AS 045','BLA01','PD11',7,1875000,'KH001.jpg');
insert into alat_musik values('IA002','Ibanez AEG8TNE','GTR01','PD03',3,3000000,'IA002.jpg');
insert into alat_musik values('CC003','Casio CDP130','PNO02','PD08',4,8075000,'CC003.jpg');
insert into alat_musik values('YG001','Yamaha G5','PNO01','PD01',1,95000000,'YG001.png');
insert into alat_musik values('CA003','Cort AF510E OP','GTR01','PD02',13,1760000,'CA003.png');
insert into alat_musik values('YD001','Yamaha DGX-660','PNO02','PD01',10,9100000,'YD001.jpg');
insert into alat_musik values('PD001','Pearl Decade DMPC927SP','DRM01','PD10',3,15400000,'PD001.jpg');
insert into alat_musik values('YP003','Yamaha PSR EW400','KBD01','PD01',7,5650000,'YP003.jpg');
insert into alat_musik values('HV001','Hofner 4/4 violin H5G','BLA01','PD11',10,6390000,'HV001.jpg');
insert into alat_musik values('YP004','Yamaha PSR e243','KBD01','PD01',13,1900000,'YP004.jpg');
insert into alat_musik values('SU002','Strauss Up 118','PNO01','PD21',3,45000000,'SU002.jpg');
insert into alat_musik values('RX001','Roland XPS10','KBD01','PD06',8,7100000,'RX001.jpg');
insert into alat_musik values('PE002','Pearl exx 726sp','DRM01','PD10',0,12600000,'PE002.png');
insert into alat_musik values('KH002','Karl Hofner AS 060','BLA01','PD11',12,2600000,'KH002.jpg');
insert into alat_musik values('SC001','Scott Cao 017 CE','BLA01','PD12',0,5600000,'SC001.jpg');
insert into alat_musik values('YU001','Yamaha U1','PNO01','PD01',2,38000000,'YU001.jpg');
insert into alat_musik values('YC003','Yamaha C-40 Black','GTR02','PD01',2,1580000,'YC003.jpg');
insert into alat_musik values('CC004','Cowboy CGC 100NS','GTR02','PD14',1,5675000,'CC004.jpg');
insert into alat_musik values('SM001','Skylark Mv005 4/4','BLA01','PD19',5,1800000,'SM001.jpg');
insert into alat_musik values('SM002','Skylark Mv007 3/4','BLA01','PD19',0,880000,'SM002.jpg');
insert into alat_musik values('YP005','Yamaha PSR E273','KBD01','PD01',11,2175000,'YP005.jpg');
insert into alat_musik values('DC001','Dreamwood Concerto 23 Inch','UKL01','PD15',14,420000,'DC001.jpg');
insert into alat_musik values('SE001','Sonor Essential Force S Drive 6-Piece','DRM01','PD18',3,16800000,'SE001.jpg');
insert into alat_musik values('SC002','Scott Cao 500','BLA01','PD12',6,7500000,'SC002.jpg');
insert into alat_musik values('YJ001','Yamaha JR1','GTR01','PD01',7,880000,'YJ001.jpg');
insert into alat_musik values('CA004','Casio Arranger CTK 2400','KBD01','PD08',12,2100000,'CA004.jpg');
insert into alat_musik values('SC003','Scott Cao 750','BLA01','PD12',8,12800000,'SC003.jpg');
insert into alat_musik values('DS001','Dreamwood Soprano 21 Inch','UKL01','PD15',10,220000,'DS001.jpg');

insert into karyawan values('KAR001','Gemma Elliot','F','ABC','Jl. Gurame No. 2','085801023312',to_date('17-09-1995','DD-MM-YYYY'),to_date('10-09-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR002','Wilfred Mercado','M','DEF','Jl. Perak Tmr 60','081319273829',to_date('06-06-1979','DD-MM-YYYY'),to_date('11-09-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR003','Aiden Castillo','M','GHI','Jl. Barata Jaya X 37','089712452489',to_date('01-07-1990','DD-MM-YYYY'),to_date('03-10-2019','DD-MM-YYYY'),'0');
insert into karyawan values('KAR004','Alexia Dupont','F','JKL','Jl. Kapas No. 1','085842880413',to_date('02-08-1991','DD-MM-YYYY'),to_date('15-11-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR005','Wiktoria Iles','F','MNO','Jl. Durian No. 90A','089694875530',to_date('24-03-1997','DD-MM-YYYY'),to_date('20-12-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR006','Jordan Landry','M','PQR','Jl. Pekojan No.10','089720482041',to_date('22-01-1995','DD-MM-YYYY'),to_date('22-01-2020','DD-MM-YYYY'),'1');
insert into karyawan values('KAR007','Celia Lu','F','STU','Jl. Perancis No. 9','089629472947',to_date('01-05-1988','DD-MM-YYYY'),to_date('14-02-2020','DD-MM-YYYY'),'1');
insert into karyawan values('KAR008','Ammara Legge','F','VWX','Jl. Raya Celuk 9','085811384684',to_date('05-10-1985','DD-MM-YYYY'),to_date('08-03-2020','DD-MM-YYYY'),'0');
insert into karyawan values('KAR009','Ayisha Pope','F','YZ1','Jl. Arjuna No. 28','081348372941',to_date('15-12-1990','DD-MM-YYYY'),to_date('08-04-2020','DD-MM-YYYY'),'1');
insert into karyawan values('KAR010','Paris Richmond','M','234','Jl. Banteng No. 8','089786335382',to_date('29-12-1980','DD-MM-YYYY'),to_date('17-05-2020','DD-MM-YYYY'),'1');

insert into customer values('CUS001','Koby Roy','M','Jl Ciputat Raya No. 6','081331125361');
insert into customer values('CUS002','Eman Mendoza','F','Jl Jend Gatot Subroto 60 Kav 36/80','089687883744');
insert into customer values('CUS003','Laurel Buckley','F','Jl Raden Patah No. 13','085712381733');
insert into customer values('CUS004','Anwar Hook','F','Jl Romo Kalisari No. 35','089619371931');
insert into customer values('CUS005','Lynn Mcneil','M','Jl. By Pass Dharma Giri No. 18','081330140147');
insert into customer values('CUS006','Gracie Byrne','F','Jl. Letjen Jamin Ginting III/85','085811391740');
insert into customer values('CUS007','Lillie-Mai Davie','F','Jl. Baret Biru III No. 28','085837463737');
insert into customer values('CUS008','Percy Krueger','M','Jl. Raya Cibeureum No. 30','089710183719');
insert into customer values('CUS009','Leila Bull','F','Jl. Karunrung No. 5','089692749173');
insert into customer values('CUS010','Bjorn Chen','M','Jl. Semolowaru No. 45','085819174927');
insert into customer values('CUS011','Yusuf Werner','M','Jl. Taman Tanah Abang III/20 A','089710371937');
insert into customer values('CUS012','Aniela Salter','F','Jl. Raya Darmo No. 155','089650284022');
insert into customer values('CUS013','Tomi Haynes','M','Jl. Raya Tomohon No. 113','085778458264');
insert into customer values('CUS014','Arissa Shaffer','F','Jl. Angsana I No. 38','089652746528');
insert into customer values('CUS015','Asa Hail','M','Jl. Yose Rizal No. 1A','088938264017');

insert into supplier values('SUP01','PT. Lancar Jaya','Kartika','089965746583','Jl. Majapahit No. 63');
insert into supplier values('SUP02','PT. Anugerah Abadi','Agus','085846374632','Jl. Opak 9');
insert into supplier values('SUP03','PT. Music For Life','Putra','089937452749','Jl. Ciledug Raya 47');
insert into supplier values('SUP04','PT. Besok Lulus','Adi','087844864911','Jl. Raya Seminyak 14');
insert into supplier values('SUP05','PT. Inti Musik','Fajar','085800968395','Jl. P Diponegoro 169');
insert into supplier values('SUP06','PT. Jaya Abadi','Fitri','085877364738','Jl. Jend Sudirman 71 S');
insert into supplier values('SUP07','PT. Musik Sejati','Sari','089938463888','Jl. Kedung Doro No. 66/X');
insert into supplier values('SUP08','PT. Musik Sejahtera','Wahyu','088937468699','Jl. Asia Afrika 39 – 43');
insert into supplier values('SUP09','PT. Maju Jaya','Ayu','089637746824','Jl. Otto Iskandardinata 372');
insert into supplier values('SUP10','PT. Setia Makmur','Reza','085833462940','Jl. Indraprasta No. 78');
insert into supplier values('SUP11','PT. Musik Utama','Andi','089909735482','Jl. Perintis Kemerdekaan No. 9');
insert into supplier values('SUP12','PT. Besok Libur','Rizky','089639273922','Jl. Kelapa Dua Gg Langgar 32');
insert into supplier values('SUP13','PT. Kerja Terus','Bayu','089628649374','Jl. Montong Buwuh No. 8');
insert into supplier values('SUP14','PT. Sudah Sukses','Retno','089692789297','Jl. Rungkut Mapan Tmr EA 15');
insert into supplier values('SUP15','PT. Angin Merdu','Budi','085836283674','Jl. P Jayakarta 117 Bl C/16');
insert into supplier values('SUP16','PT. Ageng Rezeki','Maya','089997877394','Jl. Tunjungan No. 26');
insert into supplier values('SUP17','PT. Pasti Sukses','Hadi','086847598364','Jl. Batu I No. 52A');

insert into promo values('CUCIGUDANG','Promo Cuci Gudang',1500000);
insert into promo values('DISKONBESAR','Diskon Besar',2000000);
insert into promo values('DISKONKECIL','Diskon Kecil',200000);
insert into promo values('PANGKASHARGA','Promo Pangkas Harga',1800000);
insert into promo values('HARGAMURAH','Promo Harga Murah',1300000);
insert into promo values('MEMBERSEJATI','Diskon Member Sejati',350000);
insert into promo values('PERMANEN','Promo Permanen',100000);
insert into promo values('ULTAH','Promo Hari Jadi',1250000);

insert into h_beli values('HBL0005101',to_date('14-09-2019','DD-MM-YYYY'),'KAR001','SUP01',8100000);
insert into h_beli values('HBL0005102',to_date('14-09-2019','DD-MM-YYYY'),'KAR002','SUP02',9100000);
insert into h_beli values('HBL0005103',to_date('15-09-2019','DD-MM-YYYY'),'KAR002','SUP03',8500000);
insert into h_beli values('HBL0005104',to_date('20-09-2019','DD-MM-YYYY'),'KAR001','SUP04',5000000);
insert into h_beli values('HBL0005105',to_date('03-10-2019','DD-MM-YYYY'),'KAR003','SUP05',8500000);
insert into h_beli values('HBL0005106',to_date('02-11-2019','DD-MM-YYYY'),'KAR002','SUP01',8000000);
insert into h_beli values('HBL0005107',to_date('15-11-2019','DD-MM-YYYY'),'KAR002','SUP06',17350000);
insert into h_beli values('HBL0005108',to_date('05-12-2019','DD-MM-YYYY'),'KAR003','SUP07',3000000);
insert into h_beli values('HBL0005109',to_date('07-01-2020','DD-MM-YYYY'),'KAR001','SUP08',13000000);
insert into h_beli values('HBL0005110',to_date('20-01-2020','DD-MM-YYYY'),'KAR003','SUP09',5575000);
insert into h_beli values('HBL0005111',to_date('03-02-2020','DD-MM-YYYY'),'KAR004','SUP10',40000000);
insert into h_beli values('HBL0005112',to_date('01-03-2020','DD-MM-YYYY'),'KAR002','SUP07',6000000);
insert into h_beli values('HBL0005113',to_date('17-03-2020','DD-MM-YYYY'),'KAR002','SUP11',19550000);
insert into h_beli values('HBL0005114',to_date('29-03-2020','DD-MM-YYYY'),'KAR001','SUP12',2000000);
insert into h_beli values('HBL0005115',to_date('15-04-2020','DD-MM-YYYY'),'KAR005','SUP13',21600000);
insert into h_beli values('HBL0005116',to_date('13-05-2020','DD-MM-YYYY'),'KAR001','SUP14',6600000);
insert into h_beli values('HBL0005117',to_date('10-06-2020','DD-MM-YYYY'),'KAR005','SUP15',13450000);
insert into h_beli values('HBL0005118',to_date('30-06-2020','DD-MM-YYYY'),'KAR001','SUP16',4725000);
insert into h_beli values('HBL0005119',to_date('20-07-2020','DD-MM-YYYY'),'KAR002','SUP05',3200000);
insert into h_beli values('HBL0005120',to_date('15-08-2020','DD-MM-YYYY'),'KAR002','SUP17',12400000);
insert into h_beli values('HBL0005121',to_date('15-08-2020','DD-MM-YYYY'),'KAR002','SUP17',12400000);

insert into d_beli values('HBL0005101','YA001',2700000,3);
insert into d_beli values('HBL0005101','SM002',720000,1);
insert into d_beli values('HBL0005102','YP004',1300000,2);
insert into d_beli values('HBL0005102','RX001',6500000,1);
insert into d_beli values('HBL0005103','CA004',2000000,2);
insert into d_beli values('HBL0005103','SM001',1500000,3);
insert into d_beli values('HBL0005103','SM002',700000,2);
insert into d_beli values('HBL0005104','IA002',2500000,2);
insert into d_beli values('HBL0005104','CA001',1800000,4);
insert into d_beli values('HBL0005105','TI001',8500000,1);
insert into d_beli values('HBL0005105','CC001',2800000,3);
insert into d_beli values('HBL0005106','RF001',8000000,1);
insert into d_beli values('HBL0005106','RU001',37000000,2);
insert into d_beli values('HBL0005107','YP003',5200000,1);
insert into d_beli values('HBL0005107','PE001',9350000,1);
insert into d_beli values('HBL0005107','YC001',700000,4);
insert into d_beli values('HBL0005107','YD001',8500000,2);
insert into d_beli values('HBL0005108','YJ001',600000,5);
insert into d_beli values('HBL0005108','CA001',1850000,1);
insert into d_beli values('HBL0005109','YP002',13000000,1);
insert into d_beli values('HBL0005109','PD001',13000000,1);
insert into d_beli values('HBL0005110','DS001',175000,5);
insert into d_beli values('HBL0005110','ES001',4700000,1);
insert into d_beli values('HBL0005111','IA001',2500000,2);
insert into d_beli values('HBL0005111','YU001',35000000,1);
insert into d_beli values('HBL0005112','YA001',3000000,2);
insert into d_beli values('HBL0005112','YF001',900000,2);
insert into d_beli values('HBL0005112','SU001',90000000,1);
insert into d_beli values('HBL0005113','YP001',850000,3);
insert into d_beli values('HBL0005113','YD001',8500000,2);
insert into d_beli values('HBL0005113','KK001',64000000,1);
insert into d_beli values('HBL0005114','CC004',500000,4);
insert into d_beli values('HBL0005114','KU001',37000000,2);
insert into d_beli values('HBL0005115','CA004',1800000,3);
insert into d_beli values('HBL0005115','SE001',16200000,1);
insert into d_beli values('HBL0005116','KH002',2200000,3);
insert into d_beli values('HBL0005116','SM001',1500000,5);
insert into d_beli values('HBL0005116','YC002',1600000,1);
insert into d_beli values('HBL0005117','CC002',250000,5);
insert into d_beli values('HBL0005117','HV001',6100000,2);
insert into d_beli values('HBL0005117','YF001',900000,2);
insert into d_beli values('HBL0005117','GC001',1000000,3);
insert into d_beli values('HBL0005117','YC002',1600000,2);
insert into d_beli values('HBL0005118','KH001',1575000,3);
insert into d_beli values('HBL0005118','DS001',180000,2);
insert into d_beli values('HBL0005119','YP003',1600000,2);
insert into d_beli values('HBL0005119','YP004',1700000,1);
insert into d_beli values('HBL0005120','DC001',350000,4);
insert into d_beli values('HBL0005120','RJ001',5500000,2);

insert into h_jual values('HJL0005501',to_date('09-03-2020','DD-MM-YYYY'),'CUS001','KAR001',9800000,'',9800000);
insert into h_jual values('HJL0005502',to_date('15-03-2020','DD-MM-YYYY'),'CUS002','KAR002',25676000,'DISKONBESAR',23676000);
insert into h_jual values('HJL0005503',to_date('24-03-2020','DD-MM-YYYY'),'CUS003','KAR001',5475000,'DISKONBESAR',3475000);
insert into h_jual values('HJL0005504',to_date('10-04-2020','DD-MM-YYYY'),'CUS004','KAR003',17800000,'DISKONKECIL',1580000);
insert into h_jual values('HJL0005505',to_date('15-04-2020','DD-MM-YYYY'),'CUS005','KAR002',12780000,'',12780000);
insert into h_jual values('HJL0005506',to_date('28-04-2020','DD-MM-YYYY'),'CUS006','KAR003',3415000,'',3415000);
insert into h_jual values('HJL0005507',to_date('02-05-2020','DD-MM-YYYY'),'CUS007','KAR001',75010000,'PANGKASHARGA',73210000);
insert into h_jual values('HJL0005508',to_date('07-05-2020','DD-MM-YYYY'),'CUS001','KAR004',5900000,'CUCIGUDANG',4400000);
insert into h_jual values('HJL0005509',to_date('18-05-2020','DD-MM-YYYY'),'CUS008','KAR004',25150000,'PANGKASHARGA',23350000);
insert into h_jual values('HJL0005510',to_date('08-06-2020','DD-MM-YYYY'),'CUS009','KAR005',33531000,'CUCIGUDANG',32031000);
insert into h_jual values('HJL0005511',to_date('17-06-2020','DD-MM-YYYY'),'CUS005','KAR005',21000000,'',21000000);
insert into h_jual values('HJL0005512',to_date('29-06-2020','DD-MM-YYYY'),'CUS008','KAR004',37700000,'HARGAMURAH',26400000);
insert into h_jual values('HJL0005513',to_date('06-07-2020','DD-MM-YYYY'),'CUS010','KAR006',131860000,'HARGAMURAH',130560000);
insert into h_jual values('HJL0005514',to_date('21-07-2020','DD-MM-YYYY'),'CUS011','KAR005',7400000,'DISKONKECIL',7200000);
insert into h_jual values('HJL0005515',to_date('22-07-2020','DD-MM-YYYY'),'CUS012','KAR007',106200000,'',1006200000);
insert into h_jual values('HJL0005516',to_date('08-08-2020','DD-MM-YYYY'),'CUS013','KAR008',50650000,'PANGKASHARGA',48850000);
insert into h_jual values('HJL0005517',to_date('09-08-2020','DD-MM-YYYY'),'CUS002','KAR006',8490000,'CUCIGUDANG',6990000);
insert into h_jual values('HJL0005518',to_date('04-09-2020','DD-MM-YYYY'),'CUS014','KAR006',10980000,'DISKONBESAR',8980000);
insert into h_jual values('HJL0005519',to_date('25-09-2020','DD-MM-YYYY'),'CUS015','KAR009',640000,'',640000);
insert into h_jual values('HJL0005520',to_date('29-09-2020','DD-MM-YYYY'),'CUS013','KAR007',154150000,'DISKONKECIL',153950000);

insert into d_jual values('HJL0005501','RF001',1);
insert into d_jual values('HJL0005501','YF001',1);
insert into d_jual values('HJL0005502','CT001',2);
insert into d_jual values('HJL0005502','KK002',2);
insert into d_jual values('HJL0005503','KH001',1);
insert into d_jual values('HJL0005503','SM001',2);
insert into d_jual values('HJL0005504','YC001',1);
insert into d_jual values('HJL0005504','SM002',1);
insert into d_jual values('HJL0005505','SC001',1);
insert into d_jual values('HJL0005505','CA004',3);
insert into d_jual values('HJL0005505','YJ001',1);
insert into d_jual values('HJL0005506','PE001',2);
insert into d_jual values('HJL0005506','KB001',2);
insert into d_jual values('HJL0005507','KH002',1);
insert into d_jual values('HJL0005507','KK001',1);
insert into d_jual values('HJL0005507','YA001',2);
insert into d_jual values('HJL0005508','YP002',1);
insert into d_jual values('HJL0005508','CS001',1);
insert into d_jual values('HJL0005509','YP002',1);
insert into d_jual values('HJL0005509','PE001',2);
insert into d_jual values('HJL0005510','PD001',1);
insert into d_jual values('HJL0005510','ES001',2);
insert into d_jual values('HJL0005510','RJ001',1);
insert into d_jual values('HJL0005510','YC002',1);
insert into d_jual values('HJL0005511','KU001',1);
insert into d_jual values('HJL0005511','SC003',1);
insert into d_jual values('HJL0005512','PE002',2);
insert into d_jual values('HJL0005512','KK002',1);
insert into d_jual values('HJL0005513','YC003',3);
insert into d_jual values('HJL0005513','YP002',2);
insert into d_jual values('HJL0005513','CA003',2);
insert into d_jual values('HJL0005513','TI001',2);
insert into d_jual values('HJL0005513','YG001',1);
insert into d_jual values('HJL0005514','SM001',2);
insert into d_jual values('HJL0005514','YP004',2);
insert into d_jual values('HJL0005515','YG001',1);
insert into d_jual values('HJL0005515','YP002',2);
insert into d_jual values('HJL0005516','SU002',1);
insert into d_jual values('HJL0005516','YP003',1);
insert into d_jual values('HJL0005517','HV001',1);
insert into d_jual values('HJL0005517','YF001',2);
insert into d_jual values('HJL0005518','GC001',3);
insert into d_jual values('HJL0005518','SM002',1);
insert into d_jual values('HJL0005518','CC001',2);
insert into d_jual values('HJL0005519','DS001',1);
insert into d_jual values('HJL0005519','DC001',1);
insert into d_jual values('HJL0005520','RU001',2);
insert into d_jual values('HJL0005520','LV001',1);

commit;

select h.nota_jual as "Nota", sum(d.quantity*a.harga) as "total", p.besar_potongan as "diskon", p.nama_promo as "promo", 
sum(d.quantity*a.harga)-p.besar_potongan as "subtotal"
from h_jual h
inner join d_jual d on h.nota_jual = d.nota_jual
inner join alat_musik a on d.id_alat_musik = a.id_alat_musik
left join promo p on h.kode_promo = p.kode_promo
group by h.nota_jual, p.besar_potongan, p.nama_promo
order by 1;

select h.nota_jual as "Nota", d.quantity, a.harga
from h_jual h, d_jual d, alat_musik a
where h.nota_jual = d.nota_jual and d.id_alat_musik = a.id_alat_musik
order by 1;

select a.id_alat_musik as "ID", a.nama_alat_musik as "Nama Alat Musik", j.nama_jenis as "Jenis",
p.nama_produsen as "Produsen", a.stok as "Stok", a.harga as "Harga"
from alat_musik a, jenis_alat_musik j, produsen p
where a.id_jenis = j.id_jenis and a.id_produsen = p.id_produsen
order by 1;