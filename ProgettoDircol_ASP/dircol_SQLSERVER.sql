--
-- Database: `dircol`
--


DROP SCHEMA IF EXISTS dircol;
USE dircol;

-- --------------------------------------------------------
-- Prima aggiungo le tabelle che non hanno chiavi esterne, poi ci metto quelle
-- che referenziano una tabella (quelle che hanno chiavi esterne)



--
-- Struttura della tabella `modelli`
--

DROP TABLE IF EXISTS modelli;
CREATE TABLE modelli (
  [CodModello] int NOT NULL IDENTITY(1, 1),
  [Immagine] varchar(200) NOT NULL,
  [Nome] text NOT NULL,
  [Descrizione] text NOT NULL,
  [PrezzoListino] float NOT NULL,
  [Genere] text NOT NULL,
  [Collezione] varchar(20) NOT NULL,
  PRIMARY KEY (CodModello)
);
TRUNCATE TABLE modelli;
--
-- Dump dei dati per la tabella `modelli`
--

INSERT INTO modelli (Immagine,Nome, Descrizione, PrezzoListino, Genere, Collezione) VALUES
( 'Gonna.jpg', 'Gonna 2.0', 'Gonna per ragazze di cotone', 23, 'F', 'Autunnale'),
( 'CappellinoGucci.jpg', 'CapoGuc', 'Cappellino Gucci per ragazzi verde ed oro', 45, 'F', 'Estiva'),
( 'MaglioneZaraGiallo.jpg',  'MaglioneG', 'Maglione Zara giallo', 35, 'M', 'Invernale'),
( 'PantaloniBlu.jpg',  'PantaBlu', 'Pantaloni blu Sorbino', 49.9, 'M', 'Primaverile'),
( 'MagliaVerde.jpg',  'Maglia Verde', 'T-shirt verde lime', 23, 'M', 'Estiva'),
( 'MagliaRossa.jpg',  'Maglia Rossa', 'T-shirt rosso acceso', 24.10, 'M', 'Estiva'),
( 'MagliaGialla.jpg',  'Maglia Gialla', 'T-shirt gialla con girocollo da uomo', 23, 'M', 'Estiva'),
( 'CamiciaAzzurra.jpg',  'Camicia Azzurra', 'Polo Ralph Lauren NATURAL SLIM FIT - Camicia', 103.99, 'M', 'Autunnale'),
( 'CamiciaNera.jpg',  'Camicia Nera', 'Polo Ralph Lauren NATURAL SLIM FIT - Camicia', 103.99, 'M', 'Autunnale'),
( 'JeansDonnaNeri.jpg',  'WIDE LEG FLARED', 'Jeans a Zampa', 20.99, 'F', 'Autunnale'),
( 'JeansDonnaVerdi.jpg',  'Berska Jeans', 'Jeans a sigaretta', 29.99, 'F', 'Autunnale'),
( 'ScarpeNikeDonna.jpg',  'AIR FORCE 1 FONTANKA', ' Nike Sportswear - Sneakers basse', 109.99, 'F', 'Autunnale'),
( 'FelpaDiPileDonna.jpg',  '7 AM MOUNTAIN PILE', 'Giacca leggera', 180.99, 'F', 'Autunnale'),
( 'FelpaDiPileDonnaNera.jpg',  'CHUTE', 'Giacca in pile', 82, 'F', 'Autunnale'),
( 'FelpaNikeRossaCappuccio.jpg',  'Felpa con cappuccio', 'NIKE SPORTSWEAR CLUB FLEECE HOODIE', 44.19, 'M', 'Invernale'),
( 'BerrettoNikeVerde.jpg',  'Berretto Nike', 'BEANIE FISHERMAN UNISEX', 19.99, 'M', 'Invernale'),
( 'Giacca.jpg',  'Giacca Uomo', 'Comoda Giacca da Uomo economica', 45, 'M', 'Invernale'),
( 'SharpaRossa.jpg',  'Sciarpa Carnhartt WIP', 'CLAN SCARF UNISEX', 48.99, 'M', 'Invernale');

-- --------------------------------------------------------









--
-- Struttura della tabella `puntivendita`
--

DROP TABLE IF EXISTS puntivendita;
CREATE TABLE puntivendita (
  [CodPV] int NOT NULL IDENTITY(1,1),
  [Indirizzo] varchar(50) NOT NULL,
  [Telefono] varchar(11) NOT NULL,
  [Citta] text NOT NULL,
  [DataInizio] date NOT NULL,
  [Nazione] text NOT NULL,
  PRIMARY KEY (CodPV)
);
TRUNCATE TABLE puntivendita;
--
-- Dump dei dati per la tabella `puntivendita`
--

