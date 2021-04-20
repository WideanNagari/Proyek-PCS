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
	harga number
);

create table karyawan
(
	id_karyawan varchar2(6) primary key,
	nama_karyawan varchar2(20),
	jk_karyawan varchar2(1),
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

insert into alat_musik values('RF001','Roland FP30','PNO02','PD06',3,8750000);
insert into alat_musik values('YF001','Yamaha FX310','GTR01','PD01',10,1050000);
insert into alat_musik values('GC001','Giuseppi Classic Violin GV-10','BLA01','PD13',9,1300000);
insert into alat_musik values('CA001','Cort AC120CE','GTR02','PD02',10,2090000);
insert into alat_musik values('TI001','Tama Imperialstar Ip62h6nb-Bob 6pcs','DRM01','PD17',5,8700000);
insert into alat_musik values('KU001','Kawai UM15 Upright','PNO01','PD04',0	,8200000);
insert into alat_musik values('YA001','Yamaha APX600','GTR01','PD01',7,3140000);
insert into alat_musik values('YP001','Yamaha PSS F30','KBD01','PD01',8,1150000);
insert into alat_musik values('ES001','Eastman Series VL-80 3/4','BLA01','PD20',12,5288000);
insert into alat_musik values('CC001','Casio CTX800','KBD01','PD08',9,3100000);
insert into alat_musik values('YC001','Yamaha C315','GTR02','PD01',11,900000);
insert into alat_musik values('RU001','Ritmuller UP 120 R1','PNO01','PD05',2,43000000);
insert into alat_musik values('LV001','Ludwig Vistalite Zep Series 5pc Drum Set','DRM01','PD16',7,68150000);
insert into alat_musik values('CS001','Cowboy Soprano 21','UKL01','PD14',11,300000);
insert into alat_musik values('YP002','Yamaha P45','PNO02','PD01',5,5600000);
insert into alat_musik values('CA002','Cort AD-810-OP','GTR01','PD02',6,1150000);
insert into alat_musik values('CC002','Cowboy Concert 23','UKL01','PD14',6,280000);
insert into alat_musik values('KB001','Korg B2sp','PNO02','PD07',5,7300000);
insert into alat_musik values('KK001','Kawai K300 Upright','PNO01','PD04',1,66130000);
insert into alat_musik values('SU001','Strauss Up 132','PNO01','PD21',1,95200000);
insert into alat_musik values('RJ001','Rolling JB 1026','DRM01','PD09',4,5755000);
insert into alat_musik values('YC002','Yamaha C80','GTR02','PD01',10,1800000);
insert into alat_musik values('IA001','Ibanez AEG15II LG','GTR01','PD03',7,2790000);
insert into alat_musik values('PE001','Pearl exx 725spc','DRM01','PD10',1,9775000);
insert into alat_musik values('CT001','Cowboy Tenor 25','UKL01','PD14',5,338000);
insert into alat_musik values('KK002','Korg Kross 2 88','PNO02','PD07',5,12500000);
insert into alat_musik values('KH001','Karl Hofner AS 045','BLA01','PD11',7,1875000);
insert into alat_musik values('IA002','Ibanez AEG8TNE','GTR01','PD03',3,3000000);
insert into alat_musik values('CC003','Casio CDP130','PNO02','PD08',4,8075000);
insert into alat_musik values('YG001','Yamaha G5','PNO01','PD01',1,95000000);
insert into alat_musik values('CA003','Cort AF510E OP','GTR01','PD02',13,1760000);
insert into alat_musik values('YD001','Yamaha DGX-660','PNO02','PD01',10,9100000);
insert into alat_musik values('PD001','Pearl Decade DMPC927SP','DRM01','PD10',3,15400000);
insert into alat_musik values('YP003','Yamaha PSR EW400','KBD01','PD01',7,5650000);
insert into alat_musik values('HV001','Hofner 4/4 violin H5G','BLA01','PD11',10,6390000);
insert into alat_musik values('YP004','Yamaha PSR e243','KBD01','PD01',13,1900000);
insert into alat_musik values('SU002','Strauss Up 118','PNO01','PD21',3,45000000);
insert into alat_musik values('RX001','Roland XPS10','KBD01','PD06',8,7100000);
insert into alat_musik values('PE002','Pearl exx 726sp','DRM01','PD10',0,12600000);
insert into alat_musik values('KH002','Karl Hofner AS 060','BLA01','PD11',12,2600000);
insert into alat_musik values('SC001','Scott Cao 017 CE','BLA01','PD12',0,5600000);
insert into alat_musik values('YU001','Yamaha U1','PNO01','PD01',2,38000000);
insert into alat_musik values('YC003','Yamaha C-40 Black','GTR02','PD01',2,1580000);
insert into alat_musik values('CC004','Cowboy CGC 100NS','GTR02','PD14',1,5675000);
insert into alat_musik values('SM001','Skylark Mv005 4/4','BLA01','PD19',5,1800000);
insert into alat_musik values('SM002','Skylark Mv007 3/4','BLA01','PD19',0,880000);
insert into alat_musik values('YP005','Yamaha PSR E273','KBD01','PD01',11,2175000);
insert into alat_musik values('DC001','Dreamwood Concerto 23 Inch','UKL01','PD15',14,420000);
insert into alat_musik values('SE001','Sonor Essential Force S Drive 6-Piece','DRM01','PD18',3,16800000);
insert into alat_musik values('SC002','Scott Cao 500','BLA01','PD12',6,7500000);
insert into alat_musik values('YJ001','Yamaha JR1','GTR01','PD01',7,880000);
insert into alat_musik values('CA004','Casio Arranger CTK 2400','KBD01','PD08',12,2100000);
insert into alat_musik values('SC003','Scott Cao 750','BLA01','PD12',8,12800000);
insert into alat_musik values('DS001','Dreamwood Soprano 21 Inch','UKL01','PD15',10,220000);

insert into karyawan values('KAR001','Gemma Elliot','F','Jl. Gurame No. 2','085801023312',to_date('17-09-1995','DD-MM-YYYY'),to_date('10-09-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR002','Wilfred Mercado','M','Jl. Perak Tmr 60','081319273829',to_date('06-06-1979','DD-MM-YYYY'),to_date('11-09-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR003','Aiden Castillo','M','Jl. Barata Jaya X 37','089712452489',to_date('01-07-1990','DD-MM-YYYY'),to_date('03-10-2019','DD-MM-YYYY'),'0');
insert into karyawan values('KAR004','Alexia Dupont','F','Jl. Kapas No. 1','085842880413',to_date('02-08-1991','DD-MM-YYYY'),to_date('15-11-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR005','Wiktoria Iles','F','Jl. Durian No. 90A','089694875530',to_date('24-03-1997','DD-MM-YYYY'),to_date('20-12-2019','DD-MM-YYYY'),'1');
insert into karyawan values('KAR006','Jordan Landry','M','Jl. Pekojan No.10','089720482041',to_date('22-01-1995','DD-MM-YYYY'),to_date('22-01-2020','DD-MM-YYYY'),'1');
insert into karyawan values('KAR007','Celia Lu','F','Jl. Perancis No. 9','089629472947',to_date('01-05-1988','DD-MM-YYYY'),to_date('14-02-2020','DD-MM-YYYY'),'1');
insert into karyawan values('KAR008','Ammara Legge','F','Jl. Raya Celuk 9','085811384684',to_date('05-10-1985','DD-MM-YYYY'),to_date('08-03-2020','DD-MM-YYYY'),'0');
insert into karyawan values('KAR009','Ayisha Pope','F','Jl. Arjuna No. 28','081348372941',to_date('15-12-1990','DD-MM-YYYY'),to_date('08-04-2020','DD-MM-YYYY'),'1');
insert into karyawan values('KAR010','Paris Richmond','M','Jl. Banteng No. 8','089786335382',to_date('29-12-1980','DD-MM-YYYY'),to_date('17-05-2020','DD-MM-YYYY'),'1');

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
insert into customer values('CUS016','Marshall Francis','M','Jl. Mulawarman No. 34','089610719371');
insert into customer values('CUS017','Aaminah Cisneros','F','Jl. Surabaya No. 58','081308392764');
insert into customer values('CUS018','Simrah Gough','F','Jl. Mayjen DI Panjaitan No. 59','089602792749');
insert into customer values('CUS019','Mario Patrick','M','Jl. Raya Hankam No. 2','089736284624');
insert into customer values('CUS020','Conan Atkins','M','Jl. Kelapa Dua Gg Langgar No. 32','089748265403');
insert into customer values('CUS021','Noor Morrow','F','Jl. Pemuda No. 1F','088933481639');
insert into customer values('CUS022','Keiron Oneill','M','Jl. Utan Kayu Raya No. 55-A','089755917461');
insert into customer values('CUS023','Gurveer Joyce','F','Jl. Dr Setiabudi No. 184','085811394618');
insert into customer values('CUS024','Kamil Miller','M','Jl. Jembatan Merah No. 15','088903719374');
insert into customer values('CUS025','Nico Mejia','M','Jl. Lapangan Merah No. 59','085847962946');

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
insert into promo values('PERMANEN','Promo permanen',100000);

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

insert into d_beli values('HBL0005101','YA001',2700000,3);
insert into d_beli values('HBL0005102','YP004',1300000,2);
insert into d_beli values('HBL0005102','RX001',6500000,1);
insert into d_beli values('HBL0005103','CA004',2000000,2);
insert into d_beli values('HBL0005103','SM001',1500000,3);
insert into d_beli values('HBL0005104','IA002',2500000,2);
insert into d_beli values('HBL0005105','TI001',8500000,1);
insert into d_beli values('HBL0005106','RF001',8000000,1);
insert into d_beli values('HBL0005107','YP003',5200000,1);
insert into d_beli values('HBL0005107','PE001',9350000,1);
insert into d_beli values('HBL0005107','YC001',700000,4);
insert into d_beli values('HBL0005108','YJ001',600000,5);
insert into d_beli values('HBL0005109','PD001',13000000,1);
insert into d_beli values('HBL0005110','DS001',175000,5);
insert into d_beli values('HBL0005110','ES001',4700000,1);
insert into d_beli values('HBL0005111','IA001',2500000,2);
insert into d_beli values('HBL0005111','YU001',35000000,1);
insert into d_beli values('HBL0005112','YA001',3000000,2);
insert into d_beli values('HBL0005113','YP001',850000,3);
insert into d_beli values('HBL0005113','YD001',8500000,2);
insert into d_beli values('HBL0005114','CC004',500000,4);
insert into d_beli values('HBL0005115','CA004',1800000,3);
insert into d_beli values('HBL0005115','SE001',16200000,1);
insert into d_beli values('HBL0005116','KH002',2200000,3);
insert into d_beli values('HBL0005117','CC002',250000,5);
insert into d_beli values('HBL0005117','HV001',6100000,2);
insert into d_beli values('HBL0005118','KH001',1575000,3);
insert into d_beli values('HBL0005119','YP003',1600000,2);
insert into d_beli values('HBL0005120','DC001',350000,4);
insert into d_beli values('HBL0005120','RJ001',5500000,2);

insert into h_jual values('HJL0005501',to_date('14-09-2019','DD-MM-YYYY'),'CUS001','KAR001',9800000,'DISKONBESAR',7800000);
insert into h_jual values('HJL0005502',to_date('17-09-2019','DD-MM-YYYY'),'CUS002','KAR002',676000,'',676000);
insert into h_jual values('HJL0005503',to_date('25-09-2019','DD-MM-YYYY'),'CUS003','KAR001',25000000,'DISKONBESAR',23000000);
insert into h_jual values('HJL0005504',to_date('05-10-2019','DD-MM-YYYY'),'CUS004','KAR003',5475000,'DISKONKECIL',5275000);
insert into h_jual values('HJL0005505',to_date('15-10-2019','DD-MM-YYYY'),'CUS005','KAR002',7380000,'',7380000);
insert into h_jual values('HJL0005506',to_date('01-11-2019','DD-MM-YYYY'),'CUS006','KAR003',6300000,'',6300000);
insert into h_jual values('HJL0005507',to_date('10-11-2019','DD-MM-YYYY'),'CUS007','KAR001',20430000,'PANGKASHARGA',18630000);
insert into h_jual values('HJL0005508',to_date('27-11-2019','DD-MM-YYYY'),'CUS001','KAR004',17200000,'CUCIGUDANG',15700000);
insert into h_jual values('HJL0005509',to_date('05-12-2019','DD-MM-YYYY'),'CUS008','KAR004',66130000,'PANGKASHARGA',64330000);
insert into h_jual values('HJL0005510',to_date('22-12-2019','DD-MM-YYYY'),'CUS009','KAR005',11880000,'CUCIGUDANG',10380000);
insert into h_jual values('HJL0005511',to_date('03-01-2020','DD-MM-YYYY'),'CUS005','KAR005',300000,'',300000);
insert into h_jual values('HJL0005512',to_date('16-01-2020','DD-MM-YYYY'),'CUS008','KAR004',25200000,'HARGAMURAH',23900000);
insert into h_jual values('HJL0005513',to_date('23-01-2020','DD-MM-YYYY'),'CUS010','KAR006',25976000,'HARGAMURAH',24676000);
insert into h_jual values('HJL0005514',to_date('05-02-2020','DD-MM-YYYY'),'CUS011','KAR005',5755000,'DISKONKECIL',5555000);
insert into h_jual values('HJL0005515',to_date('14-02-2020','DD-MM-YYYY'),'CUS012','KAR007',1800000,'',1800000);
insert into h_jual values('HJL0005516',to_date('09-03-2020','DD-MM-YYYY'),'CUS013','KAR008',38200000,'PANGKASHARGA',36400000);
insert into h_jual values('HJL0005517',to_date('15-03-2020','DD-MM-YYYY'),'CUS002','KAR006',12800000,'CUCIGUDANG',11300000);
insert into h_jual values('HJL0005518',to_date('24-03-2020','DD-MM-YYYY'),'CUS014','KAR006',42440000,'DISKONBESAR',40440000);
insert into h_jual values('HJL0005519',to_date('10-04-2020','DD-MM-YYYY'),'CUS015','KAR009',11200000,'PANGKASHARGA',9400000);
insert into h_jual values('HJL0005520',to_date('15-04-2020','DD-MM-YYYY'),'CUS016','KAR007',3520000,'DISKONKECIL',3320000);
insert into h_jual values('HJL0005521',to_date('28-04-2020','DD-MM-YYYY'),'CUS017','KAR008',110000000,'DISKONBESAR',108000000);
insert into h_jual values('HJL0005522',to_date('02-05-2020','DD-MM-YYYY'),'CUS018','KAR007',7400000,'',7400000);
insert into h_jual values('HJL0005523',to_date('07-05-2020','DD-MM-YYYY'),'CUS011','KAR009',95000000,'DISKONBESAR',93000000);
insert into h_jual values('HJL0005524',to_date('18-05-2020','DD-MM-YYYY'),'CUS019','KAR010',5600000,'',5600000);
insert into h_jual values('HJL0005525',to_date('08-06-2020','DD-MM-YYYY'),'CUS020','KAR010',45000000,'DISKONBESAR',43000000);
insert into h_jual values('HJL0005526',to_date('17-06-2020','DD-MM-YYYY'),'CUS021','KAR009',5650000,'',5650000);
insert into h_jual values('HJL0005527',to_date('29-06-2020','DD-MM-YYYY'),'CUS015','KAR010',6390000,'',6390000);
insert into h_jual values('HJL0005528',to_date('06-07-2020','DD-MM-YYYY'),'CUS022','KAR008',2100000,'',2100000);
insert into h_jual values('HJL0005529',to_date('21-07-2020','DD-MM-YYYY'),'CUS013','KAR009',4780000,'',4780000);
insert into h_jual values('HJL0005530',to_date('22-07-2020','DD-MM-YYYY'),'CUS018','KAR006',6200000,'',6200000);
insert into h_jual values('HJL0005531',to_date('08-08-2020','DD-MM-YYYY'),'CUS023','KAR004',640000,'',640000);
insert into h_jual values('HJL0005532',to_date('09-08-2020','DD-MM-YYYY'),'CUS024','KAR005',86000000,'CUCIGUDANG',84500000);
insert into h_jual values('HJL0005533',to_date('04-09-2020','DD-MM-YYYY'),'CUS017','KAR008',68150000,'CUCIGUDANG',66650000);
insert into h_jual values('HJL0005534',to_date('25-09-2020','DD-MM-YYYY'),'CUS020','KAR009',38000000,'HARGAMURAH',36700000);
insert into h_jual values('HJL0005535',to_date('29-09-2020','DD-MM-YYYY'),'CUS025','KAR006',43000000,'PANGKASHARGA',41200000);

insert into d_jual values('HJL0005501','RF001',1);
insert into d_jual values('HJL0005501','YF001',1);
insert into d_jual values('HJL0005502','CT001',2);
insert into d_jual values('HJL0005503','KK002',2);
insert into d_jual values('HJL0005504','KH001',1);
insert into d_jual values('HJL0005504','SM001',2);
insert into d_jual values('HJL0005505','YC001',1);
insert into d_jual values('HJL0005505','SM002',1);
insert into d_jual values('HJL0005505','SC001',1);
insert into d_jual values('HJL0005506','CA004',3);
insert into d_jual values('HJL0005507','YJ001',1);
insert into d_jual values('HJL0005507','PE001',2);
insert into d_jual values('HJL0005508','KB001',2);
insert into d_jual values('HJL0005508','KH002',1);
insert into d_jual values('HJL0005509','KK001',1);
insert into d_jual values('HJL0005510','YA001',2);
insert into d_jual values('HJL0005510','YP002',1);
insert into d_jual values('HJL0005511','CS001',1);
insert into d_jual values('HJL0005512','YP002',1);
insert into d_jual values('HJL0005512','PE001',2);
insert into d_jual values('HJL0005513','PD001',1);
insert into d_jual values('HJL0005513','ES001',2);
insert into d_jual values('HJL0005514','RJ001',1);
insert into d_jual values('HJL0005515','YC002',1);
insert into d_jual values('HJL0005516','KU001',1);
insert into d_jual values('HJL0005517','SC003',1);
insert into d_jual values('HJL0005518','PE002',2);
insert into d_jual values('HJL0005518','KK002',1);
insert into d_jual values('HJL0005518','YC003',3);
insert into d_jual values('HJL0005519','YP002',2);
insert into d_jual values('HJL0005520','CA003',2);
insert into d_jual values('HJL0005521','TI001',2);
insert into d_jual values('HJL0005521','YG001',1);
insert into d_jual values('HJL0005522','SM001',2);
insert into d_jual values('HJL0005522','YP004',2);
insert into d_jual values('HJL0005523','YG001',1);
insert into d_jual values('HJL0005524','YP002',2);
insert into d_jual values('HJL0005525','SU002',1);
insert into d_jual values('HJL0005526','YP003',1);
insert into d_jual values('HJL0005527','HV001',1);
insert into d_jual values('HJL0005528','YF001',2);
insert into d_jual values('HJL0005529','GC001',3);
insert into d_jual values('HJL0005529','SM002',1);
insert into d_jual values('HJL0005530','CC001',2);
insert into d_jual values('HJL0005531','DS001',1);
insert into d_jual values('HJL0005531','DC001',1);
insert into d_jual values('HJL0005532','RU001',2);
insert into d_jual values('HJL0005533','LV001',1);
insert into d_jual values('HJL0005534','YU001',1);
insert into d_jual values('HJL0005535','RU001',1);

commit;