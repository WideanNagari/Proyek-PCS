--function
--autogen id produsen
create or replace function autogenProdusen
return varchar2
is
	id number(10);
	id_produsen varchar2(4);
begin
	select max(substr(id_produsen,3,2))+1 into id from produsen;
	id_produsen := 'PD' || lpad(id,2,'0');
	return id_produsen;
end;
/

--autogen id karyawan
create or replace function autogenKaryawan
return varchar2
is
	id number(10);
	id_karyawan varchar2(6);
begin
	select max(substr(id_karyawan,4,3))+1 into id from karyawan;
	id_karyawan := 'KAR' || lpad(id,3,'0');
	return id_karyawan;
end;
/

--autogen id customer
create or replace function autogenCustomer
return varchar2
is
	id number(10);
	id_customer varchar2(6);
begin
	select max(substr(id_customer,4,3))+1 into id from customer;
	id_customer := 'CUS' || lpad(id,3,'0');
	return id_customer;
end;
/

--autogen id supplier
create or replace function autogenSupplier
return varchar2
is
	id number(10);
	id_supplier varchar2(5);
begin
	select max(substr(id_supplier,4,2))+1 into id from supplier;
	id_supplier := 'SUP' || lpad(id,2,'0');
	return id_supplier;
end;
/

--autogen id member
create or replace function autogenMember
return varchar2
is
	id number(10);
	id_member varchar2(4);
begin
	select max(substr(id_member,4,2))+1 into id from member;
	id_member := 'M' || lpad(id,3,'0');
	return id_member;
end;
/

--trigger

-- insert
--untuk autogen id penjualan member
CREATE OR REPLACE TRIGGER TR_P_MEMBER
BEFORE INSERT ON PENJUALAN_MEMBER
FOR EACH ROW
DECLARE
	id number(2);
	nj_member varchar2(10);
	tgl varchar2(6);
BEGIN
	select to_char(sysdate,'DDMMYY') into tgl from dual;
	nj_member := 'MB'||tgl;
	select NVL(max(substr(nota_jual_member,9,2))+1,'1') into id from penjualan_member
	where substr(nota_jual_member,1,8) = nj_member;
	nj_member := nj_member||lpad(id,2,'0');
	:new.nota_jual_member := nj_member;
	:new.status := 1;
	:new.tgl_jual := sysdate;
	update penjualan_member set status = 0 where id_customer = :new.id_customer;
END;
/

--untuk autogen id H_Beli
CREATE OR REPLACE TRIGGER TR_H_Beli
BEFORE INSERT ON H_Beli
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_beli,4,7)))+1 into id from h_beli;
	:new.nota_beli := 'HBL'||lpad(id,7,'0');
	:new.tgl_beli := sysdate;
END;
/
SHOW ERR;

--untuk autogen id D_Beli
CREATE OR REPLACE TRIGGER TR_D_Beli
BEFORE INSERT ON D_Beli
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_beli,4,7))) into id from h_beli;
	:new.nota_beli := 'HBL'||lpad(id,7,'0');
	update alat_musik set stok = stok + :new.quantity where id_alat_musik = :new.id_alat_musik;
END;
/
SHOW ERR;

--untuk autogen id D_Beli_Aksesoris
CREATE OR REPLACE TRIGGER TR_D_Beli_Aksesoris
BEFORE INSERT ON D_Beli_Aksesoris
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_beli,4,7))) into id from h_beli;
	:new.nota_beli := 'HBL'||lpad(id,7,'0');
	
	update aksesoris set stok = stok + :new.quantity where id_aksesoris = :new.id_aksesoris;
END;
/
SHOW ERR;

--untuk autogen id H_Jual
CREATE OR REPLACE TRIGGER TR_H_Jual
BEFORE INSERT ON H_Jual
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_jual,4,7)))+1 into id from h_jual;
	:new.nota_jual := 'HJL'||lpad(id,7,'0');
	
	:new.tgl_jual := sysdate;
END;
/
SHOW ERR;

--untuk autogen id D_Jual
CREATE OR REPLACE TRIGGER TR_D_Jual
BEFORE INSERT ON D_Jual
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_jual,4,7))) into id from h_jual;
	:new.nota_jual := 'HJL'||lpad(id,7,'0');
	
	update alat_musik set stok = stok - :new.quantity where id_alat_musik = :new.id_alat_musik;
