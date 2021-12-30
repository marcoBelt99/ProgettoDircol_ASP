# ProgettoDircol_ASP
Progetto per il tirocinio formativo da 12 CFU, Unife - Realizzazione di un sito web Aziendale di mia invenzione in ASP.NET Web Forms

## Rappresentazione concettuale e logica del progetto: (https://app.diagrams.net/?mode=google&gfw=1#G1CpOvL8520L8bq4log0TpyE_K_N3e4UsV)

## Struttura del progetto
Il progetto è costituito da alcuni file che rappresentano le operazioni di DML (Data Manipulation Language) delle principali entità (capo, modello, puntovendita, dipendente, vendita):
* Anzitutto, il [File .sql del database:](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/dircol_SQLSERVER.sql), che contiene il codice per il 'dump' in ambiente SQL Server.
* Home page: [Default.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Default.aspx)
* Pagina per gli inserimenti: [inserisci.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/inserisci.aspx)
* Pagina per gli aggiornamenti: [aggiorna.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/aggiorna.aspx)
* Pagina per le eliminazioni: [elimina.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/aggiorna.aspx)
Collegati ad essi, all'interno del direttorio: [Dati](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati) si trovano tutte le classi delle relative entità di base succitate:
* [Capo.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Capo.cs), [Modello.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Modello.cs), [PuntoVendita.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/PuntoVendita.cs), [Dipendente.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Dipendente.cs), [Vendita.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Vendita.cs).

Inoltre, la classe [Operazioni.cs](https://github.com/marcoBelt99/ProgettoDircol_ASP/tree/master/ProgettoDircol_ASP/Dati/Operazioni.cs) è di notevole utilità, poichè offre molte operazioni con le classi delle entità, che permettono di: estrarre alcune liste di elementi inseriti in alcune tabelle del database, contiene la stringa di connessione al DB, ed altri metodi per effettuare operaazioni utili.
Si noti che, ogni pagina **  \*.aspx ** è accompagnata dalla relativa pagina contente il **'Code Behind'**, ossia la 'business logic' della pagina, avente estensione \*.aspx.cs
Il file [visualizzaTabelle.aspx](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/visualizzaTabelle.aspx)
Di notevole importanza è il file: [Site.master](https://github.com/marcoBelt99/ProgettoDircol_ASP/blob/master/ProgettoDircol_ASP/Site.master), il quale contiene il layout di tutto il sito (di tutte le pagine che la ereditano)
Continua...

