use dircol;

DROP TABLE IF EXISTS capi;
	CREATE TABLE capi (
	  [ID] int NOT NULL IDENTITY(1, 1),
	  [Taglia] text NOT NULL,
	  [Colore] text NOT NULL,
	  [PuntoVendita] varchar(5) NOT NULL,
	  [CodModello] int NOT NULL,
	  PRIMARY KEY (ID),
	  --FOREIGN KEY (CodModello) REFERENCES modelli (CodModello)
	  -- IDENTITY(1,1) è come se fosse AUTO_INCREMENT
	);
TRUNCATE TABLE capi;