END;
/
SHOW ERR;

--untuk autogen id D_Jual_Aksesoris
CREATE OR REPLACE TRIGGER TR_D_Jual_Aksesoris
BEFORE INSERT ON D_Jual_Aksesoris
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_jual,4,7))) into id from h_jual;
	:new.nota_jual := 'HJL'||lpad(id,7,'0');
	
	update aksesoris set stok = stok - :new.quantity where id_aksesoris = :new.id_aksesoris;
END;
/
SHOW ERR;

--untuk autogen id_jenis (+kode urutannya) jenis_alat_musik
CREATE OR REPLACE TRIGGER TR_Jenis
BEFORE INSERT ON Jenis_Alat_Musik
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select count(*)+1 into id from jenis_alat_musik where substr(ID_Jenis,1,3) = upper(:new.id_jenis);
	:new.ID_Jenis := :new.ID_Jenis||lpad(id,2,'0');
	:new.nama_jenis := initcap(:new.nama_jenis);
END;
/
SHOW ERR;

--untuk autogen id alat musik
CREATE OR REPLACE TRIGGER TR_Musik
BEFORE INSERT ON Alat_Musik
FOR EACH ROW
DECLARE
	id number(10);
	id_musik varchar2(5);
	kode varchar2(2);
	nama varchar2(50);
	kurang exception;
BEGIN
	if (length(:new.nama_alat_musik)-length(replace(:new.nama_alat_musik,' ',''))<1 
		or instr(:new.nama_alat_musik,' ',1)=length(:new.nama_alat_musik)) then
		raise kurang;
	end if;
	
	nama := :new.nama_alat_musik;
	select substr(nama,1,1)||substr(nama,instr(nama,' ')+1,1) into kode from dual;
	kode := upper(kode);
	select nvl(max(substr(id_alat_musik,3,3))+1,'1') into id from alat_musik where substr(id_alat_musik,1,2) = kode;
	id_musik := kode || lpad(id,3,'0');
	:new.nama_alat_musik := initcap(:new.nama_alat_musik);
	:new.id_alat_musik := id_musik;
	exception
		when kurang then
			raise_application_error(-20002,'Nama Alat Musik Minimal 2 Kata!');
END;
/
SHOW ERR;

--untuk autogen id aksesoris
CREATE OR REPLACE TRIGGER TR_Aksesoris
BEFORE INSERT ON Aksesoris
FOR EACH ROW
DECLARE
	id number(10);
	id_aksesoris varchar2(5);
	kode varchar2(3);
	nama varchar2(50);
	kurang exception;
BEGIN
	if (length(:new.nama_aksesoris)-length(replace(:new.nama_aksesoris,' ',''))<1 
		or instr(:new.nama_aksesoris,' ',1)=length(:new.nama_aksesoris)) then
		raise kurang;
	end if;
	
	nama := :new.nama_aksesoris;
	select substr(nama,1,1)||substr(nama,instr(nama,' ')+1,1) into kode from dual;
	kode := 'A'||upper(kode);
	select nvl(max(substr(id_aksesoris,4,2))+1,'1') into id from aksesoris where substr(id_aksesoris,1,3) = kode;
	id_aksesoris := kode|| lpad(id,2,'0');
	:new.nama_aksesoris := initcap(:new.nama_aksesoris);
	:new.id_aksesoris := id_aksesoris;
	:new.keterangan := initcap(:new.keterangan);
	
	exception
		when kurang then
			raise_application_error(-20003,'Nama Aksesoris Minimal 2 Kata!');
END;
/
SHOW ERR;

--untuk cek kode_promo kembar
--untuk autogen kode urutan jenis_alat_musik saat insert
CREATE OR REPLACE TRIGGER TR_Promo
BEFORE INSERT ON Promo
FOR EACH ROW
DECLARE
    ada number(10);
	x_ada exception;
BEGIN
	select count(*) into ada from promo where kode_promo = :new.kode_promo;
	
	if(ada<>0)then
		raise x_ada;
	end if;
	
	exception
		when x_ada then
			raise_application_error(-20001,'Kode Promo Sudah Ada! Silahkan Coba Kode Lain.');
