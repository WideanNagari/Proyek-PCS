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

--autogen id alat musik
create or replace function autogenMusik(
	nama varchar2
)
return varchar2
is
	id number(10);
	id_musik varchar2(5);
	kode varchar2(2);
begin
	select substr(nama,1,1)||substr(nama,instr(nama,' ')+1,1) into kode from dual;
	kode := upper(kode);
	select max(substr(id_alat_musik,3,3))+1 into id from alat_musik where substr(id_alat_musik,1,2) = kode;
	id_musik := kode || lpad(id,3,'0');
	return id_musik;
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

--trigger

-- insert
--untuk autogen id H_Beli
CREATE OR REPLACE TRIGGER TR_H_Beli
BEFORE INSERT ON H_Beli
FOR EACH ROW
DECLARE
    id number(10);
BEGIN
	select max(to_number(substr(nota_beli,4,7)))+1 into id from h_beli;
	:new.nota_beli := 'HBL'||lpad(id,7,'0');
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
	select count(*)+1 into id from jenis_alat_musik where substr(ID_Jenis,1,3) = 'UKL';
	:new.ID_Jenis := :new.ID_Jenis||lpad(id,2,'0');
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
--delete jenis_alat_musik
delete from d_jual
where id_alat_musik in (select id_alat_musik from alat_musik where id_jenis = 'UKL01');

delete from d_beli
where id_alat_musik in (select id_alat_musik from alat_musik where id_jenis = 'UKL01');

delete from alat_musik
where id_jenis = 'UKL01';

delete from jenis_alat_musik
where id_jenis = 'UKL01';

--delete produsen
delete from d_jual
where id_alat_musik in (select id_alat_musik from alat_musik where id_produsen = 'PD02');

delete from d_beli
where id_alat_musik in (select id_alat_musik from alat_musik where id_produsen = 'PD02');

delete from alat_musik
where id_produsen = 'PD02';

delete from produsen
where id_produsen = 'PD02';

--delete alat_musik
delete from d_jual
where id_alat_musik = 'RF001';

delete from d_beli
where id_alat_musik = 'RF001';

delete from alat_musik
where id_alat_musik = 'RF001';

--delete karyawan
delete from d_jual
where nota_jual in (select nota_jual from h_jual where id_karyawan = 'KAR005');

delete from h_jual
where id_karyawan = 'KAR005';

delete from d_beli
where nota_beli in (select nota_beli from h_beli where id_karyawan = 'KAR005');

delete from h_beli
where id_karyawan = 'KAR005';

delete from karyawan
where id_karyawan = 'KAR005';

--delete customer
delete from d_jual
where nota_jual in (select nota_jual from h_jual where ID_Customer = 'CUS001');

delete from h_jual
where ID_Customer = 'CUS001';

delete from customer
where ID_Customer = 'CUS001';

--delete supplier
delete from d_beli
where nota_beli in (select nota_beli from h_beli where ID_Supplier = 'SUP05');

delete from h_beli
where ID_Supplier = 'SUP05';

delete from supplier
where ID_Supplier = 'SUP05';

--delete promo
delete from d_jual
where nota_jual in (select nota_jual from h_jual where kode_promo = 'DISKONKECIL');

delete from h_jual
where kode_promo = 'DISKONKECIL';

delete from promo
where kode_promo = 'DISKONKECIL';