INSERT INTO puntivendita (Indirizzo, Telefono, Citta, DataInizio, Nazione) VALUES
('Via del Sole 11', 2147483647, 'Catania (CT)', '1986-03-12', 'Italia'),
('Moonlight Street 15', 3403185266, 'Edimburgo', '2001-04-10', 'Inghilterra'),
('Via Giuseppe Verdi 7', 7413698745, 'Bologna', '2006-05-05', 'Italia'),
('Viale dei Pini 9', 3571236475, 'Mestre', '2009-08-07', 'Italia'),
('Street Zero 899', 1020304050, 'Cambridge', '2020-03-23', 'Inghilterra'),
('Ruta del agua 133', 3332221144, 'Barcellona', '2018-01-31', 'Spagna'),
('Avenida Independencia 23', 159753456, 'Madrid', '1991-08-23', 'Spagna');

-- --------------------------------------------------------








--
-- Struttura della tabella `capi`
--

-- --------------------------------------------------------


DROP TABLE IF EXISTS capi;
	CREATE TABLE capi (
	  [ID] int NOT NULL IDENTITY(1, 1),
	  [Taglia] text NOT NULL,
	  [Colore] text NOT NULL,
	  [PuntoVendita] int NOT NULL,
	  [CodModello] int NOT NULL,
	  PRIMARY KEY (ID),
	  FOREIGN KEY (CodModello) REFERENCES modelli (CodModello),
	  FOREIGN KEY (PuntoVendita) REFERENCES puntivendita (CodPV)
	  -- IDENTITY(1,1) � come se fosse AUTO_INCREMENT
	);
TRUNCATE TABLE capi;

INSERT INTO capi (Taglia, Colore, PuntoVendita, CodModello) VALUES
('XXS', '#FF0000', '1', 1),
('S', '#000000', '6', 2),
('L', '#FFFF00', '1', 2),
('XL', '#008000', '1', 1),
('XS', '#0000FF', '5', 3),
('M', '#800000', '4', 3),
('L', '#8F00FF', '4', 4),
('L', '	#008000', '3', 2),
('XXL', '#FF0000', '1', 3);


-- --------------------------------------------------------






--
-- Struttura della tabella `dipendenti`
--

DROP TABLE IF EXISTS dipendenti;
CREATE TABLE dipendenti (
  [Matricola] bigint NOT NULL,
  [Cognome] text NOT NULL,
  [Nome] text NOT NULL,
  [CodiceFiscale] varchar(16) NOT NULL,
  [Qualifica] text NOT NULL,
  [PuntoVendita] int NOT NULL,
  PRIMARY KEY (Matricola),
  -- KEY `PuntoVendita` (`PuntoVendita`),
  FOREIGN KEY (PuntoVendita) REFERENCES puntivendita (CodPV)
);
TRUNCATE TABLE dipendenti;
--
-- Dump dei dati per la tabella `dipendenti`
--

INSERT INTO dipendenti (Matricola, Cognome, Nome, CodiceFiscale, Qualifica, PuntoVendita) VALUES
('148881', 'Beltrame', 'Marco', 'BLTMRC99M30A059W', 'Programmatore', '4'),
('148887', 'Rossi', 'Mario', 'RSSMRO120NX34I09', 'Content Manager', '4'),
('452314', 'Morin', 'Sara', 'MRNSRA99ARIA1QP1', 'Stilista', '1'),
('852159', 'Berton', 'Francesco', 'BRTFRC8812321KN6', 'Stilista', '2'),
('369541', 'Biondi', 'Diletta', 'BNDLTTA590WQZ7L2', 'Content Manager', '4');


-- --------------------------------------------------------




--
-- Struttura della tabella `vendite`
--

DROP TABLE IF EXISTS vendite;
CREATE TABLE vendite (
  [ID] int NOT NULL IDENTITY(1,1),
  [DataVendita] date NOT NULL,
  [PrezzoVendita] float NOT NULL,
  [Matricola] bigint NOT NULL,
  [IDCapo] int NOT NULL,
  PRIMARY KEY (ID),
  -- KEY IDCapo (IDCapo),
  FOREIGN KEY (Matricola) REFERENCES dipendenti (Matricola),
  FOREIGN KEY (IDCapo) REFERENCES capi (ID)
) ;
TRUNCATE TABLE vendite;
--
-- Dump dei dati per la tabella `vendite`
--

INSERT INTO vendite (DataVendita, PrezzoVendita, Matricola, IDCapo) VALUES
('2021-11-04', 444, '148881', 9),
('2021-08-11', 512, '369541', 5);