END;
/
SHOW ERR;

--update

--delete
--delete supplier
CREATE OR REPLACE TRIGGER HAPUS_SUPPLIER
BEFORE DELETE ON SUPPLIER
FOR EACH ROW
DECLARE
BEGIN
	delete from d_beli where nota_beli in (select nota_beli from h_beli where ID_Supplier = :old.ID_Supplier);
	delete from d_beli_aksesoris where nota_beli in (select nota_beli from h_beli where ID_Supplier = :old.ID_Supplier);
	delete from h_beli where ID_Supplier = :old.ID_Supplier;
END;
/
SHOW ERR;

--delete jenis_alat_musik
CREATE OR REPLACE TRIGGER HAPUS_JENIS
BEFORE DELETE ON JENIS_ALAT_MUSIK
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual where id_alat_musik in (select id_alat_musik from alat_musik where id_jenis = :old.id_jenis);
	delete from d_beli where id_alat_musik in (select id_alat_musik from alat_musik where id_jenis = :old.id_jenis);
	delete from alat_musik where id_jenis = :old.id_jenis;
END;
/
SHOW ERR;

--delete produsen
CREATE OR REPLACE TRIGGER HAPUS_PRODUSEN
BEFORE DELETE ON PRODUSEN
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual where id_alat_musik in (select id_alat_musik from alat_musik where id_produsen = :old.id_produsen);
	delete from d_beli where id_alat_musik in (select id_alat_musik from alat_musik where id_produsen = :old.id_produsen);
	delete from alat_musik where id_produsen = :old.id_produsen;
END;
/
SHOW ERR;

--delete alat_musik
CREATE OR REPLACE TRIGGER HAPUS_MUSIK
BEFORE DELETE ON ALAT_MUSIK
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual where id_alat_musik = :old.id_alat_musik;
	delete from d_beli where id_alat_musik = :old.id_alat_musik;
END;
/
SHOW ERR;

--delete karyawan
CREATE OR REPLACE TRIGGER HAPUS_KARYAWAN
BEFORE DELETE ON KARYAWAN
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual where nota_jual in (select nota_jual from h_jual where id_karyawan = :old.id_karyawan);
	delete from d_jual_aksesoris where nota_jual in (select nota_jual from h_jual where id_karyawan = :old.id_karyawan);
	delete from h_jual where id_karyawan = :old.id_karyawan;
	delete from d_beli where nota_beli in (select nota_beli from h_beli where id_karyawan = :old.id_karyawan);
	delete from d_beli_aksesoris where nota_beli in (select nota_beli from h_beli where id_karyawan = :old.id_karyawan);
	delete from h_beli where id_karyawan = :old.id_karyawan;
END;
/
SHOW ERR;

--delete customer
CREATE OR REPLACE TRIGGER HAPUS_CUSTOMER
BEFORE DELETE ON CUSTOMER
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual where nota_jual in (select nota_jual from h_jual where ID_Customer = :old.id_customer);
	delete from d_jual_aksesoris where nota_jual in (select nota_jual from h_jual where ID_Customer = :old.id_customer);
	delete from h_jual where ID_Customer = :old.id_customer;
END;
/
SHOW ERR;

--delete promo
CREATE OR REPLACE TRIGGER HAPUS_PROMO
BEFORE DELETE ON PROMO
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual where nota_jual in (select nota_jual from h_jual where kode_promo = :old.kode_promo);
	delete from h_jual where kode_promo = :old.kode_promo;
END;
/
SHOW ERR;

--delete member
CREATE OR REPLACE TRIGGER HAPUS_MEMBER
BEFORE DELETE ON MEMBER
FOR EACH ROW
DECLARE
BEGIN
	delete from penjualan_member where id_member = :old.id_member;
END;
/
SHOW ERR;

--delete aksesoris
CREATE OR REPLACE TRIGGER HAPUS_AKSESORIS
BEFORE DELETE ON AKSESORIS
FOR EACH ROW
DECLARE
BEGIN
	delete from d_jual_aksesoris where id_aksesoris = :old.id_aksesoris;
	delete from d_beli_aksesoris where id_aksesoris = :old.id_aksesoris;
END;
/
SHOW ERR;
commit;